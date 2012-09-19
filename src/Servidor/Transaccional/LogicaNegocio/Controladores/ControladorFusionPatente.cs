using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorFusionPatente : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los Usuarios del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<FusionPatente> ConsultarTodos()
        {
            IList<FusionPatente> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<FusionPatente>> comando = FabricaComandosFusionPatente.ObtenerComandoConsultarTodos();
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
        public static bool InsertarOModificar(ref FusionPatente fusion, int hash)
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
                    ComandoBase<bool> comandoFusionPatenteContador = null;
                    ComandoBase<bool> comandoOperacionContador = null;

                    ComandoBase<Contador> comandoContadorFusionPatenteProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_PFUSIONES");
                    comandoContadorFusionPatenteProximoValor.Ejecutar();
                    Contador contadorFusionPatente = comandoContadorFusionPatenteProximoValor.Receptor.ObjetoAlmacenado;

                    comandoFusionPatenteContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorFusionPatente);
                    fusion.Id = contadorFusionPatente.ProximoValor++;

                    Operacion operacion = new Operacion();

                    ComandoBase<Contador> comandoContadorOperacionesProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_OPERACIONES");
                    comandoContadorOperacionesProximoValor.Ejecutar();

                    Contador contadorOperacion = comandoContadorOperacionesProximoValor.Receptor.ObjetoAlmacenado;

                    comandoOperacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorOperacion);
                    operacion.Id = contadorOperacion.ProximoValor++;
                    //operacion.Fecha = DateTime.Parse(String.Format("{0:dd/MM/yyyy}", System.DateTime.Now));
                    operacion.Fecha = fusion.Fecha;
                    operacion.Aplicada = 'P';
                    operacion.CodigoAplicada = fusion.Patente.Id;
                    operacion.Interno = fusion.Id;
                    operacion.Interesado = fusion.InteresadoSobreviviente;
                    operacion.Servicio = new Servicio("FU");

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    ComandoBase<bool> comando = FabricaComandosFusionPatente.ObtenerComandoInsertarOModificar(fusion);
                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    ComandoBase<bool> comandoFMT = FabricaComandosFusionPatenteTercero.ObtenerComandoInsertarOModificar(fusion.FusionPatenteTercero);
                    comandoFMT.Ejecutar();

                    ComandoBase<Patente> comandoMarca = FabricaComandosPatente.ObtenerComandoConsultarPatenteConTodo(fusion.Patente);
                    comandoMarca.Ejecutar();
                    Patente patente = comandoMarca.Receptor.ObjetoAlmacenado;
                    patente.Interesado = fusion.InteresadoSobreviviente;
                    patente.Agente = fusion.Agente;
                    patente.Poder = fusion.Poder;

                    ComandoBase<bool> comandoEditarPatente = FabricaComandosPatente.ObtenerComandoInsertarOModificar(patente);
                    comandoEditarPatente.Ejecutar();
                    bool exitosoPatente = comandoEditarPatente.Receptor.ObjetoAlmacenado;

                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if ((exitoso) && (exitosoPatente))
                    {
                        comandoFusionPatenteContador.Ejecutar();
                        comandoOperacionContador.Ejecutar();
                    }
                }
                else
                {
                    ComandoBase<bool> comando = FabricaComandosFusionPatente.ObtenerComandoInsertarOModificar(fusion);
                    comando.Ejecutar();
                    exitoso = comando.Receptor.ObjetoAlmacenado;
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
        /// Método que consulta un FusionPatente por su Id
        /// </summary>
        /// <param name="FusionPatente">FusionPatente con el Id del FusionPatente buscado</param>
        /// <returns>El FusionPatente solicitado</returns>
        public static FusionPatente ConsultarPorId(FusionPatente fusion)
        {
            FusionPatente retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<FusionPatente> comando = FabricaComandosFusionPatente.ObtenerComandoConsultarPorID(fusion);
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
        /// Método que elimina un fusion
        /// </summary>
        /// <param name="usuario">FusionPatente a eliminar</param>
        /// <param name="hash">Hash del fusion que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(FusionPatente fusion, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosFusionPatente.ObtenerComandoEliminarFusionPatente(fusion);
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
        /// <param name="fusion">FusionPatente a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(FusionPatente fusion)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosFusionPatente.ObtenerComandoVerificarExistenciaFusionPatente(fusion);
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
        /// Consulta los FusionPatente que cumplan con los filtros establecidos en el objeto enviado
        /// </summary>
        /// <param name="CambioDeDomicilio">FusionPatente a consultar</param>
        /// <returns>Lista de FusionPatente que cumplen con el filtro</returns>
        public static IList<FusionPatente> ConsultarFusionPatenteesFiltro(FusionPatente fusion)
        {
            IList<FusionPatente> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<FusionPatente>> comando = FabricaComandosFusionPatente.ObtenerComandoConsultarFusionPatenteesFiltro(fusion);
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