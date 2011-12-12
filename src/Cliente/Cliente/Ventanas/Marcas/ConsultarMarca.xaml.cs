using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using System.Windows;
using System.Windows.Input;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for AgregarAsociado.xaml
    /// </summary>
    public partial class ConsultarMarca : Page, IConsultarMarca
    {
        //private Presentado _presentador;
        private bool _cargada;

        #region IConsultarMarca

        public object Marca
        {
            get { return this._tbcPestañas.DataContext; }
            set { this._tbcPestañas.DataContext = value; }
        }

        public string IdAsociadoFiltrar
        {
            get { return this._txtIdAsociadoSolicitud.Text; }
        }

        public string NombreAsociadoFiltrar
        {
            get { return this._txtNombreAsociadoSolicitud.Text; }
        }

        public string NombreAsociado
        {
            get { return this._txtAsociadoSolicitud.Text; }
            set { this._txtAsociadoSolicitud.Text = value; }
        }

        public object Asociados
        {
            get { return this._lstAsociadosSolicitud.DataContext; }
            set { this._lstAsociadosSolicitud.DataContext = value; }
        }

        public object Asociado
        {
            get { return this._lstAsociadosSolicitud.SelectedItem; }
            set { this._lstAsociadosSolicitud.SelectedItem = value; }
        }



        public object PoderesSolicitud
        {
            get { return this._cbxPoder.DataContext; }
            set { this._cbxPoder.DataContext = value; }
        }

        public object PoderSolicitud
        {
            get { return this._cbxPoder.SelectedItem; }
            set { this._cbxPoder.SelectedItem = value; }
        }

        public object PoderesDatos
        {
            get { return this._cbxPoderDatos.DataContext; }
            set { this._cbxPoderDatos.DataContext = value; }
        }

        public object PoderDatos
        {
            get { return this._cbxPoderDatos.SelectedItem; }
            set { this._cbxPoderDatos.SelectedItem = value; }
        }

        public object Agentes
        {
            get { return this._cbxAgente.DataContext; }
            set { this._cbxAgente.DataContext = value; }
        }

        public object Agente
        {
            get { return this._cbxAgente.SelectedItem; }
            set { this._cbxAgente.SelectedItem = value; }
        }

        public object BoletinesOrdenPublicacion
        {
            get { return this._cbxOrdenPublicacion.DataContext; }
            set { this._cbxOrdenPublicacion.DataContext = value; }
        }

        public object BoletinOrdenPublicacion
        {
            get { return this._cbxOrdenPublicacion.SelectedItem; }
            set { this._cbxOrdenPublicacion.SelectedItem = value; }
        }

        public object BoletinesPublicacion
        {
            get { return this._cbxBoletinPublicacion.DataContext; }
            set { this._cbxBoletinPublicacion.DataContext = value; }
        }

        public object BoletinPublicacion
        {
            get { return this._cbxBoletinPublicacion.SelectedItem; }
            set { this._cbxBoletinPublicacion.SelectedItem = value; }
        }

        public object BoletinesConcesion
        {
            get { return this._cbxBoletinConcesion.DataContext; }
            set { this._cbxBoletinConcesion.DataContext = value; }
        }

        public object BoletinConcesion
        {
            get { return this._cbxBoletinConcesion.SelectedItem; }
            set { this._cbxBoletinConcesion.SelectedItem = value; }
        }

        public object Servicios
        {
            get { return this._cbxSituacion.DataContext; }
            set { this._cbxSituacion.DataContext = value; }
        }

        public object Servicio
        {
            get { return this._cbxSituacion.SelectedItem; }
            set { this._cbxSituacion.SelectedItem = value; }
        }

        public object Detalles
        {
            get { return this._cbxDetalleDatos.DataContext; }
            set { this._cbxDetalleDatos.DataContext = value; }
        }

        public object Detalle
        {
            get { return this._cbxDetalleDatos.SelectedItem; }
            set { this._cbxDetalleDatos.SelectedItem = value; }
        }

        public object Condicion
        {
            get { return this._cbxCondiciones.SelectedItem; }
            set { this._cbxCondiciones.SelectedItem = value; }
        }

        public object Condiciones
        {
            get { return this._cbxCondiciones.DataContext; }
            set { this._cbxCondiciones.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            throw new System.NotImplementedException();
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtDescripcionSolicitud.Focus();
        }

        public string TextoBotonModificar
        {
            get { return this._txbAceptar.Text; }
            set { this._txbAceptar.Text = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                //this._txtId.IsEnabled = value;
            }
        }

        #endregion

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
