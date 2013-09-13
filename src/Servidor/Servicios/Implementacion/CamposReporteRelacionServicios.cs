using System;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using Trascend.Bolet.LogicaNegocio.Controladores;
using Trascend.Bolet.ObjetosComunes.ContratosServicios;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Servicios.Implementacion
{
    public class CamposReporteRelacionServicios: MarshalByRefObject, ICamposReporteRelacionServicios
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Servicio que inserta o actualiza un Campo que pertenece al Reporte en la tabla que relaciona a dichas entidades
        /// </summary>
        /// <param name="campoReporteDeMarca">Campo de Reporte a insertar o actualizar</param>
        /// <param name="hash">Hash del Usuario Logueado</param>
        /// <returns>True si la operacion es satisfactoria; false en caso contrario</returns>
        public bool InsertarOModificar(CamposReporteRelacion campoReporteDeMarca, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                //bool exitoso = ControladorReporteDeMarca.InsertarOModificar(reporteDeMarca, hash);
                bool exitoso = ControladorCamposReporteRelacion.InsertarOModificar(campoReporteDeMarca, hash);

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
        /// Servicio para consultar los campos seleccionados de un reporte de marca especifico
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>Lista de campos de un reporte especifico</returns>
        public IList<CamposReporteRelacion> ConsultarCamposDeReporte(Reporte reporte)
        {

            IList<CamposReporteRelacion> camposReporteDeMarca;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion


                camposReporteDeMarca = ControladorCamposReporteRelacion.ConsultarCamposDeReporte(reporte);
                

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                return camposReporteDeMarca;
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
        /// Servicio que borra los campos definidos para un Reporte de Marca seleccionado 
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>True si el proceso se realiza exitosamente; False en caso contrario</returns>
        public bool EliminarCamposReporte(Reporte reporte)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCamposReporteRelacion.EliminarCamposReporteDeMarca(reporte);

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
        /// Servicio para eliminar un Campo Relacion de un Reporte especifico
        /// </summary>
        /// <param name="campo">Campo Relacion a eliminar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza correctamente; False en caso contrario</returns>
        public bool Eliminar(CamposReporteRelacion campo, int hash)
        {
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                bool exitoso = ControladorCamposReporteRelacion.Eliminar(campo);
                
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


        public IList<CamposReporteRelacion> ConsultarTodos()
        {
            throw new NotImplementedException();
        }

        public CamposReporteRelacion ConsultarPorId(CamposReporteRelacion entidad)
        {
            throw new NotImplementedException();
        }
        
        public IList<CamposReporteRelacion> ConsultarPorOtroCampo(string campoEntidad, string tipoOrdenamiento)
        {
            throw new NotImplementedException();
        }
        
        

        public bool VerificarExistencia(CamposReporteRelacion entidad)
        {
            throw new NotImplementedException();
        }



        
    }
}
