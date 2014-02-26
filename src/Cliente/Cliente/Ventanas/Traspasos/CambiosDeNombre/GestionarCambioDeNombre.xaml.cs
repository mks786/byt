using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Traspasos.CambiosDeNombre;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.CambiosDeDomicilio;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.CambiosDeNombre;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeNombre
{
    /// <summary>
    /// Interaction logic for CambiosDeNombre.xaml
    /// </summary>
    public partial class GestionarCambioDeNombre : Page, IGestionarCambioDeNombre
    {

        private PresentadorGestionarCambioDeNombre _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        

        #region IConsultarFusion

        public void EsMarcaNacional(bool marcaNacional)
        {
            if (marcaNacional)
            {
                this._radioExtranjero.IsChecked = !marcaNacional;
                this._radioNacional.IsChecked = marcaNacional;
            }
            else
            {
                this._radioExtranjero.IsChecked = !marcaNacional;
                this._radioNacional.IsChecked = marcaNacional;
            }
        }

        public void BorrarCerosInternacional()
        {
            if (this._txtIdMarcaInt.Text.Equals("0"))
                this._txtIdMarcaInt.Text = ""; // cambio el texto del textbox para que no aparezca el "0"
            if (this._txtIdMarcaIntCor.Text.Equals("0"))
                this._txtIdMarcaIntCor.Text = ""; // cambio el texto del textbox para que no aparezca el "0"
        }

        public string TipoClase
        {
            set { this._txtClasificacionInt.Text = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object CambioDeNombre
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

        public object InteresadoAnterior
        {
            get { return this._gridDatosInteresadoAnterior.DataContext; }
            set { this._gridDatosInteresadoAnterior.DataContext = value; }
        }

        public string NombreInteresadoAnterior
        {
            set { this._txtNombreInteresadoAnterior.Text = value; }
        }

        public string IdInteresadoAnterior
        {
            get { return this._txtIdInteresadoAnterior.Text; }
            set { this._txtIdInteresadoAnterior.Text = value; }
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

        public string IdInteresadoActual
        {
            get { return this._txtIdInteresadoActual.Text; }
            set { this._txtIdInteresadoActual.Text = value; }
        }

        public string TipoClaseNacional
        {
            get { return this._txtTipo.Text; }
            set { this._txtTipo.Text = value; }
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
                this._txtAsociado.IsEnabled = value;
                this._txtIdAsociado.IsEnabled = value;
                this._txtClaseInternacional.IsEnabled = value;
                this._txtClaseNacional.IsEnabled = value;
                this._txtExpediente.IsEnabled = value;
                this._txtIdCambioDeNombre.IsEnabled = value;
                this._txtIdMarcaFiltrar.IsEnabled = value;
                this._txtNombreMarca.IsEnabled = value;
                this._txtIdMarca.IsEnabled = value;
                this._txtNombreMarcaFiltrar.IsEnabled = value;
                this._txtNumInscripcion.IsEnabled = value;
                this._txtNumRegistro.IsEnabled = value;
                this._txtTipo.IsEnabled = value;
                this._btnConsultarMarca.IsEnabled = value;
                this._dpkFechaCambioDeNombre.IsEnabled = value;
                this._chkEtiqueta.IsEnabled = value;

                this._btnConsultarInteresadoAnterior.IsEnabled = value;
                this._txtNombreInteresadoAnterior.IsEnabled = value;
                this._txtIdInteresadoAnterior.IsEnabled = value;
                this._txtNombreInteresadoAnteriorFiltrar.IsEnabled = value;
                this._txtIdInteresadoAnteriorFiltrar.IsEnabled = value;
                this._txtPaisInteresadoAnterior.IsEnabled = value;
                this._txtCiudadInteresadoAnterior.IsEnabled = value;
                this._txtDomicilioInteresadoAnterior.IsEnabled = value;

                this._txtNombreInteresadoActual.IsEnabled = value;
                this._txtIdInteresadoActual.IsEnabled = value;
                this._txtNombreInteresadoActualFiltrar.IsEnabled = value;
                this._txtIdInteresadoActualFiltrar.IsEnabled = value;
                this._txtPaisInteresadoActual.IsEnabled = value;
                this._txtCiudadInteresadoActual.IsEnabled = value;
                this._txtDomicilioInteresadoActual.IsEnabled = value;

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

                this._txtOtros.IsEnabled = value;
                this._txtReferencia.IsEnabled = value;
                this._txtAnexo.IsEnabled = value;
                this._txtObservacion.IsEnabled = value;
                this._txtComentario.IsEnabled = value;
                this._chkAsientoEnLibro.IsEnabled = value;
                this._cbxBoletinPublicacion.IsEnabled = value;

                #region Internacional

                this._txtIdMarcaInt.IsEnabled = value;
                this._txtIdMarcaIntCor.IsEnabled = value;
                this._txtPaisInt.IsEnabled = value;
                this._txtClaseInternacionalSolicitud.IsEnabled = value;
                this._txtClasificacionInt.IsEnabled = value;

                #endregion
            }
        }

        public object Boletines
        {
            get { return this._cbxBoletinPublicacion.DataContext; }
            set { this._cbxBoletinPublicacion.DataContext = value; }
        }

        public object Boletin
        {
            get { return this._cbxBoletinPublicacion.SelectedItem; }
            set { this._cbxBoletinPublicacion.SelectedItem = value; }
        }

        public string NombreMarca
        {
            set { this._txtNombreMarca.Text = value; }
        }

        public string IdMarca
        {
            get { return this._txtIdMarca.Text; }
            set { this._txtIdMarca.Text = value; }
        }

        public object MarcasFiltradas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object MarcaFiltrada
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
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

        public GestionarCambioDeNombre(object cambioDeNombre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCambioDeNombre(this, cambioDeNombre);
        }

        /// <summary>
        /// Constructor para la consulta desde operaciones
        /// </summary>
        /// <param name="cambioDeNombre">la cambioDeDomicilio a mostrar</param>
        /// <param name="parametro">parametro que indica la visibilidad de los botones</param>
        public GestionarCambioDeNombre(object cambioDeNombre, object parametro)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCambioDeNombre(this, cambioDeNombre);

            if (parametro.GetType() == typeof(System.Windows.Visibility))
            {
                this._btnModificar.Visibility = (System.Windows.Visibility)parametro;
                this._btnEliminar.Visibility = (System.Windows.Visibility)parametro;
            }
            else if (parametro.GetType() == typeof(ConsultarCambiosDeNombre))
            {
                _presentador._ventanaPadre = parametro;
            }
        }


        /// <summary>
        /// Constructor para la consulta desde operaciones que recibe la ventana padre como parametro
        /// </summary>
        /// <param name="cambioDeNombre">la cambioDeDomicilio a mostrar</param>
        /// <param name="parametro">parametro que indica la visibilidad de los botones</param>
        /// <param name="ventanaPadre">Ventana padre desde donde se llama el Cambio de Nombre</param>
        public GestionarCambioDeNombre(object cambioDeNombre, object parametro, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            

            //this._presentador = new PresentadorGestionarCambioDeNombre(this, cambioDeNombre);
            this._presentador = new PresentadorGestionarCambioDeNombre(this, cambioDeNombre, ventanaPadre);

            if (parametro.GetType() == typeof(System.Windows.Visibility))
            {
                this._btnModificar.Visibility = (System.Windows.Visibility)parametro;
                this._btnEliminar.Visibility = (System.Windows.Visibility)parametro;
            }
            else if (parametro.GetType() == typeof(ConsultarCambiosDeNombre))
            {
                _presentador._ventanaPadre = parametro;
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

            this._txtAsociado.Background = color;
            this._txtIdAsociado.Background = color;

        }


        public void ActivarControlesAlAgregar()
        {
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdCambioDeNombre.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdCambioDeNombre.Visibility = System.Windows.Visibility.Collapsed;
            this._dpkFechaCambioDeNombre.IsEnabled = true;

            this._btnAnexo.Visibility = System.Windows.Visibility.Collapsed;
            this._btnCarpeta.Visibility = System.Windows.Visibility.Collapsed;
            this._btnPlanilla.Visibility = System.Windows.Visibility.Collapsed;
            this._btnPlanillaVan.Visibility = System.Windows.Visibility.Collapsed;
            this._btnPlanillaVienen.Visibility = System.Windows.Visibility.Collapsed;
            this._btnVerPlanilla.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextoBotonRegresar == Recursos.Etiquetas.btnRegresar)
                this._presentador.RegresarVentanaPadre();
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
            if (!this.IdPoder.Equals(""))
            {
                if (int.Parse(this.IdPoder) == int.MinValue)
                    this.IdPoder = "";
            }

            if (!this.IdInteresadoActual.Equals(""))
            {
                if (int.Parse(this.IdInteresadoActual) == int.MinValue)
                    this.IdInteresadoActual = "";
            }

            if (!this.IdInteresadoAnterior.Equals(""))
            {
                if (int.Parse(this.IdInteresadoAnterior) == int.MinValue)
                    this.IdInteresadoAnterior = "";
            }

            if (!this.IdMarca.Equals(""))
            {
                if (int.Parse(this.IdMarca) == int.MinValue)
                    this.IdMarca = "";
            }
        }


        private void _btnIrAsociados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaAsociado();
        }

        #region Eventos Marcas

        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMarcas();
        }

        private void _lstMarcas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarMarca())
            {
                GestionarVisibilidadDatosDeMarca(Visibility.Visible);
                GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

                this._btnConsultarMarca.IsDefault = false;
                this._btnModificar.IsDefault = true;
            }
        }

        private void _OrdenarMarcas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstMarcas);
        }

        private void _txtNombreMarca_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeMarca(Visibility.Collapsed);
            GestionarVisibilidadFiltroMarca(Visibility.Visible);

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

            this._btnConsultarMarca.IsDefault = false;
            this._btnModificar.IsDefault = true;
        }

        private void GestionarVisibilidadFiltroMarca(object value)
        {
            this._lblNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._txtNombreMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdMarca.Visibility = (System.Windows.Visibility)value;
            this._txtIdMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstMarcas.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarMarca.Visibility = (System.Windows.Visibility)value;
            //this._btnIrAsociados.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeMarca(object value)
        {
            this._txtNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._txtIdMarca.Visibility = (System.Windows.Visibility)value;
            this._chkEtiqueta.Visibility = (System.Windows.Visibility)value;
            this._lblNoInscripcion.Visibility = (System.Windows.Visibility)value;
            this._txtNumInscripcion.Visibility = (System.Windows.Visibility)value;
            this._lblNoRegistro.Visibility = (System.Windows.Visibility)value;
            this._txtNumRegistro.Visibility = (System.Windows.Visibility)value;
            this._lblTipo.Visibility = (System.Windows.Visibility)value;
            this._txtTipo.Visibility = (System.Windows.Visibility)value;
            this._lblClaseNacional.Visibility = (System.Windows.Visibility)value;
            this._txtClaseNacional.Visibility = (System.Windows.Visibility)value;
            this._lblClaseInternacional.Visibility = (System.Windows.Visibility)value;
            this._txtClaseInternacional.Visibility = (System.Windows.Visibility)value;
            this._lblAsociado.Visibility = (System.Windows.Visibility)value;
            this._txtAsociado.Visibility = (System.Windows.Visibility)value;
            this._txtIdAsociado.Visibility = (System.Windows.Visibility)value;
            this._btnIrAsociados.Visibility = (System.Windows.Visibility)value;
        }

        private void _txtMarcaFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarMarca.IsDefault = true;
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

                //this._btnConsultarInteresadoAnterior.IsDefault = false;
                this._btnConsultarInteresadoAnterior.IsDefault = true;
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

            //escondo el filtro de Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

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
            this._txtIdInteresadoAnterior.Visibility = (System.Windows.Visibility)value;
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
                    //this._btnConsultarPoder.IsEnabled = false;
                    this._btnConsultarPoder.IsEnabled = true;
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

            //escondo el filtro de interesado Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

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
            this._txtIdInteresadoActual.Visibility = (System.Windows.Visibility)value;
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
                this._txtNombreApoderadoFiltrar.Text = string.Empty;
                this._txtIdApoderadoFiltrar.Text = string.Empty;

                if (this._presentador.VerificarCambioAgente())
                {
                    //this._btnConsultarPoder.IsEnabled = false;
                    this._btnConsultarPoder.IsEnabled = true;
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

            //escondo el filtro de interesado Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

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
            if (((Button)sender).Name.Equals("_btnConsultarMarca"))
                this._presentador.ConsultarMarcas();
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

            //escondo el filtro de Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

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
                this._dpkFechaPoderFiltrar.Text = string.Empty;
                this._txtIdPoderFiltrar.Text = string.Empty;

                if (this._presentador.VerificarCambioPoder())
                {
                    //this._btnConsultarApoderado.IsEnabled = false;
                    //this._btnConsultarInteresadoActual.IsEnabled = false;
                    this._btnConsultarApoderado.IsEnabled = true;
                    this._btnConsultarInteresadoActual.IsEnabled = true;
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

        private void _btnVerPlanilla_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVerPlanilla();
        }

        public void ArchivoNoEncontrado(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void PintarVerPlanilla()
        {
            this._btnVerPlanilla.Background = Brushes.LightGreen;
        }
    }
}
