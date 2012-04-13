using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.CambiosDeNombrePatentes;
using Trascend.Bolet.Cliente.Presentadores.TraspasosPatentes.CambiosDeNombrePatentes;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.CambiosDeNombrePatentes
{
    /// <summary>
    /// Interaction logic for CambiosDeNombrePatentes.xaml
    /// </summary>
    public partial class GestionarCambioDeNombrePatentes : Page, IGestionarCambioDeNombrePatentes
    {

        private PresentadorGestionarCambioDeNombrePatentes _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarFusion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object CambioDeNombrePatente
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

        public string IdInteresadoAnteriorFiltrar
        {
            get { return this._txtIdInteresadoAnteriorFiltrar.Text; }
        }

        public string NombreInteresadoAnteriorFiltrar
        {
            get { return this._txtNombreInteresadoAnteriorFiltrar.Text; }
        }

        public string IdInteresadoActualFiltrar
        {
            get { return this._txtIdInteresadoActualFiltrar.Text; }
        }

        public string NombreInteresadoActualFiltrar
        {
            get { return this._txtNombreInteresadoActualFiltrar.Text; }
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

        public object InteresadoAnterior
        {
            get { return this._gridDatosInteresadoAnterior.DataContext; }
            set { this._gridDatosInteresadoAnterior.DataContext = value; }
        }

        public string NombreInteresadoAnterior
        {
            set { this._txtNombreInteresadoAnterior.Text = value; }
        }

        public object InteresadoActual
        {
            get { return this._gridDatosInteresadoActual.DataContext; }
            set { this._gridDatosInteresadoActual.DataContext = value; }
        }

        public string NombreInteresadoActual
        {
            set { this._txtNombreInteresadoActual.Text = value; }
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
            this._txtIdCambioDeNombre.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtOtros.IsEnabled = value;
                this._txtObservacion.IsEnabled = value;
                this._txtComentario.IsEnabled = value;
                this._txtIdCambioDeNombre.IsEnabled = value;
                this._txtIdPatenteFiltrar.IsEnabled = value;
                this._txtNombrePatente.IsEnabled = value;
                this._txtNombrePatenteFiltrar.IsEnabled = value;
                this._txtNumInscripcion.IsEnabled = value;
                this._txtNumRegistro.IsEnabled = value;
                this._txtTipo.IsEnabled = value;
                this._btnConsultarPatente.IsEnabled = value;
                this._dpkFechaCambioDeNombre.IsEnabled = value;
                //this._chkEtiqueta.IsEnabled = value;

                this._btnConsultarInteresadoAnterior.IsEnabled = value;
                this._txtNombreInteresadoAnterior.IsEnabled = value;
                this._txtNombreInteresadoAnteriorFiltrar.IsEnabled = value;
                this._txtIdInteresadoAnteriorFiltrar.IsEnabled = value;
                this._txtPaisInteresadoAnterior.IsEnabled = value;
                this._txtCiudadInteresadoAnterior.IsEnabled = value;
                this._txtDomicilioInteresadoAnterior.IsEnabled = value;

                this._txtNombreInteresadoActual.IsEnabled = value;
                this._txtNombreInteresadoActualFiltrar.IsEnabled = value;
                this._txtIdInteresadoActualFiltrar.IsEnabled = value;
                this._txtPaisInteresadoActual.IsEnabled = value;
                this._txtCiudadInteresadoActual.IsEnabled = value;
                this._txtDomicilioInteresadoActual.IsEnabled = value;

                this._txtNombreApoderado.IsEnabled = value;
                this._txtNombreApoderadoFiltrar.IsEnabled = value;
                this._txtIdApoderadoFiltrar.IsEnabled = value;

                this._lblIdPoder.IsEnabled = value;
                this._lblFomento.IsEnabled = value;
                this._lblAnexoPoder.IsEnabled = value;
                this._lblBoletinPoder.IsEnabled = value;
                this._lblFechaPoder.IsEnabled = value;
                this._lblFacultadPoder.IsEnabled = value;
                this._txtIdPoder.IsEnabled = value;
                this._txtFacultadPoder.IsEnabled = value;
                this._txtAnexoPoder.IsEnabled = value;
                this._txtBoletinPoder.IsEnabled = value;
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

        public object InteresadosAnteriorFiltrados
        {
            get { return this._lstInteresadosAnterior.DataContext; }
            set { this._lstInteresadosAnterior.DataContext = value; }
        }

        public object InteresadoAnteriorFiltrado
        {
            get { return this._lstInteresadosAnterior.SelectedItem; }
            set { this._lstInteresadosAnterior.SelectedItem = value; }
        }

        public object InteresadosActualFiltrados
        {
            get { return this._lstInteresadosActual.DataContext; }
            set { this._lstInteresadosActual.DataContext = value; }
        }

        public object InteresadoActualFiltrado
        {
            get { return this._lstInteresadosActual.SelectedItem; }
            set { this._lstInteresadosActual.SelectedItem = value; }
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

        public GestionarCambioDeNombrePatentes(object fusion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCambioDeNombrePatentes(this, fusion);
        }

        public void ActivarControlesAlAgregar()
        {
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdCambioDeNombre.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCambioDeNombre.Visibility = System.Windows.Visibility.Collapsed;
            this._dpkFechaCambioDeNombre.IsEnabled = false;

            this._btnAnexo.Visibility = System.Windows.Visibility.Collapsed;
            this._btnPlanilla.Visibility = System.Windows.Visibility.Collapsed;
            this._btnPlanillaVan.Visibility = System.Windows.Visibility.Collapsed;
            this._btnPlanillaVienen.Visibility = System.Windows.Visibility.Collapsed;            
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarCambioDeNombre,
                "Eliminar Cambio de nombre", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
            if (!this.IdPoder.Equals(""))
            {
                if (int.Parse(this.IdPoder) == int.MinValue)
                    this.IdPoder = "";
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

            //escondo el filtro de interesado Anterior
            GestionarVisibilidadDatosDeInteresadoAnterior(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoAnterior(Visibility.Collapsed);

            //escondo el filtro de interesado Actual
            GestionarVisibilidadDatosDeInteresadoActual(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoActual(Visibility.Collapsed);

            //escondo el filtro de Agente
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //escondo el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

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
            //this._chkEtiqueta.Visibility = (System.Windows.Visibility)value;
            this._lblNoInscripcion.Visibility = (System.Windows.Visibility)value;
            this._txtNumInscripcion.Visibility = (System.Windows.Visibility)value;
            this._lblNoRegistro.Visibility = (System.Windows.Visibility)value;
            this._txtNumRegistro.Visibility = (System.Windows.Visibility)value;
            this._lblTipo.Visibility = (System.Windows.Visibility)value;
            this._txtTipo.Visibility = (System.Windows.Visibility)value;
        }

        private void _txtPatenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPatente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        #endregion

        #region Eventos Interesado Anterior

        private void _btnConsultarInteresadoAnterior_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadosAnterior();
        }

        private void _lstInteresadosAnterior_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresadoAnterior())
            {
                GestionarVisibilidadDatosDeInteresadoAnterior(Visibility.Visible);
                GestionarVisibilidadFiltroInteresadoAnterior(Visibility.Collapsed);

                this._btnConsultarInteresadoAnterior.IsDefault = false;
                this._btnModificar.IsDefault = true;
            }
        }

        private void _OrdenarInteresadosAnterior_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosAnterior);
        }

        private void _txtInteresadoAnterior_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeInteresadoAnterior(Visibility.Collapsed);

            GestionarVisibilidadFiltroInteresadoAnterior(Visibility.Visible);

            //escondo el filtro de Patente
            GestionarVisibilidadDatosDePatente(Visibility.Visible);
            GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

            //escondo el filtro de AgenteApoderado
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //escondo el filtro de InteresadoActual
            GestionarVisibilidadDatosDeInteresadoActual(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoActual(Visibility.Collapsed);

            //escondo el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            this._btnConsultarInteresadoAnterior.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresadoAnterior(object value)
        {
            this._lblIdInteresadoAnterior.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresadoAnterior.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoAnteriorFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoAnteriorFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresadosAnterior.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresadoAnterior.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresadoAnterior(object value)
        {
            this._txtNombreInteresadoAnterior.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresadoAnterior.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresadoAnterior.Visibility = (System.Windows.Visibility)value;
            this._txtDomicilioInteresadoAnterior.Visibility = (System.Windows.Visibility)value;
        }

        #endregion

        #region Eventos Interesado Actual

        private void _btnConsultarInteresadoActual_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadosActual();
        }

        private void _lstInteresadosActual_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresadoActual())
            {
                GestionarVisibilidadDatosDeInteresadoActual(Visibility.Visible);
                GestionarVisibilidadFiltroInteresadoActual(Visibility.Collapsed);

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

        private void _OrdenarInteresadosActual_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosActual);
        }

        private void _txtInteresadoActual_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeInteresadoActual(Visibility.Collapsed);

            GestionarVisibilidadFiltroInteresadoActual(Visibility.Visible);

            //escondo el filtro de interesado Patente
            GestionarVisibilidadDatosDePatente(Visibility.Visible);
            GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

            //escondo el filtro de interesado Anterior
            GestionarVisibilidadDatosDeInteresadoAnterior(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoAnterior(Visibility.Collapsed);

            //escondo el filtro de Agente
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //escondo el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            this._btnConsultarInteresadoActual.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresadoActual(object value)
        {
            this._lblIdInteresadoActual.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresadoActual.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoActualFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoActualFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresadosActual.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresadoActual.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresadoActual(object value)
        {
            this._txtNombreInteresadoActual.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresadoActual.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresadoActual.Visibility = (System.Windows.Visibility)value;
            this._txtDomicilioInteresadoActual.Visibility = (System.Windows.Visibility)value;
        }

        public void GestionarBotonConsultarInteresado(bool value)
        {
            this._btnConsultarInteresadoActual.IsEnabled = value;
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

            //escondo el filtro de interesado Anterior
            GestionarVisibilidadDatosDeInteresadoAnterior(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoAnterior(Visibility.Collapsed);

            //escondo el filtro de interesado Actual
            GestionarVisibilidadDatosDeInteresadoActual(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoActual(Visibility.Collapsed);

            //escondo el filtro de Poder
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
            else if (((Button)sender).Name.Equals("_btnConsultarInteresadoAnterior"))
                this._presentador.ConsultarInteresadosAnterior();
            else if (((Button)sender).Name.Equals("_btnConsultarInteresadoActual"))
                this._presentador.ConsultarInteresadosActual();
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

            //escondo el filtro de Interesado Anterior
            GestionarVisibilidadDatosDeInteresadoAnterior(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoAnterior(Visibility.Collapsed);

            //escondo el filtro de Interesado Actual
            GestionarVisibilidadDatosDeInteresadoActual(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoActual(Visibility.Collapsed);

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
                    this._btnConsultarInteresadoActual.IsEnabled = false;
                    this._btnModificar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarApoderado.IsEnabled = true;
                    this._btnConsultarInteresadoActual.IsEnabled = true;
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
            this._txtBoletinPoder.Visibility = (System.Windows.Visibility)value;
            this._txtAnexoPoder.Visibility = (System.Windows.Visibility)value;
            this._txtFacultadPoder.Visibility = (System.Windows.Visibility)value;
        }

        #endregion
    }
}
