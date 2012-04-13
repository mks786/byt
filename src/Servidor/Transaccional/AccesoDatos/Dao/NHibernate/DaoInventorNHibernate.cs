using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoInventorNHibernate : DaoBaseNHibernate<Inventor, int>, IDaoInventor
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
