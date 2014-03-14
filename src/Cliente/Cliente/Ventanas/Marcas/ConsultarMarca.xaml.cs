using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using System.Windows.Resources;
using System.Windows.Media.Imaging;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for AgregarAsociado.xaml
    /// </summary>
    public partial class ConsultarMarca : Page, IConsultarMarca
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;


        private PresentadorConsultarMarca _presentador;


        private bool _cargada;

        private bool _asociadosCargados;
        private bool _interesadosCargados;
        private bool _corresponsalesCargados;
        private bool _poderesCargados;
        private bool _camposHabilitados = true;
        //---
        private bool _marcaOrigenCargada;


        #region IConsultarMarca


        public object Marca
        {
            get { return this._tbcPestanas.DataContext; }
            set { this._tbcPestanas.DataContext = value; }
        }

        public bool MarcaOrigenCargada
        {
            get { return this._marcaOrigenCargada; }
            set { this._marcaOrigenCargada = value; }
        }

        public void MarcarRadioMarcaNacional(bool esNacional)
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


        public string NumPoderDatos
        {
            get { return this._txtNumSapi.Text; }
            set { this._txtNumSapi.Text = value; }
        }


        public void BloquearModificacion()
        {
            this._tabSolicitud.Visibility = Visibility.Collapsed;
            this._btnAceptar.Visibility = Visibility.Collapsed;
            this._tabDatos.IsSelected = true;
        }


        public string IdPoderDatos
        {
            get { return this._txtPoderDatos.Text; }
            set { this._txtPoderDatos.Text = value; }
        }


        public string IdPoderSolicitud
        {
            get { return this._txtPoderSolicitud.Text; }
            set { this._txtPoderSolicitud.Text = value; }
        }


        public string SituacionDescripcion
        {
            set { this._txtSituacionDescripcion.Text = value; }
        }


        public string DetalleDescripcion
        {
            set { this._txtDetalleDescripcion.Text = value; }
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


        public object PoderesDatos
        {
            get { return this._lstPoderesDatos.DataContext; }
            set { this._lstPoderesDatos.DataContext = value; }
        }


        public object PoderDatos
        {
            get { return this._lstPoderesDatos.SelectedItem; }
            set { this._lstPoderesDatos.SelectedItem = value; }
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


        public object TiposClaseNacional
        {
            get { return this._cbxTipoClaseNacional.DataContext; }
            set { this._cbxTipoClaseNacional.DataContext = value; }
        }


        public object TipoClaseNacional
        {
            get { return this._cbxTipoClaseNacional.SelectedItem; }
            set { this._cbxTipoClaseNacional.SelectedItem = value; }
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


        public string IdInternacional
        {
            get { return this._txtClaseInternacional.Text; }
            set { this._txtClaseInternacional.Text = value; }
        }


        public string IdNacional
        {
            get { return this._txtClaseNacionalDatos.Text; }
            set { this._txtClaseNacionalDatos.Text = value; }
        }


        public bool HabilitarCampos
        {
            set
            {
                #region TextBoxs

                this._txtAsociadoDatos.IsEnabled = value;
                this._txtAsociadoSolicitud.IsEnabled = value;
                this._txtBusqueda.IsEnabled = value;
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
                //this._txtConflictoDatos.IsEnabled = value;
                //this._btnConflictoDatos.IsEnabled = value;
                this._txtIdCorresponsalDatos.IsEnabled = value;
                this._txtCorresponsalDatos.IsEnabled = value;
                this._txtIdCorresponsalSolicitud.IsEnabled = value;
                this._txtCorresponsalSolicitud.IsEnabled = value;
                this._txtCorresponsalDatos.IsEnabled = value;
                this._txtDescripcionDatos.IsEnabled = value;
                this._txtDescripcionSolicitud.IsEnabled = value;
                this._txtDistingue.IsEnabled = value;
                this._txtDistingueDatos.IsEnabled = value;
                this._txtDistingueInglesDatos.IsEnabled = value;
                this._txtEtiqueta.IsEnabled = value;
                this._txtEtiquetaDatos.IsEnabled = value;
                this._txtExptyr.IsEnabled = value;
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
                this._cbxLocalidadSolicitud.IsEnabled = value;
                this._cbxLocalidadDatos.IsEnabled = value;
                this._txtNombreAsociadoDatos.IsEnabled = value;
                this._txtNombreAsociadoSolicitud.IsEnabled = value;
                this._txtNombreInteresadoDatos.IsEnabled = value;
                this._txtNombreInteresadoSolicitud.IsEnabled = value;
                this._txtNum.IsEnabled = value;
                this._txtNumIntlDatos.IsEnabled = value;
                this._txtNumSapi.IsEnabled = value;
                this._txtOtrosImp.IsEnabled = value;
                this._txtPoderSolicitud.IsEnabled = value;
                this._txtPoderDatos.IsEnabled = value;
                this._txtPrimeraReferenciaDatos.IsEnabled = value;
                this._txtReclasificacionDatos.IsEnabled = value;
                this._txtReferencia.IsEnabled = value;
                this._txtReferenciaAsocInt.IsEnabled = value;
                this._txtReferenciaDatos.IsEnabled = value;
                this._txtSaldoPorVencer.IsEnabled = value;
                this._txtSaldoPorVencerDatos.IsEnabled = value;
                this._txtSaldoVencido.IsEnabled = value;
                this._txtSaldoVencidoDatos.IsEnabled = value;
                this._txtTotalDeuda.IsEnabled = value;
                this._txtTotalDeudaDatos.IsEnabled = value;
                this._txtSituacionDescripcion.IsEnabled = value;

                this._txtClaseInternacionalIDatos.IsEnabled = value;
                this._txtClaseInternacionalSolicitud.IsEnabled = value;
                this._txtReferenciaInteresado.IsEnabled = value;
                this._txtReferenciaInteresadoDatos.IsEnabled = value;
                this._txtMarcaOrigenSolicitud.IsEnabled = value;
                this._txtIdMarcaOrigenDatos.IsEnabled = value;
                this._lblIdentMarcaOrigenSolicitud.IsEnabled = value;
                //this._txtIdExpedienteTraspasoRenovacionDatos.IsEnabled = value;
                this._txtIdExpedienteTraspasoRenovacionSolicitud.IsEnabled = value;
                this._txtFechaCierreExpDatos.IsEnabled = value;

                #endregion

                #region ComboBoxs

                this._cbxTipoClaseNacional.IsEnabled = value;
                this._cbxAgente.IsEnabled = value;
                this._txtAsociadoInternacionalSolicitud.IsEnabled = value;
                this._txtAsociadoInternacionalDatos.IsEnabled = value;
                this._cbxBoletinConcesion.IsEnabled = value;
                this._cbxBoletinPublicacion.IsEnabled = value;
                //this._cbxCartaOrden.IsEnabled = value;
                this._cbxCondiciones.IsEnabled = value;
                this._cbxDetalleDatos.IsEnabled = value;
                this._cbxEstadoDatos.IsEnabled = value;
                //this._cbxIdiomaDatos.IsEnabled = value;
                //this._cbxMarcaOrigen.IsEnabled = value;
                //this._cbxMarcaOrigenSolicitud.IsEnabled = value; COMENTADO PARA PRUEBAS
                this._cbxOrdenPublicacion.IsEnabled = value;
                this._cbxPaisIntSolicitud.IsEnabled = value;
                this._cbxPaisIntSolicitud.IsEnabled = value;
                this._cbxPaisPrioridad.IsEnabled = value;
                this._cbxSector.IsEnabled = value;
                this._cbxSituacion.IsEnabled = value;
                this._cbxTipoMarcaSolicitud.IsEnabled = value;
                this._cbxTipoMarcaDatos.IsEnabled = value;
                this._cbxTipoReproduccion.IsEnabled = value;
                //this._chkFacturacionDatos.IsEnabled = value;
                this._btnIFacturacionDatos.IsEnabled = value;
                //this._chkDescuentoDatos.IsEnabled = value;
                this._btnDescuentoDatos.IsEnabled = value;
                //this._chkCorrespondenciaDatos.IsEnabled = value;
                this._btnCorrespondenciaDatos.IsEnabled = value;
                this._btnOtroDatos.IsEnabled = value;

                this._cbxBoletinConcesion.IsEnabled = value;
                this._cbxBoletinPublicacion.IsEnabled = value;
                this._cbxOrdenPublicacion.IsEnabled = value;

                this._cbxPaisIntDatos.IsEnabled = value;
                this._cbxPaisIntSolicitud.IsEnabled = value;

                #endregion

                #region CheckBox

                this._chkInstruccionesRenovacion.IsEnabled = value;
                this._chkRenovacionTramitente.IsEnabled = value;
                //this._chkConflicto.IsEnabled = value;
                //this._chkEtiquetaSolicitud.IsEnabled = value;
                //this._chkEtiquetaDatos.IsEnabled = value;
                //this._chkOtraInf.IsEnabled = value;
                //this._chkPoder.IsEnabled = value;
                //this._chkPoderYPrioridad.IsEnabled = value;
                //this._chkPrioridad.IsEnabled = value;
                //this._chkReclasificacionNacional.IsEnabled = value;
                //this._chkOtroDatos.IsEnabled = value;
                this._chkEtiquetaDatos.IsEnabled = value;
                this._chkEtiquetaSolicitud.IsEnabled = value;

                #endregion

                #region Botones

                //this._btnAceptar.IsEnabled = value;
                this._btnAnaqua.IsEnabled = value;
                this._btnAnexoFM02.IsEnabled = value;
                this._btnAuditoria.IsEnabled = value;
                this._btnBusquedaDatos.IsEnabled = value;
                this._btnBusquedaSolicitud.IsEnabled = value;
                //this._btnCancelar.IsEnabled = value;
                this._btnCarpeta.IsEnabled = value;
                this._btnCertificados.IsEnabled = value;
                this._btnClaseCompletaSolicitud.IsEnabled = value;
                //this._btnConflicto.IsEnabled = value;
                //this._btnConflictoELI.IsEnabled = value;
                //this._btnConflictoINC.IsEnabled = value;
                this._btnConsultarAsociadoDatos.IsEnabled = value;
                this._btnConsultarAsociadoSolicitud.IsEnabled = value;
                this._btnConsultarInteresadoDatos.IsEnabled = value;
                this._btnConsultarInteresadoSolicitud.IsEnabled = value;
                this._btnDuplicar.IsEnabled = value;
                //this._btnEnviarRecordatorios.IsEnabled = value;
                this._btnFacturacionDatos.IsEnabled = value;
                this._btnFM02.IsEnabled = value;
                this._btnFM02Venen.IsEnabled = value;
                this._btnGenCartel.IsEnabled = value;
                this._btnImprimirEdoCuenta.IsEnabled = value;
                this._btnImprimirEdoCuentaDatos.IsEnabled = value;
                this._btnInfoAdicional.IsEnabled = value;
                this._btnInfoAdicionalSolicitud.IsEnabled = value;
                this._btnInfobol.IsEnabled = value;
                this._btnIngles.IsEnabled = value;
                this._btnIntRenovacion.IsEnabled = value;
                this._btnIrReclasificar.IsEnabled = value;
                this._btnLAnexoFM02.IsEnabled = value;
                this._btnLFM02.IsEnabled = value;
                this._btnLFM02Venen.IsEnabled = value;
                //this._btnLista.IsEnabled = value;
                this._btnNoRegistro.IsEnabled = value;
                this._btnNoSolicitud.IsEnabled = value;
                this._btnOperacionesDatos.IsEnabled = value;
                //this._btnOtraInf.IsEnabled = value;
                //this._btnOtraInfELI.IsEnabled = value;
                //this._btnOtraInfINC.IsEnabled = value;
                //this._btnPoder.IsEnabled = value;
                //this._btnPoderELI.IsEnabled = value;
                //this._btnPoderINC.IsEnabled = value;
                //this._btnPoderYPrioridad.IsEnabled = value;
                //this._btnPoderYPrioridadELI.IsEnabled = value;
                //this._btnPoderYPrioridadINC.IsEnabled = value;
                //this._btnPrioridad.IsEnabled = value;
                //this._btnPrioridadELI.IsEnabled = value;
                //this._btnPrioridadINC.IsEnabled = value;
                //this._btnReclasificacionNacional.IsEnabled = value;
                //this._btnReclasificacionNacionalELI.IsEnabled = value;
                //this._btnReclasificacionNacionalINC.IsEnabled = value;
                this._btnRenovacion.IsEnabled = value;
                this._btnRevisarWeb.IsEnabled = value;
                this._btnSaldo.IsEnabled = value;
                this._btnSaldoDatos.IsEnabled = value;
                this._btnVerExpediente.IsEnabled = value;
                this._btnVerSolicitud.IsEnabled = value;
                this._btnConflictoSolicitud.IsEnabled = value;
                this._btnConflictoDatos.IsEnabled = value;
                this._btnEtiqueta.IsEnabled = value;
                this._btnEtiquetaSolicitud.IsEnabled = value;
                this._btnArchivoDatos.IsEnabled = value;
                this._camposHabilitados = value;

                #endregion

                #region DatePicker

                this._dpkFecha.IsEnabled = value;
                this._dpkFechaPrioridad.IsEnabled = value;
                //this._dpkFechaRequeridaConflicto.IsEnabled = value;
                //this._dpkFechaRequeridaOtraInf.IsEnabled = value;
                //this._dpkFechaRequeridaPoder.IsEnabled = value;
                //this._dpkFechaRequeridaPoderYPrioridad.IsEnabled = value;
                //this._dpkFechaRequeridaPrioridad.IsEnabled = value;
                //this._dpkFechaRequeridaReclasificacionNacional.IsEnabled = value;

                #endregion
            }
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
            get { return this._txtIdAsociadoIntDatosFiltrar.Text; }
            set { this._txtIdAsociadoIntDatosFiltrar.Text = value; }
        }


        public string NombreAsociadoInternacionalFiltrarDatos
        {
            get { return this._txtNombreAsociadoIntDatosFiltrar.Text; }
            set { this._txtNombreAsociadoIntDatosFiltrar.Text = value; }
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


        public object TipoClaseInternacionales
        {
            get { return this._cbxLocalidadSolicitud.DataContext; }
            set { this._cbxLocalidadSolicitud.DataContext = value; }
        }


        public object TipoClaseInternacional
        {
            get { return this._cbxLocalidadSolicitud.SelectedItem; }
            set { this._cbxLocalidadSolicitud.SelectedItem = value; }
        }


        public object TipoClaseInternacionalesDatos
        {
            get { return this._cbxLocalidadDatos.DataContext; }
            set { this._cbxLocalidadDatos.DataContext = value; }
        }


        public object TipoClaseInternacionalDatos
        {
            get { return this._cbxLocalidadDatos.SelectedItem; }
            set { this._cbxLocalidadDatos.SelectedItem = value; }
        }


        public bool EsMarcaNacional
        {
            get { return this._radioNacional.IsChecked.Value; }
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


        public string IdAsociadoDatosFiltrar
        {
            get { return this._txtIdAsociadoDatosFiltrar.Text; }
        }


        public string IdAsociadoDatos
        {
            get { return this._txtIdAsociadoDatos.Text; }
            set { this._txtIdAsociadoDatos.Text = value; }
        }


        public string NombreAsociadoSolicitudFiltrar
        {
            get { return this._txtNombreAsociadoSolicitud.Text; }
        }


        public string NombreAsociadoDatosFiltrar
        {
            get { return this._txtNombreAsociadoDatos.Text; }
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
            set
            {
                this._lstAsociadosSolicitud.SelectedItem = value;
                //this._lstAsociadosSolicitud.ScrollIntoView(value);
            }
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


        public string IdInteresadoSolicitudFiltrar
        {
            get { return this._txtIdInteresadoSolicitudFiltrar.Text; }
        }


        public string IdInteresadoSolicitud
        {
            get { return this._txtIdInteresadoSolicitud.Text; }
            set { this._txtIdInteresadoSolicitud.Text = value; }
        }


        public string IdInteresadoDatosFiltrar
        {
            get { return this._txtIdInteresadoDatosFiltrar.Text; }
        }


        public string IdInteresadoDatos
        {
            set { this._txtIdInteresadoDatos.Text = value; }
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


        public string NombreInteresadoSolicitudFiltrar
        {
            get { return this._txtNombreInteresadoSolicitud.Text; }
        }


        public string NombreInteresadoDatosFiltrar
        {
            get { return this._txtNombreInteresadoDatos.Text; }
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
            set
            {
                this._lstInteresadosSolicitud.SelectedItem = value;
                //this._lstInteresadosSolicitud.ScrollIntoView(value);
            }
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


        public string IdCorresponsalSolicitudFiltrar
        {
            get { return this._txtIdCorresponsalSolicitudFiltrar.Text; }
        }


        public string IdCorresponsalSolicitud
        {
            get { return this._txtIdCorresponsalSolicitud.Text; }
            set { this._txtIdCorresponsalSolicitud.Text = value; }
        }


        public string IdCorresponsalDatosFiltrar
        {
            get { return this._txtIdCorresponsalDatosFiltrar.Text; }
        }


        public string IdCorresponsalDatos
        {
            get { return this._txtIdCorresponsalDatos.Text; }
            set { this._txtIdCorresponsalDatos.Text = value; }
        }


        public string DescripcionCorresponsalSolicitudFiltrar
        {
            get { return this._txtDescripcionCorresponsalSolicitud.Text; }
        }


        public string DescripcionCorresponsalDatosFiltrar
        {
            get { return this._txtDescripcionCorresponsalDatos.Text; }
        }


        public string DescripcionCorresponsalSolicitud
        {
            get { return this._txtCorresponsalSolicitud.Text; }
            set { this._txtCorresponsalSolicitud.Text = value; }
        }


        public string DescripcionCorresponsalDatos
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


        public string IdCartaOrdenSolicitud
        {
            get { return this._txtIdCartaOrden.Text; }
            set { this._txtIdCartaOrden.Text = value; }
        }


        public object CartasOrdenSolicitud
        {
            get { return this._lstCartas.DataContext; }
            set { this._lstCartas.DataContext = value; }
        }


        public object CartaOrdenSolicitud
        {
            get { return this._lstCartas.SelectedItem; }
            set { this._lstCartas.SelectedItem = value; }
        }


        public string IdCartaOrdenSolicitudFiltrar
        {
            get { return this._txtIdCarta.Text; }
            set { this._txtIdCarta.Text = value; }
        }


        public string DescripcionCartaOrdenSolicitudFiltrar
        {
            get { return this._txtDescripcionCarta.Text; }
            set { this._txtDescripcionCarta.Text = value; }
        }


        public string IdCartaOrdenDatos
        {
            get { return this._txtIdCartaOrdenDatos.Text; }
            set { this._txtIdCartaOrdenDatos.Text = value; }
        }

        public object CartasOrdenDatos
        {
            get { return this._lstCartasDatos.DataContext; }
            set { this._lstCartasDatos.DataContext = value; }
        }


        public object CartaOrdenDatos
        {
            get { return this._lstCartasDatos.SelectedItem; }
            set { this._lstCartasDatos.SelectedItem = value; }
        }


        public string IdCartaOrdenDatosFiltrar
        {
            get { return this._txtIdCartaDatos.Text; }
            set { this._txtIdCartaDatos.Text = value; }
        }


        public string DescripcionCartaOrdenDatosFiltrar
        {
            get { return this._txtDescripcionCartaDatos.Text; }
            set { this._txtDescripcionCartaDatos.Text = value; }
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


        public object Sector
        {
            get { return this._cbxSector.SelectedItem; }
            set { this._cbxSector.SelectedItem = value; }
        }


        public object Sectores
        {
            get { return this._cbxSector.DataContext; }
            set { this._cbxSector.DataContext = value; }
        }


        public object StatusWeb
        {
            get { return this._cbxEstadoDatos.SelectedItem; }
            set { this._cbxEstadoDatos.SelectedItem = value; }
        }


        public object StatusWebs
        {
            get { return this._cbxEstadoDatos.DataContext; }
            set { this._cbxEstadoDatos.DataContext = value; }
        }


        public object DetalleDatos
        {
            get { return this._cbxDetalleDatos.SelectedItem; }
            set { this._cbxDetalleDatos.SelectedItem = value; }
        }


        public object DetallesDatos
        {
            get { return this._cbxDetalleDatos.DataContext; }
            set { this._cbxDetalleDatos.DataContext = value; }
        }


        public object TipoReproduccion
        {
            get { return this._cbxTipoReproduccion.SelectedItem; }
            set { this._cbxTipoReproduccion.SelectedItem = value; }
        }


        public object TipoReproducciones
        {
            get { return this._cbxTipoReproduccion.DataContext; }
            set { this._cbxTipoReproduccion.DataContext = value; }
        }


        


        


        public void PintarInfoAdicional()
        {
            this._btnInfoAdicional.Background = Brushes.LightGreen;
            this._btnInfoAdicionalSolicitud.Background = Brushes.LightGreen;
        }


        public void PintarAnaqua()
        {
            this._btnAnaqua.Background = Brushes.LightGreen;
        }


        public void PintarInfoBoles()
        {
            this._btnInfobol.Background = Brushes.LightGreen;
        }


        public void PintarOperaciones()
        {
            this._btnOperacionesDatos.Background = Brushes.LightGreen;
        }


        public void PintarBusquedas()
        {
            this._btnBusquedaDatos.Background = Brushes.LightGreen;
            this._btnBusquedaSolicitud.Background = Brushes.LightGreen;
        }


        public void PintarAuditoria()
        {
            this._btnAuditoria.Background = Brushes.LightGreen;
        }


        public void PintarArchivo()
        {
            this._btnArchivoDatos.Background = Brushes.LightGreen;
        }


        public void PintarIconoBotonCorrespondencia()
        {
            Uri resourceUri = new Uri("Images/ico_correspondencia.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;

            this._btnCorrespondenciaDatos.Background = brush;
        }


        public void PintarIconoBotonFacturacion()
        {
            Uri resourceUri = new Uri("Images/ico_facturacion.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;

            this._btnIFacturacionDatos.Background = brush;
        }


        public void PintarIconoBotonDescuento()
        {
            Uri resourceUri = new Uri("Images/ico_descuento.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;

            this._btnDescuentoDatos.Background = brush;
        }


        public void PintarIconoBotonOtros()
        {
            Uri resourceUri = new Uri("Images/ico_notipificada.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;

            this._btnOtroDatos.Background = brush;
        }


        public void PintarCertificado()
        {
            this._btnCertificados.Background = Brushes.LightGreen;
        }


        public void PintarRenovacion()
        {
            this._btnRenovacion.Background = Brushes.LightGreen;
        }


        public void PintarInstRenovacion()
        {
            this._btnIntRenovacion.Background = Brushes.LightGreen;
        }


        public void PintarFacturacion()
        {
            this._btnFacturacionDatos.Background = Brushes.LightGreen;
        }


        public void PintarLblMarcaOrigen(bool confirmacion)
        {
            if(confirmacion)
            {
                this._lblIdentMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Visible;
                this._lblIdentMarcaOrigenDatos.Visibility = System.Windows.Visibility.Visible;
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
                //color = Brushes.LightBlue;
                color = Brushes.DeepSkyBlue;
            }
            else if (tipo.Equals("3"))
            {
                //color = Brushes.LightYellow;
                color = Brushes.Red;
            }
            else if (tipo.Equals("4"))
            {
                //color = Brushes.Pink;
                color = Brushes.OrangeRed;
            }
            else color = Brushes.Transparent;

            this._txtIdAsociadoDatos.Background = color;
            this._txtIdAsociadoSolicitud.Background = color;
            this._txtAsociadoDatos.Background = color;
            this._txtAsociadoSolicitud.Background = color;
        }


        public void BorrarCeros()
        {
            this._txtClaseInternacional.Text = this._txtClaseInternacional.Text.Equals("0") ? "" : this._txtClaseInternacional.Text;
            this._txtClaseInternacional.Text = this._txtClaseInternacional.Text.Equals("0") ? "" : this._txtClaseInternacional.Text;
            this._txtClaseNacional.Text = this._txtClaseNacional.Text.Equals("0") ? "" : this._txtClaseNacional.Text;
            this._txtClaseNacionalDatos.Text = this._txtClaseNacionalDatos.Text.Equals("0") ? "" : this._txtClaseNacionalDatos.Text;

            this._txtCod.Text = this._txtCod.Text.Equals("0") ? "" : this._txtCod.Text;
            this._txtNum.Text = this._txtNum.Text.Equals("0") ? "" : this._txtNum.Text;
            this._txtCodIntlDatos.Text = this._txtCodIntlDatos.Text.Equals("0") ? "" : this._txtCodIntlDatos.Text;
            this._txtNumIntlDatos.Text = this._txtNumIntlDatos.Text.Equals("0") ? "" : this._txtNumIntlDatos.Text;
        }


        public void DeshabilitarBotonModificar()
        {
            this._btnAceptar.IsEnabled = false;
        }


        public string ClaseInternacionalMarca
        {
            get { return this._txtClaseInternacionalSolicitud.Text; }
        }


        public string ClaseInternacional
        {
            get { return this._txtClaseInternacional.Text; }
        }


        public string ClaseNacional
        {
            get { return this._txtClaseNacional.Text; }
        }


        public string DistingueDatos
        {
            get { return this._txtDistingueDatos.Text; }
            set { this._txtDistingueDatos.Text = value; }
        }


        public string DistingueSolicitud
        {
            get { return this._txtDistingue.Text; }
            set { this._txtDistingue.Text = value; }
        }


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if(opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if(opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void ArchivoNoEncontrado(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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


        public void PintarEtiqueta()
        {
            this._btnEtiquetaSolicitud.Background = Brushes.LightGreen;
            this._btnEtiqueta.Background = Brushes.LightGreen;
        }


        #endregion


        public ConsultarMarca(object marcaSeleccionada, object ventanaPadre)
        {
            
            InitializeComponent();
            this._cargada = false;
            this._asociadosCargados = false;
            this._interesadosCargados = false;
            this._corresponsalesCargados = false;
            this._poderesCargados = false;
            
            this._presentador = new PresentadorConsultarMarca(this, marcaSeleccionada, ventanaPadre);
        }


        public ConsultarMarca(object marcaSeleccionada, object ventanaPadre, bool cargarMarcaOrigen)
        {
            InitializeComponent();

            this._cargada = false;
            this._asociadosCargados = false;
            this._interesadosCargados = false;
            this._corresponsalesCargados = false;
            this._poderesCargados = false;
            this._marcaOrigenCargada = cargarMarcaOrigen;
            this._presentador = new PresentadorConsultarMarca(this, marcaSeleccionada, ventanaPadre);
        }


        public ConsultarMarca(object marcaSeleccionada, string tab)
            : this(marcaSeleccionada, (Page)null)
        {
            this._presentador.CambiarAModificar();

            foreach (TabItem item in this._tbcPestanas.Items)
            {
                if (item.Header.Equals(tab))
                    item.IsSelected = true;
            }
        }


        public ConsultarMarca(object marcaSeleccionada, string tab, object ventanaPadreConsultarMarcas)
            : this(marcaSeleccionada, ventanaPadreConsultarMarcas)
        {
            this._presentador.CambiarAModificar();

            foreach (TabItem item in this._tbcPestanas.Items)
            {
                if (item.Header.Equals(tab))
                    item.IsSelected = true;
            }
        }




        private void _cbxSituacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.DescripcionSituacion();
        }


        private void _cbxDetalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.DescripcionDetalle();
        }


        #region Funciones


        public void ConvertirEnteroMinimoABlanco()
        {
            #region Corresponsal

            if (null != this.CorresponsalDatos)
            {
                if (!this.IdCorresponsalDatos.Equals(""))
                {
                    if ((int.Parse(this.IdCorresponsalDatos) == int.MinValue))
                    {
                        this.IdCorresponsalSolicitud = "";
                        this.IdCorresponsalDatos = "";
                    }
                }

            }

            if (null != this.CorresponsalSolicitud)
            {
                if (!this.IdCorresponsalSolicitud.Equals(""))
                {
                    if (int.Parse(this.IdCorresponsalSolicitud) == int.MinValue)
                    {
                        this.IdCorresponsalSolicitud = "";
                        this.IdCorresponsalDatos = "";
                    }
                }
            }

            if (null != this.PoderDatos)
            {
                if (this.IdPoderDatos == int.MinValue.ToString())
                {
                    this.IdPoderDatos = "";
                    this.IdPoderSolicitud = "";
                }
            }

            if (null != this.PoderSolicitud)
            {
                if (this.IdPoderSolicitud == int.MinValue.ToString())
                {
                    this.IdPoderSolicitud = "";
                    this.IdPoderDatos = "";
                }
            }

            #endregion

            #region Asociados

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


            #region Marca de Origen

            if (null != this.MarcasOrigenSolicitud)
            {
                if (!this.IdMarcaOrigenSolicitud.Equals(""))
                {
                    if((int.Parse(this.IdMarcaOrigenSolicitud).Equals(int.MinValue)))
                    {
                        this.IdMarcaOrigenSolicitud = String.Empty;
                        this.IdMarcaOrigenDatos = String.Empty;
                    }
                }
            }


            if (null != this.MarcasOrigenDatos)
            {
                if (!this.IdMarcaOrigenDatos.Equals(""))
                {
                    if ((int.Parse(this.IdMarcaOrigenDatos).Equals(int.MinValue)))
                    {
                        this.IdMarcaOrigenSolicitud = String.Empty;
                        this.IdMarcaOrigenDatos = String.Empty;
                    }
                }
            }
                

            #endregion

        }


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


        private void mostrarLstMarcaOrigenSolicitud()
        {
            this._lstMarcaOrigenSolicitud.ScrollIntoView(this.IdMarcaOrigenSolicitud);
            this._txtMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstMarcaOrigenSolicitud.IsEnabled = true;
            this._btnConsultarMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdMarcaOrigenFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtIdMarcaOrigenFiltrar.Text = null;
            //this._lblNombreMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Visible;
            //this._txtNombreMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Visible;
        }


        private void mostrarLstMarcaOrigenDatos()
        {
            this._lstMarcaOrigenDatos.ScrollIntoView(this.IdMarcaOrigenDatos);
            this._txtIdMarcaOrigenDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstMarcaOrigenDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstMarcaOrigenDatos.IsEnabled = true;
            this._btnConsultarMarcaOrigenDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdMarcaOrigenDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdMarcaOrigenDatosFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtIdMarcaOrigenDatosFiltrar.Text = null;

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


        private void ocultarLstMarcaOrigenSolicitud()
        {
            this._lstMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdMarcaOrigenFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtMarcaOrigenSolicitud.Visibility = System.Windows.Visibility.Visible;
            
        }


        private void ocultarLstMarcaOrigenDatos()
        {
            this._lstMarcaOrigenDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarMarcaOrigenDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdMarcaOrigenDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdMarcaOrigenDatosFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdMarcaOrigenDatos.Visibility = System.Windows.Visibility.Visible;
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


        private void mostrarLstCorresponsalSolicutud()
        {
            this._lstCorresponsalesSolicitud.ScrollIntoView(this.CorresponsalSolicitud);
            this._txtCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstCorresponsalesSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstCorresponsalesSolicitud.IsEnabled = true;
            this._btnConsultarCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCorresponsalSolicitudFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
        }


        private void ocultarLstCorresponsalSolicutud()
        {
            this._presentador.CambiarCorresponsalSolicitud();
            this._lstCorresponsalesSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCorresponsalSolicitudFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }


        private void mostrarLstAsocaidoDatos()
        {
            this._lstAsociadosDatos.ScrollIntoView(this.AsociadoDatos);
            this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociadosDatos.IsEnabled = true;
            this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoDatosFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
        }


        private void ocultarLstAsociadoDatos()
        {
            this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoDatosFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        }


       private void mostrarLstInteresadoDatos()
        {
            this._lstInteresadosDatos.ScrollIntoView(this.InteresadoDatos);
            this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoDatosFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresadosDatos.IsEnabled = true;
            this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
        }


        private void ocultarLstInteresadoDatos()
        {
            this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoDatosFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        }


        private void mostrarLstCorresponsalDatos()
        {
            this._lstCorresponsalesDatos.ScrollIntoView(this.CorresponsalDatos);
            this._txtCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstCorresponsalesDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstCorresponsalesDatos.IsEnabled = true;
            this._btnConsultarCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCorresponsalDatosFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
        }


        private void ocultarLstCorresponsalDatos()
        {
            this._presentador.CambiarCorresponsalDatos();
            this._lstCorresponsalesDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCorresponsalDatosFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
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

            if (_camposHabilitados)
            {
                if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarModificarMarca,
                    _presentador.ObtenerIdMarca()),
                    "Modificar Marca", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    this._presentador.Modificar();
                    _camposHabilitados = false;
                }
            }
            else
                this._presentador.Modificar();
        }


        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrConsultarMarcas();
        }


        private void _btnInfoAdicional_Click(object sender, RoutedEventArgs e)
        {
            string parametro = "";
            if (((Button)sender).Name.Equals("_btnInfoAdicionalSolicitud"))
                parametro = Recursos.Etiquetas.tabSolicitud;
            else if (((Button)sender).Name.Equals("_btnInfoAdicional"))
                parametro = Recursos.Etiquetas.tabDatos;

            this._presentador.IrInfoAdicional(parametro);
        }


        private void _btnAnaqua_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrAnaqua();
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


        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }


        #endregion


        #region Eventos Solicitudes


        private void _txtAsociadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._asociadosCargados)
            {
                this._presentador.CargarAsociados();
            }

            ocultarLstCorresponsalSolicutud();
            ocultarLstInteresadoSolicutud();
            ocultarLstPoderSolicutud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoSolicitud.IsDefault = true;

            mostrarLstAsociadoSolicitud();

        }


        private void _txtMarcaOrigenSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            String cadenaPrueba = String.Empty;
            cadenaPrueba = "nada";

            this._btnAceptar.IsDefault = false;
            this._btnConsultarMarcaOrigenSolicitud.IsDefault = true;

            mostrarLstMarcaOrigenSolicitud();
            
        }



        private void _lstAsociadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoSolicitud();
            ocultarLstAsociadoSolicitud();
            ocultarLstAsociadoDatos();
            this._btnConsultarAsociadoSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }


        private void _lstMarcaOrigenSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarMarcaOrigenSolicitud();
            ocultarLstMarcaOrigenSolicitud();
            this._btnConsultarMarcaOrigenSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;

        }


        private void _lstMarcaOrigenDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarMarcaOrigenDatos();
            ocultarLstMarcaOrigenDatos();
            this._btnConsultarMarcaOrigenDatos.IsDefault = false;
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


        private void _btnConsultarMarcaOrigen_Click(object sender, RoutedEventArgs e)
        {
            Button botonPresionado = new Button();
            botonPresionado = (Button)sender;
            String filtrarEnTab = botonPresionado.Name;
            this._presentador.BuscarMarcaOrigen(filtrarEnTab);
        }


        private void _txtInteresadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._interesadosCargados)
            {
                this._presentador.CargarInteresados();
            }

            ocultarLstCorresponsalSolicutud();
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
            ocultarLstInteresadoDatos();

            this._btnConsultarInteresadoSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }


        private void _OrdenarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosSolicitud);
        }


        private void _btnConsultarCorresponsalSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarCorresponsal(0);
        }


        private void _txtCorresponsalSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._corresponsalesCargados)
            {
                this._presentador.CargarCorresponsales();
            }

            ocultarLstInteresadoSolicutud();
            ocultarLstAsociadoSolicitud();
            ocultarLstPoderSolicutud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarCorresponsalSolicitud.IsDefault = true;

            mostrarLstCorresponsalSolicutud();
        }


        private void _lstCorresponsalesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarCorresponsalSolicitud();
            ocultarLstCorresponsalSolicutud();
            ocultarLstCorresponsalDatos();

            this._btnConsultarCorresponsalSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }


        private void _OrdenarCorresponsalSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCorresponsalesSolicitud);
        }


        private void _btnClaseCompletaSolicitud_Click(object sender, RoutedEventArgs e)
        {
            if (!this._txtClaseInternacional.Text.Equals(""))
            {
                if (this._txtDistingue.Text.Equals(""))
                {
                    this._presentador.TomarClaseInternacional();
                }
                else
                {
                    if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionTomarClaseInternacional,
                    "Modificar Distingue de marca", MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        this._presentador.TomarClaseInternacional();
                    }
                }
            }
        }


        private void _btnConsultarCarta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarCarta(0);
        }


        private void _btnConsultarCartaDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarCarta(1);
        }


        private void _txtIdCartaOrden_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            String nombreCampoTexto = String.Empty;
            TextBox txtField = new TextBox();
            txtField = (TextBox)sender;
            nombreCampoTexto = txtField.Name;

            this._presentador.CargarCartaOrden(nombreCampoTexto);
            this._btnAceptar.IsDefault = false;
            this._btnConsultarCarta.IsDefault = true;
            
            mostrarLstCartasSolicitud();
        }


        private void _txtIdCartaOrdenDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            String nombreCampoTexto = String.Empty;
            TextBox txtField = new TextBox();
            txtField = (TextBox)sender;
            nombreCampoTexto = txtField.Name;

            this._presentador.CargarCartaOrden(nombreCampoTexto);
            this._btnAceptar.IsDefault = false;
            this._btnConsultarCartaDatos.IsDefault = true;

            mostrarLstCartasDatos();
        }


        public void mostrarLstCartasSolicitud()
        {
            this._btnIrCorresponsal.VerticalAlignment = VerticalAlignment.Top;
            this._lstCartas.ScrollIntoView(this.IdCartaOrdenSolicitud);
            this._txtIdCartaOrden.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdCarta.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCarta.Visibility = System.Windows.Visibility.Visible;
            this._lblDescripcionCarta.Visibility = System.Windows.Visibility.Visible;
            this._txtDescripcionCarta.Visibility = System.Windows.Visibility.Visible;
            this._btnConsultarCarta.Visibility = System.Windows.Visibility.Visible;
            this._btnConsultarCarta.Focus();
            this._lstCartas.Visibility = System.Windows.Visibility.Visible;
         
        }


        public void mostrarLstCartasDatos()
        {
            this._btnIrCorresponsalDatos.VerticalAlignment = VerticalAlignment.Top;
            this._lstCartasDatos.ScrollIntoView(this.IdCartaOrdenDatos);
            this._txtIdCartaOrdenDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdCartaDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdCartaDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblDescripcionCartaDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtDescripcionCartaDatos.Visibility = System.Windows.Visibility.Visible;
            this._btnConsultarCartaDatos.Visibility = System.Windows.Visibility.Visible;
            this._btnConsultarCartaDatos.Focus();
            this._lstCartasDatos.Visibility = System.Windows.Visibility.Visible;
        }


        public void ocultarLstCartasSolicitud()
        {
            this._btnIrCorresponsal.VerticalAlignment = VerticalAlignment.Stretch;
            this._txtIdCartaOrden.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCarta.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCarta.Visibility = System.Windows.Visibility.Collapsed;
            this._lblDescripcionCarta.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDescripcionCarta.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarCarta.Visibility = System.Windows.Visibility.Collapsed;
            this._lstCartas.Visibility = System.Windows.Visibility.Collapsed; 
        }

        public void ocultarLstCartasDatos()
        {
            this._btnIrCorresponsalDatos.VerticalAlignment = VerticalAlignment.Stretch;
            this._txtIdCartaOrdenDatos.Visibility = System.Windows.Visibility.Visible;
            this._lblIdCartaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCartaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lblDescripcionCartaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDescripcionCartaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarCartaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstCartasDatos.Visibility = System.Windows.Visibility.Collapsed;
        }
        


        private void _lstCartas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //this._presentador.CambiarCorresponsalSolicitud();
            int indiceCambio = 0;
            ListView origenEvento = new ListView();
            origenEvento = (ListView)sender;
            String nombreControl = origenEvento.Name;
            if (nombreControl.Equals("_lstCartas"))
                indiceCambio = 0;
            else if (nombreControl.Equals("_lstCartasDatos"))
                indiceCambio = 1;
            

            this._presentador.CambiarCartaOrden(indiceCambio);

            if (indiceCambio == 0)
            {
                ocultarLstCartasSolicitud();
                this._btnConsultarCarta.IsDefault = false;
            }
            else if (indiceCambio == 1)
            {
                ocultarLstCartasDatos();
                this._btnConsultarCartaDatos.IsDefault = false;
            }

            
            this._btnAceptar.IsDefault = true;
        }


        private void _OrdenarCartas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCartas);
        }


        private void _btnIrReclasificarSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }


        private void _btnInglesSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.MostrarDistingueIngles();
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


        public void mostrarLstPoderSolicitud()
        {
            this._txtPoderSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstPoderesSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstPoderesSolicitud.IsEnabled = true;
        }


        private void ocultarLstPoderSolicutud()
        {
            //this._presentador.CambiarPoderSolicitud();
            this._lstPoderesSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtPoderSolicitud.Visibility = System.Windows.Visibility.Visible;
        }


        private void _txtPoderSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._poderesCargados)
                this._presentador.CargarPoderes();

            ocultarLstCorresponsalSolicutud();
            ocultarLstAsociadoSolicitud();
            ocultarLstInteresadoSolicutud();

            mostrarLstPoderSolicitud();
        }


        private void _lstPoderesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarPoderSolicitud();
            ocultarLstPoderSolicutud();
            ocultarLstPoderDatos();
        }


        private void _OrdenarPoderSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesSolicitud);
        }


        private void _cbxTipoMarcaSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxTipoMarcaDatos.SelectedItem = ((ComboBox)sender).SelectedItem;
        }


        private void _btnDuplicar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionDuplicarMarca,
                "Duplicar Marca", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Duplicar();
            }
        }


        #endregion


        #region Eventos Datos


        private void _txtAsociadoDatos_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._asociadosCargados)
            {
                this._presentador.CargarAsociados();
            }

            ocultarLstPoderDatos();
            ocultarLstCorresponsalDatos();
            ocultarLstInteresadoDatos();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoDatos.IsDefault = true;

            mostrarLstAsocaidoDatos();
        }

        private void _txtIdMarcaOrigenDatos_GotFocus(object sender, RoutedEventArgs e)
        {

            this._btnAceptar.IsDefault = false;
            this._btnConsultarMarcaOrigenDatos.IsDefault = true;

            mostrarLstMarcaOrigenDatos();

        }


        private void _lstAsociadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoDatos();
            ocultarLstAsociadoDatos();
            ocultarLstAsociadoSolicitud();

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
        }


        private void _txtInteresadoDatos_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._interesadosCargados)
            {
                this._presentador.CargarInteresados();
            }

            ocultarLstPoderDatos();
            ocultarLstCorresponsalDatos();
            ocultarLstAsociadoDatos();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresadoDatos.IsDefault = true;

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

            this._btnConsultarInteresadoDatos.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }


        private void _OrdenarInteresadoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosDatos);
        }


        private void _btnConsultarCorresponsalDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarCorresponsal(1);
        }


        private void _txtCorresponsalDatos_GotFocus(object sender, RoutedEventArgs e)
        {

            if (!this._corresponsalesCargados)
                this._presentador.CargarCorresponsales();

            ocultarLstPoderDatos();
            ocultarLstAsociadoDatos();
            ocultarLstInteresadoDatos();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarCorresponsalDatos.IsDefault = true;

            mostrarLstCorresponsalDatos();
        }


        private void _lstCorresponsalesDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarCorresponsalDatos();
            ocultarLstCorresponsalSolicutud();
            ocultarLstCorresponsalDatos();

            this._btnConsultarCorresponsalDatos.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }


        private void _OrdenarCorresponsalDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCorresponsalesDatos);
        }


        private void mostrarLstPoderDatos()
        {
            this._txtPoderDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstPoderesDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstPoderesDatos.IsEnabled = true;
        }


        private void ocultarLstPoderDatos()
        {
            //this._presentador.CambiarPoderDatos();
            this._lstPoderesDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._txtPoderDatos.Visibility = System.Windows.Visibility.Visible;
        }


        private void _txtPoderDatos_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._poderesCargados)
                this._presentador.CargarPoderes();

            ocultarLstAsociadoDatos();
            ocultarLstCorresponsalDatos();
            ocultarLstInteresadoDatos();

            mostrarLstPoderDatos();
        }


        private void _lstPoderesDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarPoderDatos();
            ocultarLstPoderSolicutud();
            ocultarLstPoderDatos();
        }


        private void _OrdenarPoderDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesDatos);
        }


        private void _cbxTipoMarcaDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxTipoMarcaSolicitud.SelectedItem = ((ComboBox)sender).SelectedItem;
        }


        private void _btnInfobol_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrInfoBoles();
        }


        private void _btnOperacionesDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrOperaciones();
        }


        /*
         * Evento del boton No. Registro comentado por los momentos pues se debe esperar por modificaciones en la
         * pagina web del sitio SAPI
         * 
         * */
        private void _btnIrExplorador_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.IrSAPI();   **** COMENTADO HASTA RECIBIR NUEVAS INSTRUCCIONES
        }

        
        #endregion



        private void _impresion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }


        private void _btnGenCartel_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GenerarCartel();
        }


        private void _cbxTipoClaseNacional_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void _btnEtiqueta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.MostrarEtiqueta();
        }


        private void _btnRenovacion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrRenovacionDeMarca();
        }


        private void _cbxAgente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CambiarAgente();
            ocultarLstPoderSolicutud();
        }


        private void _btnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerExpediente();
        }


        private void _btnVerSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerSolicitud();

        }


        private void _btnCertificados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerCertificado();
        }


        private void _btnIntRenovacion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerInstruccionesDeRenovacion();
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


        // Nuevo boton de Marca de Origen -- DIRIMO QUINTERO
        private void _btnIrMarcaOrigen_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaMarcaOrigen();
        }



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

            this._lblNombreAsociadoIntDatosFiltrar.Visibility = Visibility.Visible;
            this._lblIdAsociadoIntDatosFiltrar.Visibility = Visibility.Visible;
            this._btnConsultarAsociadoInternacionalDatos.Visibility = Visibility.Visible;
            this._txtIdAsociadoIntDatosFiltrar.Visibility = Visibility.Visible;
            this._txtNombreAsociadoIntDatosFiltrar.Visibility = Visibility.Visible;

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

                this._lblNombreAsociadoIntDatosFiltrar.Visibility = Visibility.Collapsed;
                this._lblIdAsociadoIntDatosFiltrar.Visibility = Visibility.Collapsed;
                this._btnConsultarAsociadoInternacionalDatos.Visibility = Visibility.Collapsed;
                this._txtIdAsociadoIntDatosFiltrar.Visibility = Visibility.Collapsed;
                this._txtNombreAsociadoIntDatosFiltrar.Visibility = Visibility.Collapsed;

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


        private void _cbxLocalidadDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxLocalidadSolicitud.SelectedIndex = _cbxLocalidadDatos.SelectedIndex;
        }


        private void _cbxLocalidadSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxLocalidadDatos.SelectedIndex = _cbxLocalidadSolicitud.SelectedIndex;
        }


        private void _btnIrCorresponsal_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaCorresponsal();
        }

        private void _btnImprimirEdoCuenta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaImprimirEdoCuenta();
        }

        private void _btnFacturacionDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaFacturacionDatos();
        }

        private void _btnSaldo_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CalcularSaldos();

        }
        
        public string SaldoVencidoSolicitud
        {
            set { this._txtSaldoVencido.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value)); }
        }

        public string SaldoVencidoDatos
        {
            set { this._txtSaldoVencidoDatos.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value)); }
        }

        public string SaldoPorVencerSolicitud
        {
            set { this._txtSaldoPorVencer.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value)); }
        }

        public string SaldoPorVencerDatos
        {
            set { this._txtSaldoPorVencerDatos.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value)); }
        }

        public string TotalSolicitud
        {
            set { this._txtTotalDeuda.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value)); }
        }

        public string TotalDatos
        {
            set { this._txtTotalDeudaDatos.Text = _presentador.SetFormatoDouble2(System.Convert.ToDouble(value)); }
        }

        #region Propiedades de Marca de Origen


        public string IdMarcaOrigenSolicitud
        {
            get { return this._txtMarcaOrigenSolicitud.Text; }
            set { this._txtMarcaOrigenSolicitud.Text = value; }
        }

        public string IdMarcaOrigenDatos
        {
            get { return this._txtIdMarcaOrigenDatos.Text; }
            set { this._txtIdMarcaOrigenDatos.Text = value; }
        }



        public string IdMarcaOrigenSolicitudFiltrar
        {
            get { return this._txtIdMarcaOrigenFiltrar.Text; }
            set { this._txtIdMarcaOrigenFiltrar.Text = value; }
        }


        public string IdMarcaOrigenDatosFiltrar
        {
            get { return this._txtIdMarcaOrigenDatosFiltrar.Text; }
            set { this._txtIdMarcaOrigenDatosFiltrar.Text = value; }
        }



        public object MarcaOrigenSolicitud
        {
            get { return this._lstMarcaOrigenSolicitud.DataContext; }
            set { this._lstMarcaOrigenSolicitud.DataContext = value; }

        }


        public object MarcasOrigenSolicitud
        {
            get { return this._lstMarcaOrigenSolicitud.SelectedItem; }
            set
            {
                this._lstMarcaOrigenSolicitud.SelectedItem = value;
            }
        }

        
        public object MarcaOrigenDatos
        {
            get { return this._lstMarcaOrigenDatos.DataContext; }
            set { this._lstMarcaOrigenDatos.DataContext = value; }

        }

        
        public object MarcasOrigenDatos
        {
            get { return this._lstMarcaOrigenDatos.SelectedItem; }
            set
            {
                this._lstMarcaOrigenDatos.SelectedItem = value;
            }
        }



        #endregion


        #region Propiedades para Expediente Traspaso Renovacion

        public string IdExpTraspasoRenovacionDatos
        {
            get { return this._txtExptyr.Text; }
            set { this._txtExptyr.Text = value; }
        }


        public string IdExpTraspasoRenovacionSolicitud
        {
            get { return this._txtIdExpedienteTraspasoRenovacionSolicitud.Text; }
            set { this._txtIdExpedienteTraspasoRenovacionSolicitud.Text = value; }
        }

        #endregion

        private void _btnArchivoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrArchivo();
        }

        private void _btnVerCartaOrden_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCartaOrden();
        }

        private void _btnCorrespondenciaDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarInstruccionDeCorrespondencia();
        }

        private void _btnIFacturacionDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarInstruccionDeFacturacion();
        }

        private void _btnOtroDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaInstruccionesNoTipificadas();
        }

        private void _btnDescuentoDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaInstruccionesDeDescuento();
        }

        private void _btnExptyr_Click(object sender, RoutedEventArgs e)
        {
            String nombreBoton = String.Empty;

            Button botonPresionado = (Button)sender;
            nombreBoton = botonPresionado.Name;
            this._presentador.VerExpedienteTyR(nombreBoton);
        }

        public void PintarBotonExpTyR()
        {
            this._btnExptyrDatos.Background = Brushes.LightGreen;
            this._btnExptyrSolicitud.Background = Brushes.LightGreen;
        }


    }
}
