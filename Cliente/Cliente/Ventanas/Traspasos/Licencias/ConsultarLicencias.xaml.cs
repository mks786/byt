using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Traspasos.Licencias;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.Licencias;

namespace Trascend.Bolet.Cliente.Ventanas.Traspasos.Licencias
{
    /// <summary>
    /// Interaction logic for ConsultarTodosPoder.xaml
    /// </summary>
    public partial class ConsultarLicencias : Page, IConsultarLicencias
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarLicencias _presentador;
        private bool _cargada;

        #region IConsultarLicencias

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
            set { this._txtId.Text = value; }
        }

        public string NombreMarca
        {
            set { this._txtMarcaNombre.Text = value; }
        }

        public object LicenciaSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string IdMarcaFiltrar
        {
            get { return this._txtIdMarcaFiltrar.Text; }
            set { this._txtIdMarcaFiltrar.Text = value; }
        }

        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
            set { this._txtNombreMarcaFiltrar.Text = value; }
        }
     
        public string Fecha
        {
            get { return this._dpkFecha.SelectedDate.ToString(); }
            set { this._dpkFecha.Text = value; }
        }

        public object Marcas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object Marca
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
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

        public ListView ListaResultados
        {
            get { return this._lstResultados; }
            set { this._lstResultados = value; }
        }
   
   
        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarLicencias()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarLicencias(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            this._dpkFecha.Text = string.Empty;
            validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarLicencia();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

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

        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarMarca();
        }

        //private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.BuscarAsociado();
        //}

        //private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.BuscarInteresado();
        //}

        private void _btnConsultarMarcaFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarMarca.IsDefault = true;          
        }

        private void _btnConsultarInteresadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarMarca.IsDefault = false;            
        }

        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
            this._btnConsultarMarca.IsDefault = false;            
        }

        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;
            if (!this._txtId.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtId.Focus();
            }           

            if (!this._dpkFecha.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFecha.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }
        
        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _btnTransferir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _txtMarcaNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            this._txtMarcaNombre.Visibility = Visibility.Collapsed;

            this._txtIdMarcaFiltrar.Visibility = Visibility.Visible;
            this._txtNombreMarcaFiltrar.Visibility = Visibility.Visible;
            this._btnConsultarMarca.Visibility = Visibility.Visible;
            this._lstMarcas.Visibility = Visibility.Visible;
            this._lblCodigo.Visibility = Visibility.Visible;
            this._lblNombre.Visibility = Visibility.Visible;
        }

        private void _lstMarcas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.ElegirMarca())
            {
                this._txtMarcaNombre.Visibility = Visibility.Visible;

                this._txtIdMarcaFiltrar.Visibility = Visibility.Collapsed;
                this._txtNombreMarcaFiltrar.Visibility = Visibility.Collapsed;
                this._btnConsultarMarca.Visibility = Visibility.Collapsed;
                this._lstMarcas.Visibility = Visibility.Collapsed;
                this._lblCodigo.Visibility = Visibility.Collapsed;
                this._lblNombre.Visibility = Visibility.Collapsed;
            }
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }
    }
}
