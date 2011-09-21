using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Usuarios;
using Trascend.Bolet.Cliente.Presentadores.Usuarios;

namespace Trascend.Bolet.Cliente.Ventanas.Usuarios
{
    /// <summary>
    /// Interaction logic for ConsultarUsuario.xaml
    /// </summary>
    public partial class ConsultarUsuario : Page, IConsultarUsuario
    {
        private PresentadorConsultarUsuario _presentador;
        private bool _cargada;

        #region IConsultarUsuario

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Usuario
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object Rol
        {
            get { return this._cbxRol.SelectedItem; }
            set { this._cbxRol.SelectedItem = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
        }

        public bool HabilitarCampos
        {
            set 
            {
                this._txtNombreCompleto.IsEnabled = value;
                this._txtIniciales.IsEnabled = value;
                this._cbxRol.IsEnabled = value;
                this._cbxDepartamento.IsEnabled = value;
                this._txtEmail.IsEnabled = value;
                this._cbxAutorizar.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public object Roles
        {
            get { return this._cbxRol.DataContext; }
            set { this._cbxRol.DataContext = value; }
        }

        #endregion

        public ConsultarUsuario(object usuario)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarUsuario(this, usuario);
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarUsuario, "Eliminar Usuario", MessageBoxButton.YesNo, MessageBoxImage.Question))
                this._presentador.Eliminar();
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
