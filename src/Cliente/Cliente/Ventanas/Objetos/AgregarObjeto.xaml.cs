using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Objetos;
using Trascend.Bolet.Cliente.Presentadores.Objetos;

namespace Trascend.Bolet.Cliente.Ventanas.Objetos
{
    /// <summary>
    /// Interaction logic for AgregarObjeto.xaml
    /// </summary>
    public partial class AgregarObjeto : Page, IAgregarObjeto
    {
        private PresentadorAgregarObjeto _presentador;
        private bool _cargada;

        #region IAgregarObjeto

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Objeto
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public string Descripcion
        {
            get { return this._txtDescripcion.Text; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarObjeto()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarObjeto(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarObjeto();
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
