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
        /// Método que devuelve el DaoContadorAsignacion del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoContadorAsignacion</returns>
        public abstract IDaoContadorAsignacion ObtenerDaoContadorAsignacion();

        /// <summary>
        /// Método que devuelve el DaoInteresado del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoInteresado</returns>
        public abstract IDaoInteresado ObtenerDaoInteresado();

        /// <summary>
        /// Método que devuelve el DaoEtiqueta del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoEtiqueta</returns>
        public abstract IDaoEtiqueta ObtenerDaoEtiqueta();

        /// <summary>
        /// Método que devuelve el DaoIdioma del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoIdioma</returns>
        public abstract IDaoIdioma ObtenerDaoIdioma();

        /// <summary>
        /// Método que devuelve el DaoMoneda del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMoneda</returns>
        public abstract IDaoMoneda ObtenerDaoMoneda();

        /// <summary>
        /// Método que devuelve el DaoTarifa del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTarifa</returns>
        public abstract IDaoTarifa ObtenerDaoTarifa();

        /// <summary>
        /// Método que devuelve el DaoTipoCliente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoCliente</returns>
        public abstract IDaoTipoCliente ObtenerDaoTipoCliente();

        /// <summary>
        /// Método que devuelve el DaoDetallePago del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoDetallePago</returns>
        public abstract IDaoDetallePago ObtenerDaoDetallePago();

        /// <summary>
        /// Método que devuelve el DaoConcepto del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMoneda</returns>
        public abstract IDaoConcepto ObtenerDaoConcepto();

        /// <summary>
        /// Método que devuelve el DaoJustificacion del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoJustificacion</returns>
        public abstract IDaoJustificacion ObtenerDaoJustificacion();

        /// <summary>
        /// Método que devuelve el DaoAsociado del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoAsociado</returns>
        public abstract IDaoAsociado ObtenerDaoAsociado();

        /// <summary>
        /// Método que devuelve el DaoContadorFac del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoContadorFac</returns>
        public abstract IDaoContadorFac ObtenerDaoContadorFac();

        /// <summary>
        /// Método que devuelve el DaoContacto del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoContacto</returns>
        public abstract IDaoContacto ObtenerDaoContacto();

        /// <summary>
        /// Método que devuelve el DaoCarta del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCarta</returns>
        public abstract IDaoCarta ObtenerDaoCarta();

        /// <summary>
        /// Método que devuelve el DaoDatosTransferencia del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoDatosTransferencia</returns>
        public abstract IDaoDatosTransferencia ObtenerDaoDatosTransferencia();

        /// <summary>
        /// Método que devuelve el DaoResumen del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoResumen</returns>
        public abstract IDaoResumen ObtenerDaoResumen();


        /// <summary>
        /// Método que devuelve el DaoAnexo del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoAnexo</returns>
        public abstract IDaoAnexo ObtenerDaoAnexo();


        /// <summary>
        /// Método que devuelve el DaoRemitente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoRemitente</returns>
        public abstract IDaoRemitente ObtenerDaoRemitente();

        /// <summary>
        /// Método que devuelve el DaoMedio del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMedio</returns>
        public abstract IDaoMedio ObtenerDaoMedio();

        /// <summary>
        /// Método que devuelve el DaoCategoria del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCategoria</returns>
        public abstract IDaoCategoria ObtenerDaoCategoria();

        /// <summary>
        /// Método que devuelve el DaoCartaOut del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCategoria</returns>
        public abstract IDaoCartaOut ObtenerDaoCartaOut();

        /// <summary>
        /// Método que devuelve el DaoEntradaAlterna del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoEntradaAlterna</returns>
        public abstract IDaoEntradaAlterna ObtenerDaoEntradaAlterna();

        /// <summary>
        /// Método que devuelve el DaoAsignacion del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoAsignacion</returns>
        public abstract IDaoAsignacion ObtenerDaoAsignacion();
    }
}
