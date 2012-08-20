using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoContadorNHibernate : DaoBaseNHibernate<Contador, string>, IDaoContador
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
