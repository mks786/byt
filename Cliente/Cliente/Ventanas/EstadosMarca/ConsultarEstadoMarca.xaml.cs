using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.EstadosMarca;
using Trascend.Bolet.Cliente.Presentadores.EstadosMarca;

namespace Trascend.Bolet.Cliente.Ventanas.EstadosMarca
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class ConsultarEstadoMarca : Page, IConsultarEstadoMarca
    {

        private PresentadorConsultarEstadoMarca _presentador;
        private bool _cargada;

        #region IConsultarPais

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object EstadoMarca
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                //this._txtId.IsEnabled = value;
                this._txtDescripcion.IsEnabled = value;
                this._txtDescripcionIngles.IsEnabled = value;
            }
        }


        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion

        public ConsultarEstadoMarca(object estadoMarca)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarEstadoMarca(this, estadoMarca);

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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarPais,
                "Eliminar EstadoMarca", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
