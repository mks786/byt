using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoEstadoNHibernate : DaoBaseNHibernate<Estado, string>, IDaoEstado
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
