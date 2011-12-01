using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoMarcaNHibernate : DaoBaseNHibernate<Marca, string>, IDaoMarca
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
