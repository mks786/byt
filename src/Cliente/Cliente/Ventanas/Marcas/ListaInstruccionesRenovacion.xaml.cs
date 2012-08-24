using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using System;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for ListaInstruccionesRenovacion.xaml
    /// </summary>
    public partial class ListaInstruccionesRenovacion : Page, IListaBusquedas
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaBusquedas _presentador;
        private bool _cargada;
        private object _marca;
        private string _tab;

        #region IListaBusquedas

        public object BusquedaSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
        }

        public string Tab
        {
            get { return this._tab; }
        }

        public object BusquedaFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public object TiposBusqueda
        {
            get { return this._cbxTipoBusqueda.DataContext; }
            set { this._cbxTipoBusqueda.DataContext = value; }
        }

        public object TipoBusqueda
        {
            get { return this._cbxTipoBusqueda.SelectedItem; }
            set { this._cbxTipoBusqueda.SelectedItem = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string IdMarca
        {
            get { return this._txtIdMarca.Text; }
            set { this._txtIdMarca.Text = value; }
        }

        public string IdBusqueda
        {
            get { return this._txtIdBusqueda.Text; }
        }


        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtIdMarca.Focus();
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

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ListaInstruccionesRenovacion(object marca)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaBusquedas(this, marca);
            this._marca = marca;
        }

        public ListaInstruccionesRenovacion(object marca, string tab)
            : this(marca)
        {
            this._tab = tab;
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrConsultarMarca();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Consultar();
            this._dpkFechaBusquedaDiseno.Text = string.Empty;
            this._dpkFechaBusquedaPalabra.Text = string.Empty;
            this._dpkFechaConsigDiseno.Text = string.Empty;
            this._dpkFechaConsigPalabra.Text = string.Empty;
            this._dpkFechaResultadoBusqueda.Text = string.Empty;
            validarCamposVacios();
        }

        private void EventoIrGestionarBusqueda(object sender, EventArgs e)
        {
            if (sender.GetType().ToString().Equals("System.Windows.Controls.Button"))
                this._presentador.IrGestionarBusqueda(true);
            else
                this._presentador.IrGestionarBusqueda(false);
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
        }



        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;

            if (!this._txtIdBusqueda.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdBusqueda.Focus();
            }

            if ((this._cbxTipoBusqueda.SelectedIndex != 0) && (this._cbxTipoBusqueda.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTipoBusqueda.Focus();
            }

            if (todosCamposVacios)
                this._txtIdBusqueda.Focus();
        }



        private void _dpkFechaBusquedaPalabra_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _txtId_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }
    }
}
