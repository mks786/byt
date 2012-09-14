using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoFusionMarcaTerceroNHibernate : DaoBaseNHibernate<FusionMarcaTercero, int>, IDaoFusionMarcaTercero
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
