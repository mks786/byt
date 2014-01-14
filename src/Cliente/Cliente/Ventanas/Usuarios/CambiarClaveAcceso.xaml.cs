using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Usuarios;
using Trascend.Bolet.Cliente.Presentadores.Usuarios;

namespace Trascend.Bolet.Cliente.Ventanas.Usuarios
{
    /// <summary>
    /// Lógica de interacción para CambiarClaveAcceso.xaml
    /// </summary>
    public partial class CambiarClaveAcceso : Page, ICambiarClaveAcceso
    {

        private bool _cargada;
        private PresentadorCambiarClaveAcceso _presentador;


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="usuario">Usuario logueado a cambiarle el password</param>
        public CambiarClaveAcceso(object usuario)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorCambiarClaveAcceso(this, usuario);
        }

        #region ICambiarClaveAcceso

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtPasswordNuevo.Focus();
        }

        public object Usuario
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string Password
        {
            get { return this._txtPasswordActual.Text; }
            set { this._txtPasswordActual.Text = value; }
        }

        public string NuevoPassword
        {
            get { return this._txtPasswordNuevo.Password; }
            set { this._txtPasswordNuevo.Password = value; }
        }

        public string NuevoPassword_Rep
        {
            get { return this._txtPasswordNuevoRep.Password; }
            set { this._txtPasswordNuevoRep.Password = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtNombreCompleto.IsEnabled = value;
                this._txtIniciales.IsEnabled = value;
                this._txtPasswordActual.IsEnabled = value;
                this._txtPasswordNuevo.IsEnabled = value;
                this._txtPasswordNuevoRep.IsEnabled = value;
            }
        }

        #endregion 

        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionCambioPassword,
                "Modificación de Contraseña de Acceso", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                this._presentador.Modificar();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampoNuevoPassword();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        #endregion
        

        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion
    }
}
