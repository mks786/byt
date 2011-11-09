using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoEntradaAlternaNHibernate : DaoBaseNHibernate<EntradaAlterna, int>, IDaoEntradaAlterna
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
