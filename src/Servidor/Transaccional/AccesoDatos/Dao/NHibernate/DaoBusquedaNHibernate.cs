using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoBusquedaNHibernate : DaoBaseNHibernate<Busqueda, int>, IDaoBusqueda
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Busqueda> ObtenerBusquedasPorMarca(Marca marca)
        {
            IList<Busqueda> busquedas;

            try
            {
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerInfoBolesPorMarcas, marca.Id));
                busquedas = query.List<Busqueda>();
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

            return busquedas;
        }
    }
}
