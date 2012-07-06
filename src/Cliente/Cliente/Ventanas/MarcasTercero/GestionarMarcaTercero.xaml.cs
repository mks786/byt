using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Presentadores.MarcasTercero;
using System.Windows;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Media;
using System;

namespace Trascend.Bolet.Cliente.Ventanas.MarcasTercero
{
    /// <summary>
    /// Interaction logic for AgregarMarcasTercer.xaml
    /// </summary>
    public partial class GestionarMarcaTercero : Page, IGestionarMarcaTercero
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorGestionarMarcaTercero _presentador;
        private bool _cargada;
        private bool _agregar = false;

        private bool _asociadosCargados;
        private bool _interesadosCargados;
        private bool _corresponsalesCargados;
        private bool _poderesCargados;
        private bool _byt;


        #region IConsultarMarcaTercero

        public object MarcaTercero
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object MarcasFiltradas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object TiposCbx
        {
            get { return this._cbxTipo.DataContext; }
            set { this._cbxTipo.DataContext = value; }
        }

        public object TipoCbx
        {
            get { return this._cbxTipo.SelectedItem; }
            set { this._cbxTipo.SelectedItem = value; }
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

        public object MarcaByt
        {
            get { return this._lstMarcasB.SelectedItem; }
            set { this._lstMarcasB.SelectedItem = value; }
        }

        public object MarcasByt
        {
            get { return this._lstMarcasB.DataContext; }
            set { this._lstMarcasB.DataContext = value; }
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

        public object Situaciones
        {
            get { return this._cbxSituacion.DataContext; }
            set { this._cbxSituacion.DataContext = value; }
        }

        public object Situacion
        {
            get { return this._cbxSituacion.SelectedItem; }
            set { this._cbxSituacion.SelectedItem = value; }
        }

        public object Estados
        {
            get { return this._cbxEstado.DataContext; }
            set { this._cbxEstado.DataContext = value; }
        }

        public object Estado
        {
            get { return this._cbxEstado.SelectedItem; }
            set { this._cbxEstado.SelectedItem = value; }
        }

        public object MarcaFiltrada
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }

        public object PaisSolicitud
        {
            get { return this._cbxPaisPrioridad.SelectedItem; }
            set { this._cbxPaisPrioridad.SelectedItem = value; }
        }
        
        public CheckBox Byt
        {
            get { return this._chkByt; }
            //set { this._chkByt = value; }
        }

        public CheckBox RevWeb
        {
            get { return this._chkRevWeb; }
            //set { this._chkByt = value; }
        }

        public string IdInteresado
        {
            get { return this._txtIdInteresado.Text; }
            set { this._txtIdInteresado.Text = value; }
        }

        public string IdAsociado
        {
            get { return this._txtIdAsociado.Text; }
            set { this._txtIdAsociado.Text = value; }
        }

        public string IdInternacionalByt
        {
            get { return this._txtClaseInternacionalByt.Text; }
            set { this._txtClaseInternacionalByt.Text = value; }
        }

        public string SituacionDescripcion
        {
            set { this._txtNombreSituacion.Text = value; }
        }

        public string CNacional
        {
            get { return this._txtCNaci.Text; }
            set { this._txtCNaci.Text = value; }
        }

        public string CInternacional
        {
            get { return this._txtCInter.Text; }
            set { this._txtCInter.Text = value; }
        }

        public string IdNacionalByt
        {
            get { return this._txtClaseNacionalByt.Text; }
            set { this._txtClaseNacionalByt.Text = value; }
        }


        public string Caso
        {
            get { return this._txtCaso.Text; }
            set { this._txtCaso.Text = value; }
        }


        public object TipoBaseSolicitud
        {
            get { return this._cbxTipoBase.SelectedItem; }
            set { this._cbxTipoBase.SelectedItem = value; }
        }

        public string IdMarcaFiltrar
        {
            get { return this._txtIdMarcaFiltrar.Text; }
        }

        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
        }

