using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;
using NHibernate;
using System.Collections.Generic;
using System;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoAgenteNHibernate : DaoBaseNHibernate<Agente, string>, IDaoAgente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Agente> ObtenerAgentesYPoderes()
        {
            IList<Agente> agentes;

            try
            {
                IQuery query = Session.CreateQuery(Recursos.ConsultasHQL.ObtenerAgentesYPoderes);
                agentes = query.List<Agente>();
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

            return agentes;
        }
        
    }
}
