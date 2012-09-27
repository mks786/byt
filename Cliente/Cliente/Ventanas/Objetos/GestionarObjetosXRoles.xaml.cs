using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Objetos;
using Trascend.Bolet.Cliente.Presentadores.Objetos;

namespace Trascend.Bolet.Cliente.Ventanas.Objetos
{
    /// <summary>
    /// Interaction logic for GestionarObjetosXRoles.xaml
    /// </summary>
    public partial class GestionarObjetosXRoles : Page, IGestionarObjetosXRoles
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorGestionarObjetosXRoles _presentador;
        private bool _cargada;

        #region IGestionarObjetosXRoles

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

        public object Objetos
        {
            get { return this._lstObjeto.DataContext; }
            set { this._lstObjeto.DataContext = value; }
        }

        public object ObjetosXRoles
        {
            get { return this._lstObjXRol.DataContext; }
            set { this._lstObjXRol.DataContext = value; }
        }

        public object Roles
        {
            get { return this._lstRol.DataContext; }
            set { this._lstRol.DataContext = value; }
        }

        public object RolSeleccionado
        {
            get { return this._lstRol.SelectedItem; }
        }

        public object ObjetosSeleccionados
        {
            get { return this._lstObjeto.SelectedItems; }
        }

        public object ObjetosXRolesSeleccionados
        {
            get { return this._lstObjXRol.SelectedItems; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public string Descripcion
        {
            get { return this._txtDescripcion.Text; }
        }

        public ListView ListaRoles
        {
            get { return this._lstRol; }
            set { this._lstRol = value; }
        }
        
            public ListView ListaObjetos
        {
            get { return this._lstObjeto; }
            set { this._lstObjeto = value; }
        }

            public ListView ListaRolesXObjetos
        {
            get { return this._lstObjXRol; }
            set { this._lstObjXRol = value; }
        }
        #endregion

        public GestionarObjetosXRoles()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarObjetosXRoles(this);
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Consultar();
        }

        private void _lstRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CargarObjetosPorRol();
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