        public object Marca
        {
            get { return this._gridDatosMarca.DataContext; }
            set { this._gridDatosMarca.DataContext = value; }
        }

        public object GridByt
        {
            get { return this._gridByt.DataContext; }
            set { this._gridByt.DataContext = value; }
        }

        public object PaisesSolicitud
        {       
            get { return this._cbxPaisPrioridad.DataContext; }
            set { this._cbxPaisPrioridad.DataContext = value; }
        }

        public object TiposBaseSolicitud
        {
            get { return this._cbxTipoBase.DataContext; }
            set { this._cbxTipoBase.DataContext = value; }
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

        public string ComentarioClienteEspanol
        {
            get { return this._txtComencliEsp.Text; }
            set { this._txtComencliEsp.Text = value; }
        }

        public string ComentarioClienteIngles
        {
            get { return this._txtComencliIng.Text; }
            set { this._txtComencliIng.Text = value; }
        }

        //public string IdNacional
        //{
        //    get { return this._txtClaseNacionalDatos.Text; }
        //    set { this._txtClaseNacionalDatos.Text = value; }
        //}

         public bool BytTipoDeBaseVisible
        {
            set { this._txtTipoBase.IsEnabled = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                #region TextBoxs

                this._txtCNaci.IsEnabled = value;
                this._txtCInter.IsEnabled = value;
                this._txtAsociadoSolicitud.IsEnabled = value;
                this._txtIdInteresado.IsEnabled = value;
                this._txtIdAsociado.IsEnabled = value;

                this._txtNombreMarca.IsEnabled = value;
                this._txtNombreMarcaFiltrar.IsEnabled = value;
                this._txtIdMarcaFiltrar.IsEnabled = value;
                this._btnConsultarMarca.IsEnabled = value;
                this._txtTipoBase.IsEnabled = value;
                this._txtCodigoInscripcion.IsEnabled = value;
                this._txtCodigoRegistro.IsEnabled = value;
                this._txtComencliEsp.IsEnabled = value;
                this._txtComencliIng.IsEnabled = value;
                this._txtClaseInternacionalByt.IsEnabled = value;
                this._txtClaseNacionalByt.IsEnabled = value;
                this._txtDescripcionSolicitud.IsEnabled = value;
                this._txtDistingue.IsEnabled = value;
                this._txtEtiqueta.IsEnabled = value;
                this._txtFechaInscripcion.IsEnabled = value;
                this._txtFechaRegistro.IsEnabled = value;
                this._txtIdAsociadoSolicitud.IsEnabled = value;
                this._txtIdInteresadoSolicitud.IsEnabled = value;
                this._txtInteresadoSolicitud.IsEnabled = value;
                this._txtNombreAsociadoSolicitud.IsEnabled = value;
                this._txtNombreInteresadoSolicitud.IsEnabled = value;
                this._txtOtrosImp.IsEnabled = value;
                this._txtCaso.IsEnabled = value;


                #endregion

                #region ComboBoxs

                this._cbxBoletinConcesion.IsEnabled = value;
                this._cbxBoletinPublicacion.IsEnabled = value;
                this._cbxTipo.IsEnabled = value;
                this._cbxEstado.IsEnabled = value;
                this._cbxOrdenPublicacion.IsEnabled = value;
                this._cbxPaisPrioridad.IsEnabled = value;
                this._cbxSituacion.IsEnabled = value;
                this._cbxTipoBase.IsEnabled = value;
                this._cbxTipoDeCaso.IsEnabled = value;
                this._cbxBoletinConcesion.IsEnabled = value;
                this._cbxBoletinPublicacion.IsEnabled = value;
                this._cbxOrdenPublicacion.IsEnabled = value;

                #endregion

                #region CheckBox

                this._chkByt.IsEnabled = value;
                this._chkRevWeb.IsEnabled = value;
                this._chkEtiquetaSolicitud.IsEnabled = value;
                #endregion

                #region Botones

                //this._btnAceptar.IsEnabled = value;
                this._btnExpediente.IsEnabled = value;
               // this._btnCancelar.IsEnabled = value;
                this._btnAuditoriaDatos.IsEnabled = value;
                this._btnConsultarAsociadoSolicitud.IsEnabled = value;
                this._btnConsultarInteresadoSolicitud.IsEnabled = value;
                this._btnInfoAdicional.IsEnabled = value;
                this._btnInfobol.IsEnabled = value;
                this._btnNoRegistro.IsEnabled = value;
                this._btnNoSolicitud.IsEnabled = value;
                this._lstMarcasB.IsEnabled = value;
                this._btnMas.IsEnabled = value;
                this._btnMenos.IsEnabled = value;
                #endregion


                #region DatePicker

                
                this._dpkFechaPublicacion.IsEnabled = value;
                this._dpkFechaRenovacion.IsEnabled = value;

                #endregion
            }
        }

