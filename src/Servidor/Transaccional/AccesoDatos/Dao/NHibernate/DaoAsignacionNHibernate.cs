using NLog;
using System;
using System.Configuration;
using NHibernate;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoAsignacionNHibernate : DaoBaseNHibernate<Asignacion, int>, IDaoAsignacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();



        public IList<Asignacion> ObtenerAsignacionesPorCarta(Carta carta)
        {
            IList<Asignacion> asignaciones = null;
            IQuery query = Session.CreateQuery(string.Format(Recursos.ConsultasHQL.ObtenerAsignacionesPorCarta,carta.Id));
            asignaciones = query.List<Asignacion>();
            
            return asignaciones;
        }
    }
}
