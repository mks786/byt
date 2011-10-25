using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Internacionales;
using Trascend.Bolet.Cliente.Presentadores.Internacionales;

namespace Trascend.Bolet.Cliente.Ventanas.Internacionales
{
    /// <summary>
    /// Interaction logic for AgregarInternacional.xaml
    /// </summary>
    public partial class AgregarInternacional : Page, IAgregarInternacional
    {
        private PresentadorAgregarInternacional _presentador;
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

        public object  Internacional
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarInternacional()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarInternacional(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarInternacional();
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

        private void _txtId_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((int)e.Key == 2)
                e.Handled = false;
            else if ((int)e.Key >= 43 || (int)e.Key <= 34)
                e.Handled = true;
            else
                e.Handled = false;
        }

   
    }
}
