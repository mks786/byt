using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoContadorAsignacionNHibernate : DaoBaseNHibernate<ContadorAsignacion, string>, IDaoContadorAsignacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
