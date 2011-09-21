using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.TipoFechas;
using Trascend.Bolet.Cliente.Presentadores.TipoFechas;

namespace Trascend.Bolet.Cliente.Ventanas.TipoFechas
{
    /// <summary>
    /// Interaction logic for AgregarTipoFecha.xaml
    /// </summary>
    public partial class AgregarTipoFecha : Page, IAgregarTipoFecha
    {
        private PresentadorAgregarTipoFecha _presentador;
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

        public object  TipoFecha
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        #endregion

        public AgregarTipoFecha()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarTipoFecha(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoFecha();
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
