using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoEstatusNHibernate : DaoBaseNHibernate<Estatus, string>, IDaoEstatus
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
