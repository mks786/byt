using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CamposReporteServicios: MarshalByRefObject, ICamposReporteServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Servicio que recupera todos los campos para reportes de marcas y patentes sin discriminar el tipo de reporte
        /// </summary>
        /// <returns></returns>
        public IList<CamposReporte> ConsultarTodos()
        {
            IList<CamposReporte> campos;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                campos = ControladorCamposReporte.ConsultarTodos();
                
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

            return campos;
        }


        /// <summary>
        /// Servicio que obtiene los campos que pertenecen al reporte de Marca
        /// </summary>
        /// <returns>Lista de campos para el reporte de marca</returns>
        public IList<CamposReporte> ObtenerCamposReporteDeMarca()
        {
            IList<CamposReporte> campos;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                campos = ControladorCamposReporte.ObtenerCamposReporteDeMarca();

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

            return campos;
        }



        /// <summary>
        /// Servicio que obtiene los campos que pertenecen al reporte de Patente
        /// </summary>
        /// <returns>Lista de campos para el reporte de marca</returns>
        public IList<CamposReporte> ObtenerCamposReportePatente()
        {
            IList<CamposReporte> campos;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                campos = ControladorCamposReporte.ObtenerCamposReporteDePatente();

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

            return campos;
        }


        public CamposReporte ConsultarPorId(CamposReporte entidad)
        {
            throw new NotImplementedException();
        }

        public IList<CamposReporte> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }

        public bool InsertarOModificar(CamposReporte entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(CamposReporte entidad, int hash)
        {
            throw new NotImplementedException();
        }

        public bool VerificarExistencia(CamposReporte entidad)
        {
            throw new NotImplementedException();
        }
    }
}
