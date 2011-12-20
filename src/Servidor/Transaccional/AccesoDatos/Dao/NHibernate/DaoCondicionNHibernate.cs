using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCondicionNHibernate : DaoBaseNHibernate<Condicion, int>, IDaoCondicion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
