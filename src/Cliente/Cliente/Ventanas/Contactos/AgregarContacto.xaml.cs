using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Contactos;
using Trascend.Bolet.Cliente.Presentadores.Contactos;

namespace Trascend.Bolet.Cliente.Ventanas.Contactos
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class AgregarContacto : Page, IAgregarContacto
    {

        private PresentadorAgregarContacto _presentador;
        private bool _cargada;

        #region IAgregarContacto

        public void borrarId()
        {
            this._txtNumero.Text = string.Empty;
        }
        public object Contacto
        {
            get{return this._gridDatos.DataContext;}
            set{this._gridDatos.DataContext = value;}
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNumero.Focus();
        }
        #endregion

        public AgregarContacto(object contacto)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarContacto(this, contacto);
            
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
                //this._presentador.Eliminar();
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

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }


    }
}
