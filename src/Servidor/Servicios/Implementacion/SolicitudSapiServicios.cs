using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class SolicitudSapiServicios : MarshalByRefObject, ISolicitudSapiServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que obtiene un grupo de Solicitudes Sapi de acuerdo a un filtro aplicado
        /// </summary>
        /// <param name="solicitudAux">Solicitud Sapi usada como filtro</param>
        /// <returns>Lista de Solicitudes Sapi que cumplen con el filtro</returns>
        public IList<SolicitudSapi> ObtenerSolicitudesSapiFiltro(SolicitudSapi solicitudAux)
        {
            IList<SolicitudSapi> solicitudes; 

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                solicitudes = ControladorSolicitudSapi.ObtenerSolicitudesSapiFiltro(solicitudAux);

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

            return solicitudes;
        }

        /// <summary>
        /// Servicio que obtiene los Movimientos tipo SOLICITUD pendientes por ENTREGAR Y RECIBIR
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi filtro</param>
        /// <returns>Lista de Movimientos tipo SOLICITUD pendientes por ENTREGAR Y RECIBIR</returns>
        public IList<SolicitudSapi> ObtenerSolicitudesSapiPendientesFiltro(SolicitudSapi solicitudSapi)
        {
            IList<SolicitudSapi> solicitudes;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                solicitudes = ControladorSolicitudSapi.ObtenerSolicitudesSapiPendientesFiltro(solicitudSapi);

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

            return solicitudes;
        }


        /// <summary>
        /// Servicio que inserta o actualiza una lista de Detalles de Solicitud de Materiales SAPI
        /// </summary>
        /// <param name="solicitudesMateriales">Renglones de Detalle de las Solicitudes Materiales</param>
        /// <param name="operacion">Operacion de Base de Datos (CREATE o MODIFY)</param>
        /// <param name="hash">Hash del Usuario Logueado</param>
        /// <returns>True si la operacion se realiza correctamente; False, en caso contrario</returns>
        public bool InsertarOModificarSolicitudMaterialSapi(ref IList<SolicitudSapi> solicitudesMateriales, string operacion, int hash)
        {

            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                exitoso = ControladorSolicitudSapi.InsertarOModificar(ref solicitudesMateriales, operacion, hash);

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

            return exitoso;
        }


        /// <summary>
        /// Servicio que elimina una Solicitud de Material Sapi de la base de datos
        /// </summary>
        /// <param name="solicitud">Solicitud Sapi a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True, si la operacion se realiza correctamente; False, en caso contrario</returns>
        public bool Eliminar(SolicitudSapi solicitud, int hash)
        {

            bool exitoso = false;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                exitoso = ControladorSolicitudSapi.Eliminar(solicitud, hash);

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

            return exitoso;
        }


        public IList<SolicitudSapi> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(SolicitudSapi entidad)
        {
            throw new NotImplementedException();
        }

        public SolicitudSapi ConsultarPorId(SolicitudSapi entidad)
        {
            throw new NotImplementedException();
        }

        public IList<SolicitudSapi> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(SolicitudSapi solicitud, int hash)
        {
            throw new NotImplementedException();
        }

    }
}
