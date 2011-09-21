using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoResolucionNHibernate : DaoBaseNHibernate<Resolucion, string>, IDaoResolucion
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
