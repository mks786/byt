using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Traspasos.Licencias;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.CambiosDeNombre;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.Licencias;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.Traspasos.Licencias
{
    /// <summary>
    /// Interaction logic for GestionarFusion.xaml
    /// </summary>
    public partial class GestionarLicencia : Page, IGestionarLicencia
    {

        private PresentadorGestionarLicencia _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IGestionarLicencia

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
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

        public string IdLicencianteFiltrar
        {
            get { return this._txtIdLicencianteFiltrar.Text; }
        }

        public string NombreLicencianteFiltrar
        {
            get { return this._txtNombreLicencianteFiltrar.Text; }
        }

        public string IdLicenciatarioFiltrar
        {
            get { return this._txtIdLicenciatarioFiltrar.Text; }
        }

        public string NombreLicenciatarioFiltrar
        {
            get { return this._txtNombreLicenciatarioFiltrar.Text; }
        }

        public string IdPoderLicenciatarioFiltrar
        {
            get { return this._txtIdPoderLicenciatarioFiltrar.Text; }
        }

        public string FechaPoderLicenciatarioFiltrar
        {
            get { return this._dpkFechaPoderLicenciatarioFiltrar.Text; }
        }

        public string IdPoderLicencianteFiltrar
        {
            get { return this._txtIdPoderLicencianteFiltrar.Text; }
        }

        public string FechaPoderLicencianteFiltrar
        {
            get { return this._dpkFechaPoderLicencianteFiltrar.Text; }
        }

        public string IdApoderadoLicenciatarioFiltrar
        {
            get { return this._txtIdApoderadoLicenciatarioFiltrar.Text; }
        }

        public string NombreApoderadoLicenciatarioFiltrar
        {
            get { return this._txtNombreApoderadoLicenciatarioFiltrar.Text; }
        }

        public string IdApoderadoLicencianteFiltrar
        {
            get { return this._txtIdApoderadoLicencianteFiltrar.Text; }
        }

        public string NombreApoderadoLicencianteFiltrar
        {
            get { return this._txtNombreApoderadoLicencianteFiltrar.Text; }
        }

        public string IdMarcaFiltrar
        {
            get { return this._txtIdMarcaFiltrar.Text; }
        }

        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
        }

        public string Tipo
        {
            get { return this._txtTipo.Text; }
            set { this._txtTipo.Text = value; }
        }

        public string Ubicacion
        {
            get { return this._txtUbicacion.Text; }
            set { this._txtUbicacion.Text = value; }
        }

        public string Expediente
        {
            get { return this._txtExpediente.Text; }
            set { this._txtExpediente.Text = value; }
        }

        public string IdMarca
        {
            get { return this._txtIdMarca.Text; }
            set { this._txtIdMarca.Text = value; }
        }

        public string IdLicenciante
        {
            get { return this._txtIdLicenciante.Text; }
            set { this._txtIdLicenciante.Text = value; }
        }

        public string IdApoderadoLicenciante
        {
            get { return this._txtIdApoderadoLicenciante.Text; }
            set { this._txtIdApoderadoLicenciante.Text = value; }
        }

        public string IdLicenciatario
        {
            get { return this._txtIdLicenciatario.Text; }
            set { this._txtIdLicenciatario.Text = value; }
        }

        public string IdApoderadoLicenciatario
        {
            get { return this._txtIdApoderadoLicenciatario.Text; }
            set { this._txtIdApoderadoLicenciatario.Text = value; }
        }

        public object Marca
        {
            get { return this._gridDatosMarca.DataContext; }
            set { this._gridDatosMarca.DataContext = value; }
        }

        public object InteresadoLicenciante
        {
            get { return this._gridDatosLicenciante.DataContext; }
            set { this._gridDatosLicenciante.DataContext = value; }
        }

        public object InteresadoLicenciatario
        {
            get { return this._gridDatosLicenciatario.DataContext; }
            set { this._gridDatosLicenciatario.DataContext = value; }
        }

        public object ApoderadoLicenciante
        {
            get { return this._gridDatosApoderadoLicenciante.DataContext; }
            set { this._gridDatosApoderadoLicenciante.DataContext = value; }
        }

        public object ApoderadoLicenciatario
        {
            get { return this._gridDatosApoderadoLicenciatario.DataContext; }
            set { this._gridDatosApoderadoLicenciatario.DataContext = value; }
        }

        public object PoderLicenciante
        {
            get { return this._gridDatosPoderLicenciante.DataContext; }
            set { this._gridDatosPoderLicenciante.DataContext = value; }
        }


        public object PoderLicenciatario
        {
            get { return this._gridDatosPoderLicenciatario.DataContext; }
            set { this._gridDatosPoderLicenciatario.DataContext = value; }
        }

        public object MarcasFiltradas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object LicenciantesFiltrados
        {
            get { return this._lstLicenciantes.DataContext; }
            set { this._lstLicenciantes.DataContext = value; }
        }

        public object LicenciatariosFiltrados
        {
            get { return this._lstLicenciatarios.DataContext; }
            set { this._lstLicenciatarios.DataContext = value; }
        }

        public object ApoderadosLicencianteFiltrados
        {
            get { return this._lstApoderadosLicenciante.DataContext; }
            set { this._lstApoderadosLicenciante.DataContext = value; }
        }

        public object ApoderadosLicenciatarioFiltrados
        {
            get { return this._lstApoderadosLicenciatario.DataContext; }
            set { this._lstApoderadosLicenciatario.DataContext = value; }
        }

        public object PoderesLicencianteFiltrados
        {
            get { return this._lstPoderesLicenciante.DataContext; }
            set { this._lstPoderesLicenciante.DataContext = value; }
        }

        public object PoderesLicenciatarioFiltrados
        {
            get { return this._lstPoderesLicenciatario.DataContext; }
            set { this._lstPoderesLicenciatario.DataContext = value; }
        }

        public object Licencia
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object MarcaFiltrada
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }

        public object LicencianteFiltrado
        {
            get { return this._lstLicenciantes.SelectedItem; }
            set { this._lstLicenciantes.SelectedItem = value; }
        }

        public object LicenciatarioFiltrado
        {
            get { return this._lstLicenciatarios.SelectedItem; }
            set { this._lstLicenciatarios.SelectedItem = value; }
        }

        public object ApoderadoLicencianteFiltrado
        {
            get { return this._lstApoderadosLicenciante.SelectedItem; }
            set { this._lstApoderadosLicenciante.SelectedItem = value; }
        }

        public object ApoderadoLicenciatarioFiltrado
        {
            get { return this._lstApoderadosLicenciatario.SelectedItem; }
            set { this._lstApoderadosLicenciatario.SelectedItem = value; }
        }

        public object PoderLicencianteFiltrado
        {
            get { return this._lstPoderesLicenciante.SelectedItem; }
            set { this._lstPoderesLicenciante.SelectedItem = value; }
        }

        public object PoderLicenciatarioFiltrado
        {
            get { return this._lstPoderesLicenciatario.SelectedItem; }
            set { this._lstPoderesLicenciatario.SelectedItem = value; }
        }

        public string Region
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public void FocoPredeterminado()
        {
            this._txtIdLicencia.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtAsociado.IsEnabled = value;
                this._txtClaseInternacional.IsEnabled = value;
                this._txtClaseNacional.IsEnabled = value;
                this._txtExpediente.IsEnabled = value;
                this._txtIdLicencia.IsEnabled = value;
                this._txtIdMarcaFiltrar.IsEnabled = value;
                this._txtNombreMarca.IsEnabled = value;
                this._txtNombreMarcaFiltrar.IsEnabled = value;
                this._txtNumInscripcion.IsEnabled = value;
                this._txtNumRegistro.IsEnabled = value;
                this._chkEtiqueta.IsEnabled = value;
                this._txtTipo.IsEnabled = value;
                this._txtUbicacion.IsEnabled = value;
                this._btnConsultarMarca.IsEnabled = value;

                this._txtNombreLicenciante.IsEnabled = value;
                this._txtPaisLicenciante.IsEnabled = value;
                this._txtNacionalidadLicenciante.IsEnabled = value;
                this._txtIdLicencianteFiltrar.IsEnabled = value;
                this._txtNombreLicencianteFiltrar.IsEnabled = value;
                this._txtNombreApoderadoLicenciante.IsEnabled = value;
                this._txtNombreApoderadoLicencianteFiltrar.IsEnabled = value;
                this._txtIdApoderadoLicencianteFiltrar.IsEnabled = value;
                this._txtIdPoderLicenciante.IsEnabled = value;
                this._txtIdPoderLicencianteFiltrar.IsEnabled = value;
                this._txtAnexoPoderLicenciante.IsEnabled = value;
                this._txtBoletinPoderLicenciante.IsEnabled = value;
                this._txtFacultadPoderLicenciante.IsEnabled = value;
                this._txtNumPoderLicenciante.IsEnabled = value;
                this._txtFechaPoderLicenciante.IsEnabled = value;
                this._dpkFechaLicencia.IsEnabled = value;

                this._txtIdLicenciatario.IsEnabled = value;
                this._txtIdLicenciante.IsEnabled = value;
                this._txtIdApoderadoLicenciatario.IsEnabled = value;
                this._txtIdApoderadoLicenciante.IsEnabled = value;
                this._txtIdMarca.IsEnabled = value;

                this._txtNombreLicenciatario.IsEnabled = value;
                this._txtIdLicenciatarioFiltrar.IsEnabled = value;
                this._txtNombreLicenciatarioFiltrar.IsEnabled = value;
                this._txtNombreApoderadoLicenciatario.IsEnabled = value;
                this._txtIdApoderadoLicenciatarioFiltrar.IsEnabled = value;
                this._txtNombreApoderadoLicenciatarioFiltrar.IsEnabled = value;
                this._txtIdPoderLicenciatario.IsEnabled = value;
                this._txtIdPoderLicenciatarioFiltrar.IsEnabled = value;

                this._txtAnexoPoderLicenciatario.IsEnabled = value;
                this._txtBoletinPoderLicenciatario.IsEnabled = value;
                this._txtFacultadPoderLicenciatario.IsEnabled = value;
                this._txtNumPoderLicenciatario.IsEnabled = value;
                this._txtFechaPoderLicenciatario.IsEnabled = value;
                this._txtPaisLicenciatario.IsEnabled = value;
                this._txtNacionalidadLicenciatario.IsEnabled = value;

                this._txtObservacionLicencia.IsEnabled = value;
                this._txtOtrosLicencia.IsEnabled = value;
                this._txtReferenciaLicencia.IsEnabled = value;
                this._txtAnexoLicencia.IsEnabled = value;
                this._txtComentarioLicencia.IsEnabled = value;
                this._chkAsientoEnLibro.IsEnabled = value;
                this._cbxBoletin.IsEnabled = value;

                #region Internacional

                this._txtIdMarcaInt.IsEnabled = value;
                this._txtIdMarcaIntCor.IsEnabled = value;
                this._txtPaisInt.IsEnabled = value;
                this._txtClaseInternacionalSolicitud.IsEnabled = value;
                this._txtClasificacionInt.IsEnabled = value;

                #endregion
            }
        }

        public string NombreMarca
        {
            set { this._txtNombreMarca.Text = value; }
        }

        public string NombreLicenciante
        {
            set { this._txtNombreLicenciante.Text = value; }
        }

        public string NombreApoderadoLicenciante
        {
            set { this._txtNombreApoderadoLicenciante.Text = value; }
        }

        public string NombreApoderadoLicenciatario
        {
            set { this._txtNombreApoderadoLicenciatario.Text = value; }
        }

        public string IdPoderLicenciante
        {
            get { return this._txtIdPoderLicenciante.Text; }
            set { this._txtIdPoderLicenciante.Text = value; }
        }

        public string IdPoderLicenciatario
        {
            get { return this._txtIdPoderLicenciatario.Text; }
            set { this._txtIdPoderLicenciatario.Text = value; }
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

        public string NombreLicenciatario
        {
            set { this._txtNombreLicenciatario.Text = value; }
        }

        public string PaisLicenciante
        {
            set { this._txtPaisLicenciante.Text = value; }
        }

        public string PaisLicenciatario
        {
            set { this._txtPaisLicenciatario.Text = value; }
        }

        public string NacionalidadLicenciante
        {
            set { this._txtNacionalidadLicenciante.Text = value; }
        }

        public string NacionalidadLicenciatario
        {
            set { this._txtNacionalidadLicenciatario.Text = value; }
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

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

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

        #endregion

        public GestionarLicencia(object Licencia)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarLicencia(this, Licencia);

        }

        /// <summary>
        /// Constructor para la consulta desde operaciones
        /// </summary>
        /// <param name="Licencia">la Licencia a mostrar</param>
        /// <param name="visibilidad">parametro que indica la visibilidad de los botones</param>
        public GestionarLicencia(object Licencia, object parametro)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarLicencia(this, Licencia);
            if (parametro.GetType() == typeof(System.Windows.Visibility))
            {
                this._btnModificar.Visibility = (System.Windows.Visibility)parametro;
                this._btnEliminar.Visibility = (System.Windows.Visibility)parametro;
            }
            else if (parametro.GetType() == typeof(ConsultarLicencias))
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

        }


        public void ActivarControlesAlAgregar()
        {
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdLicencia.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdLicencia.Visibility = System.Windows.Visibility.Collapsed;
            this._dpkFechaLicencia.IsEnabled = true;
            this._btnAnexo.Visibility = System.Windows.Visibility.Collapsed;
            this._btnCarpeta.Visibility = System.Windows.Visibility.Collapsed;
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
                this._presentador.RegresarVentanaPadre();
            else if (this.TextoBotonRegresar == Recursos.Etiquetas.btnCancelar)
                this._presentador.Cancelar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarLicencia,
                "Eliminar Licencia", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name.Equals("_btnConsultarMarca"))
                this._presentador.ConsultarMarcas();
            else if (((Button)sender).Name.Equals("_btnConsultarLicenciante"))
                this._presentador.ConsultarLicenciantes();
            else if (((Button)sender).Name.Equals("_btnConsultarApoderadoLicenciante"))
                this._presentador.ConsultarApoderadosLicenciante();
            else if (((Button)sender).Name.Equals("_btnConsultarPoderLicenciante"))
                this._presentador.ConsultarPoderesLicenciante();
            else if (((Button)sender).Name.Equals("_btnConsultarLicenciatario"))
                this._presentador.ConsultarLicenciatarios();
            else if (((Button)sender).Name.Equals("_btnConsultarApoderadoLicenciatario"))
                this._presentador.ConsultarApoderadosLicenciatario();
            else if (((Button)sender).Name.Equals("_btnConsultarPoderLicenciatario"))
                this._presentador.ConsultarPoderesLicenciatario();
        }

        private void _btnPlanillaVienen_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnPlanillaVan_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnPlanilla_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnCarpeta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnAnexo_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        public void ConvertirEnteroMinimoABlanco(string tipo)
        {
            if (tipo.Equals("Licenciante"))
            {
                if (!this.IdPoderLicenciante.Equals(""))
                {
                    if (int.Parse(this.IdPoderLicenciante) == int.MinValue)
                        this.IdPoderLicenciante = "";
                }
                if (!this.IdLicenciante.Equals(""))
                {
                    if (int.Parse(this.IdLicenciante) == int.MinValue)
                        this.IdLicenciante = "";
                }
            }
            if (tipo.Equals("Licenciatario"))
            {
                if (!this.IdPoderLicenciatario.Equals(""))
                {
                    if (int.Parse(this.IdPoderLicenciatario) == int.MinValue)
                        this.IdPoderLicenciatario = "";
                }
                if (!this.IdLicenciatario.Equals(""))
                {
                    if (int.Parse(this.IdLicenciatario) == int.MinValue)
                        this.IdLicenciatario = "";
                }
            }
            if (tipo.Equals("Marca"))
            {
                if (!this.IdMarca.Equals(""))
                {
                    if (int.Parse(this.IdMarca) == int.MinValue)
                        this.IdMarca = "";
                }

            }
        }

        public void GestionarBotonConsultarInteresados(string tipo, bool value)
        {
            if (tipo.Equals("Licenciante"))
            {
                this._btnConsultarLicenciante.IsEnabled = value;
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._btnConsultarLicenciatario.IsEnabled = value;
            }
        }

        public void GestionarBotonConsultarApoderados(string tipo, bool value)
        {
            if (tipo.Equals("Licenciante"))
            {
                this._btnConsultarApoderadoLicenciante.IsEnabled = value;
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._btnConsultarApoderadoLicenciatario.IsEnabled = value;
            }
        }

        public void GestionarBotonConsultarPoderes(string tipo, bool value)
        {
            if (tipo.Equals("Licenciante"))
            {
                this._btnConsultarPoderLicenciante.IsEnabled = value;
            }
            else if (tipo.Equals("Licenciatario"))
            {
                this._btnConsultarPoderLicenciatario.IsEnabled = value;
            }
        }


        private void _btnIrAsociados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaAsociado();
        }

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
            GestionarVisibilidadDatosDeMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciante(Visibility.Visible);
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
        }

        private void GestionarVisibilidadFiltroMarca(object value)
        {
            this._lblNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._txtNombreMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdMarca.Visibility = (System.Windows.Visibility)value;
            this._txtIdMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstMarcas.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarMarca.Visibility = (System.Windows.Visibility)value;
            this._btnIrAsociados.Visibility = (System.Windows.Visibility)value;
        }

        private void _txtMarcaFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarMarca.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        #endregion

        #region Licenciante

        private void _lstLicenciantes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (this._presentador.CambiarLicenciante())
            {
                GestionarVisibilidadDatosDeLicenciante(Visibility.Visible);
                GestionarVisibilidadFiltroLicenciante(Visibility.Collapsed);

                if (this._presentador.VerificarCambioInteresado("Licenciante"))
                {
                    this._btnConsultarPoderLicenciante.IsEnabled = false;
                }
                else
                {
                    this._btnConsultarPoderLicenciante.IsEnabled = false;
                }
            }

        }

        private void _lstPoderesLicenciante_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPoderLicenciante())
            {
                GestionarVisibilidadDatosDePoderLicenciante(Visibility.Visible);
                GestionarVisibilidadFiltroPoderLicenciante(Visibility.Collapsed);

                if (this._presentador.VerificarCambioPoder("Licenciante"))
                {
                    this._btnConsultarApoderadoLicenciante.IsEnabled = false;
                    this._btnConsultarLicenciante.IsEnabled = false;
                }
                else
                {
                    this._btnConsultarApoderadoLicenciante.IsEnabled = true;
                    this._btnConsultarLicenciante.IsEnabled = true;
                }
            }
        }

        private void _lstApoderadosLicenciante_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarApoderadoLicenciante())
            {
                GestionarVisibilidadDatosDeApoderadoLicenciante(Visibility.Visible);
                GestionarVisibilidadFiltroApoderadoLicenciante(Visibility.Collapsed);

                if (this._presentador.VerificarCambioAgente("Licenciante"))
                {
                    this._btnConsultarPoderLicenciante.IsEnabled = false;
                }
                else
                {
                    this._btnConsultarPoderLicenciante.IsEnabled = true;
                }
            }
        }

        private void _OrdenarLicenciantes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstLicenciantes);
        }

        private void _OrdenarApoderadosLicenciante_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstApoderadosLicenciante);
        }

        private void _OrdenarPoderesLicenciante_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesLicenciante);
        }

        private void _txtNombreLicenciante_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciatario(Visibility.Visible);
        }

        private void _txtNombreApoderadoLicenciante_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeApoderadoLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciante(Visibility.Visible);
        }

        private void _txtIdPoderLicenciante_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDePoderLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciatario(Visibility.Visible);
        }

        private void GestionarVisibilidadDatosDeLicenciante(object value)
        {
            this._lblNombreLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtNombreLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtIdLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtPaisLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtNacionalidadLicenciante.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeApoderadoLicenciante(object value)
        {
            this._lblNombreApoderadoLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoLicenciante.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDePoderLicenciante(object value)
        {
            this._lblIdPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._lblFomentoLicenciante.Visibility = (System.Windows.Visibility)value;
            this._lblFechaPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._lblBoletinPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._lblAnexoPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._lblFacultadPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtNumPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtFechaPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtBoletinPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtAnexoPoderLicenciante.Visibility = (System.Windows.Visibility)value;
            this._txtFacultadPoderLicenciante.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadFiltroLicenciante(object value)
        {
            this._lblLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstLicenciantes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarLicenciante.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadFiltroApoderadoLicenciante(object value)
        {
            this._lblApoderadoLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreApoderadoLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdApoderadoLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstApoderadosLicenciante.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarApoderadoLicenciante.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadFiltroPoderLicenciante(object value)
        {
            this._lblPoderLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._dpkFechaPoderLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPoderLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderLicencianteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPoderesLicenciante.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPoderLicenciante.Visibility = (System.Windows.Visibility)value;

        }

        private void _txtLicencianteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarLicenciante.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtApoderadoLicencianteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarApoderadoLicenciante.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtPoderLicencianteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPoderLicenciante.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        #endregion

        #region Licenciatario

        private void _lstLicenciatarios_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarLicenciatario())
            {
                GestionarVisibilidadDatosDeLicenciatario(Visibility.Visible);
                GestionarVisibilidadFiltroLicenciatario(Visibility.Collapsed);

                if (this._presentador.VerificarCambioInteresado("Licenciatario"))
                {
                    this._btnConsultarPoderLicenciatario.IsEnabled = false;
                }
                else
                {
                    this._btnConsultarPoderLicenciatario.IsEnabled = false;
                }
            }
        }

        private void _lstApoderadosLicenciatario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (this._presentador.CambiarApoderadoLicenciatario())
            {
                GestionarVisibilidadDatosDeApoderadoLicenciatario(Visibility.Visible);
                GestionarVisibilidadFiltroApoderadoLicenciatario(Visibility.Collapsed);

                if (this._presentador.VerificarCambioAgente("Licenciatario"))
                {
                    this._btnConsultarPoderLicenciatario.IsEnabled = false;
                }
                else
                {
                    this._btnConsultarPoderLicenciatario.IsEnabled = true;
                }
            }
        }

        private void _lstPoderesLicenciatario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPoderLicenciatario())
            {
                GestionarVisibilidadDatosDePoderLicenciatario(Visibility.Visible);
                GestionarVisibilidadFiltroPoderLicenciatario(Visibility.Collapsed);
                this._btnConsultarApoderadoLicenciatario.IsEnabled = false;
                this._btnConsultarLicenciatario.IsEnabled = false;
            }
        }

        private void _OrdenarLicenciatarios_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstLicenciatarios);
        }

        private void _OrdenarApoderadosLicenciatario_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstApoderadosLicenciatario);
        }

        private void _OrdenarPoderesLicenciatario_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesLicenciatario);
        }

        private void _txtNombreLicenciatario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciatario(Visibility.Visible);
        }

        private void _txtNombreApoderadoLicenciatario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeApoderadoLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciante(Visibility.Visible);
        }

        private void _txtIdPoderLicenciatario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDePoderLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciante(Visibility.Collapsed);

            GestionarVisibilidadFiltroLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoLicenciatario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciante(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoLicenciatario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderLicenciante(Visibility.Visible);
        }

        private void GestionarVisibilidadDatosDeLicenciatario(object value)
        {
            this._lblNombreLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtNombreLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtIdLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtPaisLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtNacionalidadLicenciatario.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeApoderadoLicenciatario(object value)
        {
            this._lblNombreApoderadoLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoLicenciatario.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDePoderLicenciatario(object value)
        {
            this._lblIdPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._lblFomentoLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._lblFechaPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._lblBoletinPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._lblAnexoPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._lblFacultadPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtNumPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtFechaPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtBoletinPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtAnexoPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtFacultadPoderLicenciatario.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadFiltroLicenciatario(object value)
        {
            this._lblLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._txtIdLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstLicenciatarios.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarLicenciatario.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadFiltroApoderadoLicenciatario(object value)
        {
            this._lblApoderadoLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreApoderadoLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdApoderadoLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstApoderadosLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarApoderadoLicenciatario.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadFiltroPoderLicenciatario(object value)
        {
            this._lblPoderLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._dpkFechaPoderLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPoderLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderLicenciatarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPoderesLicenciatario.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPoderLicenciatario.Visibility = (System.Windows.Visibility)value;

        }

        private void _txtLicenciatarioFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarLicenciatario.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtApoderadoLicenciatarioFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarApoderadoLicenciatario.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtPoderLicenciatarioFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPoderLicenciatario.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        #endregion

    }
}
