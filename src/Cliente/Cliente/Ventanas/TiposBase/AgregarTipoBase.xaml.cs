using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.TiposBase;
using Trascend.Bolet.Cliente.Presentadores.TiposBase;

namespace Trascend.Bolet.Cliente.Ventanas.TiposBase
{
    /// <summary>
    /// Interaction logic for AgregarTipoBase.xaml
    /// </summary>
    public partial class AgregarTipoBase : Page, IAgregarTipoBase
    {
        private PresentadorAgregarTipoBase _presentador;
        private bool _cargada;

        #region IAgregarTipoBase

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object  TipoBase
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarTipoBase()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarTipoBase(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoBase();
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