        public string NombreMarca
        {
            set { this._txtNombreMarca.Text = value; }
            get { return this._txtNombreMarca.Text; }
        }

        public string Anexo
        {
            set { this._txtAnexo.Text = value; }
            get { return this._txtAnexo.Text; }
        }

        public string IdMarcaTercero
        {
            set { this._txtIdSolicitud.Text = value; }
            get { return this._txtIdSolicitud.Text; }
        }
   

        public string IdAsociadoSolicitudFiltrar
        {
            get { return this._txtIdAsociadoSolicitud.Text; }
        }

        //public string IdAsociadoDatosFiltrar
        //{
        //    get { return this._txtIdAsociadoDatos.Text; }
        //}

        public string NombreAsociadoSolicitudFiltrar
        {
            get { return this._txtNombreAsociadoSolicitud.Text; }
        }

        //public string NombreAsociadoDatosFiltrar
        //{
        //    get { return this._txtNombreAsociadoDatos.Text; }
        //}

        public string NombreAsociadoSolicitud
        {
            get { return this._txtAsociadoSolicitud.Text; }
            set { this._txtAsociadoSolicitud.Text = value; }
        }

        public string TipoBaseTxt
        {
            get { return this._txtTipoBase.Text; }
            set { this._txtTipoBase.Text = value; }
        }

        public string FechaPublicacion
        {
            get { return this._dpkFechaPublicacion.Text; }
            set { this._dpkFechaPublicacion.Text = value; }
        }

        public string FechaRenovacion
        {
            get { return this._dpkFechaRenovacion.Text; }
            set { this._dpkFechaRenovacion.Text = value; }
        }

        //public string NombreAsociadoDatos
        //{
        //    get { return this._txtAsociadoDatos.Text; }
        //    set { this._txtAsociadoDatos.Text = value; }
        //}

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

        //public object AsociadosDatos
        //{
        //    get { return this._lstAsociadosDatos.DataContext; }
        //    set { this._lstAsociadosDatos.DataContext = value; }
        //}

        //public object AsociadoDatos
        //{
        //    get { return this._lstAsociadosDatos.SelectedItem; }
        //    set
        //    {
        //        this._lstAsociadosDatos.SelectedItem = value;
        //        //this._lstAsociadosDatos.ScrollIntoView(value);
        //    }
        //}

        public string IdInteresadoSolicitudFiltrar
        {
            get { return this._txtIdInteresadoSolicitud.Text; }
        }

        //public string IdInteresadoDatosFiltrar
        //{
        //    get { return this._txtIdInteresadoDatos.Text; }
        //}

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
            get { return this._txtNombreInteresadoSolicitud.Text; }
        }

        public string NombreInteresadoSolicitud
        {
            get { return this._txtInteresadoSolicitud.Text; }
            set { this._txtInteresadoSolicitud.Text = value; }
        }

