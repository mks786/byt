using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorRenovacion : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos las Renovaciones del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<Renovacion> ConsultarTodos()
        {
            IList<Renovacion> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Renovacion>> comando = FabricaComandosRenovacion.ObtenerComandoConsultarTodos();
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
        /// Método que modifica un los datos de una Renovacion
        /// </summary>
        /// <param name="renovacion">Renovacion a modificar</param>
        /// <param name="hash">Hash de la renovacion que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(Renovacion renovacion, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (renovacion.Id == 0)
                {
                    ComandoBase<bool> comandoRenovacionContador = null;
                    ComandoBase<bool> comandoOperacionContador = null;

                    ComandoBase<Contador> comandoContadorRenovacionProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_RENOVACIONES");
                    comandoContadorRenovacionProximoValor.Ejecutar();
                    Contador contadorRenovacion = comandoContadorRenovacionProximoValor.Receptor.ObjetoAlmacenado;

                    comandoRenovacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorRenovacion);
                    renovacion.Id = contadorRenovacion.ProximoValor++;

                    Operacion operacion = new Operacion();

                    ComandoBase<Contador> comandoContadorOperacionesProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_OPERACIONES");
                    comandoContadorOperacionesProximoValor.Ejecutar();

                    Contador contadorOperacion = comandoContadorOperacionesProximoValor.Receptor.ObjetoAlmacenado;

                    comandoOperacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorOperacion);
                    operacion.Id = contadorOperacion.ProximoValor++;
                    operacion.Fecha = System.DateTime.Now;
                    operacion.Aplicada = 'M';
                    operacion.CodigoAplicada = renovacion.Marca.Id;
                    operacion.Interno = renovacion.Id;
                    operacion.Servicio = new Servicio("RN");

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    ComandoBase<Marca> comandoMarca = FabricaComandosMarca.ObtenerComandoConsultarMarcaConTodo(renovacion.Marca);
                    comandoMarca.Ejecutar();
                    Marca marca = comandoMarca.Receptor.ObjetoAlmacenado;
                    marca.Interesado = renovacion.Interesado;
                    marca.Agente = renovacion.Agente;
                    marca.Poder = renovacion.Poder;
                    marca.FechaRenovacion = renovacion.FechaProxima;

                    ComandoBase<bool> comandoEditarMarca = FabricaComandosMarca.ObtenerComandoInsertarOModificar(marca);
                    comandoEditarMarca.Ejecutar();
                    bool exitosomarca = comandoEditarMarca.Receptor.ObjetoAlmacenado;

                    ComandoBase<bool> comando = FabricaComandosRenovacion.ObtenerComandoInsertarOModificar(renovacion);
                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    exitoso = comando.Receptor.ObjetoAlmacenado;



                    if ((exitoso) && (exitosomarca))
                    {
                        comandoRenovacionContador.Ejecutar();
                        comandoOperacionContador.Ejecutar();
                    }
                }
                else
                {
                    ComandoBase<bool> comando = FabricaComandosRenovacion.ObtenerComandoInsertarOModificar(renovacion);
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
        /// Método que consulta una Renovacion por su Id
        /// </summary>
        /// <param name="renovacion">Renovacion con el Id de la Renovacion buscada</param>
        /// <returns>La Renovacion solicitada</returns>
        public static Renovacion ConsultarPorId(Renovacion renovacion)
        {
            Renovacion retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<Renovacion> comando = FabricaComandosRenovacion.ObtenerComandoConsultarPorID(renovacion);
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
        /// Método que consulta una Renovacion por su Id
        /// </summary>
        /// <param name="renovacion">Renovacion con el Id de la Renovacion buscada</param>
        /// <returns>La Renovacion solicitada</returns>
        public static int ConsultarUltimaRenovacion(Renovacion renovacion)
        {
            int retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<int> comando = FabricaComandosRenovacion.ObtenerComandoConsultarUltimaRenovacion(renovacion);
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
        /// Método que elimina una Renovacion
        /// </summary>
        /// <param name="renovacion">Renovacion a eliminar</param>
        /// <param name="hash">Hash de la renovacion que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(Renovacion renovacion, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

            

                ComandoBase<bool> comando = FabricaComandosRenovacion.ObtenerComandoEliminarRenovacion(renovacion);
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
        /// Verifica si la Renovacion existe
        /// </summary>
        /// <param name="renovacion">Renovacion a verificar</param>
        /// <returns>True de existir, false en caso contrario</returns>
        public static bool VerificarExistencia(Renovacion renovacion)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosRenovacion.ObtenerComandoVerificarExistenciaRenovacion(renovacion);
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

        public static IList<Renovacion> ConsultarRenovacionesFiltro(Renovacion renovacion)
        {
            IList<Renovacion> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<Renovacion>> comando = FabricaComandosRenovacion.ObtenerComandoConsultarRenovacionesFiltro(renovacion);
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