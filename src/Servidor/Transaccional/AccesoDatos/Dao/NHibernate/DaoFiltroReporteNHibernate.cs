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
    class DaoFiltroReporteNHibernate : DaoBaseNHibernate<FiltroReporte, int>, IDaoFiltroReporte
    {
        
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<FiltroReporte> ObtenerFiltrosDeReporte(Reporte reporte)
        {
            IList<FiltroReporte> retorno;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerFiltrosDeReporte, reporte.Id.ToString()));
                retorno = query.List<FiltroReporte>();

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
