using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorFusion : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los Usuarios del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<Fusion> ConsultarTodos()
        {
            IList<Fusion> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Fusion>> comando = FabricaComandosFusion.ObtenerComandoConsultarTodos();
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            return retorno;
        }


        /// <summary>
        /// Método que modifica un los datos de un Usuario
        /// </summary>
        /// <param name="fusion">Usuario a modificar</param>
        /// <param name="hash">Hash del usuario que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(ref Fusion fusion, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (fusion.Id == 0)
                {
                    ComandoBase<bool> comandoFusionContador = null;
                    ComandoBase<bool> comandoOperacionContador = null;

                    ComandoBase<Contador> comandoContadorFusionProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_MFUSIONES");
                    comandoContadorFusionProximoValor.Ejecutar();
                    Contador contadorFusion = comandoContadorFusionProximoValor.Receptor.ObjetoAlmacenado;

                    comandoFusionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorFusion);
                    fusion.Id = contadorFusion.ProximoValor++;

                    Operacion operacion = new Operacion();

                    ComandoBase<Contador> comandoContadorOperacionesProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_OPERACIONES");
                    comandoContadorOperacionesProximoValor.Ejecutar();

                    Contador contadorOperacion = comandoContadorOperacionesProximoValor.Receptor.ObjetoAlmacenado;

                    comandoOperacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorOperacion);
                    operacion.Id = contadorOperacion.ProximoValor++;
                    //operacion.Fecha = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", System.DateTime.Now));
                    operacion.Fecha = fusion.Fecha;
                    operacion.Aplicada = 'M';
                    operacion.CodigoAplicada = fusion.Marca.Id;
                    operacion.Interno = fusion.Id;
                    operacion.Interesado = fusion.InteresadoSobreviviente;
                    operacion.Servicio = new Servicio("FU");
                    operacion.CadenaDeCambios = fusion.CadenaDeCambios;

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    ComandoBase<bool> comando = FabricaComandosFusion.ObtenerComandoInsertarOModificar(fusion);
                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    fusion.FusionMarcaTercero.Fusion.Id = fusion.Id;
                    ComandoBase<bool> comandoFMT = FabricaComandosFusionMarcaTercero.ObtenerComandoInsertarOModificar(fusion.FusionMarcaTercero);
                    comandoFMT.Ejecutar();

                    fusion.Marca.Interesado = fusion.InteresadoSobreviviente;
                    fusion.Marca.Agente = fusion.Agente;
                    fusion.Marca.Poder = fusion.Poder;

                    ComandoBase<bool> comandoEditarMarca = FabricaComandosMarca.ObtenerComandoInsertarOModificar(fusion.Marca);
                    comandoEditarMarca.Ejecutar();

                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if (exitoso)
                    {
                        //ComandoBase<Marca> comandoMarca = FabricaComandosMarca.ObtenerComandoConsultarMarcaConTodo(fusion.Marca);
                        //comandoMarca.Ejecutar();
                        //Marca marcaAEditar = comandoMarca.Receptor.ObjetoAlmacenado;
                        //marcaAEditar.Interesado = fusion.InteresadoSobreviviente;
                        //marcaAEditar.Poder = fusion.Poder;
                        //marcaAEditar.Agente = fusion.Agente;

                        //ComandoBase<bool> comandoInsertarMarca = FabricaComandosMarca.ObtenerComandoInsertarOModificar(marcaAEditar);
                        //comandoInsertarMarca.Ejecutar();
                        //exitoso = comandoInsertarMarca.Receptor.ObjetoAlmacenado;

                        if (exitoso)
                        {
                            comandoFusionContador.Ejecutar();
                            comandoOperacionContador.Ejecutar();
                        }
                    }
                }
                else
                {
                    ComandoBase<bool> comando = FabricaComandosFusion.ObtenerComandoInsertarOModificar(fusion);

                    Operacion operacionAux = new Operacion();
                    operacionAux.Interno = fusion.Id;
                    operacionAux.Servicio = new Servicio("FU");
                    operacionAux.Aplicada = 'M';
                    operacionAux.Marca = fusion.Marca;

                    IList<Operacion> operaciones = ControladorOperacion.ConsultarOperacionesFiltro(operacionAux);
                    Operacion operacion = operaciones[0];
                    operacion.CadenaDeCambios = fusion.CadenaDeCambios;

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    ComandoBase<bool> comandoFMT = FabricaComandosFusionMarcaTercero.ObtenerComandoInsertarOModificar(fusion.FusionMarcaTercero);
                    comandoFMT.Ejecutar();

                    exitoso = comando.Receptor.ObjetoAlmacenado;
                }

                if (exitoso)
                {
                    ComandoBase<Marca> comandoObtenerMarca = FabricaComandosMarca.ObtenerComandoConsultarMarcaConTodo(fusion.Marca);
                    comandoObtenerMarca.Ejecutar();
                    Marca marcaModificar = comandoObtenerMarca.Receptor.ObjetoAlmacenado;

                    marcaModificar.Interesado = fusion.InteresadoSobreviviente;
                    marcaModificar.Poder = fusion.Poder;

                    ComandoBase<bool> comandoMarca = FabricaComandosMarca.ObtenerComandoInsertarOModificar(fusion.Marca);
                    comandoMarca.Ejecutar();
                    exitoso = exitoso && comandoMarca.Receptor.ObjetoAlmacenado;
                }


                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            return exitoso;
        }


        /// <summary>
        /// Método que consulta un Fusion por su Id
        /// </summary>
        /// <param name="Fusion">Fusion con el Id del Fusion buscado</param>
        /// <returns>El Fusion solicitado</returns>
        public static Fusion ConsultarPorId(Fusion fusion)
        {
            Fusion retorno = fusion;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                ComandoBase<FusionMarcaTercero> comandoFusion = FabricaComandosFusionMarcaTercero.ObtenerComandoConsultarPorID(retorno);
                comandoFusion.Ejecutar();

                retorno.FusionMarcaTercero = comandoFusion.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            return retorno;
        }


        /// <summary>
        /// Método que elimina un fusion
        /// </summary>
        /// <param name="usuario">Fusion a eliminar</param>
        /// <param name="hash">Hash del fusion que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Fusion fusion, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosFusion.ObtenerComandoEliminarFusion(fusion);
                comando.Ejecutar();
                exitoso = true;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

            return exitoso;
        }


        /// <summary>
        /// Verifica si el fusion existe
        /// </summary>
        /// <param name="fusion">Fusion a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(Fusion fusion)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosFusion.ObtenerComandoVerificarExistenciaFusion(fusion);
                comando.Ejecutar();
                existe = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

            return existe;
        }


        /// <summary>
        /// Consulta los Fusion que cumplan con los filtros establecidos en el objeto enviado
        /// </summary>
        /// <param name="CambioDeDomicilio">Fusion a consultar</param>
        /// <returns>Lista de Fusion que cumplen con el filtro</returns>
        public static IList<Fusion> ConsultarFusionesFiltro(Fusion fusion)
        {
            IList<Fusion> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Fusion>> comando = FabricaComandosFusion.ObtenerComandoConsultarFusionesFiltro(fusion);
                comando.Ejecutar();
                retorno = comando.Receptor.ObjetoAlmacenado;

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }

            return retorno;
        }

    }
}