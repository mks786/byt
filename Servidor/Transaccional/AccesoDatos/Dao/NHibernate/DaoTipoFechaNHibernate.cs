using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoTipoFechaNHibernate : DaoBaseNHibernate<TipoFecha, string>, IDaoTipoFecha
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
