using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using System.Windows;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for AgregarAsociado.xaml
    /// </summary>
    public partial class AgregarMarca : Page, IAgregarMarca
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorAgregarMarca _presentador;

        private bool _cargada;
        private bool _asociadosCargados;
        private bool _interesadosCargados;
        private bool _corresponsalesCargados;
        private bool _poderesCargados;

        #region IAgregarMarca

        public object Marca
        {
            get { return this._tbcPestañas.DataContext; }
            set { this._tbcPestañas.DataContext = value; }
        }
       
        public string NumPoderDatos
        {
            get { return this._txtPoderDatos.Text; }
            set { this._txtPoderDatos.Text = value; }
        }

        public string NumPoderSolicitud
        {
            get { return this._txtPoderSolicitud.Text; }
            set { this._txtPoderSolicitud.Text = value; }
        }

        public string IdInternacional
        {
            get { return this._txtClaseInternacionalSolicitud.Text; }
            set { this._txtClaseInternacionalSolicitud.Text = value; }
        }
        
         public string Sapi
        {
            get { return this._txtNumSapi.Text; }
            set { this._txtNumSapi.Text = value; }
        }

        public string IdNacional
        {
            get { return this._txtClaseNacionalDatos.Text; }
            set { this._txtClaseNacionalDatos.Text = value; }
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

        public string SituacionDescripcion
        {
            set { this._txtSituacionDescripcion.Text = value; }
        }

        public string DetalleDescripcion
        {
            set { this._txtDetalleDescripcion.Text = value; }
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
            get { return this._txtIdInteresadoSolicitudFiltrar.Text; }
        }

        public string IdInteresadoSolicitud
        {
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
            get { return this._lstPoderesSolicitud.DataContext; }
            set { this._lstPoderesSolicitud.DataContext = value; }
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

        public string DistingueDatos
        {
            set { this._txtDistingueDatos.Text = value; }
        }

        public string DistingueSolicitud
        {
            set { this._txtDistingue.Text = value; }
        }

        public void BorrarCeros()
        {
            this._txtClaseInternacionalSolicitud.Text = this._txtClaseInternacionalSolicitud.Text.Equals("0") ? "" : this._txtClaseInternacionalSolicitud.Text;
            this._txtClaseInternacionalDatos.Text = this._txtClaseInternacionalDatos.Text.Equals("0") ? "" : this._txtClaseInternacionalDatos.Text;
            this._txtClaseNacional.Text = this._txtClaseNacional.Text.Equals("0") ? "" : this._txtClaseNacional.Text;
            this._txtClaseNacionalDatos.Text = this._txtClaseNacionalDatos.Text.Equals("0") ? "" : this._txtClaseNacionalDatos.Text;
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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


        public AgregarMarca(object marca)
        {
            InitializeComponent();

            this._cargada = false;
            this._asociadosCargados = false;
            this._interesadosCargados = false;
            this._corresponsalesCargados = false;
            this._poderesCargados = false;
            this._presentador = new PresentadorAgregarMarca(this,marca);
        }

        private void _cbxSituacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.DescripcionSituacion();
        }

        #region funciones


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


        public void ConvertirEnteroMinimoABlanco()
        {

            if (!this.NumPoderSolicitud.Equals(""))
            {
                if ((int.Parse(this.NumPoderSolicitud) == int.MinValue))
                {
                    this.NumPoderSolicitud = "";
                    this.NumPoderDatos = "";
                }
            }

            if (!this.NumPoderDatos.Equals(""))
            {
                if ((int.Parse(this.NumPoderDatos) == int.MinValue))
                {
                    this.NumPoderDatos = "";
                    this.NumPoderSolicitud = "";
                }
            }

            #region Corresponsal

            if (null != this.CorresponsalDatos)
            {
                if (!this.IdCorresponsalDatos.Equals(""))
                {
                    if ((int.Parse(this.IdCorresponsalDatos) == int.MinValue) )
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

        }

        
        private void mostrarLstAsociadoSolicitud()
        {
            this._lstAsociadosSolicitud.ScrollIntoView(this.AsociadoSolicitud);
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociadosSolicitud.IsEnabled = true;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoSolicitudFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        
        private void ocultarLstAsociadoSolicitud()
        {
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoSolicitudFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        
        private void mostrarLstInteresadoSolicitud()
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

        
        private void ocultarLstInteresadoSolicitud()
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

        
        private void mostrarLstCorresponsalSolicitud()
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

        
        private void ocultarLstCorresponsalSolicitud()
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

        
        public void mostrarLstPoderSolicitud()
        {
            this._lstPoderesSolicitud.ScrollIntoView(this.PoderSolicitud);
            this._txtPoderSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstPoderesSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstPoderesSolicitud.IsEnabled = true;
        }

        
        public void ocultarLstPoderSolicutud()
        {
            this._presentador.CambiarPoderSolicitud();
            this._lstPoderesSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtPoderSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        
        private void mostrarLstAsocaidoDatos()
        {
            this._lstAsociadosDatos.ScrollIntoView(this.CorresponsalDatos);
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
            this._lstInteresadosDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresadosDatos.IsEnabled = true;
            this._btnConsultarInteresadoDatos.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoDatosFiltrar.Visibility = System.Windows.Visibility.Visible;
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

        
        private void mostrarLstPoderDatos()
        {
            this._lstPoderesDatos.ScrollIntoView(this.PoderDatos);
            this._txtPoderDatos.Visibility = System.Windows.Visibility.Collapsed;
            this._lstPoderesDatos.Visibility = System.Windows.Visibility.Visible;
            this._lstPoderesDatos.IsEnabled = true;
        }

        
        private void ocultarLstPoderDatos()
        {
            this._presentador.CambiarPoderDatos();
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
            if (!this._asociadosCargados)
            {
                this._presentador.CargarAsociados();
            }

            ocultarLstCorresponsalSolicitud();
            ocultarLstInteresadoSolicitud();
            ocultarLstPoderSolicutud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociadoSolicitud.IsDefault = true;

            mostrarLstAsociadoSolicitud();
        }

        
        private void _lstAsociadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoSolicitud();
            ocultarLstAsociadoSolicitud();
            ocultarLstAsociadoDatos();
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

            ocultarLstCorresponsalSolicitud();
            ocultarLstAsociadoSolicitud();
            ocultarLstPoderSolicutud();

            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresadoSolicitud.IsDefault = true;

            mostrarLstInteresadoSolicitud();
        }

        
        private void _btnConsultarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado(0);
        }

        
        private void _lstInteresadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarInteresadoSolicitud();
            ocultarLstInteresadoSolicitud();
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

            ocultarLstInteresadoSolicitud();
            ocultarLstAsociadoSolicitud();
            ocultarLstPoderSolicutud();


            this._btnAceptar.IsDefault = false;
            this._btnConsultarCorresponsalSolicitud.IsDefault = true;

            mostrarLstCorresponsalSolicitud();
        }

        
        private void _lstCorresponsalesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarPoderSolicitud();
            ocultarLstCorresponsalSolicitud();
            ocultarLstCorresponsalDatos();

            this._btnConsultarCorresponsalSolicitud.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        
        private void _OrdenarCorresponsalSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCorresponsalesSolicitud);
        }

        
        private void _txtPoderSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._poderesCargados)
                this._presentador.CargarPoderes();
           
            


            ocultarLstCorresponsalSolicitud();
            ocultarLstAsociadoSolicitud();
            ocultarLstInteresadoSolicitud();

           // mostrarLstPoderSolicitud();
        }

        
        private void _lstPoderesSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarCorresponsalSolicitud();
            ocultarLstPoderSolicutud();
            ocultarLstPoderDatos();
        }

        
        private void _OrdenarPoderSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesSolicitud);
        }

        
        private void _btnClaseCompletaSolicitud_Click(object sender, RoutedEventArgs e)
        {
            if (!this._txtClaseInternacionalSolicitud.Text.Equals(""))
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

        
        private void _cbxTipoMarcaSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._cbxTipoMarcaDatos.SelectedItem = ((ComboBox)sender).SelectedItem;
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
            ocultarLstInteresadoSolicitud();

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
            ocultarLstCorresponsalSolicitud();
            ocultarLstCorresponsalDatos();

            this._btnConsultarCorresponsalDatos.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        
        private void _OrdenarCorresponsalDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCorresponsalesDatos);
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

        
        private void _btnIrExplorador_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrSAPI();
        }

        private void _cbxDetalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.DescripcionDetalle();
        }

        
        #endregion       

        
        private void _cbxTipoClaseNacional_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _cbxAgenteSolicitud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ocultarLstPoderSolicutud();
        }


    }
}