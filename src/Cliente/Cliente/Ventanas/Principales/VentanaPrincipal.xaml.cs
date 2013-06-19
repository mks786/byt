using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Recursos;
using Trascend.Bolet.Cliente.Presentadores.Principales;
using Trascend.Bolet.Cliente.Contratos.Principales;
using Trascend.Bolet.Cliente.Ventanas.Logines;

namespace Trascend.Bolet.Cliente.Ventanas.Principales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window, IVentanaPrincipal
    {
        private PresentadorVentanaPrincipal _presentador;


        #region Singleton

        private static VentanaPrincipal _ventanaPrincipal;

        public static VentanaPrincipal ObtenerInstancia
        {
            get
            {
                if (null == _ventanaPrincipal)
                    _ventanaPrincipal = new VentanaPrincipal();
                return _ventanaPrincipal;
            }
        }

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        private VentanaPrincipal()
        {
            InitializeComponent();
            this._presentador = new PresentadorVentanaPrincipal(this);

            //Colocamos la página principal en el Frame principal
            this.Contenedor.Navigate(PaginaPrincipal.ObtenerInstancia);
        }

        #endregion

        #region IVentanaPrincipal

        public Frame Contenedor
        {
            get { return this._framePrincipal; }
        }

        public Menu Menu
        {
            get { return this._menuPrincipal; }
        }

        public void mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        #region Eventos del menú

        #region Correspondencia

        private void _menuItemAnexoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarAnexos();
        }

        private void _menuItemAnexoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarAnexo();
        }

        private void _menuItemResumenConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarResumenes();
        }

        private void _menuItemResumenAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarResumen();
        }

        private void _menuItemRemitenteAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarRemitente();
        }

        private void _menuItemRemitenteConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarRemitentes();
        }
        private void _menuItemMedioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMedios();
        }

        private void _menuItemMedioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMedio();
        }

        private void _menuItemCategoriaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCategorias();
        }

        private void _menuItemCategoriaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCategoria();

        }

        private void _menuItemEntradaAlternaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarEntradasAlternas();
        }
        private void _menuItemTransferirPlantilla_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.TransferirPlantilla();
        }

        private void _menuItemEntradaAlternaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarEntradaAlterna();
        }

        private void _menuItemCartaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCartas();
        }

        private void _menuItemCartaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCarta();
        }

        #endregion

        #region Tablas

        //Metodo de prueba para consultar Inventores - CODIGO DE EJEMPLO -----------------
        private void _menuItemInventoresConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInventores();
            //this._presentador.IrConsultarInventores();
        }
        //--------------------------------------------------------------------------------

        private void _menuItemAgenteAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarAgente();
        }

        private void _menuItemAgenteConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarAgentes();
        }

        private void _menuItemBoletinConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarBoletines();
        }

        private void _menuItemBoletinAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarBoletin();
        }

        private void _menuItemEstadoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarEstados();
        }

        private void _menuItemEstadoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarEstados();
        }

        private void _menuItemInternacionalConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInternacionales();
        }

        private void _menuItemInternacionalAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarInternacional();
        }

        private void _menuItemNacionalConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarNacionales();
        }

        private void _menuItemNacionalAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarNacionales();
        }

        private void _menuItemObjetoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarObjetos();
        }

        private void _menuItemObjetoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarObjeto();
        }

        private void _menuItemGestionarObjetosXRoles_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarObjetosXRoles();
        }

        private void _menuItemPaisConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPais();
        }

        private void _menuItemPaisAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarPais();
        }

        private void _menuItemResolucionConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarResoluciones();
        }

        private void _menuItemResolucionAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarResolucion();
        }

        private void _menuItemRolConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarRoles();
        }

        private void _menuItemRolAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarRol();
        }

        private void _menuItemTipoInfobolConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarTipoInfoboles();
        }

        private void _menuItemTipoInfobolAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoInfobol();
        }

        private void _menuItemUsuarioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarUsuarios();
        }

        private void _menuItemUsuarioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarUsuarios();
        }

        private void _menuItemTipoFechaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarTipoFechas();
        }

        private void _menuItemTipoFechaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoFecha();
        }

        private void _menuItemEstatusAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarEstatus();
        }

        private void _menuItemEstatusConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarEstatuses();
        }

        private void _menuItemPoderConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPoderes();

        }

        private void _menuItemPoderAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarPoder();
        }

        private void _menuItemGestionarPoderesXAgentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarPoderXAgente();
        }

        private void _menuItemInteresadoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresados();
        }

        private void _menuItemInteresadoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarInteresado();
        }

        private void _menuItemAsociadoConsultar_Click(object sender, RoutedEventArgs e)
        {
             this._presentador.ConsultarAsociados();
        }

        private void _menuItemAsociadoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarAsociado();
        }

        private void _menuItemCorresponsalConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCorresponsales();
        }

        private void _menuItemCorresponsalAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCorresponsal();
        }

        private void _menuItemTipoEmailAsociadoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarTipoEmailAsociado();
        }

        private void _menuItemTipoEmailAsociadoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarTipoEmailAsociado();
        }

        #endregion

        #region Marcas

        private void _menuItemMarcasConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMarcas();
        }

        private void _menuItemMarcasAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMarca();
        }

        private void _menuItemCesionesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCesiones();
        }

        private void _menuItemCesionAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCesion();
        }

        private void _menuItemFusionesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFusiones();
        }

        private void _menuItemFusionAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFusion();
        }

        private void _menuItemCambioDeNombre_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CambioDeNombre();
        }

        private void _menuItemCambiosPeticionarioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCambiosDePeticionario();
        }

        private void _menuItemCambiosPeticionarioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCambioPeticionario();
        }

        private void _menuItemLicenciasConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrConsultarLicencias();
        }

        private void _menuItemLicenciaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarLicencia();
        }

        private void _menuItemCambiosDeDomicilioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCambiosDeDomicilio();
        }

        private void _menuItemCambiosDeDomicilioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCambioDeDomicilio();
        }

        private void _menuItemCambiosDeNombreConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCambiosDeNombre();
        }

        private void _menuItemCambiosNombreAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCambioDeNombre();
        }

        private void _menuItemRecordatorio_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.Recordatorios();
        }

        private void _menuItemMarcasATercerosConsultar_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.ConsultarMarcaATerceros();
        }

        private void _menuItemMarcasATercerosGestionar_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.GestionarMarcaATercero();
        }
        private void _menuItemTipoBaseConsultar_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.ConsultarTipoBase();
        }

        private void _menuItemTipoBaseGestionar_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.GestionarTipoBase();
        }

        private void _menuItemEstadosMarcaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarEstadosMarca();
        }

        private void _menuItemEstadosMarcaGestionar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarEstadosMarcas();
        }

        private void _menuItemAbandonoConsultar_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.ConsultarAbandonos();
        }

        private void _menuItemAbandonoGestionar_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.GestionarAbandonos();
        }

        private void _menuItemRenovacionesConsultar_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.ConsultarRenovacionMarca();
        }

        private void _menuItemRenovacionesAgregar_Click(object sender, RoutedEventArgs e)
        {
            //llamada
            this._presentador.GestionarRenovacionMarca();
        }

        private void _menuItemEstadosMarcasConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarEstadosMarca();
        }

        private void _menuItemEstadosMarcaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarEstadosMarcas();
        }

        private void _menuItemCartas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrCarta(((MenuItem)sender).Name);
        }

        #region Escritos

        private void _menuItemEscritos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrEscrito(((MenuItem)sender).Name);
        }

        #endregion

        #endregion

        #region Patentes

        private void _menuItemPatentesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPatentes();
        }

        private void _menuItemPatentesAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarPatente();
        }

        private void _menuItemAnualidadesConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarAnualidades();
        }

        private void _menuItemAnualidadesAgregarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarAnualidades();
        }

        private void _menuItemAbandonoPatenteConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarAbandonosPatente();
        }

        private void _menuItemAbandonoPatenteGestionarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarAbandonoPatente();
        }

        private void _menuItemCopiaCertificadaPatentePatente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _menuItemCopiaCertificadaPatente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _menuItemCorreccionErrorMaterialPatente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _menuItemPatentesATercerosConsultarPatente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _menuItemPatentesATercerosGestionarPatente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _menuItemEstadosPatentesConsultarPatente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _menuItemEstadosPatenteAgregarPatente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _menuItemTipoBaseConsultarPatente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _menuItemTipoBaseGestionarPatente_Click(object sender, RoutedEventArgs e)
        {

        }

        #region TraspasosPatentes

        private void _menuItemCesionesPatentesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCesionesPatentes();
        }

        private void _menuItemCesionPatentesAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCesionPatentes();
        }

        private void _menuItemFusionesPatentesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFusionesPatentes();
        }

        private void _menuItemFusionPatentesAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFusionPatentes();
        }

        private void _menuItemCambiosDeDomicilioPatentesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCambiosDeDomicilioPatentes();
        }

        private void _menuItemCambiosDeDomicilioPatentesAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCambioDeDomicilioPatentes();
        }

        private void _menuItemCambiosDeNombrePatentesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCambiosDeNombrePatentes();
        }

        private void _menuItemCambiosNombrePatentesAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCambioDeNombrePatentes();
        }

        private void _menuItemCambiosPeticionarioPatentesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCambioPeticionarioPatentes();
        }

        private void _menuItemCambiosPeticionarioPatentesAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCambioPeticionarioPatentes();
        }

        private void _menuItemLicenciasPatentesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrConsultarLicenciasPatentes();
        }

        private void _menuItemLicenciaPatentesAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarLicenciaPatentes();
        }

        #endregion

        #region Escritos

        private void _menuItemEscritosPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrEscritoPatentes(((MenuItem)sender).Name);
        }

        #endregion

        #endregion

        #region Facturacion
        private void _fac_menuItemTasaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTasa();
        }

        private void _fac_menuItemTasaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarTasas();
        }

        private void _fac_menuItemAnualidadAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacAnualidad();
        }

        private void _fac_menuItemAnualidadConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacAnualidades();
        }

        private void _fac_menuItemDetalleEnvioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarDetalleEnvio();
        }

        private void _fac_menuItemDetalleEnvioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarDetalleEnvios();
        }

        private void _fac_menuItemDetallePagoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarDetallePago();
        }

        private void _fac_menuItemDetallePagoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarDetallePagos();
        }


        private void _fac_menuItemDocumentosMarcaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarDocumentosMarca();
        }

        private void _fac_menuItemDocumentosMarcaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarDocumentosMarcas();
        }
        private void _fac_menuItemDocumentosPatenteAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarDocumentosPatente();
        }

        private void _fac_menuItemDocumentosPatenteConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarDocumentosPatentes();
        }
        private void _fac_menuItemDocumentosTraduccionAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarDocumentosTraduccion();
        }

        private void _fac_menuItemDocumentosTraduccionConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarDocumentosTraducciones();
        }
        private void _fac_menuItemFacRecursoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacRecurso();
        }

        private void _fac_menuItemFacRecursoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacRecursos();
        }
        private void _fac_menuItemGuiaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarGuia();
        }

        private void _fac_menuItemGuiaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarGuias();
        }
        private void _fac_menuItemImpuestoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarImpuesto();
        }

        private void _fac_menuItemImpuestoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarImpuestos();
        }
        private void _fac_menuItemMaterialAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMaterial();
        }

        private void _fac_menuItemMaterialConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMateriales();
        }
        private void _fac_menuItemMotivoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMotivo();
        }

        private void _fac_menuItemMotivoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMotivos();
        }

        private void _fac_menuItemFacTarifaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacTarifas();
        }

        private void _fac_menuItemFacTarifaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacTarifa();
        }

        private void _fac_menuItemSociedadAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarSociedad();
        }

        private void _fac_menuItemSociedadConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarSociedades();
        }
        private void _fac_menuItemTipoClaseAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoClase();
        }

        private void _fac_menuItemTipoClaseConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarTipoClases();
        }
        private void _fac_menuItemTipoMarcaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoMarca();
        }

        private void _fac_menuItemTipoMarcaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarTipoMarcas();
        }
        private void _fac_menuItemTipoPatenteAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoPatente();
        }

        private void _fac_menuItemTipoPatenteConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarTipoPatentes();
        }

        private void _fac_menuItemServicioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarServicio();
        }

        private void _fac_menuItemServicioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarServicios();
        }



        private void _fac_menuItemCorrespondenciaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCorrespondencia();
        }

        private void _fac_menuItemCorrespondenciaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCorrespondencias();
        }

        private void _fac_menuItemFacJustificacionAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacJustificacion();
        }

        private void _fac_menuItemFacJustificacionConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacJustificaciones();
        }

        private void _fac_menuItemEtiquetaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarEtiqueta();
        }

        private void _fac_menuItemEtiquetaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarEtiquetas();
        }

        private void _fac_menuItemDepartamentoServicioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarDepartamentoServicio();
        }

        private void _fac_menuItemDepartamentoServicioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarDepartamentoServicios();
        }

        private void _fac_menuItemDesgloseServicioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarDesgloseServicio();
        }

        private void _fac_menuItemDesgloseServicioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarDesgloseServicios();
        }

        private void _fac_menuItemTarifaServicioAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTarifaServicio();
        }

        private void _fac_menuItemNumeroControl_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.NumeroControl();
        }

        private void _fac_menuItemTarifaServicioConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarTarifaServicios();
        }

        private void _fac_menuItemConceptoGestionAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarConceptoGestion();
        }

        private void _fac_menuItemConceptoGestionConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarConceptoGestiones();
        }

        private void _fac_menuItemMediosGestionAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMediosGestion();
        }

        private void _fac_menuItemMediosGestionConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMediosGestiones();
        }

        private void _fac_menuItemViGestionAsociadoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarViGestionAsociados();
        }
        private void _fac_menuItemChequeRecidoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarChequeRecido();
        }

        private void _fac_menuItemChequeRecidoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarChequeRecidos();
        }

        private void _fac_menuItemChequeRecidoConsultarDeposito_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarDepositoChequeRecidos();
        }

        private void _fac_menuItemFacPagoBoliviaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacPagoBolivias();
        }

        private void _fac_menuItemFacPagoBoliviaConsultarPago_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPagoFacPagoBolivias();
        }
        private void _fac_menuItemFacPagoBoliviaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacPagoBolivia();
        }

        private void _fac_menuItemFacCobroConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacCobros();
        }

        private void _fac_menuItemFacCobroAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacCobro();
        }


        private void _fac_menuItemFacCreditoConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacCreditos();
        }

        private void _fac_menuItemFacCreditoAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacCredito();
        }

        private void _fac_menuItemFacFacturaProformaAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacFacturaProforma();
        }

        private void _fac_menuItemFacFacturaProformaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacFacturaProformas();
        }

        private void _fac_menuItemFacFacturaProformaConsultarPendientes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacFacturaProformasPendientes();
        }
        private void _fac_menuItemFacFacturaProformaConsultarDepartamentos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacFacturaProformasDepartamentos();
        }
        private void _fac_menuItemFacFacturaProformaConsultarAutorizacion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacFacturaProformasAutorizacion();
        }
        private void _fac_menuItemFacFacturaProformaConsultarTodas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacFacturaProformasTodas();
        }
        private void _fac_menuItemProformaaFactura_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ProformaaFactura();
        }
        private void _fac_menuItemFacFacturaConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacFacturas();
        }

        private void _fac_menuItemFacFacturaAnulacion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacFacturaAnulada();
        }

        private void _fac_menuItemFacFacturaConsultarAnuladas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacFacturasAnuladas();
        }

        private void _fac_menuItemFacturaDigital_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacturasDigitales();
        }

        private void _fac_menuItemFacEstadoCuenta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacEstadoCuenta();
        }

        private void _fac_menuItemFacPendientesRpt_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacPendientesRpt();
        }


        private void _fac_menuItemFacEnvioAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacEnvioAsociado();
        }

        private void _fac_menuItemFacGestionAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFacGestion();
        }

        private void _fac_menuItemFacGestionConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacGestiones();
        }

        private void _fac_menuItemFacInternacional_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFacInternacionalesProforma();
        }

        private void _fac_menuItemConsultaStatementPorProcesar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaStatementPorProcesar();
        }

        private void _fac_menuItemConsultaFacturacionAnuladas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaFacturacionAnuladas();
        }

        private void _fac_menuItemConsultaFacturacionAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaFacturacionAsociado();
        }

        private void _fac_menuItemConsultaFacturacionVigenteAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaFacturacionVigenteAsociado();
        }

        private void _fac_menuItemConsultaFacturasAnuladas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaFacturasAnuladas();
        }

        private void _fac_menuItemConsultaFacturasAnuladasFisicas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaFacturasAnuladasFisicas();
        }

        private void _fac_menuItemConsultaCreditosAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaCreditosAsociado();
        }

        private void _fac_menuItemConsultaCreditosVigentesAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaCreditosVigentesAsociado();
        }

        private void _fac_menuItemConsultaPagosAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaPagosAsociado();
        }

        private void _fac_menuItemConsultaOperacionesAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultaOperacionesAsociado();
        }

        private void _fac_menuItemResumenOperacionesRpt_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ResumenOperacionesRpt();
        }

        private void _fac_menuItemVentasTotalesRevRpt_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VentasTotalesRevRpt();
        }

        private void _fac_menuItemVentasTotalesRpt_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VentasTotalesRpt();
        }

        private void _fac_menuItemFacturaEncabezado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacturaEncabezado();
        }

        private void _fac_menuItemFacturaDetalle_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacturaDetalle();
        }

        private void _fac_menuItemFacAsociadoProfit_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacAsociadoProfit();
        }

        private void _fac_menuItemFacFacturacionPendiente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacFacturacionPendiente();
        }

        private void _fac_menuItemFacFacturacionPendienteVieja_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacFacturacionPendienteVieja();
        }

        private void _fac_menuItemFacFacturacionLoteVieja_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.FacFacturacionLoteVieja();
        }

        #endregion

        #endregion

        #region Eventos de la ventana

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            //Al ejecutar la siClienteente línea se dispara el evento "Window_Closing"

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionSalirDelSistema,
                "Salir del sistema", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Salir();
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Métodos

        public void AplicarPermisologia()
        {
            this._presentador.AplicarPermisologia();
        }

        #endregion

        private void _menuItemInventores_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrConsultarInventores();
        }

       
    }
}