        //public string NombreInteresadoDatos
        //{
        //    get { return this._txtInteresadoDatos.Text; }
        //    set { this._txtInteresadoDatos.Text = value; }
        //}

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

        //public object InteresadosDatos
        //{
        //    get { return this._lstInteresadosDatos.DataContext; }
        //    set { this._lstInteresadosDatos.DataContext = value; }
        //}

        //public object InteresadoDatos
        //{
        //    get { return this._lstInteresadosDatos.SelectedItem; }
        //    set
        //    {
        //        this._lstInteresadosDatos.SelectedItem = value;
        //        //this._lstInteresadosDatos.ScrollIntoView(value);
        //    }
        //}

        //public string IdCorresponsalSolicitudFiltrar
        //{
        //    get { return this._txtIdCorresponsalSolicitud.Text; }
        //}

        //public string IdCorresponsalDatosFiltrar
        //{
        //    get { return this._txtIdCorresponsalDatos.Text; }
        //}

        //public string DescripcionCorresponsalSolicitudFiltrar
        //{
        //    get { return this._txtDescripcionCorresponsalSolicitud.Text; }
        //}

        //public string DescripcionCorresponsalDatosFiltrar
        //{
        //    get { return this._txtDescripcionCorresponsalDatos.Text; }
        //}

        //public string DescripcionCorresponsalSolicitud
        //{
        //    get { return this._txtCorresponsalSolicitud.Text; }
        //    set { this._txtCorresponsalSolicitud.Text = value; }
        //}

        //public string DescripcionCorresponsalDatos
        //{
        //    get { return this._txtCorresponsalDatos.Text; }
        //    set { this._txtCorresponsalDatos.Text = value; }
        //}

        //public object CorresponsalesSolicitud
        //{
        //    get { return this._lstCorresponsalesSolicitud.DataContext; }
        //    set { this._lstCorresponsalesSolicitud.DataContext = value; }
        //}

        //public object CorresponsalSolicitud
        //{
        //    get { return this._lstCorresponsalesSolicitud.SelectedItem; }
        //    set { this._lstCorresponsalesSolicitud.SelectedItem = value; }
        //}

        //public object CorresponsalesDatos
        //{
        //    get { return this._lstCorresponsalesDatos.DataContext; }
        //    set { this._lstCorresponsalesDatos.DataContext = value; }
        //}

        //public object CorresponsalDatos
        //{
        //    get { return this._lstCorresponsalesDatos.SelectedItem; }
        //    set { this._lstCorresponsalesDatos.SelectedItem = value; }
        //}

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

        public object TipoDeCaso
        {
            get { return this._cbxTipoDeCaso.SelectedItem; }
            set { this._cbxTipoDeCaso.SelectedItem = value; }
        }

        public object TiposDeCasos
        {
            get { return this._cbxTipoDeCaso.DataContext; }
            set { this._cbxTipoDeCaso.DataContext = value; }
        }

        //public object Sector
        //{
        //    get { return this._cbxSector.SelectedItem; }
        //    set { this._cbxSector.SelectedItem = value; }
        //}

        //public object Sectores
        //{
        //    get { return this._cbxSector.DataContext; }
        //    set { this._cbxSector.DataContext = value; }
        //}

        //public object StatusWeb
        //{
        //    get { return this._cbxEstadoDatos.SelectedItem; }
        //    set { this._cbxEstadoDatos.SelectedItem = value; }
        //}

        //public object StatusWebs
        //{
        //    get { return this._cbxEstadoDatos.DataContext; }
        //    set { this._cbxEstadoDatos.DataContext = value; }
        //}

        //public object TipoReproduccion
        //{
        //    get { return this._cbxTipoReproduccion.SelectedItem; }
        //    set { this._cbxTipoReproduccion.SelectedItem = value; }
        //}

        //public object TipoReproducciones
        //{
        //    get { return this._cbxTipoReproduccion.DataContext; }
        //    set { this._cbxTipoReproduccion.DataContext = value; }
        //}

