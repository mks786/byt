using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoContadorAuditoriaNHibernate : DaoBaseNHibernate<ContadorAuditoria, string>, IDaoContadorAuditoria
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
