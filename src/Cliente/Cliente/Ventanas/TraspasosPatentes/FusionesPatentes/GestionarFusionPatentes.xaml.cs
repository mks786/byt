using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.FusionesPatentes;
using Trascend.Bolet.Cliente.Presentadores.TraspasosPatentes.FusionesPatentes;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.FusionesPatentes
{
    /// <summary>
    /// Interaction logic for GestionarFusionPatentes.xaml
    /// </summary>
    public partial class GestionarFusionPatentes : Page, IGestionarFusionPatentes
    {

        private PresentadorGestionarFusionPatentes _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarFusion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public string Expediente
        {
            get { return this._txtExpediente.Text; }
            set { this._txtExpediente.Text = value; }
        }

        public string Tipo
        {
            get { return this._txtTipo.Text; }
            set { this._txtTipo.Text = value; }
        }

        public object Boletines
        {
            get { return this._cbxBoletin.DataContext; }
            set { this._cbxBoletin.DataContext = value; }
        }

        public object Boletin
        {
            get { return this._cbxBoletin.SelectedItem; }
            set { this._cbxBoletin.SelectedItem = value; }
        }

        public object FusionPatente
        {
            get
            {
                return this._gridDatos.DataContext;
            }
            set
            {
                this._gridDatos.DataContext = value;
            }
        }

        public string IdAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdInteresadoEntreFiltrar
        {
            get { return this._txtIdInteresadoEntreFiltrar.Text; }
        }

        public string NombreInteresadoEntreFiltrar
        {
            get { return this._txtNombreInteresadoEntreFiltrar.Text; }
        }

        public string IdInteresadoSobrevivienteFiltrar
        {
            get { return this._txtIdInteresadoSobrevivienteFiltrar.Text; }
        }

        public string NombreInteresadoSobrevivienteFiltrar
        {
            get { return this._txtNombreInteresadoSobrevivienteFiltrar.Text; }
        }

        public string IdAgenteFiltrar
        {
            get { return this._txtIdApoderadoFiltrar.Text; }
        }

        public string NombreAgenteFiltrar
        {
            get { return this._txtNombreApoderadoFiltrar.Text; }
        }

        public string IdPatenteFiltrar
        {
            get { return this._txtIdPatenteFiltrar.Text; }
        }

        public string NombrePatenteFiltrar
        {
            get { return this._txtNombrePatenteFiltrar.Text; }
        }

        public object Patente
        {
            get { return this._gridDatosPatente.DataContext; }
            set { this._gridDatosPatente.DataContext = value; }
        }

        public object InteresadoEntre
        {
            get { return this._gridDatosInteresadoEntre.DataContext; }
            set { this._gridDatosInteresadoEntre.DataContext = value; }
        }

        public string NombreInteresadoEntre
        {
            set { this._txtNombreInteresadoEntre.Text = value; }
        }

        public string IdInteresadoEntre
        {
            get { return this._txtIdInteresadoEntre.Text; }
            set { this._txtIdInteresadoEntre.Text = value; }
        }

        public object InteresadoSobreviviente
        {
            get { return this._gridDatosInteresadoSobreviviente.DataContext; }
            set { this._gridDatosInteresadoSobreviviente.DataContext = value; }
        }

        public string NombreInteresadoSobreviviente
        {
            set { this._txtNombreInteresadoSobreviviente.Text = value; }
        }

        public string IdInteresadoSobreviviente
        {
            get { return this._txtIdInteresadoSobreviviente.Text; }
            set { this._txtIdInteresadoSobreviviente.Text = value; }
        }

        public string Region
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
            this._txtIdFusion.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtAsociado.IsEnabled = value;
                this._txtIdAsociado.IsEnabled = value;
                this._txtExpediente.IsEnabled = value;
                this._txtIdFusion.IsEnabled = value;
                this._txtIdPatenteFiltrar.IsEnabled = value;
                this._txtNombrePatente.IsEnabled = value;
                this._txtIdPatente.IsEnabled = value;
                this._txtNombrePatenteFiltrar.IsEnabled = value;
                this._txtNumInscripcion.IsEnabled = value;
                this._txtNumRegistro.IsEnabled = value;
                this._txtTipo.IsEnabled = value;
                this._btnConsultarPatente.IsEnabled = value;
                this._dpkFechaFusion.IsEnabled = value;

                this._btnConsultarInteresadoEntre.IsEnabled = value;
                this._txtNombreInteresadoEntre.IsEnabled = value;
                this._txtIdInteresadoEntre.IsEnabled = value;
                this._txtNombreInteresadoEntreFiltrar.IsEnabled = value;
                this._txtIdInteresadoEntreFiltrar.IsEnabled = value;
                this._txtPaisInteresadoEntre.IsEnabled = value;
                this._txtCiudadInteresadoEntre.IsEnabled = value;

                this._txtIdPatenteTercero.IsEnabled = value;
                this._txtPaisPatenteTercero.IsEnabled = value;
                this._txtNacionalidadPatenteTercero.IsEnabled = value;
                this._txtEstadoPatenteTercero.IsEnabled = value;
                this._txtDomicilioPatenteTercero.IsEnabled = value;

                this._txtNombreInteresadoSobreviviente.IsEnabled = value;
                this._txtIdInteresadoSobreviviente.IsEnabled = value;
                this._txtNombreInteresadoSobrevivienteFiltrar.IsEnabled = value;
                this._txtIdInteresadoSobrevivienteFiltrar.IsEnabled = value;
                this._txtPaisInteresadoSobreviviente.IsEnabled = value;
                this._txtCiudadInteresadoSobreviviente.IsEnabled = value;

                this._txtNombreApoderado.IsEnabled = value;
                this._txtIdApoderado.IsEnabled = value;
                this._txtNombreApoderadoFiltrar.IsEnabled = value;
                this._txtIdApoderadoFiltrar.IsEnabled = value;

                this._lblIdPoder.IsEnabled = value;
                this._lblFomento.IsEnabled = value;
                this._lblAnexoPoder.IsEnabled = value;
                this._lblBoletinPoder.IsEnabled = value;
                this._lblFechaPoder.IsEnabled = value;
                this._lblFacultadPoder.IsEnabled = value;
                this._txtIdPoder.IsEnabled = value;
                this._cbxBoletin.IsEnabled = value;
                this._txtAnexoPoder.IsEnabled = value;
                this._txtFacultadPoder.IsEnabled = value;
                this._txtFechaPoder.IsEnabled = value;
                this._txtNumPoder.IsEnabled = value;

                this._lblPoderFiltrar.IsEnabled = value;
                this._lblIdPoderFiltrar.IsEnabled = value;
                this._lblIdPoderFiltrar.IsEnabled = value;
                this._txtIdPoderFiltrar.IsEnabled = value;
                this._dpkFechaPoderFiltrar.IsEnabled = value;

            }
        }

        public string NombrePatente
        {
            set { this._txtNombrePatente.Text = value; }
        }

        public string IdPatente
        {
            get { return this._txtIdPatente.Text; }
            set { this._txtIdPatente.Text = value; }
        }

        public object PatentesFiltradas
        {
            get { return this._lstPatentes.DataContext; }
            set { this._lstPatentes.DataContext = value; }
        }

        public object PatenteFiltrada
        {
            get { return this._lstPatentes.SelectedItem; }
            set { this._lstPatentes.SelectedItem = value; }
        }

        public object InteresadosEntreFiltrados
        {
            get { return this._lstInteresadosEntre.DataContext; }
            set { this._lstInteresadosEntre.DataContext = value; }
        }

        public object InteresadoEntreFiltrado
        {
            get { return this._lstInteresadosEntre.SelectedItem; }
            set { this._lstInteresadosEntre.SelectedItem = value; }
        }

        public object InteresadosSobrevivienteFiltrados
        {
            get { return this._lstInteresadosSobreviviente.DataContext; }
            set { this._lstInteresadosSobreviviente.DataContext = value; }
        }

        public object InteresadoSobrevivienteFiltrado
        {
            get { return this._lstInteresadosSobreviviente.SelectedItem; }
            set { this._lstInteresadosSobreviviente.SelectedItem = value; }
        }

        public object AgenteApoderado
        {
            get { return this._gridDatosApoderado.DataContext; }
            set { this._gridDatosApoderado.DataContext = value; }
        }

        public string NombreAgenteApoderado
        {
            set { this._txtNombreApoderado.Text = value; }
        }

        public string IdAgenteApoderado
        {
            get { return this._txtIdApoderado.Text; }
            set { this._txtIdApoderado.Text = value; }
        }

        public string IdAgenteApoderadoFiltrar
        {
            get { return this._txtIdApoderadoFiltrar.Text; }
        }

        public string NombreAgenteApoderadoFiltrar
        {
            get { return this._txtNombreApoderadoFiltrar.Text; }
        }

        public object AgenteApoderadoFiltrados
        {
            get { return this._lstApoderados.DataContext; }
            set { this._lstApoderados.DataContext = value; }
        }

        public object AgenteApoderadoFiltrado
        {
            get { return this._lstApoderados.SelectedItem; }
            set { this._lstApoderados.SelectedItem = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public string TextoBotonRegresar
        {
            get { return this._txbRegresar.Text; }
            set { this._txbRegresar.Text = value; }
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

        public object Poder
        {
            get { return this._gridDatosPoder.DataContext; }
            set { this._gridDatosPoder.DataContext = value; }
        }

        public string IdPoder
        {
            get { return this._txtIdPoder.Text; }
            set { this._txtIdPoder.Text = value; }
        }

        public string IdPoderFiltrar
        {
            get { return this._txtIdPoderFiltrar.Text; }
        }

        public string FechaPoderFiltrar
        {
            get { return this._dpkFechaPoderFiltrar.Text; }
        }

        public object PoderesFiltrados
        {
            get { return this._lstPoderes.DataContext; }
            set { this._lstPoderes.DataContext = value; }
        }

        public object PoderFiltrado
        {
            get { return this._lstPoderes.SelectedItem; }
            set { this._lstPoderes.SelectedItem = value; }
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion

        public GestionarFusionPatentes(object fusion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFusionPatentes(this, fusion);
        }

        /// <summary>
        /// Constructor para la consulta desde operaciones
        /// </summary>
        /// <param name="fusion">la fusion a mostrar</param>
        /// <param name="visibilidad">parametro que indica la visibilidad de los botones</param>
        public GestionarFusionPatentes(object fusion, object visibilidad)
        {
            InitializeComponent();
            this._cargada = false;
            this._btnModificar.Visibility = (System.Windows.Visibility)visibilidad;
            this._btnEliminar.Visibility = (System.Windows.Visibility)visibilidad;
            this._presentador = new PresentadorGestionarFusionPatentes(this, fusion);
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

            this._txtAsociado.Background = color;
            this._txtIdAsociado.Background = color;

        }

        public void ActivarControlesAlAgregar()
        {
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdFusion.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdFusion.Visibility = System.Windows.Visibility.Collapsed;
            this._dpkFechaFusion.IsEnabled = true;

            this._btnAnexo.Visibility = System.Windows.Visibility.Collapsed;
            //this._btnCarpeta.Visibility = System.Windows.Visibility.Collapsed;
            this._btnPlanilla.Visibility = System.Windows.Visibility.Collapsed;            
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {            
            if (this.TextoBotonRegresar == Recursos.Etiquetas.btnRegresar)
                this._presentador.Regresar();
            else if (this.TextoBotonRegresar == Recursos.Etiquetas.btnCancelar)
                this._presentador.Cancelar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarFusion,
                "Eliminar FusionPatente", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
        }

        private void _btnPlanilla_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnPlanillaVan_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnPlanillaVienen_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnAnexo_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnCarpeta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        public void ConvertirEnteroMinimoABlanco()
        {

            if (!this.IdPatente.Equals(""))
            {
                if (int.Parse(this.IdPatente) == int.MinValue)
                    this.IdPatente = "";
            }

            if (!this.IdPoder.Equals(""))
            {
                if (int.Parse(this.IdPoder) == int.MinValue)
                    this.IdPoder = "";
            }

            if (!this.IdInteresadoEntre.Equals(""))
            {
                if (int.Parse(this.IdInteresadoEntre) == int.MinValue)
                    this.IdInteresadoEntre = "";
            }

            if (!this.IdInteresadoSobreviviente.Equals(""))
            {
                if (int.Parse(this.IdInteresadoSobreviviente) == int.MinValue)
                    this.IdInteresadoSobreviviente = "";
            }

        }

        #region Eventos Patentes

        private void _btnConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPatentes();
        }

        private void _lstPatentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPatente())
            {
                GestionarVisibilidadDatosDePatente(Visibility.Visible);
                GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

                this._btnConsultarPatente.IsDefault = false;
                this._btnModificar.IsDefault = true;
            }
        }

        private void _OrdenarPatentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPatentes);
        }

        private void _txtNombrePatente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDePatente(Visibility.Collapsed);
            GestionarVisibilidadFiltroPatente(Visibility.Visible);

            //escondo el filtro de interesado Entre
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

            //escondo el filtro de interesado Sobreviviente
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

            //escondo el filtro de Agente
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            this._btnConsultarPatente.IsDefault = false;
            this._btnModificar.IsDefault = true;
        }

        private void GestionarVisibilidadFiltroPatente(object value)
        {
            this._lblNombrePatente.Visibility = (System.Windows.Visibility)value;
            this._txtNombrePatenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPatente.Visibility = (System.Windows.Visibility)value;
            this._txtIdPatenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPatentes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPatente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDePatente(object value)
        {
            this._txtNombrePatente.Visibility = (System.Windows.Visibility)value;
            this._txtIdPatente.Visibility = (System.Windows.Visibility)value;
            this._lblNoInscripcion.Visibility = (System.Windows.Visibility)value;
            this._txtNumInscripcion.Visibility = (System.Windows.Visibility)value;
            this._lblNoRegistro.Visibility = (System.Windows.Visibility)value;
            this._txtNumRegistro.Visibility = (System.Windows.Visibility)value;
            this._lblTipo.Visibility = (System.Windows.Visibility)value;
            this._txtTipo.Visibility = (System.Windows.Visibility)value;
            this._lblAsociado.Visibility = (System.Windows.Visibility)value;
            this._txtAsociado.Visibility = (System.Windows.Visibility)value;
            this._txtIdAsociado.Visibility = (System.Windows.Visibility)value;
        }

        private void _txtPatenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPatente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        #endregion

        #region Eventos Interesado Entre

        private void _btnConsultarInteresadoEntre_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadosEntre();
        }

        private void _lstInteresadosEntre_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresadoEntre())
            {
                GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
                GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

                this._btnConsultarInteresadoEntre.IsDefault = false;
                this._btnModificar.IsDefault = true;
            }
        }

        private void _OrdenarInteresadosEntre_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosEntre);
        }

        private void _txtInteresadoEntre_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Muestro el filtro de InteresadoSobreviviente
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Collapsed);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Visible);

            //escondo el filtro de Patente
            GestionarVisibilidadDatosDePatente(Visibility.Visible);
            GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

            //escondo el filtro de AgenteApoderado
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //escondo el filtro de InteresadoSobreviviente
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

            
        }

        private void _txtInteresadoEntreFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarInteresadoEntre.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresadoEntre(object value)
        {
            this._lblIdInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoEntreFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoEntreFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresadosEntre.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresadoEntre.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresadoEntre(object value)
        {                        
            this._txtNombreInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtDomicilioPatenteTercero.Visibility = (System.Windows.Visibility)value;
            
            this._txtPaisPatenteTercero.Visibility = (System.Windows.Visibility)value;
            this._txtNacionalidadPatenteTercero.Visibility = (System.Windows.Visibility)value;
            this._txtIdPatenteTercero.Visibility = (System.Windows.Visibility)value;            
            this._txtEstadoPatenteTercero.Visibility = (System.Windows.Visibility)value;
            this._lblY.Visibility = (System.Windows.Visibility)value;
            this._lblEstadoPatenteTercero.Visibility = (System.Windows.Visibility)value;
            this._lblDomicilioPatenteTercero.Visibility = (System.Windows.Visibility)value;
            this._lblPaisPatenteTercero.Visibility = (System.Windows.Visibility)value;
            this._lblNacionalidadPatenteTercero.Visibility = (System.Windows.Visibility)value;
            this._lblNombrePatenteTercero.Visibility = (System.Windows.Visibility)value;
            

        }

        #endregion

        #region Eventos Interesado Sobreviviente

        private void _btnConsultarInteresadoSobreviviente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadosSobreviviente();
        }

        private void _lstInteresadosSobreviviente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresadoSobreviviente())
            {
                GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
                GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

                if (this._presentador.VerificarCambioInteresado())
                {
                    this._btnConsultarPoder.IsEnabled = false;
                    this._btnModificar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarPoder.IsEnabled = true;
                }
            }
        }

        private void _OrdenarInteresadosSobreviviente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosSobreviviente);
        }

        private void _txtInteresadoSobreviviente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Collapsed);

            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Visible);

            //escondo el filtro de interesado Patente
            GestionarVisibilidadDatosDePatente(Visibility.Visible);
            GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

            //escondo el filtro de interesado Entre
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

            //escondo el filtro de Agente
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //escondo el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            
        }

        private void _txtInteresadoSobrevivienteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarInteresadoSobreviviente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresadoSobreviviente(object value)
        {
            this._lblIdInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoSobrevivienteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoSobrevivienteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresadosSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresadoSobreviviente(object value)
        {
            this._txtNombreInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
        }

        public void GestionarBotonConsultarInteresado(bool value)
        {                          
            this._btnConsultarInteresadoSobreviviente.IsEnabled = value;           
        }


        #endregion

        #region Eventos Agente Apoderado

        private void _btnConsultarApoderado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarApoderados();
        }       

        private void _lstApoderados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {          
            if (this._presentador.CambiarApoderado())
            {
                GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
                GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

                if (this._presentador.VerificarCambioAgente())
                {
                    this._btnConsultarPoder.IsEnabled = false;
                    this._btnModificar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarPoder.IsEnabled = true;
                }
            }
        }

        private void _OrdenarApoderados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstApoderados);
        }
        
        private void _txtApoderadoFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarApoderado.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtNombreApoderado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Collapsed);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Visible);

            //escondo el filtro de interesado Patente
            GestionarVisibilidadDatosDePatente(Visibility.Visible);
            GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

            //escondo el filtro de interesado Entre
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

            //escondo el filtro de interesado Sobreviviente
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

            //escondo el filtro de poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            this._btnConsultarApoderado.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroAgenteApoderado(object value)
        {
            this._lblIdApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreApoderadoFiltrar.Visibility = (System.Windows.Visibility)value; 
            this._txtNombreApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstApoderados.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarApoderado.Visibility = (System.Windows.Visibility)value;
            
        }

        private void GestionarVisibilidadDatosDeAgenteApoderado(object value)
        {
            this._txtNombreApoderado.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderado.Visibility = (System.Windows.Visibility)value;
        }

        public void GestionarBotonConsultarApoderado(bool value)
        {                           
            this._btnConsultarApoderado.IsEnabled = value;           
        }

        #endregion

        #region Poderes

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name.Equals("_btnConsultarPatente"))
                this._presentador.ConsultarPatentes();
            else if (((Button)sender).Name.Equals("_btnConsultarInteresadoEntre"))
                this._presentador.ConsultarInteresadosEntre();
            else if (((Button)sender).Name.Equals("_btnConsultarInteresadoSobreviviente"))
                this._presentador.ConsultarInteresadosSobreviviente();
            else if (((Button)sender).Name.Equals("_btnConsultarApoderado"))
                this._presentador.ConsultarApoderados();
            else if (((Button)sender).Name.Equals("_btnConsultarPoder"))            
                this._presentador.ConsultarPoderes();           
        }

        private void _txtPoderFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPoder.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtIdPoder_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            //escondo el filtro de Patente
            GestionarVisibilidadDatosDePatente(Visibility.Visible);
            GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

            //escondo el filtro de Interesado Entre
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

            //escondo el filtro de Interesado Sobreviviente
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

            //escondo el filtro de Agente Apoderado
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //Muestro el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Collapsed);            
            GestionarVisibilidadFiltroPoder(Visibility.Visible);            
           
        }

        private void _OrdenarPoderes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderes);
        }

        private void _lstPoderes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPoder())
            {
                GestionarVisibilidadDatosDePoder(Visibility.Visible);
                GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

                if (this._presentador.VerificarCambioPoder())
                {
                    this._btnConsultarApoderado.IsEnabled = false;
                    this._btnConsultarInteresadoSobreviviente.IsEnabled = false;
                    this._btnModificar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarApoderado.IsEnabled = true;
                    this._btnConsultarInteresadoSobreviviente.IsEnabled = true;
                }
            }
        }       

        public void GestionarBotonConsultarPoder(bool value)
        {                
            this._btnConsultarPoder.IsEnabled = value;          
        }

        private void GestionarVisibilidadFiltroPoder(object value)
        {
            this._lblPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._dpkFechaPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPoderes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPoder.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadDatosDePoder(object value)
        {
            this._lblIdPoder.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoder.Visibility = (System.Windows.Visibility)value;
            this._lblFomento.Visibility = (System.Windows.Visibility)value;
            this._lblFechaPoder.Visibility = (System.Windows.Visibility)value;
            this._lblBoletinPoder.Visibility = (System.Windows.Visibility)value;
            this._lblAnexoPoder.Visibility = (System.Windows.Visibility)value;
            this._lblFacultadPoder.Visibility = (System.Windows.Visibility)value;
            this._txtNumPoder.Visibility = (System.Windows.Visibility)value;
            this._txtFechaPoder.Visibility = (System.Windows.Visibility)value;
            this._txtFacultadPoder.Visibility = (System.Windows.Visibility)value;
            this._txtAnexoPoder.Visibility = (System.Windows.Visibility)value;
            this._cbxBoletin.Visibility = (System.Windows.Visibility)value;
        }

        #endregion
    }
}