        public void PintarInfoAdicional()
        {
            this._btnInfoAdicional.Background = Brushes.LightGreen;
        }

        //public void PintarAnaqua()
        //{
        //    this._btnAnaqua.Background = Brushes.LightGreen;
        //}

        public void PintarInfoBoles()
        {
            this._btnInfobol.Background = Brushes.LightGreen;
        }

        //public void PintarOperaciones()
        //{
        //    this._btnOperacionesDatos.Background = Brushes.LightGreen;
        //}

        //public void PintarBusquedas()
        //{
        //    this._btnBusquedaDatos.Background = Brushes.LightGreen;
        //    //this._btnBusquedaSolicitud.Background = Brushes.LightGreen;
        //}

        public void PintarAuditoria()
        {
            this._btnAuditoriaDatos.Background = Brushes.LightGreen;
        }

        public void BorrarCeros()
        {
         //   this._txtClaseInternacional.Text = this._txtClaseInternacional.Text.Equals("0") ? "" : this._txtClaseInternacional.Text;
         //   this._txtClaseInternacional.Text = this._txtClaseInternacional.Text.Equals("0") ? "" : this._txtClaseInternacional.Text;
            this._txtClaseInternacionalByt.Text = this._txtClaseInternacionalByt.Text.Equals("0") ? "" : this._txtClaseInternacionalByt.Text;
            this._txtClaseNacionalByt.Text = this._txtClaseNacionalByt.Text.Equals("0") ? "" : this._txtClaseNacionalByt.Text;
         //   this._txtClaseNacional.Text = this._txtClaseNacional.Text.Equals("0") ? "" : this._txtClaseNacional.Text;
            //this._txtClaseNacionalDatos.Text = this._txtClaseNacionalDatos.Text.Equals("0") ? "" : this._txtClaseNacionalDatos.Text;
        }

        //public string ClaseInternacional
        //{
        //    get { return this._txtClaseInternacional.Text; }
        //}

        //public string ClaseNacional
        //{
        //    get { return this._txtClaseNacional.Text; }
        //}
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


        public GestionarMarcaTercero(object marcaTerceroSeleccionada)
        {
            InitializeComponent();

            this._cargada = false;
            this._asociadosCargados = false;
            this._interesadosCargados = false;
            this._corresponsalesCargados = false;
            this._poderesCargados = false;
            this._byt = false;
            if (null == marcaTerceroSeleccionada)
            {
                this._btnNuevoAnexo.Visibility = System.Windows.Visibility.Collapsed;
                this._btnAuditoriaDatos.Visibility = System.Windows.Visibility.Collapsed;
                this._btnExpediente.Visibility = System.Windows.Visibility.Collapsed;
                this._btnInfoAdicional.Visibility = System.Windows.Visibility.Collapsed;
                this._btnInfobol.Visibility= System.Windows.Visibility.Collapsed;
            }
            this._presentador = new PresentadorGestionarMarcaTercero(this, marcaTerceroSeleccionada);
        }

        public GestionarMarcaTercero(object marcaTerceroSeleccionada, string tab)
            : this(marcaTerceroSeleccionada)
        {
            this._presentador.CambiarAModificar();

            //foreach (TabItem item in this._tbcPestañas.Items)
            //{
            //    if (item.Header.Equals(tab))
            //        item.IsSelected = true;
            //}
        }

