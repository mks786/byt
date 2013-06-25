using System.Configuration;
using Trascend.Bolet.AccesoDatos.Contrato;

namespace Trascend.Bolet.AccesoDatos.Fabrica
{
    public abstract class FabricaDaoBase : IFabricaDaoBase
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
        /// Método que devuelve el DaoEstadoMarca del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoEstadoMarca</returns>
        public abstract IDaoEstadoMarca ObtenerDaoEstadoMarca();

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
        /// Método que devuelve el DaoTipoBase del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoBase</returns>
        public abstract IDaoTipoBase ObtenerDaoTipoBase();
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
        /// Método que devuelve el DaoTipoDocumento del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoDocumento</returns>
        public abstract IDaoTipoDocumento ObtenerDaoTipoDocumento();


        /// <summary>
        /// Método que devuelve el DaoTipoDocumento del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoDocumento</returns>
        public abstract IDaoAlmacen ObtenerDaoAlmacen();


        /// <summary>
        /// Método que devuelve el DaoTipoCaja del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoDocumento</returns>
        public abstract IDaoTipoCaja ObtenerDaoTipoCaja();

        /// <summary>
        /// Método que devuelve el DaoCaja del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCaja</returns>
        public abstract IDaoCaja ObtenerDaoCaja();


        /// <summary>
        /// Método que devuelve el DaoCaja del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoArchivo</returns>
        public abstract IDaoArchivo ObtenerDaoArchivo();

        
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

        /// <summary>
        /// Método que devuelve el DaoListaDatosValores del manejador que se esté utilizando
        /// </summary>
        /// <returns>DaoListaDatosValores</returns>
        public abstract IDaoListaDatosValores ObtenerDaoListaDatosValores();

        /// <summary>
        /// Método que devuelve el DaoListaDatosDominio del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoListaDatosDominio</returns>
        public abstract IDaoListaDatosDominio ObtenerDaoListaDatosDominio();

        /// <summary>
        /// Método que devuelve el DaoMarca del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMarca</returns>
        public abstract IDaoMarca ObtenerDaoMarca();

        /// <summary>
        /// Método que devuelve el DaoCorresponsal del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCorresponsal</returns>
        public abstract IDaoCorresponsal ObtenerDaoCorresponsal();

        /// <summary>
        /// Método que devuelve el DaoServicio del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoServicio</returns>
        public abstract IDaoServicio ObtenerDaoServicio();

        /// <summary>
        /// Método que devuelve el DaoTipoEstado del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoEstado</returns>
        public abstract IDaoTipoEstado ObtenerDaoTipoEstado();

        /// <summary>
        /// Método que devuelve el DaoCondicion del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCondicion</returns>
        public abstract IDaoCondicion ObtenerDaoCondicion();

        /// <summary>
        /// Método que devuelve el DaoAnaqua del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCoIDaoAnaquandicion</returns>
        public abstract IDaoAnaqua ObtenerDaoAnaqua();

        /// <summary>
        /// Método que devuelve el DaoInfoAdicional del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoInfoAdicional</returns>
        public abstract IDaoInfoAdicional ObtenerDaoInfoAdicional();

        /// <summary>
        /// Método que devuelve el DaoInfoBol del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoInfoBol</returns>
        public abstract IDaoInfoBol ObtenerDaoInfoBol();

        /// <summary>
        /// Método que devuelve el DaoInfoBol del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoInfoBolMarcaTer</returns>
        public abstract IDaoInfoBolMarcaTer ObtenerDaoInfoBolMarcaTer();


        /// <summary>
        /// Método que devuelve el DaoBusqueda del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoBusqueda</returns>
        public abstract IDaoBusqueda ObtenerDaoBusqueda();

        /// <summary>
        /// Método que devuelve el DaoOperacion del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoOperacion</returns>
        public abstract IDaoOperacion ObtenerDaoOperacion();

        /// <summary>
        /// Método que devuelve el DaoStatusWeb del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoStatusWeb</returns>
        public abstract IDaoStatusWeb ObtenerDaoStatusWeb();

        /// <summary>
        /// Método que devuelve el DaoFusion del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoFusion</returns>
        public abstract IDaoFusion ObtenerDaoFusion();

        /// <summary>
        /// Método que devuelve el DaoFusionPatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoFusionPatente</returns>
        public abstract IDaoFusionPatente ObtenerDaoFusionPatente();

        /// <summary>
        /// Método que devuelve el DaoCesion del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCesion</returns>
        /// 
        public abstract IDaoCesion ObtenerDaoCesion();

        /// <summary>
        /// Método que devuelve el DaoCesionPatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCesionPatente</returns>
        /// 
        public abstract IDaoCesionPatente ObtenerDaoCesionPatente();

