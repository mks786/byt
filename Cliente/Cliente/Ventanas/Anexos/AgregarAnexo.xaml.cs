using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Anexos;
using Trascend.Bolet.Cliente.Presentadores.Anexos;

namespace Trascend.Bolet.Cliente.Ventanas.Anexos
{
    /// <summary>
    /// Interaction logic for AgregarAnexo.xaml
    /// </summary>
    public partial class AgregarAnexo : Page, IAgregarAnexo
    {
        private PresentadorAgregarAnexo _presentador;
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

        public object  Anexo
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarAnexo()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarAnexo(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarAnexo();
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
