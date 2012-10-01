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
        private bool _agentesCargados;
        private bool _camposHabilitados = true;


        #region IGestionarPatente

        public object Patente
        {
            get { return this._tbcPestanas.DataContext; }
            set { this._tbcPestanas.DataContext = value; }
        }


        #region Solicitud


        public string PoderSolicitud
        {
            get { return this._txtPoderSolicitud.Text; }
            set { this._txtPoderSolicitud.Text = value; }
        }


        public string NumPoderSolicitud
        {
            get { return this._txtFomentoSolicitud.Text; }
            set { this._txtFomentoSolicitud.Text = value; }
        }


        public object PoderesSolicitudFiltrar
        {
            get { return this._lstPoderesSolicitud.DataContext; }
            set { this._lstPoderesSolicitud.DataContext = value; }
        }


        public object PoderSolicitudFiltrar
        {
            get { return this._lstPoderesSolicitud.SelectedItem; }
            set { this._lstPoderesSolicitud.SelectedItem = value; }
        }


        public string AgenteSolicitud
        {
            get { return this._txtAgenteSolicitud.Text; }
            set { this._txtAgenteSolicitud.Text = value; }
        }


        public string IdAgenteSolicitud
        {
            get { return this._txtIdAgenteSolicitud.Text; }
            set { this._txtIdAgenteSolicitud.Text = value; }
        }


        public object AgentesSolicitudFiltrar
        {
            get { return this._lstAgentesSolicitud.DataContext; }
            set { this._lstAgentesSolicitud.DataContext = value; }
        }


        public object AgenteSolicitudFiltrar
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


        public object TiposPatenteSolicitud
        {
            get { return this._cbxTipoSolicitud.DataContext; }
            set { this._cbxTipoSolicitud.DataContext = value; }
        }


        public object TipoPatenteSolicitud
        {
            get { return this._cbxTipoSolicitud.SelectedItem; }
            set { this._cbxTipoSolicitud.SelectedItem = value; }
        }


        public object PresentacionesPatenteSolicitud
        {
            get { return this._cbxPresentacionSolicitud.DataContext; }
            set { this._cbxPresentacionSolicitud.DataContext = value; }
        }


        public object PresentacionPatenteSolicitud
        {
            get { return this._cbxPresentacionSolicitud.SelectedItem; }
            set { this._cbxPresentacionSolicitud.SelectedItem = value; }
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
            get { return this._txtIdInteresadoDatos.Text; }
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


        public void PintarAsociado(string tipo)
        {
            SolidColorBrush color;

            if (tipo.Equals("1"))
            {
                color = Brushes.LightGreen;
            }
            else if (tipo.Equals("2"))
            {
                color = Brushes.LightBlue;
            }
            else if (tipo.Equals("3"))
            {
                color = Brushes.LightYellow;
            }
            else if (tipo.Equals("4"))
            {
                color = Brushes.Pink;
            }
            else color = Brushes.Transparent;

            this._txtIdAsociadoDatos.Background = color;
            this._txtIdAsociadoSolicitud.Background = color;
            this._txtAsociadoDatos.Background = color;
            this._txtAsociadoSolicitud.Background = color;
        }


        public void PintarInfoAdicionalSolicitud()
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


        //public void PintarDisenoReporteSolicitud()
        //{
        //    this._btnDisenoReporteSolicitud.Background = Brushes.LightGreen;
        //}


        public void PintarCasoEspecialSolicitud()
        {
            this._btnCasoEspecialSolicitud.Background = Brushes.LightGreen;
        }


        public void PintarDocumentosSolicitud()
        {
            this._btnDocumentosSolicitud.Background = Brushes.LightGreen;
        }


        public void PintarImprimirEdoDeCuenta()
        {
        }


        public void PintarSaldos()
        {
        }


        #endregion


        #region Datos

        public string PoderDatos
        {
            get { return this._txtPoderDatos.Text; }
            set { this._txtPoderDatos.Text = value; }
        }


        public object PoderesDatosFiltrar
        {
            get { return this._lstPoderesDatos.DataContext; }
            set { this._lstPoderesDatos.DataContext = value; }
        }


        public object PoderDatosFiltrar
        {
            get { return this._lstPoderesDatos.SelectedItem; }
            set { this._lstPoderesDatos.SelectedItem = value; }
        }


        public object PaisDatos
        {
            get { return this._cbxPaisDatos.SelectedItem; }
            set { this._cbxPaisDatos.SelectedItem = value; }
        }


        public object PaisesDatos
        {
            get { return this._cbxPaisDatos.DataContext; }
            set { this._cbxPaisDatos.DataContext = value; }
        }


        public object TiposPatenteDatos
        {
            get { return this._cbxTipoDatos.DataContext; }
            set { this._cbxTipoDatos.DataContext = value; }
        }


        public object TipoPatenteDatos
        {
            get { return this._cbxTipoDatos.SelectedItem; }
            set { this._cbxTipoDatos.SelectedItem = value; }
        }


        public object PresentacionesPatenteDatos
        {
            get { return this._cbxPresentacionDatos.DataContext; }
            set { this._cbxPresentacionDatos.DataContext = value; }
        }


        public object PresentacionPatenteDatos
        {
            get { return this._cbxPresentacionDatos.SelectedItem; }
            set { this._cbxPresentacionDatos.SelectedItem = value; }
        }


        public object StatusesWebDatos
        {
            get { return this._cbxEstatusWebDatos.DataContext; }
            set { this._cbxEstatusWebDatos.DataContext = value; }
        }


        public object StatusWebDatos
        {
            get { return this._cbxEstatusWebDatos.SelectedItem; }
            set { this._cbxEstatusWebDatos.SelectedItem = value; }
        }


        public object BoletinesOrdenPublicacionDatos
        {
            get { return this._cbxOrdenPublicacionDatos.DataContext; }
            set { this._cbxOrdenPublicacionDatos.DataContext = value; }
        }


        public object BoletinOrdenPublicacionDatos
        {
            get { return this._cbxOrdenPublicacionDatos.SelectedItem; }
            set { this._cbxOrdenPublicacionDatos.SelectedItem = value; }
        }


        public object BoletinesPublicacionDatos
        {
            get { return this._cbxBoletinPublicacionDatos.DataContext; }
            set { this._cbxBoletinPublicacionDatos.DataContext = value; }
        }


        public object BoletinPublicacionDatos
        {
            get { return this._cbxBoletinPublicacionDatos.SelectedItem; }
            set { this._cbxBoletinPublicacionDatos.SelectedItem = value; }
        }


        public object BoletinesConcesionDatos
        {
            get { return this._cbxBoletinConcesionDatos.DataContext; }
            set { this._cbxBoletinConcesionDatos.DataContext = value; }
        }


        public object BoletinConcesionDatos
        {
            get { return this._cbxBoletinConcesionDatos.SelectedItem; }
            set { this._cbxBoletinConcesionDatos.SelectedItem = value; }
        }


        public object SituacionesDatos
        {
            get { return this._cbxSituacionDatos.DataContext; }
            set { this._cbxSituacionDatos.DataContext = value; }
        }


        public object SituacionDatos
        {
            get { return this._cbxSituacionDatos.SelectedItem; }
            set { this._cbxSituacionDatos.SelectedItem = value; }
        }


        public object DetallesDatos
        {
            get { return this._cbxDetalleDatos.DataContext; }
            set { this._cbxDetalleDatos.DataContext = value; }
        }


        public object DetalleDatos
        {
            get { return this._cbxDetalleDatos.SelectedItem; }
            set { this._cbxDetalleDatos.SelectedItem = value; }
        }


        public string AbandonoDatos
        {
            get { return this._txtAbandonoDatos.Text; }
            set { this._txtAbandonoDatos.Text = value; }
        }


        public string AnualidadDatos
        {
            get { return this._txtAnualidadDatos.Text; }
            set { this._txtAnualidadDatos.Text = value; }
        }


        public string IdAsociadoDatosFiltrar
        {
            get { return this._txtIdAsociadoDatosFiltrar.Text; }
        }


        public string IdAsociadoDatos
        {
            get { return this._txtIdAsociadoDatos.Text; }
            set { this._txtIdAsociadoDatos.Text = value; }
        }


        public string NombreAsociadoDatosFiltrar
        {
            get { return this._txtNombreAsociadoDatos.Text; }
        }


        public string NombreAsociadoDatos
        {
            get { return this._txtAsociadoDatos.Text; }
            set { this._txtAsociadoDatos.Text = value; }
        }


        public object AsociadosDatos
        {
            get { return this._lstAsociadosDatos.DataContext; }
            set { this._lstAsociadosDatos.DataContext = value; }
        }


        public object AsociadoDatos
        {
            get { return this._lstAsociadosDatos.SelectedItem; }
            set
            {
                this._lstAsociadosDatos.SelectedItem = value;
                //this._lstAsociadosDatos.ScrollIntoView(value);
            }
        }


        public string IdInteresadoDatosFiltrar
        {
            get { return this._txtIdInteresadoDatosFiltrar.Text; }
        }


        public string IdInteresadoDatos
        {
            get { return this._txtIdInteresadoDatos.Text; }
            set { this._txtIdInteresadoDatos.Text = value; }
        }


        public string InteresadoPaisDatos
        {
            get { return this._txtPaisDatos.Text; }
            set { this._txtPaisDatos.Text = value; }
        }


        public string InteresadoEstadoDatos
        {
            get { return this._txtEstadoDatos.Text; }
            set { this._txtEstadoDatos.Text = value; }
        }


        public string NombreInteresadoDatosFiltrar
        {
            get { return this._txtNombreInteresadoDatos.Text; }
        }


        public string NombreInteresadoDatos
        {
            get { return this._txtInteresadoDatos.Text; }
            set { this._txtInteresadoDatos.Text = value; }
        }


        public object InteresadosDatos
        {
            get { return this._lstInteresadosDatos.DataContext; }
            set { this._lstInteresadosDatos.DataContext = value; }
        }


        public object InteresadoDatos
        {
            get { return this._lstInteresadosDatos.SelectedItem; }
            set
            {
                this._lstInteresadosDatos.SelectedItem = value;
                //this._lstInteresadosDatos.ScrollIntoView(value);
            }
        }


        public string FechaTermino
        {
            get { return this._txtFechaRenovacionDatos.Text; }
            set { this._txtFechaRenovacionDatos.Text = value; }
        }


        public string Duracion
        {
            get { return this._txtDuracion.Text; }
            set { this._txtDuracion.Text = value; }
        }


        public void PintarInventoresDatos()
        {
            this._btnInventoresDatos.Background = Brushes.LightGreen;
        }


        public void PintarInfoBolDatos()
        {
            this._btnInfoBolDatos.Background = Brushes.LightGreen;
        }


        public void PintarDisenoDatos()
        {
            this._btnDisenoDatos.Background = Brushes.LightGreen;
        }


        public void PintarAuditoriaDatos()
        {
            this._btnAuditoriaDatos.Background = Brushes.LightGreen;
        }


        public void PintarMemoriaDatos()
        {
            this._btnVerMemoriaDatos.Background = Brushes.LightGreen;
        }


        public void PintarFechasDatos()
        {
            this._btnFechasDatos.Background = Brushes.LightGreen;
        }


        public void PintarOperacionesDatos()
        {
            this._btnOperacionDatos.Background = Brushes.LightGreen;
        }


        #endregion


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


        public string SituacionDescripcion
        {
            set { this._txtSituacionDescripcion.Text = value; }
        }


        public bool PoderesEstanCargados
        {
            get { return this._poderesCargados; }
            set { this._poderesCargados = value; }
        }


        public bool AgentesEstanCargados
        {
            get { return this._agentesCargados; }
            set { this._agentesCargados = value; }
        }


        public void ArchivoNoEncontrado()
        {
            MessageBox.Show(Recursos.MensajesConElUsuario.ErrorCertificadoNoEncontrado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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


        public string TextoBotonRegresar
        {
            get { return this._txbCancelar.Text; }
            set { this._txbCancelar.Text = value; }
        }


        public void OcultarTabSolicitud()
        {
            this._tabSolicitud.Visibility = Visibility.Collapsed;
            this._tabDatos.IsSelected = true;
        }


        public void SeleccionarTabSolicitud()
        {
            this._tabSolicitud.IsSelected = true;
        }


        public bool ConfirmarAccion(string Titulo, string Mensaje)
        {
            bool retorno = false;
            if (MessageBoxResult.Yes == MessageBox.Show(Mensaje,
                    Titulo, MessageBoxButton.YesNo, MessageBoxImage.Question))
                retorno = true;

            return retorno;
        }


        public bool HabilitarCampos
        {
            set
            {
                #region TextBoxs

                //Solicitud
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
                this._txtFomentoSolicitud.IsEnabled = value;
                this._txtAgenteSolicitud.IsEnabled = value;
                this._txtIdAgenteSolicitud.IsEnabled = value;
                this._txtResumenSolicitud.IsEnabled = value;
                this._txtObservacionSolicitud.IsEnabled = value;
                this._txtOrdenSolicitud.IsEnabled = value;


                //Datos

                this._txtAsociadoDatos.IsEnabled = value;
                this._txtDescripcionDatos.IsEnabled = value;
                this._txtIdAsociadoDatos.IsEnabled = value;
                this._txtIdInteresadoDatos.IsEnabled = value;
                this._txtIdDatos.IsEnabled = value;
                this._txtInteresadoDatos.IsEnabled = value;
                this._txtNombreAsociadoDatos.IsEnabled = value;
                this._txtNombreInteresadoDatos.IsEnabled = value;
                this._txtPoderDatos.IsEnabled = value;
                this._txtCasoDatos.IsEnabled = value;
                this._txtPaisDatos.IsEnabled = value;
                this._txtEstadoDatos.IsEnabled = value;
                this._txtCasoDatos.IsEnabled = value;
                this._txtPrioridadDatos.IsEnabled = value;
                this._txtResumenDatos.IsEnabled = value;
                this._txtOrdenDatos.IsEnabled = value;
                this._txtAbandonoDatos.IsEnabled = value;
                this._txtAnualidadDatos.IsEnabled = value;
                this._txtFechaRenovacionDatos.IsEnabled = value;
                this._txtDuracion.IsEnabled = value;

                this._txtComentarioDatos.IsEnabled = value;
                this._txtCodigoRegistroDatos.IsEnabled = value;
                this._txtCodigoInscripcionDatos.IsEnabled = value;
                //this._txtUbicacionDatos.IsEnabled = value;
                //this._txtExpedienteDatos.IsEnabled = value;

                #endregion

                #region Datepicker

                //Solicitud
                this._dpkFechaInscripcionSolicitud.IsEnabled = value;
                this._dpkFechaPrioridadSolicitud.IsEnabled = value;
                this._dpkFechaOrdenSolicitud.IsEnabled = value;

                //Datos
                this._dpkFechaPrioridadDatos.IsEnabled = value;
                this._dpkFechaOrdenDatos.IsEnabled = value;
                //this._dpkFechaPublicacionDatos.IsEnabled = value;
                this._dpkFechaBaseDatos.IsEnabled = value;
                this._dpkFechaRegistroDatos.IsEnabled = value;
                this._dpkFechaInscripcionDatos.IsEnabled = value;

                #endregion

                #region ComboBoxs

                //Solicitud
                this._cbxPresentacionSolicitud.IsEnabled = value;
                this._cbxTipoSolicitud.IsEnabled = value;
                this._cbxPaisSolicitud.IsEnabled = value;

                //Datos
                this._cbxPresentacionDatos.IsEnabled = value;
                this._cbxTipoDatos.IsEnabled = value;
                this._cbxPaisDatos.IsEnabled = value;
                this._cbxDetalleDatos.IsEnabled = value;
                this._cbxSituacionDatos.IsEnabled = value;
                this._cbxEstatusWebDatos.IsEnabled = value;
                this._cbxBoletinConcesionDatos.IsEnabled = value;
                this._cbxBoletinPublicacionDatos.IsEnabled = value;
                this._cbxOrdenPublicacionDatos.IsEnabled = value;

                #endregion

                #region CheckBox

                this._chkMemoriaTraducidaSolicitud.IsEnabled = value;
                this._chkRevisadoWeb.IsEnabled = value;

                #endregion

                #region Botones

                //this._btnAceptar.IsEnabled = value;
                //this._btnCancelar.IsEnabled = value;

                //Solicitud
                this._btnConsultarAsociadoSolicitud.IsEnabled = value;
                this._btnConsultarInteresadoSolicitud.IsEnabled = value;
                this._btnInfoAdicionalSolicitud.IsEnabled = value;
                this._btnSaldosSolicitud.IsEnabled = value;
                this._btnDisenoSolicitud.IsEnabled = value;
                //this._btnDisenoReporteSolicitud.IsEnabled = value;
                this._btnDocumentosSolicitud.IsEnabled = value;
                this._btnInfoAdicionalSolicitud.IsEnabled = value;
                this._btnCasoEspecialSolicitud.IsEnabled = value;
                this._btnDisenoReporteSolicitud.IsEnabled = value;
                this._btnInventoresSolicitud.IsEnabled = value;
                this._btnImprimirEdoDeCuentaSolicitud.IsEnabled = value;

                //Datos
                this._btnDisenoDatos.IsEnabled = value;
                this._btnConsultarInteresadoDatos.IsEnabled = value;
                this._btnConsultarAsociadoDatos.IsEnabled = value;
                this._btnInventoresDatos.IsEnabled = value;
                this._btnNoSolicitudDatos.IsEnabled = value;
                this._btnNoRegistroDatos.IsEnabled = value;
                this._btnOperacionDatos.IsEnabled = value;
                this._btnAuditoriaDatos.IsEnabled = value;
                this._btnVerSolicitudDatos.IsEnabled = value;
                this._btnInfoBolDatos.IsEnabled = value;
                this._btnFechasDatos.IsEnabled = value;
                this._btnVerTituloDatos.IsEnabled = value;
                this._btnVerSolicitudDatos.IsEnabled = value;
                this._btnVerMemoriaDatos.IsEnabled = value;
                this._btnVerMemoriaRutaDatos.IsEnabled = value;
                this._btnVerExpedienteDatos.IsEnabled = value;
                //this._btnArchivoDatos.IsEnabled = value;
                this._btnFacturacionDatos.IsEnabled = value;
                this._btnCertificadoDatos.IsEnabled = value;
                this._btnCertificadoDatos.IsEnabled = value;
                this._camposHabilitados = value;
                #endregion

            }
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


        public string IdAsociadoInternacionalFiltrar
        {
            get { return this._txtIdAsociadoIntFiltrar.Text; }
            set { this._txtIdAsociadoIntFiltrar.Text = value; }
        }


        public string NombreAsociadoInternacionalFiltrar
        {
            get { return this._txtNombreAsociadoIntFiltrar.Text; }
            set { this._txtNombreAsociadoIntFiltrar.Text = value; }
        }


        public object AsociadosInternacionales
        {
            get { return this._lstAsociadosInternacionalesSolicitud.DataContext; }
            set { this._lstAsociadosInternacionalesSolicitud.DataContext = value; }
        }


        public object AsociadoInternacional
        {
            get { return this._lstAsociadosInternacionalesSolicitud.SelectedItem; }
            set { this._lstAsociadosInternacionalesSolicitud.SelectedItem = value; }
        }


        public string IdAsociadoInternacionalFiltrarDatos
        {
            get { return this._txtIdAsociadoIntFiltrarDatos.Text; }
            set { this._txtIdAsociadoIntFiltrarDatos.Text = value; }
        }


        public string NombreAsociadoInternacionalFiltrarDatos
        {
            get { return this._txtNombreAsociadoIntFiltrarDatos.Text; }
            set { this._txtNombreAsociadoIntFiltrarDatos.Text = value; }
        }


        public object AsociadosInternacionalesDatos
        {
            get { return this._lstAsociadosInternacionalesDatos.DataContext; }
            set { this._lstAsociadosInternacionalesDatos.DataContext = value; }
        }


        public object AsociadoInternacionalDatos
        {
            get { return this._lstAsociadosInternacionalesDatos.SelectedItem; }
            set { this._lstAsociadosInternacionalesDatos.SelectedItem = value; }
        }


        public string TextoAsociadoInternacional
        {
            set
            {
                this._txtAsociadoInternacionalDatos.Text = value;
                this._txtAsociadoInternacionalSolicitud.Text = value;
            }

        }


        public object PaisesInternacionales
        {
            get { return this._cbxPaisIntSolicitud.DataContext; }
            set { this._cbxPaisIntSolicitud.DataContext = value; }
        }


        public object PaisInternacional
        {
            get { return this._cbxPaisIntSolicitud.SelectedItem; }
            set { this._cbxPaisIntSolicitud.SelectedItem = value; }
        }


        public object PaisesInternacionalesDatos
        {
            get { return this._cbxPaisIntDatos.DataContext; }
            set { this._cbxPaisIntDatos.DataContext = value; }
        }


        public object PaisInternacionalDatos
        {
            get { return this._cbxPaisIntDatos.SelectedItem; }
            set { this._cbxPaisIntDatos.SelectedItem = value; }
        }


        public bool EsPatenteNacional
        {
            get { return this._radioNacional.IsChecked.Value; }
        }


        public void MarcarRadioPatenteNacional(bool esNacional)
        {
            if (esNacional)
            {
                this._radioExtranjero.IsChecked = false;
                this._radioNacional.IsChecked = true;
            }
            else
            {
                this._radioExtranjero.IsChecked = true;
                this._radioNacional.IsChecked = false;
            }
        }

        public void BorrarCeros()
        {
            this._txtCod.Text = this._txtCod.Text.Equals("0") ? string.Empty : this._txtCod.Text;
            this._txtCodDatos.Text = this._txtCodDatos.Text.Equals("0") ? string.Empty : this._txtCodDatos.Text;
            this._txtNum.Text = this._txtNum.Text.Equals("0") ? string.Empty : this._txtNum.Text;
            this._txtNumDatos.Text = this._txtNumDatos.Text.Equals("0") ? string.Empty : this._txtNumDatos.Text;
        }

        #endregion


        public GestionarPatente(object patenteSeleccionada, object ventanaAVolver)
        {
            InitializeComponent();

            this._cargada = false;
            this._asociadosCargados = false;
            this._interesadosCargados = false;
            this._poderesCargados = false;
            this._presentador = new PresentadorGestionarPatente(this, patenteSeleccionada, ventanaAVolver);
        }

        public GestionarPatente(object patenteSeleccionada, string tab)
            : this(patenteSeleccionada, (Page)null)
        {
            this._presentador.CambiarAModificar();

            foreach (TabItem item in this._tbcPestanas.Items)
            {
                if (item.Header.Equals(tab))
                    item.IsSelected = true;
            }
        }

        private void _cbxSituacionDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.DescripcionSituacion();
        }

        #region Funciones Solicitud

        private void MostrarLstAsociadoSolicitud()
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

        private void OcultarLstAsociadoSolicitud()
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

        private void MostrarLstInteresadoSolicitud()
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

        private void OcultarLstInteresadoSolicitud()
        {
            //this._presentador.CambiarInteresadoSolicitud();
            this._lstInteresadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoSolicitudFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void MostrarLstPoderSolicitud()
        {
            this._txtPoderSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstPoderesSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstPoderesSolicitud.IsEnabled = true;
        }

        private void OcultarLstPoderSolicitud()
        {
            //this._presentador.CambiarPoderSolicitud();
            this._lstPoderesSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtPoderSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        private void MostrarLstAgenteSolicitud()
        {
            this._txtAgenteSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAgentesSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstAgentesSolicitud.IsEnabled = true;
        }

        private void OcultarLstAgenteSolicitud()
        {
            //this._presentador.CambiarAgenteSolicitud();

            this._lstAgentesSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAgenteSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion

        #region Funciones Datos

        private void MostrarLstAsociadoDatos()
        {
            this._lstAsociadosDatos.ScrollIntoView(this.AsociadoDatos);
            this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociadosDatos.IsEnabled = true;
            this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoDatosFiltrar.Visibility = System.Windows.Visibility.Visible;
        }

        private void OcultarLstAsociadoDatos()
        {
            this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoDatosFiltrar.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void MostrarLstInteresadoDatos()
        {
            this._lstInteresadosDatos.ScrollIntoView(this.InteresadoDatos);
            this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresadosDatos.IsEnabled = true;
            this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoDatosFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
        }

        private void OcultarLstInteresadoDatos()
        {
            //this._presentador.CambiarInteresadoDatos();
            this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoDatosFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void MostrarLstPoderDatos()
        {
            this._txtPoderDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstPoderesDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstPoderesDatos.IsEnabled = true;
        }

        private void OcultarLstPoderDatos()
        {
            //this._presentador.CambiarPoderDatos();
            this._lstPoderesDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtPoderDatos.Visibility = System.Windows.Visibility.Visible;
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
            else
                this._presentador.ActualizarTitulo();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (_camposHabilitados)
            {
                //if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarModificarPatente,
                //    _presentador.ObtenerIdPatente()),
                //    "Modificar Patente", MessageBoxButton.YesNo, MessageBoxImage.Question))
                //{
                this._presentador.Modificar();
                _camposHabilitados = false;
                //}
            }
            else
                this._presentador.Modificar();

        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrConsultarPatentes();
        }

        public void ActivarControlesAlAgregar()
        {

            #region Solicitud

            this._btnDisenoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnDocumentosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnCasoEspecialSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnInventoresSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnInfoAdicionalSolicitud.Visibility = System.Windows.Visibility.Collapsed;

            this._txtIdSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdDatos.Visibility = System.Windows.Visibility.Collapsed;

            #endregion

            #region Datos

            this._btnDisenoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnOperacionDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnAuditoriaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnInventoresDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnVerSolicitudDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnInfoBolDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnFechasDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnVerTituloDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnVerMemoriaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnVerExpedienteDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnFacturacionDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnVerMemoriaRutaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnCertificadoDatos.Visibility = System.Windows.Visibility.Collapsed;

            this._lblAnualidad.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAnualidadDatos.Visibility = System.Windows.Visibility.Collapsed;

            this._lblAbandono.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAbandonoDatos.Visibility = System.Windows.Visibility.Collapsed;

            this._spdrBotones.Visibility = System.Windows.Visibility.Collapsed;


            this._txtDescripcionSolicitud.IsReadOnly = false;
            this._txtDescripcionDatos.IsReadOnly = false;

            this._cbxSituacionDatos.IsEnabled = false;

            #endregion

        }

        public void ConvertirEnteroMinimoABlanco()
        {

            #region Poder

            if (null != this.PoderDatos)
            {
                if (!this.PoderDatos.Equals(""))
                {
                    if (int.Parse(this.PoderDatos) == int.MinValue)
                    {
                        this.PoderDatos = "";
                        this.PoderSolicitud = "";
                    }
                }
            }

            if (null != this.PoderSolicitud)
            {
                if (!this.PoderSolicitud.Equals(""))
                {
                    if (int.Parse(this.PoderSolicitud) == int.MinValue)
                    {
                        this.PoderDatos = "";
                        this.PoderSolicitud = "";
                    }
                }
            }

            #endregion

            #region Interesado

            if (null != this.InteresadoDatos)
            {
                if (!this.IdInteresadoDatos.Equals(""))
                {
                    if (int.Parse(this.IdInteresadoDatos) == int.MinValue)
                    {
                        this.IdInteresadoSolicitud = "";
                        this.IdInteresadoDatos = "";
                    }
                }
            }

            if (null != this.InteresadoSolicitud)
            {
                if (!this.IdInteresadoSolicitud.Equals(""))
                {
                    if (int.Parse(this.IdInteresadoSolicitud) == int.MinValue)
                    {
                        this.IdInteresadoSolicitud = "";
                        this.IdInteresadoDatos = "";
                    }
                }
            }

            #endregion

            #region Asociado

            if (null != this.AsociadoDatos)
            {
                if (!this.IdAsociadoDatos.Equals(""))
                {
                    if (int.Parse(this.IdAsociadoDatos) == int.MinValue)
                    {
                        this.IdAsociadoDatos = "";
                        this.IdAsociadoSolicitud = "";
                    }
                }
            }

            if (null != this.AsociadoSolicitud)
            {
                if (!this.IdAsociadoSolicitud.Equals(""))
                {
                    if (int.Parse(this.IdAsociadoSolicitud) == int.MinValue)
                    {
                        this.IdAsociadoDatos = "";
                        this.IdAsociadoSolicitud = "";
                    }
                }
            }

            #endregion
        }

        #endregion

        #region Eventos Solicitudes

        private void _txtAsociadoSolicitud_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!this._asociadosCargados)
            {
                this._presentador.CargarAsociados();
            }


            OcultarLstInteresadoSolicitud();
            OcultarLstPoderSolicitud();
            OcultarLstAgenteSolicitud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoSolicitud.IsDefault = true;

            MostrarLstAsociadoSolicitud();

        }

        private void _lstAsociadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoSolicitud();
            OcultarLstAsociadoSolicitud();
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
            _asociadosCargados = true;
        }

        private void _txtInteresadoSolicitud_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!this._interesadosCargados)
            {
                this._presentador.CargarInteresados();
            }

            OcultarLstAsociadoSolicitud();
            OcultarLstPoderSolicitud();
            OcultarLstAgenteSolicitud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresadoSolicitud.IsDefault = true;

            MostrarLstInteresadoSolicitud();
        }

        private void _btnConsultarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado(0);
            _interesadosCargados = true;
        }

        private void _lstInteresadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarInteresadoSolicitud();
            OcultarLstInteresadoSolicitud();

            this._btnConsultarInteresadoSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _OrdenarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosSolicitud);
        }

        private void _txtPoderSolicitud_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this._presentador.CargarPoderes();

            OcultarLstAsociadoSolicitud();
            OcultarLstInteresadoSolicitud();
            OcultarLstAgenteSolicitud();

            MostrarLstPoderSolicitud();
        }

        private void _lstPoderesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarPoderSolicitud();
            OcultarLstPoderSolicitud();
            //ocultarLstPoderDatos();
        }

        private void _OrdenarPoderSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesSolicitud);
        }

        private void _btnConsultarPoderesSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _OrdenarAgenteSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAgentesSolicitud);
        }

        private void _txtAgenteSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (!this._agentesCargados)
            //    this._presentador.CargarAgentes();

            OcultarLstAsociadoSolicitud();
            OcultarLstInteresadoSolicitud();
            OcultarLstPoderSolicitud();

            MostrarLstAgenteSolicitud();
        }

        private void _btnConsultarAgenteSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _lstAgentesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAgenteSolicitud();
            OcultarLstAgenteSolicitud();
        }

        private void _cbxTipoPatenteSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxTipoDatos.SelectedItem = ((ComboBox)sender).SelectedItem;
        }

        private void _cbxPresentacionSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxPresentacionDatos.SelectedItem = ((ComboBox)sender).SelectedItem;
        }

        private void _cbxPaisSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxPaisDatos.SelectedItem = ((ComboBox)sender).SelectedItem;
        }

        private void _btnInventoresSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Inventores();
        }

        private void _btnImprimirEdoCuentaSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnDisenoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.MostrarDiseno();
        }

        private void _btnSaldoSolicitud_Click(object sender, RoutedEventArgs e)
        {

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

        private void _btnDisenoReporteSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnDocumentosSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnCasoEspecialSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Eventos Datos

        private void _txtAsociadoDatos_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!this._asociadosCargados)
            {
                this._presentador.CargarAsociados();
            }


            OcultarLstInteresadoDatos();
            OcultarLstPoderDatos();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoDatos.IsDefault = true;

            MostrarLstAsociadoDatos();

        }

        private void _lstAsociadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoDatos();
            OcultarLstAsociadoDatos();
            this._btnConsultarAsociadoDatos.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _OrdenarAsociadoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAsociadosDatos);
        }

        private void _btnConsultarAsociadoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado(1);
            _asociadosCargados = true;
        }

        private void _txtInteresadoDatos_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!this._interesadosCargados)
            {
                this._presentador.CargarInteresados();
            }

            OcultarLstAsociadoDatos();
            OcultarLstPoderDatos();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresadoDatos.IsDefault = true;

            MostrarLstInteresadoDatos();
        }

        private void _btnConsultarInteresadoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado(1);
            _interesadosCargados = true;
        }

        private void _lstInteresadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarInteresadoDatos();
            OcultarLstInteresadoDatos();

            this._btnConsultarInteresadoDatos.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _OrdenarInteresadoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosDatos);
        }

        private void _txtPoderDatos_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!this._poderesCargados)
                this._presentador.CargarPoderes();

            OcultarLstAsociadoDatos();
            OcultarLstInteresadoDatos();

            MostrarLstPoderDatos();
        }

        private void _lstPoderesDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarPoderDatos();
            OcultarLstPoderDatos();

        }

        private void _OrdenarPoderDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesDatos);
        }

        private void _cbxTipoPatenteDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxTipoSolicitud.SelectedItem = ((ComboBox)sender).SelectedItem;
        }

        private void _cbxPaisDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxPaisSolicitud.SelectedItem = ((ComboBox)sender).SelectedItem;
        }

        private void _cbxPresentacionDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxPresentacionSolicitud.SelectedItem = ((ComboBox)sender).SelectedItem;
        }

        private void _btnConsultarAgenteDatos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnOperacionesDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrOperaciones();
        }

        private void _btnAuditoriaDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }

        private void _btnInfoBolDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrInfoBoles();
        }

        private void _btnDisenoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.MostrarDiseno();
        }

        private void _btnInventoresDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Inventores();
        }

        private void _btnFechasDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerFechas();
        }

        private void _btnVerSolicitudDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerSolicitud();
        }

        private void _btnVerTituloDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerTitulo();
        }

        private void _btnVerMemoriaDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerMemoria();
        }

        private void _btnVerMemoriaRutaDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerMemoriaRuta();
        }

        private void _btnVerExpedienteDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerExpediente();
        }

        private void _btnArchivoDatos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnFacturacionDatos_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void _btnVerMemoriaDatos_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void _btnCertificadoDatos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnIrExplorador_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrSAPI();
        }

        #endregion

        //private void _txtAsociadoInternacionalSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void _lstAsociadosInternacionalesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void _cbxPaisIntSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void _btnConsultarAsociadoInternacionalSolicitud_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void _txtAsociadoInternacionalDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void _btnConsultarAsociadoInternacionalDatos_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void _lstAsociadosInternacionalesDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //}

        //private void _cbxPaisIntDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}




        private void _lstAsociadosInternacionalesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociadoInternacionalSolicitud())
            {

                this._txtAsociadoInternacionalSolicitud.Visibility = Visibility.Visible;

                this._lblNombreAsociadoIntFiltrar.Visibility = Visibility.Collapsed;
                this._lblIdAsociadoIntFiltrar.Visibility = Visibility.Collapsed;
                this._btnConsultarAsociadoInternacionalSolicitud.Visibility = Visibility.Collapsed;
                this._txtIdAsociadoIntFiltrar.Visibility = Visibility.Collapsed;
                this._txtNombreAsociadoIntFiltrar.Visibility = Visibility.Collapsed;

                this._lstAsociadosInternacionalesSolicitud.Visibility = Visibility.Collapsed;

                this._btnAceptar.IsDefault = false;
                this._btnConsultarAsociadoInternacionalSolicitud.IsDefault = false;
            }
        }


        private void _txtAsociadoInternacionalSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._txtAsociadoInternacionalSolicitud.Visibility = Visibility.Collapsed;

            this._lblNombreAsociadoIntFiltrar.Visibility = Visibility.Visible;
            this._lblIdAsociadoIntFiltrar.Visibility = Visibility.Visible;
            this._btnConsultarAsociadoInternacionalSolicitud.Visibility = Visibility.Visible;
            this._txtIdAsociadoIntFiltrar.Visibility = Visibility.Visible;
            this._txtNombreAsociadoIntFiltrar.Visibility = Visibility.Visible;

            this._lstAsociadosInternacionalesSolicitud.Visibility = Visibility.Visible;

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoInternacionalSolicitud.IsDefault = true;
        }


        private void _txtAsociadoInternacionalDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._txtAsociadoInternacionalDatos.Visibility = Visibility.Collapsed;

            this._lblNombreAsociadoIntFiltrarDatos.Visibility = Visibility.Visible;
            this._lblIdAsociadoIntFiltrarDatos.Visibility = Visibility.Visible;
            this._btnConsultarAsociadoInternacionalDatos.Visibility = Visibility.Visible;
            this._txtIdAsociadoIntFiltrarDatos.Visibility = Visibility.Visible;
            this._txtNombreAsociadoIntFiltrarDatos.Visibility = Visibility.Visible;

            this._lstAsociadosInternacionalesDatos.Visibility = Visibility.Visible;

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoInternacionalDatos.IsDefault = true;
        }


        private void _btnConsultarAsociadoInternacionalSolicitud_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.ConsultarAsociado())
            {
            }
            else
            { }
        }


        private void _btnConsultarAsociadoInternacionalDatos_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.ConsultarAsociadoDatos())
            {
            }
            else
            { }
        }


        private void _lstAsociadosInternacionalesDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (this._presentador.CambiarAsociadoInternacionalDatos())
            {
                this._txtAsociadoInternacionalDatos.Visibility = Visibility.Visible;

                this._lblNombreAsociadoIntFiltrarDatos.Visibility = Visibility.Collapsed;
                this._lblIdAsociadoIntFiltrarDatos.Visibility = Visibility.Collapsed;
                this._btnConsultarAsociadoInternacionalDatos.Visibility = Visibility.Collapsed;
                this._txtIdAsociadoIntFiltrarDatos.Visibility = Visibility.Collapsed;
                this._txtNombreAsociadoIntFiltrarDatos.Visibility = Visibility.Collapsed;

                this._lstAsociadosInternacionalesDatos.Visibility = Visibility.Collapsed;



                this._btnAceptar.IsDefault = true;
                this._btnConsultarAsociadoInternacionalDatos.IsDefault = false;
            }
        }


        private void _cbxPaisIntSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxPaisIntDatos.SelectedIndex = _cbxPaisIntSolicitud.SelectedIndex;
        }


        private void _cbxPaisIntDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxPaisIntSolicitud.SelectedIndex = _cbxPaisIntDatos.SelectedIndex;
        }


        private void _btnIrInteresados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaInteresado();
        }


        private void _btnIrPoder_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaPoder();
        }


        private void _btnIrAsociados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaAsociado();
        }



        public void CambiarLabelsPorBotones()
        {
            this._lblAsociado.Visibility = Visibility.Visible;
            this._btnIrAsociados.Visibility = Visibility.Collapsed;

            this._lblAsociadoDatos.Visibility = Visibility.Visible;
            this._btnIrAsociadosDatos.Visibility = Visibility.Collapsed;


            this._lblInteresado.Visibility = Visibility.Visible;
            this._btnIrInteresados.Visibility = Visibility.Collapsed;

            this._lblInteresadoDatos.Visibility = Visibility.Visible;
            this._btnIrInteresadosDatos.Visibility = Visibility.Collapsed;


            this._lblPoder.Visibility = Visibility.Visible;
            this._btnIrPoder.Visibility = Visibility.Collapsed;

            this._lblPoderDatos.Visibility = Visibility.Visible;
            this._btnIrPoderDatos.Visibility = Visibility.Collapsed;
        }
    }
}
