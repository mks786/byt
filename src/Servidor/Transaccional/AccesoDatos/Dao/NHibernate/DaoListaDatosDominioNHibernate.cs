using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using NHibernate;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoListaDatosDominioNHibernate : DaoBaseNHibernate<ListaDatosDominio, int>, IDaoListaDatosDominio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<ListaDatosDominio> ObtenerListaDatosDominioPorParametro(ListaDatosDominio parametro)
        {
            IList<ListaDatosDominio> listaDatos = null;
            IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerListaDatosDominioPorParametro, parametro.Filtro));
            listaDatos = query.List<ListaDatosDominio>();

            return listaDatos;
        }
    }
}
