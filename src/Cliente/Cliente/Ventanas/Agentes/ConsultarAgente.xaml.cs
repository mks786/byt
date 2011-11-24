using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Agentes;
using Trascend.Bolet.Cliente.Presentadores.Agentes;

namespace Trascend.Bolet.Cliente.Ventanas.Agentes
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class ConsultarAgente : Page, IConsultarAgente
    {

        private PresentadorConsultarAgente _presentador;
        private bool _cargada;

        #region IConsultarAgente

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Agente
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set 
            { 
                this._txtNombre.IsEnabled = value;
                this._txtDomicilio.IsEnabled = value;
                this._txtTelefono.IsEnabled = value;
                this._cbxEstadoCivil.IsEnabled = value;
                this._cbxSexo.IsEnabled = value;
                this._txtNumeroAbogado.IsEnabled = value;
                this._txtNumeroImpresoAbogado.IsEnabled = value;
                this._txtNumeroPropiedad.IsEnabled = value;
                this._txtCCI.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public char GetEstadoCivil
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxEstadoCivil.Text))
                    return ((string)this._cbxEstadoCivil.Text)[0];
                else
                    return ' ';
            }
        }

        public object Sexo
        {
            get { return this._cbxSexo.SelectedItem; }
            set { this._cbxSexo.SelectedItem = value; }
        }

        public object Sexos
        {
            get { return this._cbxSexo.DataContext; }
            set { this._cbxSexo.DataContext = value; }
        }

        public string SetEstadoCivil
        {
            set { this._cbxEstadoCivil.Text = value; }
        }

        #endregion

        public ConsultarAgente(object agente)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarAgente(this, agente);
            
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
