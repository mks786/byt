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
    class DaoCamposReporteNHibernate: DaoBaseNHibernate<CamposReporte,string>, IDaoCamposReporte
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo que obtiene los campos que estan definidos para el reporte de marca
        /// </summary>
        /// <returns>Lista de campos del reporte de marca</returns>
        public IList<CamposReporte> ObtenerCamposReporteDeMarca()
        {
            IList<CamposReporte> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerCamposDeReporteDeMarca));
                retorno = query.List<CamposReporte>();

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
        /// Metodo que obtiene los campos que estan definidos para el reporte de patente
        /// </summary>
        /// <returns>Lista de campos del reporte de patente</returns>
        public IList<CamposReporte> ObtenerCamposReporteDePatente()
        {
            IList<CamposReporte> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerCamposDeReporteDePatente));
                retorno = query.List<CamposReporte>();

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


    }
}