        #region Funciones

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name.Equals("_btnConsultarMarca"))
                this._presentador.ConsultarMarcas();
        }      

        private void mostrarLstAsociadoSolicitud()
        {
            this._lstAsociadosSolicitud.ScrollIntoView(this.AsociadoSolicitud);
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociado.Visibility = System.Windows.Visibility.Collapsed;
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
            this._txtIdAsociado.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void mostrarLstInteresadoSolicutud()
        {
            this._lstInteresadosSolicitud.ScrollIntoView(this.InteresadoSolicitud);
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresado.Visibility = System.Windows.Visibility.Collapsed;
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
            this._txtIdInteresado.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        //private void mostrarLstCorresponsalSolicutud()
        //{
        //    this._lstCorresponsalesSolicitud.ScrollIntoView(this.CorresponsalSolicitud);
        //    this._txtCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lstCorresponsalesSolicitud.Visibility = System.Windows.Visibility.Visible;
        //    this._lstCorresponsalesSolicitud.IsEnabled = true;
        //    this._btnConsultarCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
        //    this._txtIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
        //    this._txtDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
        //    this._lblDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
        //}

        //private void ocultarLstCorresponsalSolicutud()
        //{
        //    this._presentador.CambiarCorresponsalSolicitud();
        //    this._lstCorresponsalesSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        //    this._btnConsultarCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtCorresponsalSolicitud.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lblDescripcionCorresponsalSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        //}

        //private void mostrarLstAsocaidoDatos()
        //{
        //    this._lstAsociadosDatos.ScrollIntoView(this.AsociadoDatos);
        //    this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lstAsociadosDatos.IsEnabled = true;
        //    this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
        //}

        //private void ocultarLstAsociadoDatos()
        //{
        //    this._lstAsociadosDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._btnConsultarAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtAsociadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lblNombreAsociadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //}

        //private void mostrarLstInteresadoDatos()
        //{
        //    this._lstInteresadosDatos.ScrollIntoView(this.InteresadoDatos);
        //    this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lstInteresadosDatos.IsEnabled = true;
        //    this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
        //}

        //private void ocultarLstInteresadoDatos()
        //{
        //    this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lblNombreInteresadoDatos.Visibility = System.Windows.Visibility.Collapsed;
        //}

        //private void mostrarLstCorresponsalDatos()
        //{
        //    this._lstCorresponsalesDatos.ScrollIntoView(this.CorresponsalDatos);
        //    this._txtCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lstCorresponsalesDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lstCorresponsalesDatos.IsEnabled = true;
        //    this._btnConsultarCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._txtIdCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._txtDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
        //}

        //private void ocultarLstCorresponsalDatos()
        //{
        //    this._presentador.CambiarCorresponsalDatos();
        //    this._lstCorresponsalesDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._btnConsultarCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtIdCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtCorresponsalDatos.Visibility = System.Windows.Visibility.Visible;
        //    this._lblIdCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
        //    this._lblDescripcionCorresponsalDatos.Visibility = System.Windows.Visibility.Collapsed;
        //}

        #endregion

        #region Eventos generales

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                if (this._presentador.CargarMarcasByt())
                    this._lstMarcasB.Visibility = System.Windows.Visibility.Visible;
                else
                    this._lstMarcasB.Visibility = System.Windows.Visibility.Collapsed;
                
                EstaCargada = true;
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
            else color = Brushes.White;

            this._txtAsociadoSolicitud.Background = color;
            this._txtIdAsociado.Background = color;

        }


        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
            this._btnNuevoAnexo.Visibility = System.Windows.Visibility.Visible;
            this._btnAuditoriaDatos.Visibility = System.Windows.Visibility.Visible;
            this._btnExpediente.Visibility = System.Windows.Visibility.Visible;
            this._btnInfoAdicional.Visibility = System.Windows.Visibility.Visible;
            this._btnInfobol.Visibility = System.Windows.Visibility.Visible;
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (_agregar)
                this._presentador.Cancelar();
            this._presentador.IrConsultarMarcasTercero();
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

        //private void _btnAnaqua_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.IrAnaqua();
        //}

        //private void _btnBusqueda_Click(object sender, RoutedEventArgs e)
        //{
        //    string parametro = "";
        //    if (((Button)sender).Name.Equals("_btnBusquedaSolicitud"))
        //        parametro = Recursos.Etiquetas.tabSolicitud;
        //    else if (((Button)sender).Name.Equals("_btnBusquedaDatos"))
        //        parametro = Recursos.Etiquetas.tabDatos;

        //    this._presentador.IrBusquedas(parametro);
        //}

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

            //ocultarLstCorresponsalSolicutud();
            ocultarLstInteresadoSolicutud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoSolicitud.IsDefault = true;

            mostrarLstAsociadoSolicitud();

        }

        private void _lstAsociadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoSolicitud();
            ocultarLstAsociadoSolicitud();
            //ocultarLstAsociadoDatos();
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

        private void _txtInteresadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._interesadosCargados)
            {
                this._presentador.CargarInteresados();
            }

            //ocultarLstCorresponsalSolicutud();
            ocultarLstAsociadoSolicitud();

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
            //ocultarLstInteresadoDatos();

            this._btnConsultarInteresadoSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _OrdenarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosSolicitud);
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


        private void _txtPoderSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._poderesCargados)
                this._presentador.CargarPoderes();

            //ocultarLstCorresponsalSolicutud();
            ocultarLstAsociadoSolicitud();
            ocultarLstInteresadoSolicutud();

        }




        //private void _cbxTipoMarcaTerceroSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    this._cbxTipoMarcaTerceroDatos.SelectedItem = ((ComboBox)sender).SelectedItem;
        //}

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void _btnDuplicar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionDuplicarMarcaTercero,
                "Duplicar MarcaTercero", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Duplicar();
            }
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
        //    //ocultarLstCorresponsalDatos();
        //    //ocultarLstInteresadoDatos();

        //    this._btnAceptar.IsDefault = false;
        //    //this._btnConsultarAsociadoDatos.IsDefault = true;

        //    //mostrarLstAsocaidoDatos();
        //}

        //private void _lstAsociadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    this._presentador.CambiarAsociadoDatos();
        //    //ocultarLstAsociadoDatos();
        //    ocultarLstAsociadoSolicitud();

        //    //this._btnConsultarAsociadoDatos.IsDefault = false;
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
        //    //ocultarLstCorresponsalDatos();
        //    //ocultarLstAsociadoDatos();

        //    this._btnAceptar.IsDefault = false;
        //    //this._btnConsultarInteresadoDatos.IsDefault = true;

        //    //mostrarLstInteresadoDatos();
        //}

        //private void _btnConsultarInteresadoDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.BuscarInteresado(1);
        //}

        //private void _lstInteresadosDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    this._presentador.CambiarInteresadoDatos();
        //    //ocultarLstInteresadoDatos();
        //    ocultarLstInteresadoSolicutud();

        //    //this._btnConsultarInteresadoDatos.IsDefault = false;
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
        //    //ocultarLstCorresponsalDatos();
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

        //private void _cbxTipoMarcaTerceroDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    this._cbxTipoMarcaTerceroSolicitud.SelectedItem = ((ComboBox)sender).SelectedItem;
        //}

        private void _btnInfobol_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrInfoBoles();
        }

        //private void _btnOperacionesDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.IrOperaciones();
        //}

        private void _btnIrExplorador_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrSAPI();
        }

        #endregion

        #region BYT

        public void MostrarByt()
        {
            _agregar = true;
            this._gridByt.Visibility = System.Windows.Visibility.Visible;
        }

        private void ValidarByt()
        {
            this._chkByt.IsChecked = true;
            this._byt = true;
        }


        #endregion

        #region Marca

        private void _lstMarcas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (this._presentador.CambiarMarca())
            {
                GestionarVisibilidadDatosDeMarca(Visibility.Visible);
                GestionarVisibilidadFiltroMarca(Visibility.Collapsed);
            }

        }

        private void _OrdenarMarcas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstMarcas);
        }

        private void _txtNombreMarca_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((bool)this._chkByt.IsChecked)
            {
                GestionarVisibilidadDatosDeMarca(Visibility.Collapsed);

                GestionarVisibilidadFiltroMarca(Visibility.Visible);
                this._txtClaseInternacionalByt.IsEnabled = false;
                this._txtClaseNacionalByt.IsEnabled = false;
            }
        }

        private void GestionarVisibilidadDatosDeMarca(object value)
        {
            this._txtNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._txtClaseInternacionalByt.Visibility = (System.Windows.Visibility)value;
            this._txtClaseNacionalByt.Visibility = (System.Windows.Visibility)value;
            this.lblClaseNac.Visibility = (System.Windows.Visibility)value;
            this.lblClaseInt.Visibility = (System.Windows.Visibility)value;
            //this._chkEtiqueta.Visibility = (System.Windows.Visibility)value;
         }

        private void GestionarVisibilidadFiltroMarca(object value)
        {
            this._lblNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._txtNombreMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdMarca.Visibility = (System.Windows.Visibility)value;
            this._txtIdMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstMarcas.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarMarca.Visibility = (System.Windows.Visibility)value;
        }

        private void _txtMarcaFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarMarca.IsDefault = true;
        }

        #endregion

        // Funcion Ir a Imprimir, DESCOMENTAR
        private void _impresion_Click(object sender, RoutedEventArgs e)
        {
        //    this._presentador.IrImprimir(((Button)sender).Name);
        }



        private void _chkByt_Click(object sender, RoutedEventArgs e)
        {
            
            this._txtClaseInternacionalByt.IsEnabled = false;
            this._txtClaseNacionalByt.IsEnabled = false;
            this._txtTipoBase.IsEnabled = false;

            if ((bool)!this._chkByt.IsChecked)
            {
                this._txtTipoBase.IsEnabled = true;
                GestionarVisibilidadDatosDeMarca(Visibility.Visible);
                this._txtClaseInternacionalByt.IsEnabled = false;
                this._txtClaseNacionalByt.IsEnabled = false;

                GestionarVisibilidadFiltroMarca(Visibility.Collapsed);
                this._txtClaseInternacionalByt.IsEnabled = true;
                this._txtClaseNacionalByt.IsEnabled = true;

            }
        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {

            if ((this._presentador.AgregarMarcaByt()) && (this._lstMarcasB.Visibility == System.Windows.Visibility.Collapsed))
                this._lstMarcasB.Visibility = System.Windows.Visibility.Visible;
            this._presentador.LimpiarMarcasByt();

        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.DeshabilitarMarcasByt())
            {
                this._lstMarcasB.Visibility = System.Windows.Visibility.Collapsed;
            }

        }


        private void _cbxTipoBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((bool)this._chkByt.IsChecked)
            {
                this._presentador.CargarTipoBaseCombo();
            }
            else
            {
                this._txtTipoBase.IsEnabled = true;
            }
        }

        
        private void _cbxSituacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.DescripcionSituacion();
        }


        public void AgregarMarcaByt()
        {
            throw new NotImplementedException();
        }

        public void DeshabilitarMarcasByt()
        {
            throw new NotImplementedException();
        }


        public void CargarMarcasByt()
        {
            throw new NotImplementedException();
        }

        public void LimpiarMarcasByt()
        {
            throw new NotImplementedException();
        }

        private void _chkRevWeb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnAuditoriaDatos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnArchivo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnExpediente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnNuevoAnexo_Click(object sender, RoutedEventArgs e)
        {
            
            this._presentador.NuevoAnexo();
            this._btnNuevoAnexo.Visibility = System.Windows.Visibility.Collapsed;
            this._lstMarcasB.Visibility = System.Windows.Visibility.Collapsed;
            this.HabilitarCampos = true;
            this._btnAuditoriaDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._btnExpediente.Visibility = System.Windows.Visibility.Collapsed;
            this._btnInfoAdicional.Visibility = System.Windows.Visibility.Collapsed;
            this._btnInfobol.Visibility = System.Windows.Visibility.Collapsed;
        }


    }
}
