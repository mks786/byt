using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoServicioNHibernate : DaoBaseNHibernate<Servicio, string>, IDaoServicio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
