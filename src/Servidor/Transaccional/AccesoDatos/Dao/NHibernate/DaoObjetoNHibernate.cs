using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoObjetoNHibernate : DaoBaseNHibernate<Objeto, string>, IDaoObjeto
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