        /// <summary>
        /// Método que devuelve el DaoCambioDeDomicilio del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCambioDeDomicilio</returns>
        public abstract IDaoCambioDeDomicilio ObtenerDaoCambioDeDomicilio();

        /// <summary>
        /// Método que devuelve el DaoCambioPeticionario del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCambioPeticionario</returns>
        public abstract IDaoCambioPeticionario ObtenerDaoCambioPeticionario();

        /// <summary>
        /// Método que devuelve el DaoCambioNombre del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCambioNombre</returns>
        public abstract IDaoCambioDeNombre ObtenerDaoCambioDeNombre();

        /// <summary>
        /// Método que devuelve el DaoCambioDeDomicilioPatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCambioDeDomicilioPatente</returns>
        public abstract IDaoCambioDeDomicilioPatente ObtenerDaoCambioDeDomicilioPatente();

        /// <summary>
        /// Método que devuelve el DaoAnualidad del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoAnualidad</returns>
        public abstract IDaoAnualidad ObtenerDaoAnualidad();
        /// <summary>
        /// Método que devuelve el DaoCambioNombrePatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCambioNombrePatente</returns>
        public abstract IDaoCambioDeNombrePatente ObtenerDaoCambioDeNombrePatente();

        /// <summary>
        /// Método que devuelve el DaoCambioPeticionarioPatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoCambioPeticionarioPatente</returns>
        public abstract IDaoCambioPeticionarioPatente ObtenerDaoCambioPeticionarioPatente();

        /// <summary>
        /// Método que devuelve el DaoLicencia del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoLicencia</returns>
        public abstract IDaoLicencia ObtenerDaoLicencia();

        /// <summary>
        /// Método que devuelve el DaoLicenciaPatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoLicenciaPatente</returns>
        public abstract IDaoLicenciaPatente ObtenerDaoLicenciaPatente();

        /// <summary>
        /// Método que devuelve el DaoMarcaTercero del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMarcaTercero</returns>
        public abstract IDaoMarcaTercero ObtenerDaoMarcaTercero();

        /// <summary>
        /// Método que devuelve el DaoMarcabaseTercero del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMarcaBaseTercero</returns>
        public abstract IDaoMarcaBaseTercero ObtenerDaoMarcaBaseTercero();

        /// <summary>
        /// Método que devuelve el DaoPlanilla del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoPlanilla</returns>
        public abstract IDaoPlanilla ObtenerDaoPlanilla();

        /// <summary>
        /// Método que devuelve el DaoRenovacion del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoRenovacion</returns>
        public abstract IDaoRenovacion ObtenerDaoRenovacion();

        /// <summary>
        /// Método que devuelve el DaoPatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoPatente</returns>
        public abstract IDaoPatente ObtenerDaoPatente();

        /// <summary>
        /// Método que devuelve el DaoInventor del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoInventor</returns>
        public abstract IDaoInventor ObtenerDaoInventor();

        /// <summary>
        /// Método que devuelve el DaoInfoBolPatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoInfoBolPatente</returns>
        public abstract IDaoInfoBolPatente ObtenerDaoInfoBolPatente();


        /// <summary>
        /// Método que devuelve el DaoFecha del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoFecha</returns>
        public abstract IDaoFecha ObtenerDaoFecha();


        /// <summary>
        /// Método que devuelve el DaoMemoria del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMemoria</returns>
        public abstract IDaoMemoria ObtenerDaoMemoria();


        /// <summary>
        /// Método que devuelve el DaoMemoria del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMemoria</returns>
        /// 
        public abstract IDaoInstruccionDeRenovacion ObtenerDaoInstruccionDeRenovacion();


        /// <summary>
        /// Método que devuelve el DaoMemoria del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMemoria</returns>
        /// 
        public abstract IDaoFusionMarcaTercero ObtenerDaoFusionMarcaTercero();


        /// <summary>
        /// Método que devuelve el DaoMemoria del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoMemoria</returns>
        public abstract IDaoFusionPatenteTercero ObtenerDaoFusionPatenteTercero();


        /// <summary>
        /// Método que devuelve el DaoReportePatente del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoReportePatente</returns>
        public abstract IDaoReportePatente ObtenerDaoReportePatente();


        /// <summary>
        /// Método que devuelve el DaoEmailAsociado del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoEmailAsociado</returns>
        public abstract IDaoEmailAsociado ObtenerDaoEmailAsociado();


        /// <summary>
        /// Método que devuelve el DaoTipoEmailAsociado del manejador que se esté utilizando
        /// </summary>
        /// <returns>IDaoTipoEmailAsociado</returns>
        public abstract IDaoTipoEmailAsociado ObtenerDaoTipoEmailAsociado();
    }
}
