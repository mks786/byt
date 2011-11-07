using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoCartaNHibernate : DaoBaseNHibernate<Carta, int>, IDaoCarta
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IList<Carta> ObtenerCartasFiltro(Carta carta)
        {
            IList<Carta> cartas = null;
            bool variosFiltros = false;
            string filtro = "";
            string cabecera = string.Format(Recursos.ConsultasHQL.CabeceraObtenerCarta);
            if ((null != carta) && (carta.Id != null))
            {
                filtro = string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaId, carta.Id);
                variosFiltros = true;
            }
            if ((null != carta.Asociado) && (!carta.Asociado.Id.Equals("")))
            {
                if (variosFiltros)
                    filtro += " and ";
                filtro += string.Format(Recursos.ConsultasHQL.FiltroObtenerCartaIdAsociado, carta.Asociado.Id);
                variosFiltros = true;
            }
            IQuery query = Session.CreateQuery(cabecera + filtro);
            cartas = query.List<Carta>();
            return cartas;
        }
    }
}
