using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoJustificacionNHibernate : DaoBaseNHibernate<Justificacion, int>, IDaoJustificacion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
