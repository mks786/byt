using NLog;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    class DaoBoletinNHibernate : DaoBaseNHibernate<Boletin, string>, IDaoBoletin
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
