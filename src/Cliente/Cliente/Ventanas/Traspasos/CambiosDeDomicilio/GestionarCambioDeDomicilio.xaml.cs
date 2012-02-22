using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Traspasos.CambiosDeDomicilio;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.CambiosDeDomicilio;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Traspasos.CambiosDeDomicilio
{
    /// <summary>
    /// Interaction logic for CambiosDeDomicilio.xaml
    /// </summary>
    public partial class GestionarCambioDeDomicilio : Page, IGestionarCambioDeDomicilio
    {

        private PresentadorGestionarCambioDeDomicilio _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarFusion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object CambioDeDomicilio
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
            this._txtId.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtAsociado.IsEnabled = value;
                this._txtClaseInternacional.IsEnabled = value;
                this._txtClaseNacional.IsEnabled = value;
                this._txtExpediente.IsEnabled = value;
                this._txtId.IsEnabled = value;
                this._txtIdMarcaFiltrar.IsEnabled = value;
                this._txtNombreMarca.IsEnabled = value;
                this._txtNombreMarcaFiltrar.IsEnabled = value;
                this._txtNumInscripcion.IsEnabled = value;
                this._txtNumRegistro.IsEnabled = value;
                this._txtTipo.IsEnabled = value;
                this._btnConsultarMarca.IsEnabled = value;
                this._dpkFechaBoletin.IsEnabled = value;
                this._chkEtiqueta.IsEnabled = value;

                _btnConsultarInteresadoAnterior.IsEnabled = value;
                _txtNombreInteresadoAnterior.IsEnabled = value;
                _txtNombreInteresadoAnteriorFiltrar.IsEnabled = value;
                _txtIdInteresadoAnteriorFiltrar.IsEnabled = value;
                _txtPaisInteresadoAnterior.IsEnabled = value;
                _txtCiudadInteresadoAnterior.IsEnabled = value;
                _txtDomicilioInteresadoAnterior.IsEnabled = value;

                _btnConsultarInteresadoActual.IsEnabled = value;
                _txtNombreInteresadoActual.IsEnabled = value;
                _txtNombreInteresadoActualFiltrar.IsEnabled = value;
                _txtIdInteresadoActualFiltrar.IsEnabled = value;
                _txtPaisInteresadoActual.IsEnabled = value;
                _txtCiudadInteresadoActual.IsEnabled = value;
                _txtDomicilioInteresadoActual.IsEnabled = value;

                _btnConsultarApoderado.IsEnabled = value;
                _txtNombreApoderado.IsEnabled = value;
                _txtNombreApoderadoFiltrar.IsEnabled = value;
                _txtIdApoderadoFiltrar.IsEnabled = value;
            }
        }

        public string NombreMarca
        {
            set { this._txtNombreMarca.Text = value; }
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

        public GestionarCambioDeDomicilio(object fusion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCambioDeDomicilio(this, fusion);
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarPais,
                "Eliminar Pais", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
        }

        private void GestionarVisibilidadDatosDeMarca(object value)
        {
            this._txtNombreMarca.Visibility = (System.Windows.Visibility)value;
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

            //escondo el filtro de Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            //escondo el filtro de AgenteApoderado
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //escondo el filtro de InteresadoActual
            GestionarVisibilidadDatosDeInteresadoActual(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoActual(Visibility.Collapsed);

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

                this._btnConsultarInteresadoActual.IsDefault = false;
                this._btnModificar.IsDefault = true;
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

        #endregion

        #region Eventos Agente Apoderado

        private void _btnConsultarApoderado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarApoderados();
        }

        private void _lstApoderadosCedente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void _lstApoderados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarApoderado())
            {
                GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
                GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

                this._btnConsultarApoderado.IsDefault = false;
                this._btnModificar.IsDefault = true;
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

            this._btnConsultarApoderado.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroAgenteApoderado(object value)
        {
            this._lblIdApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstApoderados.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarApoderado.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeAgenteApoderado(object value)
        {
            this._txtNombreApoderado.Visibility = (System.Windows.Visibility)value;
        }

        #endregion
    }
}
