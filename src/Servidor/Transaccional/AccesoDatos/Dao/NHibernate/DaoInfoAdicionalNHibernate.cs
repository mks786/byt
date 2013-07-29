using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoInfoAdicionalNHibernate : DaoBaseNHibernate<InfoAdicional, string>, IDaoInfoAdicional
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<InfoAdicional> ObtenerInfoAdicionalDistingueInglesFiltro(InfoAdicional infoAdicional)
        {
            IList<InfoAdicional> InfoAdicionales;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInfoAdicionalDistingueIngles, infoAdicional.Info, infoAdicional.Id));
                InfoAdicionales = query.List<InfoAdicional>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.exObtenerInfobolesPorMarca);
            }
            finally
            {
                Session.Close();
            }

            return InfoAdicionales;
        }
    }
}
