using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoFechaMarcaNHibernate : DaoBaseNHibernate<FechaMarca, string>, IDaoFechaMarca
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<FechaMarca> ObtenerFechasPorMarca(Marca marca)
        {
            IList<FechaMarca> Fechas;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerFechasPorMarcasCertificados, marca.Id));
                Fechas = query.List<FechaMarca>();

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

            return Fechas;
        }
    }
}
