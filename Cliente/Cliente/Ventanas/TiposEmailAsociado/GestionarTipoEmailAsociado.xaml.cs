using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.TiposEmailAsociado;
using Trascend.Bolet.Cliente.Presentadores.TiposEmailAsociado;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.TiposEmailAsociado
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class GestionarTipoEmailAsociado : Page, IGestionarTipoEmailAsociado
    {

        private PresentadorGestionarTipoEmailAsociado _presentador;
        private bool _cargada;

        #region IGestionarTipoEmailAsociado

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object TipoEmailAsociado
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtId.IsEnabled = value;
                this._txtDescripcion.IsEnabled = value;
                this._txtFuncion.IsEnabled = value;
                this._cbxDepartamento.IsEnabled = value;
            }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public void PintarAuditoria()
        {
            this._btnAuditoria.Visibility = Visibility.Visible;
            this._btnAuditoria.Background = Brushes.LightGreen;
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public GestionarTipoEmailAsociado(object tipoEmail, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarTipoEmailAsociado(this, tipoEmail, ventanaPadre);

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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarTipoEmail,
                "Eliminar Tipo Email", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }
    }
}
