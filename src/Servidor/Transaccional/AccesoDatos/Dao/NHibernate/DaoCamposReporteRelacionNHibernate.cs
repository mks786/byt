using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoCamposReporteRelacionNHibernate: DaoBaseNHibernate<CamposReporteRelacion,int>, IDaoCamposReporteRelacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public IList<CamposReporteRelacion> ObtenerCamposDeReporte(Reporte reporteDeMarca)
        {
            IList<CamposReporteRelacion> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerCamposSeleccionadosDeReporte,reporteDeMarca.Id.ToString()));
                retorno = query.List<CamposReporteRelacion>();
                
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerArchivoPorId);
            }
            finally
            {
                Session.Close();
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que elimina los campos definidos para un reporte de marca especifico
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>True si se realiza correctamente; False en caso contrario</returns>
        public bool EliminarCamposReporte(Reporte reporteDeMarca)
        {
            bool retorno = true;
            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.EliminarCamposReporteDeMarcaPorCodigoReporte, reporteDeMarca.Id));
                int a = query.ExecuteUpdate();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExEjecutarProcedimientoBD);
            }
            finally
            {
                Session.Close();
            }
            return retorno;
        }


    }
}
