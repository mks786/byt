using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Fabrica
{
    public abstract class FabricaDaoBase: IFabricaDaoBase
    {
        private static readonly string[] _manejadores = { "NHibernate" };

        /// <summary>
        /// Método que devuelve la FabricaDao del manejador que se esté usando
        /// </summary>
        /// <returns>FabricaDao del manejador que se esté usando</returns>
        public static FabricaDaoBase ObtenerFabricaDao()
        {
            string manejador = ConfigurationManager.AppSettings[@"ManejadorBD"];

            if (manejador == _manejadores[0])
            {
                return FabricaDaoNHibernate.ObtenerInstancia;
            }
            else
                return null;
        }

        /// <summary>
        /// Método que devuelve el DaoAgente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoAgente</returns>
        public abstract IDaoAgente ObtenerDaoAgente();

        /// <summary>
        /// Método que devuelve el DaoAuditoria del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoAuditoria</returns>
        public abstract IDaoAuditoria ObtenerDaoAuditoria();

        /// <summary>
        /// Método que devuelve el DaoDepartamento del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoDepartamento</returns>
        public abstract IDaoDepartamento ObtenerDaoDepartamento();

        /// <summary>
        /// Método que devuelve el DaoEstado del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoEstado</returns>
        public abstract IDaoEstado ObtenerDaoEstado();

        /// <summary>
        /// Método que devuelve el DaoEstatus del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoEstatus</returns>
        public abstract IDaoEstatus ObtenerDaoEstatus();


        /// <summary>
        /// Método que devuelve el DaoInternacional del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoInternacional</returns>
        public abstract IDaoInternacional ObtenerDaoInternacional();

        /// <summary>
        /// Método que devuelve el DaoNacional del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoNacional</returns>
        public abstract IDaoNacional ObtenerDaoNacional();

        /// <summary>
        /// Método que devuelve el DaoObjeto del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoObjeto</returns>
        public abstract IDaoObjeto ObtenerDaoObjeto();

        /// <summary>
        /// Método que devuelve el DaoPais del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoPais</returns>
        public abstract IDaoPais ObtenerDaoPais();

        /// <summary>
        /// Método que devuelve el DaoResolucion del manejador que se esté utilizando
        /// </summary>
        /// <returns>Daoresolucion</returns>
        public abstract IDaoResolucion ObtenerDaoResolucion();

        /// <summary>
        /// Método que devuelve el DaoRol del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoRol</returns>
        public abstract IDaoRol ObtenerDaoRol();

        /// <summary>
        /// Método que devuelve el DaoTipoFecha del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoFecha</returns>
        public abstract IDaoTipoFecha ObtenerDaoTipoFecha();

        /// <summary>
        /// Método que devuelve el DaoTipoInfobol del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoInfobol</returns>
        public abstract IDaoTipoInfobol ObtenerDaoTipoInfobol();

        /// <summary>
        /// Método que devuelve el DaoUsuario del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoUsuario</returns>
        public abstract IDaoUsuario ObtenerDaoUsuario();

        /// <summary>
        /// Método que devuelve el DaoBoletin del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoBoletin</returns>
        public abstract IDaoBoletin ObtenerDaoBoletin();

        /// <summary>
        /// Método que devuelve el DaoPoder del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoBoletin</returns>
        public abstract IDaoPoder ObtenerDaoPoder();

        /// <summary>
        /// Método que devuelve el DaoContador del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoContador</returns>
        public abstract IDaoContador ObtenerDaoContador();

        /// <summary>
        /// Método que devuelve el DaoContadorAuditoria del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoContadorAuditoria</returns>
        public abstract IDaoContadorAuditoria ObtenerDaoContadorAuditoria();

        /// <summary>
        /// Método que devuelve el DaoInteresado del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoInteresado</returns>
        public abstract IDaoInteresado ObtenerDaoInteresado();
    }
}
