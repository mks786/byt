using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Renovaciones;
using Trascend.Bolet.Cliente.Presentadores.Renovaciones;

namespace Trascend.Bolet.Cliente.Ventanas.Renovaciones
{
    /// <summary>
    /// Interaction logic for ConsultarTodosPoder.xaml
    /// </summary>
    public partial class ConsultarRenovaciones : Page, IConsultarRenovaciones
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarRenovaciones _presentador;
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

        public object RenovacionSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
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

        public string FechaFiltrar
        {
            get { return this._dpkFecha.Text; }
            set { this._dpkFecha.Text = value; }
        }

        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
            set { this._txtNombreMarcaFiltrar.Text = value; }
        }

        public string RegistroMarcaFiltrar
        {
            get { return this._txtRegistroMarcaFiltrar.Text; }
            set { this._txtRegistroMarcaFiltrar.Text = value; }
        }

        public object Interesados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._lstInteresados.SelectedItem; }
            set { this._lstInteresados.SelectedItem = value; }
        }

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresadoFiltrar.Text; }
            set { this._txtIdInteresadoFiltrar.Text = value; }
        }

        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresadoFiltrar.Text; }
            set { this._txtNombreInteresadoFiltrar.Text = value; }
        }

        public void MostrarBotonVolverAMarca()
        {
            this._btnVolverMarca.Visibility = Visibility.Visible;
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

        public string IdMarcaFiltrada
        {
            get { return this._txtIdMarca.Text; }
            set { this._txtIdMarca.Text = value; }
        }

        public string InteresadoFiltrado
        {
            get { return this._txtInteresado.Text; }
            set { this._txtInteresado.Text = value; }
        }

        public string IdInteresadoFiltrado
        {
            get { return this._txtIdInteresado.Text; }
            set { this._txtIdInteresado.Text = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarRenovaciones()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarRenovaciones(this);
        }
        /// <summary>
        /// Constructor para cargar las ventanas de una Marca
        /// </summary>
        public ConsultarRenovaciones(object marca, object ventanaPadre) :this()
        {
            this._presentador = new PresentadorConsultarRenovaciones(this,marca);
            this._presentador._ventanaPadre = ventanaPadre;
        }

        public void ConvertirEnteroMinimoABlanco()
        {
            if (!this.IdMarcaFiltrada.Equals(""))
            {
                if (int.Parse(this.IdMarcaFiltrada) == int.MinValue)
                    this.IdMarcaFiltrada = "";
            }

            if (!this.IdInteresadoFiltrado.Equals(""))
            {
                if (int.Parse(this.IdInteresadoFiltrado) == int.MinValue)
                    this.IdInteresadoFiltrado = "";
            }
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
            this._presentador.IrConsultarRenovacion();
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

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado();
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

        private void _btnConsultarInteresadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = true;
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
            GestionarVisibilidadFiltroInteresado(false);

            this._txtIdMarcaFiltrar.Focus();

        }

        private void _txtInteresado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroInteresado(true);
            GestionarVisibilidadFiltroMarca(false);

            this._txtIdInteresadoFiltrar.Focus();
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
                this._lblRegistroMarca.Visibility = Visibility.Visible;
                this._txtRegistroMarcaFiltrar.Visibility = Visibility.Visible;
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
                this._lblRegistroMarca.Visibility = Visibility.Collapsed;
                this._txtRegistroMarcaFiltrar.Visibility = Visibility.Collapsed;

            }
        }

        private void GestionarVisibilidadFiltroInteresado(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtInteresado.Visibility = Visibility.Collapsed;
                this._txtIdInteresado.Visibility = Visibility.Collapsed;

                this._txtIdInteresadoFiltrar.Visibility = Visibility.Visible;
                this._txtNombreInteresadoFiltrar.Visibility = Visibility.Visible;
                this._lblIdInteresado.Visibility = Visibility.Visible;
                this._lblNombreInteresado.Visibility = Visibility.Visible;
                this._lstInteresados.Visibility = Visibility.Visible;
                this._btnConsultarInteresado.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtInteresado.Visibility = Visibility.Visible;
                this._txtIdInteresado.Visibility = Visibility.Visible;

                this._txtIdInteresadoFiltrar.Visibility = Visibility.Collapsed;
                this._txtNombreInteresadoFiltrar.Visibility = Visibility.Collapsed;
                this._lblIdInteresado.Visibility = Visibility.Collapsed;
                this._lblNombreInteresado.Visibility = Visibility.Collapsed;
                this._lstInteresados.Visibility = Visibility.Collapsed;
                this._btnConsultarInteresado.Visibility = Visibility.Collapsed;
                

            }
        }

        private void _lstMarcas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarMarca())
                GestionarVisibilidadFiltroMarca(false);

            this._btnConsultarMarca.IsDefault = false;
            this._btnConsultar.IsDefault = true;
        }

        private void _lstInteresados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresado())
                GestionarVisibilidadFiltroInteresado(false);

            this._btnConsultarInteresado.IsDefault = false;
            this._btnConsultar.IsDefault = true;
        }

        private void _btnVolverMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VolverAMarca();
        }


        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }       
        
    }
}
