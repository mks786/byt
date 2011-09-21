using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Estatuses;
using Trascend.Bolet.Cliente.Presentadores.Estatuses;

namespace Trascend.Bolet.Cliente.Ventanas.Estatuses
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class ConsultarEstatus : Page, IConsultarEstatus
    {

        private PresentadorConsultarEstatus _presentador;
        private bool _cargada;

        #region IConsultarEstatus

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Estatus
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set 
            {
                this._txtDescripcion.IsEnabled = value;
                this._txtDescripcionIngles.IsEnabled = value;
                this._txtStatusProximoPaso.IsEnabled = value;
                this._txtStatusProximoPasoIngles.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion

        public ConsultarEstatus(object estatus)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarEstatus(this, estatus);
            
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarEstatus,
                "Eliminar Estatus", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
