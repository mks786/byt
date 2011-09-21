using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Usuarios;
using Trascend.Bolet.Cliente.Presentadores.Usuarios;

namespace Trascend.Bolet.Cliente.Ventanas.Usuarios
{
    /// <summary>
    /// Interaction logic for ConsultarTodosUsuario.xaml
    /// </summary>
    public partial class ConsultarUsuarios : Page, IConsultarUsuarios
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarUsuarios _presentador;
        private bool _cargada;

        #region IConsultarUsuarios

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

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public string NombreCompleto
        {
            get { return this._txtNombre.Text; }
        }

        public string Iniciales
        {
            get { return this._txtIniciales.Text; }
        }

        public object Rol
        {
            get { return this._cbxRol.SelectedItem; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
        }

        public string Email
        {
            get { return this._txtEmail.Text; }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object Roles
        {
            get { return this._cbxRol.DataContext; }
            set { this._cbxRol.DataContext = value; }
        }

        public object UsuarioSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
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
        public ConsultarUsuarios()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarUsuarios(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Consultar();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarUsuario();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void _pagConsultarUsuarios_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }
    }
}
