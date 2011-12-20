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
    public partial class AgregarMarca : Page, IAgregarMarca
    {
        private PresentadorAgregarMarca _presentador;
        private bool _cargada;

        #region IAgregarMarca


        public object Marca
        {
            get { return this._tbcPestañas.DataContext; }
            set { this._tbcPestañas.DataContext = value; }
        }

        public string IdAsociadoSolicitudFiltrar
        {
            get { return this._txtIdAsociadoSolicitud.Text; }
        }

        public string IdAsociadoDatosFiltrar
        {
            get { return this._txtIdAsociadoDatos.Text; }
        }

        public string NombreAsociadoSolicitudFiltrar
        {
            get { return this._txtNombreAsociadoSolicitud.Text; }
        }

        public string NombreAsociadoDatosFiltrar
        {
            get { return this._txtNombreAsociadoSolicitud.Text; }
        }

        public string NombreAsociadoSolicitud
        {
            get { return this._txtAsociadoSolicitud.Text; }
            set { this._txtAsociadoSolicitud.Text = value; }
        }

        public string NombreAsociadoDatos
        {
            get { return this._txtAsociadoDatos.Text; }
            set { this._txtAsociadoDatos.Text = value; }
        }

        public string InteresadoPaisSolicitud
        {
            get { return this._txtPaisSolicitud.Text; }
            set { this._txtPaisSolicitud.Text = value; }
        }

        public string InteresadoCiudadSolicitud
        {
            get { return this._txtCiudadSolicitud.Text; }
            set { this._txtCiudadSolicitud.Text = value; }
        }

        public object AsociadosSolicitud
        {
            get { return this._lstAsociadosSolicitud.DataContext; }
            set { this._lstAsociadosSolicitud.DataContext = value; }
        }

        public object AsociadoSolicitud
        {
            get { return this._lstAsociadosSolicitud.SelectedItem; }
            set { this._lstAsociadosSolicitud.SelectedItem = value; }
        }

        public object AsociadosDatos
        {
            get { return this._lstAsociadosDatos.DataContext; }
            set { this._lstAsociadosDatos.DataContext = value; }
        }

        public object AsociadoDatos
        {
            get { return this._lstAsociadosDatos.SelectedItem; }
            set { this._lstAsociadosDatos.SelectedItem = value; }
        }


        public string IdInteresadoSolicitudFiltrar
        {
            get { return this._txtIdInteresadoSolicitud.Text; }
        }

        public string IdInteresadoDatosFiltrar
        {
            get { return this._txtIdInteresadoDatos.Text; }
        }

        public string NombreInteresadoSolicitudFiltrar
        {
            get { return this._txtNombreInteresadoSolicitud.Text; }
        }

        public string NombreInteresadoDatosFiltrar
        {
            get { return this._txtNombreInteresadoSolicitud.Text; }
        }

        public string NombreInteresadoSolicitud
        {
            get { return this._txtInteresadoSolicitud.Text; }
            set { this._txtInteresadoSolicitud.Text = value; }
        }

        public string NombreInteresadoDatos
        {
            get { return this._txtInteresadoDatos.Text; }
            set { this._txtInteresadoDatos.Text = value; }
        }

        public object InteresadosSolicitud
        {
            get { return this._lstInteresadosSolicitud.DataContext; }
            set { this._lstInteresadosSolicitud.DataContext = value; }
        }

        public object InteresadoSolicitud
        {
            get { return this._lstInteresadosSolicitud.SelectedItem; }
            set { this._lstInteresadosSolicitud.SelectedItem = value; }
        }

        public object InteresadosDatos
        {
            get { return this._lstInteresadosDatos.DataContext; }
            set { this._lstInteresadosDatos.DataContext = value; }
        }

        public object InteresadoDatos
        {
            get { return this._lstInteresadosDatos.SelectedItem; }
            set { this._lstInteresadosDatos.SelectedItem = value; }
        }

        public string IdCorresponsalSolicitudFiltrar
        {
            get { return this._txtIdCorresponsalSolicitud.Text; }
        }

        public string IdCorresponsalDatosFiltrar
        {
            get { return this._txtIdCorresponsalDatos.Text; }
        }

        public string DescipcionCorresponsalSolicitudFiltrar
        {
            get { return this._txtDescripcionCorresponsalSolicitud.Text; }
        }

        public string DescipcionCorresponsalDatosFiltrar
        {
            get { return this._txtDescripcionCorresponsalDatos.Text; }
        }

        public string DescipcionCorresponsalSolicitud
        {
            get { return this._txtCorresponsalSolicitud.Text; }
            set { this._txtCorresponsalSolicitud.Text = value; }
        }

        public string DescipcionCorresponsalDatos
        {
            get { return this._txtCorresponsalDatos.Text; }
            set { this._txtCorresponsalDatos.Text = value; }
        }

        public object CorresponsalesSolicitud
        {
            get { return this._lstCorresponsalesSolicitud.DataContext; }
            set { this._lstCorresponsalesSolicitud.DataContext = value; }
        }

        public object CorresponsalSolicitud
        {
            get { return this._lstCorresponsalesSolicitud.SelectedItem; }
            set { this._lstCorresponsalesSolicitud.SelectedItem = value; }
        }

        public object CorresponsalesDatos
        {
            get { return this._lstCorresponsalesDatos.DataContext; }
            set { this._lstCorresponsalesDatos.DataContext = value; }
        }

        public object CorresponsalDatos
        {
            get { return this._lstCorresponsalesDatos.SelectedItem; }
            set { this._lstCorresponsalesDatos.SelectedItem = value; }
        }

        public object PoderesSolicitud
        {
            get { return this._cbxPoderSolicitud.DataContext; }
            set { this._cbxPoderSolicitud.DataContext = value; }
        }

        public object PoderSolicitud
        {
            get { return this._cbxPoderSolicitud.SelectedItem; }
            set { this._cbxPoderSolicitud.SelectedItem = value; }
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
            get { return this._cbxAgenteSolicitud.DataContext; }
            set { this._cbxAgenteSolicitud.DataContext = value; }
        }

        public object Agente
        {
            get { return this._cbxAgenteSolicitud.SelectedItem; }
            set { this._cbxAgenteSolicitud.SelectedItem = value; }
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

        public object Detalle
        {
            get { return this._cbxDetalleDatos.DataContext; }
            set { this._cbxDetalleDatos.DataContext = value; }
        }

        public object Detalles
        {
            get { return this._cbxDetalleDatos.SelectedItem; }
            set { this._cbxDetalleDatos.SelectedItem = value; }
        }

        public object Condiciones
        {
            get { return this._cbxCondiciones.DataContext; }
            set { this._cbxCondiciones.DataContext = value; }
        }

        public object Condicion
        {
            get { return this._cbxCondiciones.SelectedItem; }
            set { this._cbxCondiciones.SelectedItem = value; }
        }

        public object PaisesSolicitud
        {
            get { return this._cbxPaisPrioridadSolicitud.DataContext; }
            set { this._cbxPaisPrioridadSolicitud.DataContext = value; }
        }

        public object PaisSolicitud
        {
            get { return this._cbxPaisPrioridadSolicitud.SelectedItem; }
            set { this._cbxPaisPrioridadSolicitud.SelectedItem = value; }
        }

        public object TipoMarcasSolicitud
        {
            get { return this._cbxTipoMarcaSolicitud.DataContext; }
            set { this._cbxTipoMarcaSolicitud.DataContext = value; }
        }

        public object TipoMarcaSolicitud
        {
            get { return this._cbxTipoMarcaSolicitud.SelectedItem; }
            set { this._cbxTipoMarcaSolicitud.SelectedItem = value; }
        }

        public object TipoMarcasDatos
        {
            get { return this._cbxTipoMarcaDatos.DataContext; }
            set { this._cbxTipoMarcaDatos.DataContext = value; }
        }

        public object TipoMarcaDatos
        {
            get { return this._cbxTipoMarcaDatos.SelectedItem; }
            set { this._cbxTipoMarcaDatos.SelectedItem = value; }
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

        #endregion

        public AgregarMarca()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarMarca(this);
        }

        #region funciones

        private void mostrarLstAsociadoSolicitud()
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

        private void ocultarLstAsociadoSolicitud()
        {
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void mostrarLstInteresadoSolicutud()
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

        private void ocultarLstInteresadoSolicutud()
        {
            this._presentador.CambiarInteresadoSolicitud();
            this._lstInteresadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void mostrarLstCorresponsalSolicutud()
        {
            this._txtCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstCorresponsalesSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstCorresponsalesSolicitud.IsEnabled = true;
            this._btnConsultarCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        private void ocultarLstCorresponsalSolicutud()
        {
            this._presentador.CambiarCorresponsalSolicitud();
            this._lstCorresponsalesSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void mostrarLstAsocaidoDatos()
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

        private void ocultarLstAsociadoDatos()
        {
            this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void mostrarLstInteresadoDatos()
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

        private void ocultarLstInteresadoDatos()
        {
            this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void mostrarLstCorresponsalDatos()
        {
            this._txtCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstCorresponsalesDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstCorresponsalesDatos.IsEnabled = true;
            this._btnConsultarCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
        }

        private void ocultarLstCorresponsalDatos()
        {
            this._presentador.CambiarCorresponsalDatos();
            this._lstCorresponsalesDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
        }



        #endregion

        #region Eventos generales

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        #endregion

        #region Eventos Solicitudes

        private void _txtAsociadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            mostrarLstAsociadoSolicitud();
        }

        private void _lstAsociadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoSolicitud();
            ocultarLstAsociadoSolicitud();
            ocultarLstAsociadoDatos();
        }

        private void _btnConsultarAsociadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado(0);
        }

        private void _txtInteresadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            mostrarLstInteresadoSolicutud();
        }

        private void _btnConsultarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado(0);
        }

        private void _lstInteresadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarInteresadoSolicitud();
            ocultarLstInteresadoSolicutud();
            ocultarLstInteresadoDatos();
        }

        private void _btnConsultarCorresponsalSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarCorresponsal(0);
        }

        private void _txtCorresponsalSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            mostrarLstCorresponsalSolicutud();
        }

        private void _lstCorresponsalesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarCorresponsalSolicitud();
            ocultarLstCorresponsalSolicutud();
            ocultarLstCorresponsalDatos();
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
            mostrarLstAsocaidoDatos();
        }

        private void _lstAsociadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoDatos();
            ocultarLstAsociadoDatos();
            ocultarLstAsociadoSolicitud();
        }

        private void _btnConsultarAsociadoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado(1);
        }

        private void _txtInteresadoDatos_GotFocus(object sender, RoutedEventArgs e)
        {
            mostrarLstInteresadoDatos();
        }

        private void _btnConsultarInteresadoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado(1);
        }

        private void _lstInteresadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarInteresadoDatos(); 
            ocultarLstInteresadoDatos();
            ocultarLstInteresadoSolicutud();
        }

        private void _btnConsultarCorresponsalDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarCorresponsal(0);
        }

        private void _txtCorresponsalDatos_GotFocus(object sender, RoutedEventArgs e)
        {
            mostrarLstCorresponsalDatos();
        }

        private void _lstCorresponsalesDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarCorresponsalDatos();
            ocultarLstCorresponsalSolicutud();
            ocultarLstCorresponsalDatos();
        }

        #endregion


    }
}
