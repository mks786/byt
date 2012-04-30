using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Presentadores.Patentes;
using System.Windows;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Media;
using System;

namespace Trascend.Bolet.Cliente.Ventanas.Patentes
{
    /// <summary>
    /// Interaction logic for GestionarPatente.xaml
    /// </summary>
    public partial class GestionarPatente : Page, IGestionarPatente
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorGestionarPatente _presentador;
        private bool _cargada;

        private bool _asociadosCargados;
        private bool _interesadosCargados;
        private bool _corresponsalesCargados;
        private bool _poderesCargados;


        #region IGestionarPatente

        public object Patente
        {
            get { return this._tbcPestanas.DataContext; }
            set { this._tbcPestanas.DataContext = value; }
        }

        public string NumPoderSolicitud
        {
            get { return this._txtPoderSolicitud.Text; }
            set { this._txtPoderSolicitud.Text = value; }
        }

        public object PoderesSolicitud
        {
            get { return this._lstPoderesSolicitud.DataContext; }
            set { this._lstPoderesSolicitud.DataContext = value; }
        }

        public object PoderSolicitud
        {
            get { return this._lstPoderesSolicitud.SelectedItem; }
            set { this._lstPoderesSolicitud.SelectedItem = value; }
        }

        public object Agentes
        {
            get { return this._lstAgentesSolicitud.DataContext; }
            set { this._lstAgentesSolicitud.DataContext = value; }
        }

        public object Agente
        {
            get { return this._lstAgentesSolicitud.SelectedItem; }
            set { this._lstAgentesSolicitud.SelectedItem = value; }
        }

        public object PaisSolicitud
        {
            get { return this._cbxPaisSolicitud.SelectedItem; }
            set { this._cbxPaisSolicitud.SelectedItem = value; }
        }

        public object PaisesSolicitud
        {
            get { return this._cbxPaisSolicitud.DataContext; }
            set { this._cbxPaisSolicitud.DataContext = value; }
        }

        public object TipoPatentesSolicitud
        {
            get { return this._cbxTipoSolicitud.DataContext; }
            set { this._cbxTipoSolicitud.DataContext = value; }
        }

        public object TipoPatenteSolicitud
        {
            get { return this._cbxTipoSolicitud.SelectedItem; }
            set { this._cbxTipoSolicitud.SelectedItem = value; }
        }

        public void Mensaje(string mensaje)
        {
            throw new System.NotImplementedException();
        }

