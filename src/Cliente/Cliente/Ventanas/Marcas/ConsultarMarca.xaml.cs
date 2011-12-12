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
        //private bool _cargada;

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
            get
            {
                return this._lstAsociadosSolicitud.SelectedItem;
            }
            set
            {
                this._lstAsociadosSolicitud.SelectedItem = value;
            }
        }



        public object PoderesSolicitud
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object PoderesDatos
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Agentes
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object BoletinesOrdenPublicacion
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object BoletinesPublicacion
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object BoletinesConcesion
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Servicios
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Detalles
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Condicion
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object PoderSolicitud
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object PoderDatos
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Agente
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object BoletinOrdenPublicacion
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object BoletinPublicacion
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object BoletinConcesion
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Servicio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Detalle
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public object Condiciones
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void Mensaje(string mensaje)
        {
            throw new System.NotImplementedException();
        }

        public bool EstaCargada
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void FocoPredeterminado()
        {
            throw new System.NotImplementedException();
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
