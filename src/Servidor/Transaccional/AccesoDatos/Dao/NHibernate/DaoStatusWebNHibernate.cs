using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoStatusWebNHibernate : DaoBaseNHibernate<StatusWeb, string>, IDaoStatusWeb
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
