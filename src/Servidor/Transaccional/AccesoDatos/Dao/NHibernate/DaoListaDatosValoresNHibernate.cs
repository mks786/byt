using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using NHibernate.Criterion;
using System;
using System.Configuration;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoListaDatosValoresNHibernate : DaoBaseNHibernate<ListaDatosValores, string>, IDaoListaDatosValores
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<ListaDatosValores> ObtenerListaDatosValoresPorParametro(ListaDatosValores listaDatosValores)
        {
            IList<ListaDatosValores> listaDaosValores;

            try
            {
                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Entrando al Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion

                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerListaDatosValoresPorParametro, listaDatosValores.Id));                

                listaDaosValores = query.List<ListaDatosValores>();

                #region trace
                if (ConfigurationManager.AppSettings["Ambiente"].ToString().Equals("Desarrollo"))
                    logger.Debug("Saliendo del Método {0}", (new System.Diagnostics.StackFrame()).GetMethod().Name);
                #endregion
                //listaDaosValores = Session.CreateCriteria(typeof(IDaoListaDatosValores)).AddOrder(Order.Asc(listaDatosValores.Id)).List<ListaDatosValores>();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException(Recursos.Errores.ExConsultarTodos);
            }
            finally
            {
                Session.Close();
            }

            return listaDaosValores;
        }
    }
}
