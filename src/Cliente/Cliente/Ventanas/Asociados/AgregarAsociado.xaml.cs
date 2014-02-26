using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Presentadores.Asociados;
using System.Windows;
using System.Windows.Input;

namespace Trascend.Bolet.Cliente.Ventanas.Asociados
{
    /// <summary>
    /// Interaction logic for AgregarAsociado.xaml
    /// </summary>
    public partial class AgregarAsociado : Page, IAgregarAsociado
    {
        private PresentadorAgregarAsociado _presentador;
        private bool _cargada;

        #region IAgregarAsociado

        public object Asociado
        {
            get { return this._tbcPestañas.DataContext; }
            set { this._tbcPestañas.DataContext = value; }
        }

        public object Pais
        {
            get { return this._cbxPaisDatos.SelectedItem; }
            set { this._cbxPaisDatos.SelectedItem = value; }
        }

        public object Paises
        {
            get { return this._cbxPaisDatos.DataContext; }
            set { this._cbxPaisDatos.DataContext = value; }
        }

        public object Idioma
        {
            get { return this._cbxIdiomaDatos.SelectedItem; }
            set { this._cbxIdiomaDatos.SelectedItem = value; }
        }

        public object Idiomas
        {
            get { return this._cbxIdiomaDatos.DataContext; }
            set { this._cbxIdiomaDatos.DataContext = value; }
        }

        public object Moneda
        {
            get { return this._cbxMonedaDatos.SelectedItem; }
            set { this._cbxMonedaDatos.SelectedItem = value; }
        }

        public object Monedas
        {
            get { return this._cbxMonedaDatos.DataContext; }
            set { this._cbxMonedaDatos.DataContext = value; }
        }

        public object Descuento
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public object Descuentos
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public object TipoCliente
        {
            get { return this._cbxTipoClienteAdministracion.SelectedItem; }
            set { this._cbxTipoClienteAdministracion.SelectedItem = value; }
        }

        public object TiposClientes
        {
            get { return this._cbxTipoClienteAdministracion.DataContext; }
            set { this._cbxTipoClienteAdministracion.DataContext = value; }
        }

        public object Etiqueta
        {
            get { return this._cbxEtiquetaAdministracion.SelectedItem; }
            set { this._cbxEtiquetaAdministracion.SelectedItem = value; }
        }

        public object Etiquetas
        {
            get { return this._cbxEtiquetaAdministracion.DataContext; }
            set { this._cbxEtiquetaAdministracion.DataContext = value; }
        }

        public object DetallePago
        {
            get { return this._cbxDetallePagoAdministracion.SelectedItem; }
            set { this._cbxDetallePagoAdministracion.SelectedItem = value; }
        }

        public object DetallesPagos
        {
            get { return this._cbxDetallePagoAdministracion.DataContext; }
            set { this._cbxDetallePagoAdministracion.DataContext = value; }
        }

        public object Tarifa
        {
            get { return this._cbxTarifaAdministracion.SelectedItem; }
            set { this._cbxTarifaAdministracion.SelectedItem = value; }
        }

        public object Tarifas
        {
            get { return this._cbxTarifaAdministracion.DataContext; }
            set { this._cbxTarifaAdministracion.DataContext = value; }
        }

        public object TipoPersonas
        {

            get { return this._cbxTipoPersonaDatos.DataContext; }
            set { this._cbxTipoPersonaDatos.DataContext = value; }
        }

        public object TipoPersona
        {

            get { return this._cbxTipoPersonaDatos.SelectedItem; }
            set { this._cbxTipoPersonaDatos.SelectedItem = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNombreDatos.Focus();
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion

        public AgregarAsociado()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarAsociado(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }        

        private void _chkAlertaAdministracion_Click(object sender, RoutedEventArgs e)
        {
            if (!this._chkAlertaAdministracion.IsChecked.Value)
            {
                this._txtAlarmaAdministracion.IsEnabled = false;
            }
            else
            {
                this._txtAlarmaAdministracion.IsEnabled = true;
            }
        }

        private void _btnIrWeb_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrWebAsociado();
        }

        private void _btnVerEtiqueta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerEtiqueta();
        }


        private void _btnImprimirEdoCuenta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaImprimirEdoCuenta();
        }

        private void _btnSaldo_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CalcularSaldos();

        }

        private void _btnCXPINTDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaCXPINTDatos();

        }            

        public string SaldoVencidoSolicitud
        {
            set { this._txtVencidoADatos.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value));
            this._txtVencidoAAdministracion.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value));
            }
        }

        public string SaldoPorVencerSolicitud
        {
            set { this._txtPorVencerDatos.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value));
            this._txtPorVencerAdministracion.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value));
            }
        }

        public string TotalSolicitud
        {
            set { this._txtTotalDatos.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value));
            this._txtTotalAdministracion.Text =_presentador.SetFormatoDouble2(System.Convert.ToDouble( value));
            }
        }

        public string MSaldoPendiente
        {
            set { this._txtSaldoPendienteDatos.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value));
            this._txtSaldoPendienteAdministracion.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value));
            }
        }

        public object OrigenClientes
        {
            get { return this._cbxOrigenClienteAdministracion.DataContext; }
            set { this._cbxOrigenClienteAdministracion.DataContext = value; }
        }

        public object OrigenCliente
        {
            get { return this._cbxOrigenClienteAdministracion.SelectedItem; }
            set { this._cbxOrigenClienteAdministracion.SelectedItem = value; }
        }


        public string CartaDomicilioDatos
        {
            get { return this._txtCartaDomicilioDatos.Text; }
            set { this._txtCartaDomicilioDatos.Text = value; }
        }

        private void _btnCartaDomicilioDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCorrespondenciaDeDomicilio();
        }

    }
}
