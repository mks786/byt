using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoIdiomaNHibernate : DaoBaseNHibernate<Idioma, string>, IDaoIdioma
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
