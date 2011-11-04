using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoCartaOutNHibernate : DaoBaseNHibernate<CartaOut, int>, IDaoCartaOut
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
