using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class PresentacionSapiDetalleServicios : MarshalByRefObject, IPresentacionSapiDetalleServicios
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que inserta o actualiza un Renglon de Detalle de la Solicitud de Presentacion Sapi
        /// </summary>
        /// <param name="presentacionSapiDetalle">Detalle de la Presentacion Sapi a insertar o actualizar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza exitosamente; False, en caso contrario</returns>
        public bool InsertarOModificar(PresentacionSapiDetalle presentacionSapiDetalle, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorPresentacionSapiDetalle.InsertarOModificar(presentacionSapiDetalle, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }

        /// <summary>
        /// Servicio que elimina un renglon de detalle de la Solicitud de Presentacion Sapi
        /// </summary>
        /// <param name="presentacionSapiDetalle">Detalle de la Presentacion Sapi a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza exitosamente; False, en caso contrario</returns>
        public bool Eliminar(PresentacionSapiDetalle presentacionSapiDetalle, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorPresentacionSapiDetalle.Eliminar(presentacionSapiDetalle, hash);

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return exitoso;
            }
            catch (ApplicationException ex)
            {
                logger.Error(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }
        }


        /// <summary>
        /// Servicio que, usando un filtro, obtiene un conjunto de Solicitudes de Presentaciones SAPI
        /// </summary>
        /// <param name="filtro">Filtro utilizado para obtener un conjunto de Solicitudes de Presentaciones Sapi</param>
        /// <returns>Lista de Solicitudes de Presentaciones Sapi de acuerdo a un filtro determinado</returns>
        public IList<PresentacionSapiDetalle> ObtenerSolicitudesPresentacionSapiFiltro(PresentacionSapiDetalle filtro)
        {
            IList<PresentacionSapiDetalle> presentaciones = null;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                presentaciones = ControladorPresentacionSapiDetalle.ConsultarSolicitudesPresetacionSapiFiltro(filtro);


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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Errores.MensajesAlServidor.ErrorInesperadoServidor);
            }

            return presentaciones;
        }



        public IList<PresentacionSapiDetalle> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public PresentacionSapiDetalle ConsultarPorId(PresentacionSapiDetalle entidad)
        {
            throw new NotImplementedException();
        }

        public IList<PresentacionSapiDetalle> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(PresentacionSapiDetalle entidad)
        {
            throw new NotImplementedException();
        }
    }
}
