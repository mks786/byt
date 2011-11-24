using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Agentes;
using Trascend.Bolet.Cliente.Presentadores.Agentes;

namespace Trascend.Bolet.Cliente.Ventanas.Agentes
{
    /// <summary>
    /// Interaction logic for ConsultarUsuario.xaml
    /// </summary>
    public partial class AgregarAgente : Page, IAgregarAgente
    {
        private PresentadorAgregarAgente _presentador;
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

        public object EstadosCivil
        {

            get { return this._cbxEstadoCivil.DataContext; }
            set { this._cbxEstadoCivil.DataContext = value; }
        }

        public object EstadoCivil
        {

            get { return this._cbxEstadoCivil.SelectedItem; }
            set { this._cbxEstadoCivil.SelectedItem = value; }
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

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarAgente()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarAgente(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
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
