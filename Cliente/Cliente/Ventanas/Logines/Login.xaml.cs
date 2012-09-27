using System.Windows;
using Trascend.Bolet.Cliente.Contratos.Logines;
using Trascend.Bolet.Cliente.Presentadores.Logines;
using Trascend.Bolet.Cliente.Ventanas.Principales;

namespace Trascend.Bolet.Cliente.Ventanas.Logines
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, ILogin
    {

        private PresentadorLogin _presentador;
        private bool _cargada;

        #region ILogin

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        { 
            this._txtLogin.Focus(); 
        }

        public string Id
        {
            get { return this._txtLogin.Text; }
        }

        public string Password
        {
            get { return this._txtPassword.Password; }
        }

        public string MensajeError
        {
            get { return this._txtMensajeError.Text; }
            set { this._txtMensajeError.Text = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public Login()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorLogin(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Salir();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Autenticacion();
            if(this.MensajeError.Equals(""))
                this.Close();
        }
    }
}
