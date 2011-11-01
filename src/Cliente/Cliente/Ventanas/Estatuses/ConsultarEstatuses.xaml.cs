using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Estatuses;
using Trascend.Bolet.Cliente.Presentadores.Estatuses;

namespace Trascend.Bolet.Cliente.Ventanas.Estatuses
{
    /// <summary>
    /// Interaction logic for ConsultarObjetos.xaml
    /// </summary>
    public partial class ConsultarEstatuses : Page, IConsultarEstatuses
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarEstatuses _presentador;
        private bool _cargada;

        #region IConsultarEstatuses

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object EstatusFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public object EstatusSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }

        }

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

        #endregion

        public ConsultarEstatuses()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarEstatuses(this);

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
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarEstatus();
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

            if (!this._txtDescripcion.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDescripcion.Focus();
            }

            if (!this._txtDescripcionIngles.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDescripcionIngles.Focus();
            }

            if (!this._txtStatusProximoPaso.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtStatusProximoPaso.Focus();
            }

            if (!this._txtStatusProximoPasoIngles.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtStatusProximoPasoIngles.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }
    }
}
