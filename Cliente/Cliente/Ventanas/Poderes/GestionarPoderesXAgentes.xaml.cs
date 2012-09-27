using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Presentadores.Poderes;

namespace Trascend.Bolet.Cliente.Ventanas.Poderes
{
    /// <summary>
    /// Interaction logic for GestionarPoderesXAgentes.xaml
    /// </summary>
    public partial class GestionarPoderesXAgentes : Page, IGestionarPoderesXAgentes
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorGestionarPoderesXAgentes _presentador;
        private bool _cargada;

        #region IGestionarPoderesXAgentes

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

        public object Poderes
        {
            get { return this._lstObjeto.DataContext; }
            set { this._lstObjeto.DataContext = value; }
        }

        public object PoderesXAgentes
        {
            get { return this._lstObjXRol.DataContext; }
            set { this._lstObjXRol.DataContext = value; }
        }

        public object Agentes
        {
            get { return this._lstRol.DataContext; }
            set { this._lstRol.DataContext = value; }
        }

        public object AgenteSeleccionado
        {
            get { return this._lstRol.SelectedItem; }
        }

        public object PoderesSeleccionados
        {
            get { return this._lstObjeto.SelectedItems; }
        }

        public object PoderesXAgentesSeleccionados
        {
            get { return this._lstObjXRol.SelectedItems; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public string NumPoder
        {
            get { return this._txtNumPoder.Text; }
        }

        public ListView ListaAgentes
        {
            get { return this._lstRol; }
            set { this._lstRol = value; }
        }
        
            public ListView ListaPoderes
        {
            get { return this._lstObjeto; }
            set { this._lstObjeto = value; }
        }

            public ListView ListaAgentesXPoderes
        {
            get { return this._lstObjXRol; }
            set { this._lstObjXRol = value; }
        }
        #endregion

            public GestionarPoderesXAgentes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarPoderesXAgentes(this);
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Consultar();
        }

        private void _lstRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CargarPoderesPorAgente();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Agregar();
        }

        private void _btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Quitar();
        }

        private void _lstObjeto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.Agregar();
        }

        private void _lstObjXRol_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.Quitar();
        }

        private void _OrdenarRoles_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader,1);
        }

        private void _OrdenarObjetos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader,2);
        }

        private void _OrdenarRolesXObjetos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader,3);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }
    }
}
