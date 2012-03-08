using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoTipoBaseNHibernate : DaoBaseNHibernate<TipoBase, string>, IDaoTipoBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
