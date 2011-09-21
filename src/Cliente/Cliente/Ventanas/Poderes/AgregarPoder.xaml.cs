using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Presentadores.Poderes;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Poderes
{
    /// <summary>
    /// Interaction logic for ConsultarPoder.xaml
    /// </summary>
    public partial class AgregarPoder : Page, IAgregarPoder
    {
        private GridViewColumnHeader _CurSortCol = null;
        private PresentadorAgregarPoder _presentador;
        private bool _cargada;
        private bool _conInteresado;
        private SortAdorner _CurAdorner = null;

        #region IAgregarPoder

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNumPoder.Focus();
        }

        public object Poder
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
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
            set
            {
                this._lstInteresados.SelectedItem = value;
                this._lstInteresados.ScrollIntoView(value);
            }
        }

        public bool InteresadoEsEditable
        {
            get { return this._lstInteresados.IsEnabled; }
            set { this._lstInteresados.IsEnabled = value; }
        }

        public string TextoBotonCancelar
        {
            get { return this._txbCancelar.Text; }
            set { this._txbCancelar.Text = value; }
        }

        public bool ConInteresado
        {
            get { return this._conInteresado; }
            set { this._conInteresado = value; }
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
            get { return this._lstInteresados; }
            set { this._lstInteresados = value; }
        }

        #endregion

        public AgregarPoder()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarPoder(this);
        }

        public AgregarPoder(object interesado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarPoder(this, interesado);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (this._conInteresado)
                this._presentador.Regresar();
            else
                this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }
    }
}
