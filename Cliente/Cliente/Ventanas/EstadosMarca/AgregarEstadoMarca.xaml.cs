using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.EstadosMarca;
using Trascend.Bolet.Cliente.Presentadores.EstadosMarca;

namespace Trascend.Bolet.Cliente.Ventanas.EstadosMarca
{
    /// <summary>
    /// Interaction logic for AgregarEstadoMarca.xaml
    /// </summary>
    public partial class AgregarEstadoMarca : Page, IAgregarEstadoMarca
    {
        private PresentadorAgregarEstadoMarca _presentador;
        private bool _cargada;

        #region IAgregarEstado

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object  EstadoMarca
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarEstadoMarca()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarEstadoMarca(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarEstadoMarca();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
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
