using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Estatuses;
using Trascend.Bolet.Cliente.Presentadores.Estatuses;

namespace Trascend.Bolet.Cliente.Ventanas.Estatuses
{
    /// <summary>
    /// Interaction logic for ConsultarUsuario.xaml
    /// </summary>
    public partial class AgregarEstatus : Page, IAgregarEstatus
    {
        private PresentadorAgregarEstatus _presentador;
        private bool _cargada;

        #region IConsultarEstatus

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Estatus
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        #endregion

        public AgregarEstatus()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarEstatus(this);
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
