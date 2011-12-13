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
        private PresentadorConsultarMarca _presentador;
        private bool _cargada;

        #region IConsultarMarca

        public object Marca
        {
            get { return this._tbcPestañas.DataContext; }
            set { this._tbcPestañas.DataContext = value; }
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

        public object PaisSolicitud
        {
            get { return this._cbxPaisPrioridad.SelectedItem; }
            set { this._cbxPaisPrioridad.SelectedItem = value; }
        }

        public object PaisesSolicitud
        {
            get { return this._cbxPaisPrioridad.DataContext; }
            set { this._cbxPaisPrioridad.DataContext = value; }
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
                #region TextBoxs

                this._txtAsociadoDatos.IsEnabled = value;
                this._txtAsociadoSolicitud.IsEnabled = value;
                this._txtBusqueda.IsEnabled = value;
                this._txtCiudad.IsEnabled = value;
                this._txtCartaOrdenDatos.IsEnabled = value;
                this._txtCiudad.IsEnabled = value;
                this._txtClaseInternacional.IsEnabled = value;
                this._txtClaseInternacionalDatos.IsEnabled = value;
                this._txtClaseNacional.IsEnabled = value;
                this._txtClaseNacionalDatos.IsEnabled = value;
                this._txtCod.IsEnabled = value;
                this._txtCodigoInscripcion.IsEnabled = value;
                this._txtCodigoInscripcionSolicitud.IsEnabled = value;
                this._txtCodigoPrioridad.IsEnabled = value;
                this._txtCodigoRegistro.IsEnabled = value;
                this._txtCodIntlDatos.IsEnabled = value;
                this._txtComentarioDatos.IsEnabled = value;
                this._txtConflictoDatos.IsEnabled = value;
                this._txtCorrespondenciaDatos.IsEnabled = value;
                this._txtCorresponsal.IsEnabled = value;
                this._txtCorresponsalDatos.IsEnabled = value;
                this._txtDescripcionDatos.IsEnabled = value;
                this._txtDescripcionSolicitud.IsEnabled = value;
                this._txtDescuentoDatos.IsEnabled = value;
                this._txtDistingue.IsEnabled = value;
                this._txtDistingueDatos.IsEnabled = value;
                this._txtDistingueInglesDatos.IsEnabled = value;
                this._txtEtiqueta.IsEnabled = value;
                this._txtEtiquetaDatos.IsEnabled = value;
                this._txtExptyr.IsEnabled = value;
                this._txtFacturacionDatos.IsEnabled = value;
                this._txtFechaInscripcion.IsEnabled = value;
                this._txtFechaRegistro.IsEnabled = value;
                this._txtFechaRenovacion.IsEnabled = value;
                this._txtIdAsociadoDatos.IsEnabled = value;
                this._txtIdAsociadoSolicitud.IsEnabled = value;
                this._txtIdDatos.IsEnabled = value;
                this._txtIdInteresadoDatos.IsEnabled = value;
                this._txtIdInteresadoSolicitud.IsEnabled = value;
                this._txtIdSolicitud.IsEnabled = value;
                this._txtInteresadoDatos.IsEnabled = value;
                this._txtInteresadoSolicitud.IsEnabled = value;
                this._txtITipoDatos.IsEnabled = value;
                this._txtLocalidad.IsEnabled = value;
                this._txtLocalidadDatos.IsEnabled = value;
                this._txtNombreAsociadoDatos.IsEnabled = value;
                this._txtNombreAsociadoSolicitud.IsEnabled = value;
                this._txtNombreInteresadoDatos.IsEnabled = value;
                this._txtNombreInteresadoSolicitud.IsEnabled = value;
                this._txtNum.IsEnabled = value;
                this._txtNumIntlDatos.IsEnabled = value;
                this._txtNumSapi.IsEnabled = value;
                this._txtOtroDatos.IsEnabled = value;
                this._txtOtrosImp.IsEnabled = value;
                this._txtPais.IsEnabled = value;
                this._txtPrimeraReferenciaDatos.IsEnabled = value;
                this._txtReclasificacionDatos.IsEnabled = value;
                this._txtReferencia.IsEnabled = value;
                this._txtReferenciaAsocInt.IsEnabled = value;
                this._txtReferenciaDatos.IsEnabled = value;
                this._txtSaldoPorVencer.IsEnabled = value;
                this._txtSaldoVencido.IsEnabled = value;
                this._txtTipoClaseNacional.IsEnabled = value;
                this._txtTotalDeuda.IsEnabled = value;
                
                #endregion

                #region ComboBoxs

                this._cbxAgente.IsEnabled = value;
                this._cbxAsociadoInteresadoDatos.IsEnabled = value;
                this._cbxAsocInt.IsEnabled = value;
                this._cbxBoletinConcesion.IsEnabled = value;
                this._cbxBoletinPublicacion.IsEnabled = value;
                this._cbxCondiciones.IsEnabled = value;
                this._cbxConflicto.IsEnabled = value;
                this._cbxDetalleDatos.IsEnabled = value;
                this._cbxEstadoDatos.IsEnabled = value;
                this._cbxIdiomaDatos.IsEnabled = value;
                this._cbxMarcaOrigen.IsEnabled = value;
                this._cbxMarcaOrigenSolicitud.IsEnabled = value;
                this._cbxOrdenPublicacion.IsEnabled = value;
                this._cbxPais.IsEnabled = value;
                this._cbxPaisDatos.IsEnabled = value;
                this._cbxPaisPrioridad.IsEnabled = value;
                this._cbxPoder.IsEnabled = value;
                this._cbxPoderDatos.IsEnabled = value;
                this._cbxSector.IsEnabled = value;
                this._cbxSituacion.IsEnabled = value;
                this._cbxTipoMarca.IsEnabled = value;
                this._cbxTipoReproduccion.IsEnabled = value;

                #endregion

                #region CheckBox

                this._checkBoxInstruccionesRenovacion.IsEnabled = value;
                this._checkBoxRenovacionTramitente.IsEnabled = value;
                this._chkConflicto.IsEnabled = value;
                this._chkEtiqueta.IsEnabled = value;
                this._chkOtraInf.IsEnabled = value;
                this._chkPoder.IsEnabled = value;
                this._chkPoderYPrioridad.IsEnabled = value;
                this._chkPrioridad.IsEnabled = value;
                this._chkReclasificacionNacional.IsEnabled = value;

                #endregion

                #region Botones

                this._btnAceptar.IsEnabled = value;
                this._btnAnaqua.IsEnabled = value;
                this._btnAnexoFM02.IsEnabled = value;
                this._btnAuditoria.IsEnabled = value;
                this._btnBusqueda.IsEnabled = value;
                this._btnBusquedas.IsEnabled = value;
                this._btnCancelar.IsEnabled = value;
                this._btnCarpeta.IsEnabled = value;
                this._btnCertificados.IsEnabled = value;
                this._btnClaseCompleta.IsEnabled = value;
                this._btnConflicto.IsEnabled = value;
                this._btnConflictoELI.IsEnabled = value;
                this._btnConflictoINC.IsEnabled = value;
                this._btnConsultarAsociadoDatos.IsEnabled = value;
                this._btnConsultarAsociadoSolicitud.IsEnabled = value;
                this._btnConsultarInteresadoDatos.IsEnabled = value;
                this._btnConsultarInteresadoSolicitud.IsEnabled = value;
                this._btnDuplicar.IsEnabled = value;
                this._btnEnviarRecordatorios.IsEnabled = value;
                this._btnFacturacionDatos.IsEnabled = value;
                this._btnFM02.IsEnabled = value;
                this._btnFM02Venen.IsEnabled = value;
                this._btnGenCartel.IsEnabled = value;
                this._btnImprimirEdoCuenta.IsEnabled = value;
                this._btnInfoAdicional.IsEnabled = value;
                this._btnInfoAdicionalSolicitud.IsEnabled = value;
                this._btnInfobol.IsEnabled = value;
                this._btnIngles.IsEnabled = value;
                this._btnIntRenovacion.IsEnabled = value;
                this._btnIrReclasificar.IsEnabled = value;
                this._btnLAnexoFM02.IsEnabled = value;
                this._btnLFM02.IsEnabled = value;
                this._btnLFM02Venen.IsEnabled = value;
                this._btnLista.IsEnabled = value;
                this._btnNoRegistro.IsEnabled = value;
                this._btnNoSolicitud.IsEnabled = value;
                this._btnOperacionesDatos.IsEnabled = value;
                this._btnOtraInf.IsEnabled = value;
                this._btnOtraInfELI.IsEnabled = value;
                this._btnOtraInfINC.IsEnabled = value;
                this._btnPoder.IsEnabled = value;
                this._btnPoderELI.IsEnabled = value;
                this._btnPoderINC.IsEnabled = value;
                this._btnPoderYPrioridad.IsEnabled = value;
                this._btnPoderYPrioridadELI.IsEnabled = value;
                this._btnPoderYPrioridadINC.IsEnabled = value;
                this._btnPrioridad.IsEnabled = value;
                this._btnPrioridadELI.IsEnabled = value;
                this._btnPrioridadINC.IsEnabled = value;
                this._btnReclasificacionNacional.IsEnabled = value;
                this._btnReclasificacionNacionalELI.IsEnabled = value;
                this._btnReclasificacionNacionalINC.IsEnabled = value;
                this._btnRenovacion.IsEnabled = value;
                this._btnRevisarWeb.IsEnabled = value;
                this._btnSaldo.IsEnabled = value;
                this._btnVerDocDatos.IsEnabled = value;

                #endregion

                #region DatePicker

                this._dpkFecha.IsEnabled = value;
                this._dpkFechaPrioridad.IsEnabled = value;
                this._dpkFechaRequeridaConflicto.IsEnabled = value;
                this._dpkFechaRequeridaOtraInf.IsEnabled = value;
                this._dpkFechaRequeridaPoder.IsEnabled = value;
                this._dpkFechaRequeridaPoderYPrioridad.IsEnabled = value;
                this._dpkFechaRequeridaPrioridad.IsEnabled = value;
                this._dpkFechaRequeridaReclasificacionNacional.IsEnabled = value;

                #endregion
            }
        }

        public string IdAsociadoSolicitudFiltrar
        {
            get { return this._txtIdAsociadoSolicitud.Text; }
        }

        public string IdAsociadoSDatosFiltrar
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

        public object Interesados
        {
            get { return this._lstInteresadosSolicitud.DataContext; }
            set { this._lstInteresadosSolicitud.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._lstInteresadosSolicitud.SelectedItem; }
            set { this._lstInteresadosSolicitud.SelectedItem = value; }
        }
        #endregion

        public ConsultarMarca(object marcaSeleccionada)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarMarca(this, marcaSeleccionada);
        }

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
