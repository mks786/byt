using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Contactos;
using Trascend.Bolet.Cliente.Presentadores.Contactos;

namespace Trascend.Bolet.Cliente.Ventanas.Contactos
{
    /// <summary>
    /// Interaction logic for ConsultarContacto.xaml
    /// </summary>
    public partial class ConsultarContacto : Page, IConsultarContacto
    {

        private PresentadorConsultarContacto _presentador;
        private bool _cargada;

        #region IConsultarContacto

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnModificar.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtNombre.IsEnabled = value;
                this._txtTelefono.IsEnabled = value;
                this._txtFax.IsEnabled = value;
                this._txtCargo.IsEnabled = value;
                this._txtCorrespondencia.IsEnabled = value;
                this._cbxDepartamento.IsEnabled = value;
                this._txtEmail.IsEnabled = value;
                this._cbxUso.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public object Contacto
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public string setFuncion
        {
            set
            {
                this._cbxUso.Text = value;
            }
        }

        public string getFuncion
        {
            get
            {
                if (!string.Equals("", this._cbxUso.Text))
                {
                    return ((string)this._cbxUso.Text);
                }
                return "";
            }
        }

        public string getCorrespondencia
        {
            get { return this._txtCorrespondencia.Text; }
        }

        public string setCorrespondencia
        {
            set { this._txtCorrespondencia.Text = value; }
        }

        public void mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        #endregion

        public ConsultarContacto(object contacto, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarContacto(this, contacto, ventanaPadre);

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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarContacto,
                "Eliminar Contacto", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
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
