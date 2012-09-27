using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoTarifaNHibernate : DaoBaseNHibernate<Tarifa, string>, IDaoTarifa
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
