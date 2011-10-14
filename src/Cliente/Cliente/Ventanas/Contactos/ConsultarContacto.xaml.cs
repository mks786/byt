using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Contactos;
using Trascend.Bolet.Cliente.Presentadores.Contactos;

namespace Trascend.Bolet.Cliente.Ventanas.Contactos
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
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
            this._txtNombre.Focus();
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
                this._txtFuncion.IsEnabled = value;
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
            set{this._gridDatos.DataContext = value;}
        }

        #endregion

        public ConsultarContacto(object contacto)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarContacto(this, contacto);
            
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarAgente,
                "Eliminar Agente", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
