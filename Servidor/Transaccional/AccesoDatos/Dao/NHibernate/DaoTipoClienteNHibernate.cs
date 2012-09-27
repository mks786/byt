using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoTipoClienteNHibernate : DaoBaseNHibernate<TipoCliente, string>, IDaoTipoCliente
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
