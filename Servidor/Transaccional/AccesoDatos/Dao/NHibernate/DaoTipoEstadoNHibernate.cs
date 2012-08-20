using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoTipoEstadoNHibernate : DaoBaseNHibernate<TipoEstado, string>, IDaoTipoEstado
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
