using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoMedioNHibernate : DaoBaseNHibernate<Medio, string>, IDaoMedio
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
