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
    public partial class ConsultarMarca : Page
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

        public ConsultarMarca()
        {
            InitializeComponent();
            //this._cargada = false;
            //this._presentador = new PresentadorAgregarAsociado(this);
        }

        #region Eventos generales

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //if (!EstaCargada)
            //{
            //    this._presentador.CargarPagina();
            //    EstaCargada = true;
            //}
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Aceptar();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Cancelar();
        }

        #endregion

        #region Eventos Solicitudes

        private void _txtAsociadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociadosSolicitud.IsEnabled = true;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;

        }

        private void _lstAsociadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //this._presentador.CambiarAsociado();
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void _btnConsultarAsociadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.BuscarAsociado();
        }

        private void _txtInteresadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresadosSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresadosSolicitud.IsEnabled = true;
            this._btnConsultarInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;

        }

        private void _btnConsultarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.BuscarInteresado();
        }

        private void _lstInteresadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //this._presentador.CambiarInteresado();
            this._lstInteresadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void _btnClaseCompletaSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnIrReclasificarSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnInglesSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnImprimirEdoCuentaSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnSaldoSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnPoderSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Eventos Datos

        private void _txtAsociadoDatos_GotFocus(object sender, RoutedEventArgs e)
        {
            this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociadosDatos.IsEnabled = true;
            this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Visible;

        }

        private void _lstAsociadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //this._presentador.CambiarAsociado();
            this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void _btnConsultarAsociadoDatos_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.BuscarAsociado();
        }

        private void _txtInteresadoDatos_GotFocus(object sender, RoutedEventArgs e)
        {
            this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresadosDatos.IsEnabled = true;
            this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Visible;

        }

        private void _btnConsultarInteresadoDatos_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.BuscarInteresado();
        }

        private void _lstInteresadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //this._presentador.CambiarInteresado();
            this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion
    }
}
