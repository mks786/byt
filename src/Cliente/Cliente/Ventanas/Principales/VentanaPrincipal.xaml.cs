using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Recursos;
using Trascend.Bolet.Cliente.Presentadores.Principales;
using Trascend.Bolet.Cliente.Contratos.Principales;

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

        #region Historia

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

        private void _menuItemFusionesConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarFusiones();
        }

        private void _menuItemCambioDeDomicilio_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CambioDeDomicilio();
        }

        private void _menuItemCambioDeNombre_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CambioDeNombre();
        }

        private void _menuItemCambioPeticionario_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CambioPeticionario();
        }

        private void _menuItemLicencias_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Licencias();
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

    }
}
