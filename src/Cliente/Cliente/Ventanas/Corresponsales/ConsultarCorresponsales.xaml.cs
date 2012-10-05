using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Corresponsales;
using Trascend.Bolet.Cliente.Presentadores.Corresponsales;

namespace Trascend.Bolet.Cliente.Ventanas.Corresponsales
{
    /// <summary>
    /// Interaction logic for ConsultarCorresponsales.xaml
    /// </summary>
    public partial class ConsultarCorresponsales : Page, IConsultarCorresponsales
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarCorresponsales _presentador;
        private bool _cargada;

        #region IConsultarCorresponsales

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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object CorresponsalFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public object CorresponsalSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }

        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        public string GetIdCorresponsal
        {
            get { return this._txtId.Text; }
        }

        #endregion

        public ConsultarCorresponsales()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarCorresponsales(this);

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

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
                this._btnConsultar.Focus();
                this._presentador.Consultar();
                this._lblHits.Text = this.ListaResultados.Items.Count.ToString();
                validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarCorresponsal();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
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

            if (!this._txtNombre.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNombre.Focus();
            }

            //if (!this._txtDomicilio.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtDomicilio.Focus();
            //}

            //if (!this._txtTelefono.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtTelefono.Focus();
            //}

            //if ((this._cbxPais.SelectedIndex != 0) && (this._cbxPais.SelectedIndex != -1))
            //{
            //    todosCamposVacios = false;
            //    this._cbxPais.Focus();
            //}

            //if ((this._cbxIdioma.SelectedIndex != 0) && (this._cbxIdioma.SelectedIndex != -1))
            //{
            //    todosCamposVacios = false;
            //    this._cbxIdioma.Focus();
            //}

            //if (!this._txtNumeroImpresoAbogado.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtNumeroImpresoAbogado.Focus();
            //}

            //if (!this._txtNumeroAbogado.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtNumeroAbogado.Focus();
            //}

            //if (!this._txtNumeroPropiedad.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtNumeroPropiedad.Focus();
            //}

            //if (!this._txtCCI.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtCCI.Focus();
            //}

            if (todosCamposVacios)
                this._txtId.Focus();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }
    }
}
