using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Traspasos.Fusiones
{
    /// <summary>
    /// Interaction logic for GestionarFusion.xaml
    /// </summary>
    public partial class GestionarFusion : Page, IGestionarFusion
    {

        private PresentadorGestionarFusion _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarFusion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Fusion
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
            get { throw new System.NotImplementedException(); }
        }

        public string NombreInteresadoSobrevivienteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdAgenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreAgenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
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

        public object InteresadoEntre
        {
            get { return this._gridDatosInteresadoEntre.DataContext; }
            set { this._gridDatosInteresadoEntre.DataContext = value; }
        }

        public string NombreInteresadoEntre
        {
            set { this._txtNombreInteresadoEntre.Text = value; }
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

                _btnConsultarInteresadoEntre.IsEnabled = value;
                _txtNombreInteresadoEntre.IsEnabled = value;
                _txtNombreInteresadoEntreFiltrar.IsEnabled = value;
                _txtIdInteresadoEntreFiltrar.IsEnabled = value;
                _txtPaisInteresadoEntre.IsEnabled = value;
                _txtCiudadInteresadoEntre.IsEnabled = value;
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

        public GestionarFusion(object fusion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFusion(this, fusion);
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
            
            this._btnConsultarMarca.IsEnabled = false;
            this._btnModificar.IsEnabled = true;
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

        #region Eventos Interesado Entre

        private void _btnConsultarInteresadoEntre_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresados();
        }

        private void _lstInteresadosEntre_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresado())
            {
                GestionarVisibilidadDatosDeInteresado(Visibility.Visible);
                GestionarVisibilidadFiltroInteresado(Visibility.Collapsed);

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
            GestionarVisibilidadDatosDeInteresado(Visibility.Collapsed);

            GestionarVisibilidadFiltroInteresado(Visibility.Visible);

            this._btnConsultarInteresadoEntre.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresado(object value)
        {
            this._lblIdInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoEntreFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoEntreFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresadosEntre.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresadoEntre.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresado(object value)
        {
            this._txtNombreInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresadoEntre.Visibility = (System.Windows.Visibility)value;
        }

        #endregion

    }
}
