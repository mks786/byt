using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoPaisNHibernate : DaoBaseNHibernate<Pais, int>, IDaoPais
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
