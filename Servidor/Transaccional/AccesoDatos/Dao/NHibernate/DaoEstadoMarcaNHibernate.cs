using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoEstadoMarcaNHibernate : DaoBaseNHibernate<EstadoMarca, string>, IDaoEstadoMarca
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
