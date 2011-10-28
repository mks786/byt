using NLog;
using Trascend.Bolet.AccesoDatos.Contrato;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Dao.NHibernate
{
    public class DaoAnexoNHibernate : DaoBaseNHibernate<Anexo, string>, IDaoAnexo
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
    }
}
