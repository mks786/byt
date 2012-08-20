using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Objetos;
using Trascend.Bolet.Cliente.Presentadores.Objetos;
using System.Windows.Input;

namespace Trascend.Bolet.Cliente.Ventanas.Objetos
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class ConsultarObjeto : Page, IConsultarObjeto
    {

        private PresentadorConsultarObjeto _presentador;
        private bool _cargada;

        #region IConsultarObjeto

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Objeto
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

        public ConsultarObjeto(object objeto)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarObjeto(this, objeto);
            
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarObjeto, "Eliminar Objeto", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        /*private void Page_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                object elemento = Keyboard.FocusedElement;
                if (elemento.GetType().Equals(typeof(RichTextBox)))
                {
                    RichTextBox campo = (RichTextBox)elemento;
                    campo.Height = 50;
                }
            }
        }*/
    }
}
