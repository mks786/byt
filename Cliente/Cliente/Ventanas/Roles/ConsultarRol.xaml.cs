using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Roles;
using Trascend.Bolet.Cliente.Presentadores.Roles;

namespace Trascend.Bolet.Cliente.Ventanas.Roles
{
    /// <summary>
    /// Interaction logic for ConsultarRol.xaml
    /// </summary>
    public partial class ConsultarRol : Page, IConsultarRol
    {
        private PresentadorConsultarRol _presentador;
        private bool _cargada;

        #region IConsultarRol

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Rol
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

        public bool HabilitarCampos
        {
            set { this._txtDescripcion.IsEnabled = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion


        public ConsultarRol(object rol)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarRol(this, rol);
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarRol, "Eliminar Rol", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
