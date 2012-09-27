using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoConceptoNHibernate : DaoBaseNHibernate<Concepto, string>, IDaoConcepto
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
