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
    /// Interaction logic for ListaBusquedas.xaml
    /// </summary>
    public partial class ListaBusquedas : Page, IListaBusquedas
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaBusquedas _presentador;
        private bool _cargada;

        #region IListaBusquedas

        public object BusquedaSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
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

        public string Id
        {
            get { return this._txtId.Text; }
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


        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ListaBusquedas(object marca)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaBusquedas(this, marca);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Consultar();
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
            //if (!this._txtId.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtId.Focus();
            //}

            //if (!this._txtNumPoder.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtNumPoder.Focus();
            //}

            //if ((this._cbxBoletin.SelectedIndex != 0) && (this._cbxBoletin.SelectedIndex != -1))
            //{
            //    todosCamposVacios = false;
            //    this._cbxBoletin.Focus();
            //}

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

            if (todosCamposVacios)
                this._txtId.Focus();
        }



        private void _dpkFechaBusquedaPalabra_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _txtId_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }


    }
}
