using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoPoderNHibernate : DaoBaseNHibernate<Poder, int>, IDaoPoder
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Poder> ObtenerPoderesPorInteresado(Interesado interesado)
        {
            IList<Poder> poderes;

            try
            {
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerPoderesPorInteresado, interesado.Id));
                poderes = query.List<Poder>();
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

            return poderes;
        }
    }
}
