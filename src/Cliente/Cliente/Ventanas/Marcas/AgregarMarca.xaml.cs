using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Presentadores.Asociados;
using System.Windows;
using System.Windows.Input;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for AgregarAsociado.xaml
    /// </summary>
    public partial class AgregarMarca : Page
    {
        //private PresentadorAgregarAsociado _presentador;
        //private bool _cargada;

        //#region IAgregarAsociado

        //public object Asociado
        //{
        //    get { return this._tbcPestañas.DataContext; }
        //    set { this._tbcPestañas.DataContext = value; }
        //}

        //public object Pais
        //{
        //    get { return this._cbxPaisDatos.SelectedItem; }
        //    set { this._cbxPaisDatos.SelectedItem = value; }
        //}

        //public object Paises
        //{
        //    get { return this._cbxPaisDatos.DataContext; }
        //    set { this._cbxPaisDatos.DataContext = value; }
        //}

        //public object Idioma
        //{
        //    get { return this._cbxIdiomaDatos.SelectedItem; }
        //    set { this._cbxIdiomaDatos.SelectedItem = value; }
        //}

        //public object Idiomas
        //{
        //    get { return this._cbxIdiomaDatos.DataContext; }
        //    set { this._cbxIdiomaDatos.DataContext = value; }
        //}

        //public object Moneda
        //{
        //    get { return this._cbxMonedaDatos.SelectedItem; }
        //    set { this._cbxMonedaDatos.SelectedItem = value; }
        //}

        //public object Monedas
        //{
        //    get { return this._cbxMonedaDatos.DataContext; }
        //    set { this._cbxMonedaDatos.DataContext = value; }
        //}

        //public object Descuento
        //{
        //    get { throw new System.NotImplementedException(); }
        //    set { throw new System.NotImplementedException(); }
        //}

        //public object Descuentos
        //{
        //    get { throw new System.NotImplementedException(); }
        //    set { throw new System.NotImplementedException(); }
        //}

        //public object TipoCliente
        //{
        //    get { return this._cbxTipoClienteAdministracion.SelectedItem; }
        //    set { this._cbxTipoClienteAdministracion.SelectedItem = value; }
        //}

        //public object TiposClientes
        //{
        //    get { return this._cbxTipoClienteAdministracion.DataContext; }
        //    set { this._cbxTipoClienteAdministracion.DataContext = value; }
        //}

        //public object Etiqueta
        //{
        //    get { return this._cbxEtiquetaAdministracion.SelectedItem; }
        //    set { this._cbxEtiquetaAdministracion.SelectedItem = value; }
        //}

        //public object Etiquetas
        //{
        //    get { return this._cbxEtiquetaAdministracion.DataContext; }
        //    set { this._cbxEtiquetaAdministracion.DataContext = value; }
        //}

        //public object DetallePago
        //{
        //    get { return this._cbxDetallePagoAdministracion.SelectedItem; }
        //    set { this._cbxDetallePagoAdministracion.SelectedItem = value; }
        //}

        //public object DetallesPagos
        //{
        //    get { return this._cbxDetallePagoAdministracion.DataContext; }
        //    set { this._cbxDetallePagoAdministracion.DataContext = value; }
        //}

        //public object Tarifa
        //{
        //    get { return this._cbxTarifaAdministracion.SelectedItem; }
        //    set { this._cbxTarifaAdministracion.SelectedItem = value; }
        //}

        //public object Tarifas
        //{
        //    get { return this._cbxTarifaAdministracion.DataContext; }
        //    set { this._cbxTarifaAdministracion.DataContext = value; }
        //}

        //public object TipoPersonas
        //{

        //    get { return this._cbxTipoPersonaDatos.DataContext; }
        //    set { this._cbxTipoPersonaDatos.DataContext = value; }
        //}

        //public object TipoPersona
        //{

        //    get { return this._cbxTipoPersonaDatos.SelectedItem; }
        //    set { this._cbxTipoPersonaDatos.SelectedItem = value; }
        //}

        //public bool EstaCargada
        //{
        //    get { return this._cargada; }
        //    set { this._cargada = value; }
        //}

        //public void FocoPredeterminado()
        //{
        //    this._txtNombreDatos.Focus();
        //}

        //#endregion

        public AgregarMarca()
        {
            InitializeComponent();
            //this._cargada = false;
            //this._presentador = new PresentadorAgregarAsociado(this);
        }

        //private void _txtAsociado_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    this._txtAsociado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lstAsociados.Visibility = System.Windows.Visibility.Visible;
        //    this._lstAsociados.IsEnabled = true;
        //    this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible;
        //    this._txtIdAsociado.Visibility = System.Windows.Visibility.Visible;
        //    this._txtNombreAsociado.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdAsociado.Visibility = System.Windows.Visibility.Visible;
        //    this._lblNombreAsociado.Visibility = System.Windows.Visibility.Visible;

        //}

        //private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    //this._presentador.CambiarAsociado();
        //    this._lstAsociados.Visibility = System.Windows.Visibility.Collapsed;
        //    this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtIdAsociado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtNombreAsociado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtAsociado.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdAsociado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lblNombreAsociado.Visibility = System.Windows.Visibility.Collapsed;

        //}

        //private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        //{
        //    //this._presentador.BuscarAsociado();
        //}

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Cancelar();
        }
        
        //private void _txtInteresado_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    this._txtInteresado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lstInteresados.Visibility = System.Windows.Visibility.Visible;
        //    this._lstInteresados.IsEnabled = true;
        //    this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Visible;
        //    this._txtIdInteresado.Visibility = System.Windows.Visibility.Visible;
        //    this._txtNombreInteresado.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdInteresado.Visibility = System.Windows.Visibility.Visible;
        //    this._lblNombreInteresado.Visibility = System.Windows.Visibility.Visible;

        //}

        //private void _txtInteresado_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    //this._presentador.CambiarInteresado();
        //    this._lstInteresados.Visibility = System.Windows.Visibility.Collapsed;
        //    this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtIdInteresado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtNombreInteresado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtInteresado.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdInteresado.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lblNombreInteresado.Visibility = System.Windows.Visibility.Collapsed;

        //}

        //private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        //{
        //    //this._presentador.BuscarInteresado();
        //}

        //private void _lstInteresados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    //this._presentador.Cancelar();
        //}

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Aceptar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //if (!EstaCargada)
            //{
            //    this._presentador.CargarPagina();
            //    EstaCargada = true;
            //}
        }


        //private void _soloNumero_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(this._txtDiasCreditoAdministracion.Text, "[^0-9]"))
        //    {
        //        this._txtDiasCreditoAdministracion.Text = "";
        //    }
        //}

        //private void _soloNumero_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
        //        e.Handled = true;

        //}

        //private void _txtDescuentoAdministracion_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if ((!string.Equals(e.Key.ToString(),"OemComma"))||(this._txtDescuentoAdministracion.Text.Contains(",")))
        //    {
        //        if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
        //            e.Handled = true;
        //    }

            //if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
            //    e.Handled = true;
        

        private void _chkAlertaAdministracion_Click(object sender, RoutedEventArgs e)
        {
            //if (!this._chkAlertaAdministracion.IsChecked.Value)
            //{
            //    this._txtAlarmaAdministracion.IsEnabled = false;
            //}
            //else
            //{
            //    this._txtAlarmaAdministracion.IsEnabled = true;
            //}
        }

        private void _btnClaseCompleta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnIngles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnImprimirEdoCuenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnSaldo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnPoder_Click(object sender, RoutedEventArgs e)
        {

        }


        //private void _btnJustificacionesDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.IrListaJustificaciones();
        //}
    }
}
