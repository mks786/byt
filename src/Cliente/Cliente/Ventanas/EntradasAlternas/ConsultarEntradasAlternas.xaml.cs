using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.EntradasAlternas;
using Trascend.Bolet.Cliente.Presentadores.EntradasAlternas;

namespace Trascend.Bolet.Cliente.Ventanas.EntradasAlternas
{
    /// <summary>
    /// Interaction logic for ConsultarObjetos.xaml
    /// </summary>
    public partial class ConsultarEntradasAlternas : Page, IConsultarEntradasAlternas
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarEntradasAlternas _presentador;
        private bool _cargada;

        #region IConsultarEntradasAlternas

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
        
        public object EntradaAlternaSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
            set { this._txtId.Text = value; }
        }

        public string Descripcion
        {
            get { return this._txtDescripcion.Text; }
            set { this._txtDescripcion.Text = value; }
        }

        public string FechaEntradaAlterna
        {
            get { return this._dpkFecha.SelectedDate.ToString(); }
            set { this._dpkFecha.Text = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object CategoriaSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }

        }

        public object Medio
        {
            get { return this._cbxMedio.SelectedItem; }
            set { this._cbxMedio.SelectedItem = value; }
        }

        public object Medios
        {
            get { return this._cbxMedio.DataContext; }
            set { this._cbxMedio.DataContext = value; }
        }

        public object Receptor
        {
            get { return this._cbxReceptor.SelectedItem; }
            set { this._cbxReceptor.SelectedItem = value; }
        }

        public object Receptores
        {
            get { return this._cbxReceptor.DataContext; }
            set { this._cbxReceptor.DataContext = value; }
        }

        public object Remitente
        {
            get { return this._cbxRemitente.SelectedItem; }
            set { this._cbxRemitente.SelectedItem = value; }
        }

        public object Remitentes
        {
            get { return this._cbxRemitente.DataContext; }
            set { this._cbxRemitente.DataContext = value; }
        }

        public object Categoria
        {
            get { return this._cbxCategoria.SelectedItem; }
            set { this._cbxCategoria.SelectedItem = value; }
        }

        public object Categorias
        {
            get { return this._cbxCategoria.DataContext; }
            set { this._cbxCategoria.DataContext = value; }
        }

        public object TiposAcuse
        {
            get { return this._cbxAcuse.DataContext; }
            set { this._cbxAcuse.DataContext = value; }
        }

        public object TipoAcuse
        {
            get { return this._cbxAcuse.SelectedItem; }
            set { this._cbxAcuse.SelectedItem = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        #endregion

        public ConsultarEntradasAlternas()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarEntradasAlternas(this);

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
                validarCamposVacios();
                this._dpkFecha.Text = string.Empty;
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarEntradaAlterna();
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

            if ((this._cbxCategoria.SelectedIndex != 0) && (this._cbxCategoria.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxCategoria.Focus();
            }

            if ((this._cbxMedio.SelectedIndex != 0) && (this._cbxMedio.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxMedio.Focus();
            }

            if ((this._cbxReceptor.SelectedIndex != 0) && (this._cbxReceptor.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxReceptor.Focus();
            }

            if ((this._cbxRemitente.SelectedIndex != 0) && (this._cbxRemitente.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxRemitente.Focus();
            }

            if ((this._cbxAcuse.SelectedIndex != 0) && (this._cbxAcuse.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxAcuse.Focus();
            }

            if (!this._txtDescripcion.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDescripcion.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }
    }
}
