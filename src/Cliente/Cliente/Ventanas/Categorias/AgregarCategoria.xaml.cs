using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Categorias;
using Trascend.Bolet.Cliente.Presentadores.Categorias;

namespace Trascend.Bolet.Cliente.Ventanas.Categorias
{
    /// <summary>
    /// Interaction logic for AgregarCategoria.xaml
    /// </summary>
    public partial class AgregarCategoria : Page, IAgregarCategoria
    {
        private PresentadorAgregarCategoria _presentador;
        private bool _cargada;

        #region IAgregarCategoria

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Categoria
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado 
        /// </summary>
        public AgregarCategoria()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarCategoria(this);
        }

        public AgregarCategoria(object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarCategoria(this, ventanaPadre);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCategoria();
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
