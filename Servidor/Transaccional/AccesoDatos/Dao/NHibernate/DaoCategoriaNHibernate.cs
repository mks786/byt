using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoCategoriaNHibernate : DaoBaseNHibernate<Categoria, string>, IDaoCategoria
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
