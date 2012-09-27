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
            this._txtNumSAPI.Focus();
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

        public object Agentes
        {
            get { return this._cbxAgente.DataContext; }
            set { this._cbxAgente.DataContext = value; }
        }

        public object Agente
        {
            get { return this._cbxAgente.SelectedItem; }
            set { this._cbxAgente.SelectedItem = value; }
        }

        public object Apoderados
        {
            get { return this._lstAgentes.DataContext; }
            set { this._lstAgentes.DataContext = value; }
        }

        public object Apoderado
        {
            get { return this._lstAgentes.SelectedItem; }
            set { this._lstAgentes.SelectedItem = value; }
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

        public string NombreInteresado{
            set { this._txtNombreInteresado.Text = value; }
        }

        public string IdInteresadoConsultar
        {
            get { return this._txtIdInteresadoFiltrar.Text; }
        }

        public string NombreInteresadoConsultar
        {
            get { return this._txtNombreInteresadoFiltrar.Text; }
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

        private void _cbxAgente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _txtIdAgente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void _lstAgentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void _OrdenarAgentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAgentes);
        }

        private void _txtIdAgenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            //this._btnConsultarAgente.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void _btnConsultarAgente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _txtNombreInteresado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._txtIdInteresadoFiltrar.Focus();
            this._btnConsultarInteresado.IsDefault = true;
            this._btnAceptar.IsDefault = false;

            MostrarListInteresados();
        }

        private void MostrarListInteresados()
        {
            this._txtNombreInteresado.Visibility = Visibility.Collapsed;
            this._lblNombreInteresadoFiltrar.Visibility = Visibility.Visible;
            this._txtNombreInteresadoFiltrar.Visibility = Visibility.Visible;
            this._lblIdInteresadoFiltrar.Visibility = Visibility.Visible;
            this._txtIdInteresadoFiltrar.Visibility = Visibility.Visible;
            this._btnConsultarInteresado.Visibility = Visibility.Visible;
            this._lstInteresados.Visibility = Visibility.Visible;
            this._colInteresados.Height = new System.Windows.GridLength(180);
        }

        private void MostrarNombreInteresado()
        {
            this._txtNombreInteresado.Visibility = Visibility.Visible;
            this._lblNombreInteresadoFiltrar.Visibility = Visibility.Collapsed;
            this._txtNombreInteresadoFiltrar.Visibility = Visibility.Collapsed;
            this._lblIdInteresadoFiltrar.Visibility = Visibility.Collapsed;
            this._txtIdInteresadoFiltrar.Visibility = Visibility.Collapsed;
            this._btnConsultarInteresado.Visibility = Visibility.Collapsed;
            this._lstInteresados.Visibility = Visibility.Collapsed;
            this._colInteresados.Height = new System.Windows.GridLength(30);
        }

        private void _lstInteresados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._lstInteresados.SelectedItem != null) 
            {
                _presentador.CambiarInteresado();
                MostrarNombreInteresado();
            }
        }

        private void _txtIdInteresadoFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadoFiltro();
        }

        private void _btnAgregarAgente_Click(object sender, RoutedEventArgs e)
        {
            _presentador.AgregarAgente();
        }

        private void _btnQuitarAgente_Click(object sender, RoutedEventArgs e)
        {
            _presentador.EliminarAgente();

        }
    }
}