        public bool MensajeAlerta(string mensaje)
        {
            bool retorno = false;

            if (MessageBoxResult.Yes == MessageBox.Show(mensaje,
                "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question))
                retorno = true;

            return retorno;
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
                #region TextBoxs
                
                this._txtAsociadoSolicitud.IsEnabled = value;
                
                this._txtDescripcionSolicitud.IsEnabled = value;
                this._txtIdAsociadoSolicitud.IsEnabled = value;
                this._txtIdInteresadoSolicitud.IsEnabled = value;
                this._txtIdSolicitud.IsEnabled = value;
                this._txtInteresadoSolicitud.IsEnabled = value;
                this._txtNombreAsociadoSolicitud.IsEnabled = value;
                this._txtNombreInteresadoSolicitud.IsEnabled = value;
                this._txtPoderSolicitud.IsEnabled = value;
                this._txtSaldoPorVencerSolicitud.IsEnabled = value;
                this._txtSaldoVencidoSolicitud.IsEnabled = value;
                this._txtTotalSolicitud.IsEnabled = value;
                this._txtCasoSolicitud.IsEnabled = value;
                this._txtPaisSolicitud.IsEnabled = value;
                this._txtEstadoSolicitud.IsEnabled = value;
                this._txtCasoSolicitud.IsEnabled = value;
                this._txtPrioridadSolicitud.IsEnabled = value;
                this._txtOmisionSolicitud.IsEnabled = value;
                this._txtObservacion1Solicitud.IsEnabled = value;
                this._txtInscripcionSolicitud.IsEnabled = value;
                this._txtFechaInscripcionSolicitud.IsEnabled = value;
                this._txtFomentoSolicitud.IsEnabled = value;
                this._txtAgenteSolicitud.IsEnabled = value;
                this._txtIdAgenteSolicitud.IsEnabled = value;
                this._txtResumenSolicitud.IsEnabled = value;
                this._txtObservacionSolicitud.IsEnabled = value;
                this._txtOrdenSolicitud.IsEnabled = value;
                this._txtFechaInscripcionSolicitud.IsEnabled = value;
                this._txtFechaOrdenSolicitud.IsEnabled = value;
                this._txtFechaPrioridadSolicitud.IsEnabled = value;

                #endregion

                #region ComboBoxs
         
                this._cbxPresentacion.IsEnabled = value;
                this._cbxTipoSolicitud.IsEnabled = value;
                this._cbxPaisSolicitud.IsEnabled = value;

                #endregion

                #region CheckBox

                this._chkMemoriaTraducidaSolicitud.IsEnabled = value;
                
                #endregion

                #region Botones

                this._btnAceptar.IsEnabled = value;
                this._btnCancelar.IsEnabled = value;
                this._btnConsultarAsociadoSolicitud.IsEnabled = value;
                this._btnConsultarInteresadoSolicitud.IsEnabled = value;
                this._btnInfoAdicionalSolicitud.IsEnabled = value;
                this._btnSaldosSolicitud.IsEnabled = value;
                this._btnDisenoSolicitud.IsEnabled = value;
                this._btnDisenoReporteSolicitud.IsEnabled = value;
                this._btnDocumentosSolicitud.IsEnabled = value;
                this._btnInfoAdicionalSolicitud.IsEnabled = value;
                this._btnCasoEspecialSolicitud.IsEnabled = value;
                this._btnDisenoReporteSolicitud.IsEnabled = value;
                this._btnInventoresSolicitud.IsEnabled = value;
                this._btnImprimirEdoDeCuentaSolicitud.IsEnabled = value;

                #endregion

            }
        }

        public string IdAsociadoSolicitudFiltrar
        {
            get { return this._txtIdAsociadoSolicitudFiltrar.Text; }
        }

        public string IdAsociadoSolicitud
        {
            get { return this._txtIdAsociadoSolicitud.Text; }
            set { this._txtIdAsociadoSolicitud.Text = value; }
        }

        public string NombreAsociadoSolicitudFiltrar
        {
            get { return this._txtNombreAsociadoSolicitud.Text; }
        }

        public string NombreAsociadoSolicitud
        {
            get { return this._txtAsociadoSolicitud.Text; }
            set { this._txtAsociadoSolicitud.Text = value; }
        }

        public object AsociadosSolicitud
        {
            get { return this._lstAsociadosSolicitud.DataContext; }
            set { this._lstAsociadosSolicitud.DataContext = value; }
        }

        public object AsociadoSolicitud
        {
            get { return this._lstAsociadosSolicitud.SelectedItem; }
            set
            {
                this._lstAsociadosSolicitud.SelectedItem = value;
                //this._lstAsociadosSolicitud.ScrollIntoView(value);
            }
        }

        public string IdInteresadoSolicitudFiltrar
        {
            get { return this._txtIdInteresadoSolicitudFiltrar.Text; }
        }

        public string IdInteresadoSolicitud
        {
            set { this._txtIdInteresadoSolicitud.Text = value; }
        }

        public string InteresadoPaisSolicitud
        {
            get { return this._txtPaisSolicitud.Text; }
            set { this._txtPaisSolicitud.Text = value; }
        }

        public string InteresadoEstadoSolicitud
        {
            get { return this._txtEstadoSolicitud.Text; }
            set { this._txtEstadoSolicitud.Text = value; }
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

        public object InteresadosSolicitud
        {
            get { return this._lstInteresadosSolicitud.DataContext; }
            set { this._lstInteresadosSolicitud.DataContext = value; }
        }

        public object InteresadoSolicitud
        {
            get { return this._lstInteresadosSolicitud.SelectedItem; }
            set
            {
                this._lstInteresadosSolicitud.SelectedItem = value;
                //this._lstInteresadosSolicitud.ScrollIntoView(value);
            }
        }

        public bool AsociadosEstanCargados
        {
            get { return this._asociadosCargados; }
            set { this._asociadosCargados = value; }
        }

        public bool InteresadosEstanCargados
        {
            get { return this._interesadosCargados; }
            set { this._interesadosCargados = value; }
        }

        public bool CorresponsalesEstanCargados
        {
            get { return this._corresponsalesCargados; }
            set { this._corresponsalesCargados = value; }
        }

        public bool PoderesEstanCargados
        {
            get { return this._poderesCargados; }
            set { this._poderesCargados = value; }
        }

        public void PintarInfoAdicional()
        {
            this._btnInfoAdicionalSolicitud.Background = Brushes.LightGreen;
        }

        public void PintarInventoresSolicitud()
        {
            this._btnInventoresSolicitud.Background = Brushes.LightGreen;
        }

        public void PintarDisenoSolicitud()
        {
            this._btnDisenoSolicitud.Background = Brushes.LightGreen;
        }

        public void PintarDisenoReporteSolicitud()
        {
            this._btnDisenoReporteSolicitud.Background = Brushes.LightGreen;
        }

        public void PintarCasoEspecialSolicitud()
        {
            this._btnCasoEspecialSolicitud.Background = Brushes.LightGreen;
        }

        public void PintarDocumentosSolicitud()
        {
            this._btnDocumentosSolicitud.Background = Brushes.LightGreen;
        }

        public void ArchivoNoEncontrado()
        {
            MessageBox.Show(Recursos.MensajesConElUsuario.ErrorCertificadoNoEncontrado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void PintarImprimirEdoDeCuenta()
        {
        }

        public void PintarSaldos()
        {
        }

        public GridViewColumnHeader CurSortCol
        {
            get { return _CurSortCol; }
            set { _CurSortCol = value; }
        }

        public SortAdorner CurAdorner
        {
            get { return _CurAdorner; }
            set { _CurAdorner = value; }
        }

        #endregion

        public GestionarPatente(object patenteSeleccionada)
        {
            InitializeComponent();

            this._cargada = false;
            this._asociadosCargados = false;
            this._interesadosCargados = false;
            this._poderesCargados = false;
            this._presentador = new PresentadorGestionarPatente(this, patenteSeleccionada);
        }

        public GestionarPatente(object patenteSeleccionada, string tab)
            : this(patenteSeleccionada)
        {
            this._presentador.CambiarAModificar();

            foreach (TabItem item in this._tbcPestanas.Items)
            {
                if (item.Header.Equals(tab))
                    item.IsSelected = true;
            }
        }

        #region Funciones

        private void mostrarLstAsociadoSolicitud()
        {
            this._lstAsociadosSolicitud.ScrollIntoView(this.AsociadoSolicitud);
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociadosSolicitud.IsEnabled = true;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;            
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoSolicitudFiltrar.Visibility = System.Windows.Visibility.Visible;
        }

        private void ocultarLstAsociadoSolicitud()
        {
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoSolicitudFiltrar.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void mostrarLstInteresadoSolicutud()
        {
            this._lstInteresadosSolicitud.ScrollIntoView(this.InteresadoSolicitud);
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresadosSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresadosSolicitud.IsEnabled = true;
            this._btnConsultarInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoSolicitudFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        private void ocultarLstInteresadoSolicutud()
        {
            this._presentador.CambiarInteresadoSolicitud();
            this._lstInteresadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoSolicitudFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;            
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
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
            this._presentador.Modificar();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrConsultarPatentes();
        }

        private void _btnInfoAdicionalSolicitud_Click(object sender, RoutedEventArgs e)
        {
            string parametro = "";
            if (((Button)sender).Name.Equals("_btnInfoAdicionalSolicitud"))
                parametro = Recursos.Etiquetas.tabSolicitud;
            else if (((Button)sender).Name.Equals("_btnInfoAdicional"))
                parametro = Recursos.Etiquetas.tabDatos;

            this._presentador.IrInfoAdicional(parametro);
        }

        private void _btnBusqueda_Click(object sender, RoutedEventArgs e)
        {
            string parametro = "";
            if (((Button)sender).Name.Equals("_btnBusquedaSolicitud"))
                parametro = Recursos.Etiquetas.tabSolicitud;
            else if (((Button)sender).Name.Equals("_btnBusquedaDatos"))
                parametro = Recursos.Etiquetas.tabDatos;

            this._presentador.IrBusquedas(parametro);
        }

        private void _btnDisenoSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void _btnDisenoReporteSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnConsultarPoderesSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnConsultarAgenteSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnDocumentosSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnCasoEspecialSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Eventos Solicitudes

        private void _txtAsociadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._asociadosCargados)
            {
                this._presentador.CargarAsociados();
            }

            
            ocultarLstInteresadoSolicutud();
            ocultarLstPoderSolicutud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoSolicitud.IsDefault = true;

            mostrarLstAsociadoSolicitud();

        }

        private void _lstAsociadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoSolicitud();
            ocultarLstAsociadoSolicitud();
            this._btnConsultarAsociadoSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _OrdenarAsociadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAsociadosSolicitud);
        }

        private void _btnConsultarAsociadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado(0);
        }

        private void _txtAgenteSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void _OrdenarAgenteSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAgentesSolicitud);
        }

        private void _txtInteresadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._interesadosCargados)
            {
                this._presentador.CargarInteresados();
            }

            ocultarLstAsociadoSolicitud();
            ocultarLstPoderSolicutud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresadoSolicitud.IsDefault = true;

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
            
            this._btnConsultarInteresadoSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _OrdenarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosSolicitud);
        }

        private void _btnInventoresSolicitud_Click(object sender, RoutedEventArgs e)
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

        private void mostrarLstPoderSolicitud()
        {
            this._txtPoderSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstPoderesSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstPoderesSolicitud.IsEnabled = true;
        }

        private void ocultarLstPoderSolicutud()
        {
            this._presentador.CambiarPoderSolicitud();
            this._lstPoderesSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtPoderSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        private void _txtPoderSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._poderesCargados)
                this._presentador.CargarPoderes();

            ocultarLstAsociadoSolicitud();
            ocultarLstInteresadoSolicutud();

            mostrarLstPoderSolicitud();
        }

        private void _lstPoderesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ocultarLstPoderSolicutud();
            //ocultarLstPoderDatos();
        }

        private void _OrdenarPoderSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesSolicitud);
        }

        private void _cbxTipoPatenteSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //this._cbxTipoPatenteDatos.SelectedItem = ((ComboBox)sender).SelectedItem;
        }

        private void _txtIdAgenteSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void _lstAgentesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }


        #endregion

        #region Eventos Datos

        //private void _txtAsociadoDatos_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (!this._asociadosCargados)
        //    {
        //        this._presentador.CargarAsociados();
        //    }

        //    ocultarLstPoderDatos();
        //    ocultarLstCorresponsalDatos();
        //    ocultarLstInteresadoDatos();

        //    this._btnAceptar.IsDefault = false;
        //    this._btnConsultarAsociadoDatos.IsDefault = true;

        //    mostrarLstAsocaidoDatos();
        //}

        //private void _lstAsociadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    this._presentador.CambiarAsociadoDatos();
        //    ocultarLstAsociadoDatos();
        //    ocultarLstAsociadoSolicitud();

        //    this._btnConsultarAsociadoDatos.IsDefault = false;
        //    this._btnAceptar.IsDefault = true;
        //}

        //private void _OrdenarAsociadoDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAsociadosDatos);
        //}

        //private void _btnConsultarAsociadoDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.BuscarAsociado(1);
        //}

        //private void _txtInteresadoDatos_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (!this._interesadosCargados)
        //    {
        //        this._presentador.CargarInteresados();
        //    }

        //    ocultarLstPoderDatos();
        //    ocultarLstCorresponsalDatos();
        //    ocultarLstAsociadoDatos();

        //    this._btnAceptar.IsDefault = false;
        //    this._btnConsultarInteresadoDatos.IsDefault = true;

        //    mostrarLstInteresadoDatos();
        //}

        //private void _btnConsultarInteresadoDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.BuscarInteresado(1);
        //}

        //private void _lstInteresadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    this._presentador.CambiarInteresadoDatos();
        //    ocultarLstInteresadoDatos();
        //    ocultarLstInteresadoSolicutud();

        //    this._btnConsultarInteresadoDatos.IsDefault = false;
        //    this._btnAceptar.IsDefault = true;
        //}

        //private void _OrdenarInteresadoDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosDatos);
        //}

        //private void _btnConsultarCorresponsalDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.BuscarCorresponsal(1);
        //}

        //private void _txtCorresponsalDatos_GotFocus(object sender, RoutedEventArgs e)
        //{

        //    if (!this._corresponsalesCargados)
        //        this._presentador.CargarCorresponsales();

        //    ocultarLstPoderDatos();
        //    ocultarLstAsociadoDatos();
        //    ocultarLstInteresadoDatos();

        //    this._btnAceptar.IsDefault = false;
        //    this._btnConsultarCorresponsalDatos.IsDefault = true;

        //    mostrarLstCorresponsalDatos();
        //}

        //private void _lstCorresponsalesDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    this._presentador.CambiarCorresponsalDatos();
        //    ocultarLstCorresponsalSolicutud();
        //    ocultarLstCorresponsalDatos();

        //    this._btnConsultarCorresponsalDatos.IsDefault = false;
        //    this._btnAceptar.IsDefault = true;
        //}

        //private void _OrdenarCorresponsalDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCorresponsalesDatos);
        //}

        //private void mostrarLstPoderDatos()
        //{
        //    this._txtPoderDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lstPoderesDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lstPoderesDatos.IsEnabled = true;
        //}

        //private void ocultarLstPoderDatos()
        //{
        //    this._presentador.CambiarPoderDatos();
        //    this._lstPoderesDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtPoderDatos.Visibility = System.Windows.Visibility.Visible;
        //}

        //private void _txtPoderDatos_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    if (!this._poderesCargados)
        //        this._presentador.CargarPoderes();

        //    ocultarLstAsociadoDatos();
        //    ocultarLstCorresponsalDatos();
        //    ocultarLstInteresadoDatos();

        //    mostrarLstPoderDatos();
        //}

        //private void _lstPoderesDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    this._presentador.CambiarPoderDatos();
        //    ocultarLstPoderSolicutud();
        //    ocultarLstPoderDatos();
        //}

        //private void _OrdenarPoderDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesDatos);
        //}

        //private void _cbxTipoPatenteDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    this._cbxTipoPatenteSolicitud.SelectedItem = ((ComboBox)sender).SelectedItem;
        //}

        
        #endregion

        private void _btnGenCartel_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GenerarCartel();
        }

        private void _btnCertificados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Certificado();
        }

        private void _cbxTipoClaseNacional_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
