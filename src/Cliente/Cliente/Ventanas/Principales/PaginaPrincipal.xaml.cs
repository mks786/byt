using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Principales;
using System.Windows.Media;
using Trascend.Bolet.Cliente.Presentadores.Principales;
using System;

namespace Trascend.Bolet.Cliente.Ventanas.Principales
{
    /// <summary>
    /// Interaction logic for PaginaPrincipal.xaml
    /// </summary>
    public partial class PaginaPrincipal : Page, IPaginaPrincipal
    {
        private bool _cargada;
        private PresentadorPaginaPrincipal _presentador;

        #region IPaginaPrincipal

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            //this._txtId.Focus();
        }


        /// <summary>
        /// Propiedad del mensaje de la pantalla principal de error
        /// </summary>
        public string MensajeError
        {
            get { return this._txtMensajeError.Text; }
            set
            {
                this._txtMensajeUsuario.Text = "";
                this._txtMensajeError.Text = value;
            }
        }

        /// <summary>
        /// Propiedad del mensaje de la pantalla principal con el usuario
        /// </summary>
        public string MensajeUsuario
        {
            get { return this._txtMensajeUsuario.Text; }
            set
            {
                this._txtMensajeError.Text = "";
                this._txtMensajeUsuario.Text = value;
            }
        }

        #endregion

        #region Singleton

        private static PaginaPrincipal _instancia;

        /// <summary>
        /// Propiedad que devuelve la instancia de la clase
        /// </summary>
        public static PaginaPrincipal ObtenerInstancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new PaginaPrincipal();
                }
                return _instancia;
            }
        }

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        private PaginaPrincipal()
        {
            InitializeComponent();
            _cargada = false;
            this._presentador = new PresentadorPaginaPrincipal(this);
        }

        #endregion

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this._presentador.CargarPagina();
        }

    }
}
