using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoNacionalNHibernate : DaoBaseNHibernate<Nacional, int>, IDaoNacional
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
