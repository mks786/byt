using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Estados;
using Trascend.Bolet.Cliente.Presentadores.Estados;

namespace Trascend.Bolet.Cliente.Ventanas.Estados
{
    /// <summary>
    /// Interaction logic for AgregarEstado.xaml
    /// </summary>
    public partial class AgregarEstado : Page, IAgregarEstado
    {
        private PresentadorAgregarEstado _presentador;
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

        public object  Estado
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarEstado()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarEstado(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarEstado();
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
