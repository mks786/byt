using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Fabrica;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.LogicaNegocio.Controladores
{
    public class ControladorCambioDeDomicilio : ControladorBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Método que devuelve todos los CambioDeDomicilio del sistema
        /// </summary>
        /// <returns></returns>
        public static IList<CambioDeDomicilio> ConsultarTodos()
        {
            IList<CambioDeDomicilio> retorno;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CambioDeDomicilio>> comando = FabricaComandosCambioDeDomicilio.ObtenerComandoConsultarTodos();
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
        /// Método que modifica un los datos de un CambioDeDomicilio
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio a modificar</param>
        /// <param name="hash">Hash del CambioDeDomicilio que va a realizar la operacion</param>
        /// <returns>True si la modificación fue exitosa, en caso contrario False</returns>
        public static bool InsertarOModificar(CambioDeDomicilio cambioDeDomicilio, int hash)
        {
            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                if (cambioDeDomicilio.Id == 0)
                {
                    ComandoBase<bool> comandoCambioDeDomicilioContador = null;
                    ComandoBase<bool> comandoOperacionContador = null;

                    ComandoBase<Contador> comandoContadorCambioDeDomicilioProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_MDOMICILIOS");
                    comandoContadorCambioDeDomicilioProximoValor.Ejecutar();
                    Contador contadorCambioDeDomicilio = comandoContadorCambioDeDomicilioProximoValor.Receptor.ObjetoAlmacenado;

                    comandoCambioDeDomicilioContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorCambioDeDomicilio);
                    cambioDeDomicilio.Id = contadorCambioDeDomicilio.ProximoValor++;

                    Operacion operacion = new Operacion();

                    ComandoBase<Contador> comandoContadorOperacionesProximoValor = FabricaComandosContador.ObtenerComandoConsultarPorId("MYP_OPERACIONES");
                    comandoContadorOperacionesProximoValor.Ejecutar();

                    Contador contadorOperacion = comandoContadorOperacionesProximoValor.Receptor.ObjetoAlmacenado;

                    comandoOperacionContador = FabricaComandosContador.ObtenerComandoInsertarOModificar(contadorOperacion);
                    operacion.Id = contadorOperacion.ProximoValor++;
                    operacion.Fecha = System.DateTime.Now;
                    operacion.Aplicada = 'M';
                    operacion.CodigoAplicada = cambioDeDomicilio.Marca.Id;
                    operacion.Interno = cambioDeDomicilio.Id;
                    operacion.Servicio = new Servicio("CD");

                    ComandoBase<bool> comandoOperacion = FabricaComandosOperacion.ObtenerComandoInsertarOModificar(operacion);

                    ComandoBase<bool> comando = FabricaComandosCambioDeDomicilio.ObtenerComandoInsertarOModificar(cambioDeDomicilio);
                    comando.Ejecutar();
                    comandoOperacion.Ejecutar();

                    exitoso = comando.Receptor.ObjetoAlmacenado;

                    if (exitoso)
                    {
                        comandoCambioDeDomicilioContador.Ejecutar();
                        comandoOperacionContador.Ejecutar();
                    }
                }
                else
                {
                    ComandoBase<bool> comando = FabricaComandosCambioDeDomicilio.ObtenerComandoInsertarOModificar(cambioDeDomicilio);
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
        /// Método que consulta un CambioDeDomicilio por su Id
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio con el Id del CambioDeDomicilio buscado</param>
        /// <returns>El CambioDeDomicilio solicitado</returns>
        public static CambioDeDomicilio ConsultarPorId(CambioDeDomicilio cambioDeDomicilio)
        {
            CambioDeDomicilio retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<CambioDeDomicilio> comando = FabricaComandosCambioDeDomicilio.ObtenerComandoConsultarPorID(cambioDeDomicilio);
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
        /// Método que elimina un CambioDeDomicilio
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio a eliminar</param>
        /// <param name="hash">Hash del CambioDeDomicilio que va a realizar la operacion</param>
        /// <returns>True si la eliminacion fue exitosa, en caso contrario False</returns>
        public static bool Eliminar(CambioDeDomicilio cambioDeDomicilio, int hash)
        {
            bool exitoso = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCambioDeDomicilio.ObtenerComandoEliminarCambioDeDomicilio(cambioDeDomicilio);
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
        /// Verifica si el CambioDeDomicilio existe
        /// </summary>
        /// <param name="cambioDeDomicilio">CambioDeDomicilio a verificar</param>
        /// <returns>True de existir, false en caso conrario</returns>
        public static bool VerificarExistencia(CambioDeDomicilio cambioDeDomicilio)
        {
            bool existe = false;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<bool> comando = FabricaComandosCambioDeDomicilio.ObtenerComandoVerificarExistenciaCambioDeDomicilio(cambioDeDomicilio);
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
        public static IList<CambioDeDomicilio> ConsultarCambioDeDomicilioFiltro(CambioDeDomicilio cambioDeDomicilio)
        {
            IList<CambioDeDomicilio> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                ComandoBase<IList<CambioDeDomicilio>> comando = FabricaComandosCambioDeDomicilio.ObtenerComandoConsultarCambiosDeDomicilioFiltro(cambioDeDomicilio);
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