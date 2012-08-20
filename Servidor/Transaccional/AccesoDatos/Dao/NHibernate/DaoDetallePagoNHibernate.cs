using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoDetallePagoNHibernate : DaoBaseNHibernate<DetallePago, string>, IDaoDetallePago
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
