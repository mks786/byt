using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Paises;
using Trascend.Bolet.Cliente.Presentadores.Paises;

namespace Trascend.Bolet.Cliente.Ventanas.Paises
{
    /// <summary>
    /// Interaction logic for ConsultarUsuario.xaml
    /// </summary>
    public partial class AgregarPais : Page, IAgregarPais
    {
        private PresentadorAgregarPais _presentador;
        private bool _cargada;

        #region IConsultarPais

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Pais
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        #endregion

        public AgregarPais()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarPais(this);
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
