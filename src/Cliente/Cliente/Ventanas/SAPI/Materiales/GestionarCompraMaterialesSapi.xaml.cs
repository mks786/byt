using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales;

namespace Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales
{
    /// <summary>
    /// Lógica de interacción para GestionarCompraMaterialesSapi.xaml
    /// </summary>
    public partial class GestionarCompraMaterialesSapi : Page, IGestionarCompraMaterialesSapi 
    {

        private PresentadorGestionarCompraMaterialesSapi _presentador;
        private bool _cargada;


        #region IGestionarCompraMaterialesSapi

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtIdCompraMaterial.Focus();
        }

        public object CompraSapi
        {
            get { return this._stkCompraMaterialSapi.DataContext; }
            set { this._stkCompraMaterialSapi.DataContext = value; }
        }

        public object Materiales
        {
            get { return this._cbxMaterialesCompra.DataContext; }
            set { this._cbxMaterialesCompra.DataContext = value; }
        }

        public object Material
        {
            get { return this._cbxMaterialesCompra.SelectedItem; }
            set { this._cbxMaterialesCompra.SelectedItem = value; }
        }

        public string IdCompraSapi
        {
            get { return this._txtIdCompraMaterial.Text; }
            set { this._txtIdCompraMaterial.Text = value; }
        }

        public string CantidadMaterial
        {
            get { return this._txtCantidadMaterialCompraMaterial.Text; }
            set { this._txtCantidadMaterialCompraMaterial.Text = value; }
        }

        public object DetallesDeCompraSapi
        {
            get { return this._lstDetalleCompra.DataContext; }
            set { this._lstDetalleCompra.DataContext = value; }
        }

        public object DetalleDeCompraSapi
        {
            get { return this._lstDetalleCompra.SelectedItem; }
            set { this._lstDetalleCompra.SelectedItem = value; }
        }

        public string PorcentajeImpuesto
        {
            get { return this._txtIvaCompraMaterial.Text; }
            set { this._txtIvaCompraMaterial.Text = value; }
        }

        public string MontoImporte
        {
            get { return this._txtSubtotalCompraMaterial.Text; }
            set { this._txtSubtotalCompraMaterial.Text = value; }
        }

        public string MontoIva
        {
            get { return this._txtMontoIvaCompraMaterial.Text; }
            set { this._txtMontoIvaCompraMaterial.Text = value; }
        }

        public string TotalCompraSapi
        {
            get { return this._txtTotalCompraMaterial.Text; }
            set { this._txtTotalCompraMaterial.Text = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbAceptar.Text; }
            set { this._txbAceptar.Text = value; }
        }

        public string FechaCompraSapi
        {
            get { return this._dpkFechaCompraMaterial.Text; }
            set { this._dpkFechaCompraMaterial.Text = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtIdCompraMaterial.IsEnabled = value;
                this._dpkFechaCompraMaterial.IsEnabled = value;
                this._cbxMaterialesCompra.IsEnabled = value;
                this._lstDetalleCompra.IsEnabled = value;
                this._txtSubtotalCompraMaterial.IsEnabled = value;
                this._txtIvaCompraMaterial.IsEnabled = value;
                this._txtMontoIvaCompraMaterial.IsEnabled = value;
                this._txtTotalCompraMaterial.IsEnabled = value;
                this._btnTotalizarCompra.IsEnabled = value;
            }
        }

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado sin parametros
        /// </summary>
        public GestionarCompraMaterialesSapi()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCompraMaterialesSapi(this,null,null);
        }

        /// <summary>
        /// Constructor predeterminado que recibe la compa y una ventana padre
        /// </summary>
        /// <param name="compra">Compra Sapi de Materiales</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public GestionarCompraMaterialesSapi(object compra, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCompraMaterialesSapi(this, compra, ventanaPadre);
        }

        #endregion


        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
            else
                this._presentador.ActualizarTitulo();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        

        private void _cbxMaterialesCompra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._cbxMaterialesCompra.SelectedIndex != 0)
            {
                ActivarCampoCantidad(true);
                ActivarBotonesIncluirYBorrar(true);
            }
            else
            {
                ActivarCampoCantidad(false);
                ActivarBotonesIncluirYBorrar(false);
            }
        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarRenglonDeDetalleCompraSapi();
        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.QuitarRenglonDeDetalleCompraSapi();
        }

        private void _btnTotalizarCompra_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CalcularTotalCompra();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = String.Empty;

            if (this._presentador.InsertarOModificarCompraSapi())
                mensaje = Recursos.MensajesConElUsuario.ConfirmarAgregarNuevaCompraSAPI;
            else
                mensaje = Recursos.MensajesConElUsuario.ConfirmarModificarCompraSAPI;
            

            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(mensaje,
                "Registro Compra SAPI", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Aceptar();
            }

            
        }

        private void _btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.InicializarPantalla();
        }

        private void _btnNuevaCompra_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionNuevaCompraSapi,
                "Nueva Compra SAPI", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.InicializarPantalla();
            }
        }

        private void _btnVerFacturaSAPI_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerFacturaCompraSAPI();
        }

        #endregion

        
        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ActivarCampoCantidad(bool status)
        {
            this._txtCantidadMaterialCompraMaterial.IsEnabled = status;
        }

        public void ActivarBotonesIncluirYBorrar(bool status)
        {
            this._btnMas.IsEnabled = status;
            this._btnMenos.IsEnabled = status;
        }

        public void SeleccionarPrimerItem()
        {
            this._cbxMaterialesCompra.SelectedIndex = 0;
        }

        public void OcultarBotonesAlConsultar()
        {
            this._btnAceptar.Visibility = System.Windows.Visibility.Collapsed;
            this._btnLimpiar.Visibility = System.Windows.Visibility.Collapsed;
            this._btnNuevaCompra.Visibility = System.Windows.Visibility.Collapsed;
            this._btnTotalizarCompra.Visibility = System.Windows.Visibility.Collapsed;
            this._btnVerFacturaSAPI.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCompraMaterial.IsEnabled = false;
            this._dpkFechaCompraMaterial.IsEnabled = false;
            this._cbxMaterialesCompra.IsEnabled = false;
            ActivarCampoCantidad(false);
        }

        public void PintarBotonVerFacturaSAPI()
        {
            this._btnVerFacturaSAPI.Background = Brushes.LightGreen;
        }

        public void ArchivoNoEncontrado(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void DeshabilitarBotonAceptar()
        {
            this._btnAceptar.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        

        

        
        

    }
}
