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

        public IList<Agente> ObtenerAgentesFiltro(Agente agente)
        {
            IList<Agente> agentes = null;
            bool variosFiltros = false;
            string filtro = "";
            string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAgente);
            if ((null != agente) && (!string.IsNullOrEmpty(agente.Id)))
            {
                filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteId, agente.Id);
                variosFiltros = true;
            }
            if (!string.IsNullOrEmpty(agente.Nombre))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteNombre, agente.Nombre);
            }
            IQuery query = Session.CreateQuery(cabecera + filtro);
            agentes = query.List<Agente>();
            return agentes;
        }

        public IList<Agente> ObtenerAgentesSinPoderesFiltro(Agente agente)
        {
            IList<Agente> agentes = null;
            bool variosFiltros = false;
            string filtro = "";
            string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerAgenteDesinflados);
            if ((null != agente) && (!string.IsNullOrEmpty(agente.Id)))
            {
                filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteId, agente.Id);
                variosFiltros = true;
            }
            if (!string.IsNullOrEmpty(agente.Nombre))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerAgenteNombre, agente.Nombre);
            }
            IQuery query = Session.CreateQuery(cabecera + filtro);
            agentes = query.List<Agente>();
            return agentes;
        }

        public IList<Agente> ObtenerAgentesDeUnPoder(Poder poder)
        {
            IList<Agente> agentes;

            try
            {
                
                IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerAgentesDeUnPoder,poder.Id));
                Poder poderAux = query.UniqueResult<Poder>();

                agentes = poderAux.Agentes;
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
