using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Estados;
using Trascend.Bolet.Cliente.Presentadores.Estados;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Estados
{
    /// <summary>
    /// Interaction logic for ConsultarEstado.xaml
    /// </summary>
    public partial class ConsultarEstado : Page, IConsultarEstado
    {
        private PresentadorConsultarEstado _presentador;
        private bool _cargada;

        #region IConsultarEstado

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Estado
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
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


        public ConsultarEstado(object estado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarEstado(this, (Estado)estado);
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarEstado, "Eliminar Estado", MessageBoxButton.YesNo, MessageBoxImage.Question))
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