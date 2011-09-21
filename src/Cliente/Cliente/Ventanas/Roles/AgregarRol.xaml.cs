using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Roles;
using Trascend.Bolet.Cliente.Presentadores.Roles;

namespace Trascend.Bolet.Cliente.Ventanas.Roles
{
    /// <summary>
    /// Interaction logic for AgregarObjeto.xaml
    /// </summary>
    public partial class AgregarRol : Page, IAgregarRol
    {
        private PresentadorAgregarRol _presentador;
        private bool _cargada;

        #region IAgregarRol

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object  Rol
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public string Descripcion
        {
            get { return this._txtDescripcion.Text; }
        }

        #endregion

        public AgregarRol()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarRol(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarRol();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
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
