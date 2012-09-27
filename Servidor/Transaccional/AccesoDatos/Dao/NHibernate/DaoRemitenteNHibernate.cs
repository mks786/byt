using System;
using System.Configuration;
using NHibernate;
using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoRemitenteNHibernate : DaoBaseNHibernate<Remitente, string>, IDaoRemitente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
