using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Abandonos;
using Trascend.Bolet.Cliente.Presentadores.Abandonos;

namespace Trascend.Bolet.Cliente.Ventanas.Abandonos
{
    /// <summary>
    /// Interaction logic for ConsultarAbandonos.xaml
    /// </summary>
    public partial class ConsultarAbandonos : Page, IConsultarAbandonos
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarAbandonos _presentador;
        private bool _cargada;

        #region IConsultarRenovaciones

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

        public object AbandonoSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
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

        public string IdMarcaFiltrar
        {
            get { return this._txtIdMarcaFiltrar.Text; }
            set { this._txtIdMarcaFiltrar.Text = value; }
        }

        public string Fecha
        {
            get { return this._dpkFecha.Text; }
            set { this._dpkFecha.Text = value; }
        }

        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
            set { this._txtNombreMarcaFiltrar.Text = value; }
        }

        public string NombreMarca
        {
            set { this._txtMarca.Text = value; }
        }

        public string IdMarca
        {
            get { return this._txtIdMarca.Text; }
            set { this._txtIdMarca.Text = value; }
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }      

        public string MarcaFiltrada
        {
            get { return this._txtMarca.Text; }
            set { this._txtMarca.Text = value; }           
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarAbandonos()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarAbandonos(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();            
            validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarAbandono();
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

        public void ConvertirEnteroMinimoABlanco()
        {
            if (!this.IdMarca.Equals(""))
            {
                if (int.Parse(this.IdMarca) == int.MinValue)
                    this.IdMarca = "";
            }
        }
        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMarca();
        }          

        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
            this._btnConsultarMarca.IsDefault = false;           
        }

        private void _btnConsultarMarcaFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarMarca.IsDefault = true;
        }

        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

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

            if (todosCamposVacios)
                this._txtId.Focus();
        }      
      
        private void _txtMarca_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroMarca(true);            
        }        

        private void GestionarVisibilidadFiltroMarca(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtMarca.Visibility = Visibility.Collapsed;
                this._txtIdMarca.Visibility = Visibility.Collapsed;

                this._txtIdMarcaFiltrar.Visibility = Visibility.Visible;
                this._txtNombreMarcaFiltrar.Visibility = Visibility.Visible;
                this._lblIdMarca.Visibility = Visibility.Visible;
                this._lblNombreMarca.Visibility = Visibility.Visible;
                this._lstMarcas.Visibility = Visibility.Visible;
                this._btnConsultarMarca.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtMarca.Visibility = Visibility.Visible;
                this._txtIdMarca.Visibility = Visibility.Visible;

                this._txtIdMarcaFiltrar.Visibility = Visibility.Collapsed;
                this._txtNombreMarcaFiltrar.Visibility = Visibility.Collapsed;
                this._lblIdMarca.Visibility = Visibility.Collapsed;
                this._lblNombreMarca.Visibility = Visibility.Collapsed;
                this._lstMarcas.Visibility = Visibility.Collapsed;
                this._btnConsultarMarca.Visibility = Visibility.Collapsed;
            }
        }     

        private void _lstMarcas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.ElegirMarca())
                GestionarVisibilidadFiltroMarca(false);
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        } 
    }
}
