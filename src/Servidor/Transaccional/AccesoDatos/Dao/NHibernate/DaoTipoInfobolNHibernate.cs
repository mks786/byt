using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoTipoInfobolNHibernate : DaoBaseNHibernate<TipoInfobol, string>, IDaoTipoInfobol
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
