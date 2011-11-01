using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Estado;
using Trascend.Bolet.Cliente.Presentadores.Estados;

namespace Trascend.Bolet.Cliente.Ventanas.Estados
{
    /// <summary>
    /// Interaction logic for ConsultarEstados.xaml
    /// </summary>
    public partial class ConsultarEstados : Page, IConsultarEstados
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarEstados _presentador;
        private bool _cargada;

        #region IConsultarEstados

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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object EstadoSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            
        }

        public object EstadoFiltrar 
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public ListView ListaResultados
        {
            get { return this._lstResultados; }
            set { this._lstResultados = value; }
        }

        #endregion

        public ConsultarEstados()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarEstados(this);

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
            this._presentador.IrConsultarEstado();
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
            {
                this._presentador.ActualizarTitulo();
            }
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

            if (!this._txtDescripcion.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDescripcion.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }
    }
}
