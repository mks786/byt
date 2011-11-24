using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;

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
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerListaDatosValoresPorParametro, listaDatosValores.Id));
                listaDaosValores = query.List<ListaDatosValores>();
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
