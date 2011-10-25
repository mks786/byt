using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.TipoInfoboles;
using Trascend.Bolet.Cliente.Presentadores.TipoInfoboles;

namespace Trascend.Bolet.Cliente.Ventanas.TipoInfoboles
{
    /// <summary>
    /// Interaction logic for AgregarTipoInfobol.xaml
    /// </summary>
    public partial class AgregarTipoInfobol : Page, IAgregarTipoInfobol
    {
        private PresentadorAgregarTipoInfobol _presentador;
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

        public object  TipoInfobol
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarTipoInfobol()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarTipoInfobol(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoInfobol();
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
