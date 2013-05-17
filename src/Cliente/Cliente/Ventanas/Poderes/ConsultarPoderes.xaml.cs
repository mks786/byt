using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Presentadores.Poderes;

namespace Trascend.Bolet.Cliente.Ventanas.Poderes
{
    /// <summary>
    /// Interaction logic for ConsultarTodosPoder.xaml
    /// </summary>
    public partial class ConsultarPoderes : Page, IConsultarPoderes
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarPoderes _presentador;
        private bool _cargada;

        #region IConsultarPoderes



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

        public string NumPoder
        {
            get { return this._txtNumPoder.Text; }
            set { this._txtNumPoder.Text = value; }
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

        //public string Facultad
        //{
        //    get { return this._txtFacultad.Text; }
        //    set { this._txtFacultad.Text = value; }
        //}

        //public string Anexo
        //{
        //    get { return this._txtAnexo.Text; }
        //    set { this._txtAnexo.Text = value; }
        //}

        //public string Observaciones
        //{
        //    get { return this._txtObservaciones.Text; }
        //    set { this._txtObservaciones.Text = value; }
        //}

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object PoderSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
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

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresado.Text; }
            set { this._txtIdInteresado.Text = value; }
        }
        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresado.Text; }
            set { this._txtNombreInteresado.Text = value; }
        }

        public string NombreInteresadoBuscar
        {
            get { return this._txtNombreInteresadoBuscar.Text; }
            set { this._txtNombreInteresadoBuscar.Text = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarPoderes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarPoderes(this);
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
            this._presentador.IrConsultarPoder();
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

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado();
        }

        private void _btnConsultarInteresadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = true;
        }

        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
            this._btnConsultarInteresado.IsDefault = false;
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

            if (!this._txtNumPoder.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNumPoder.Focus();
            }

            if ((this._cbxBoletin.SelectedIndex != 0) && (this._cbxBoletin.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxBoletin.Focus();
            }

            //if (!this._txtFacultad.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtFacultad.Focus();
            //}

            //if (!this._txtAnexo.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtAnexo.Focus();
            //}

            //if (!this._txtObservaciones.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtObservaciones.Focus();
            //}

            if (!this._txtNombreInteresadoBuscar.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNombreInteresadoBuscar.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _lstInteresados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string prueba = string.Empty;
            prueba = "prueba";
            if (this._presentador.CambiarInteresado())
                ocultarCamposBusquedaInteresado();

        }

        private void _txtNombreInteresadoBuscar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ocultarCampoInteresadoABuscar();
            mostarCamposBusquedaInteresado();
        }

        //private void ocultarCampoInteresadoABuscar()
        //{
        //    this._lblInteresadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
        //    this._txtNombreInteresadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
        //}

        private void mostarCamposBusquedaInteresado()
        {
            this._lblIdInteresado.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresado.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresado.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresado.Visibility = System.Windows.Visibility.Visible;
            this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados.Visibility = System.Windows.Visibility.Visible;
        }

        private void ocultarCamposBusquedaInteresado()
        {
            this._lblIdInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados.Visibility = System.Windows.Visibility.Collapsed;
        }


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


    }
}
