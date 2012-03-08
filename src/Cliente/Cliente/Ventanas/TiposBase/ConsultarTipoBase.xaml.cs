using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.TiposBase;
using Trascend.Bolet.Cliente.Presentadores.TiposBase;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.TiposBase
{
    /// <summary>
    /// Interaction logic for ConsultarTipoBase.xaml
    /// </summary>
    public partial class ConsultarTipoBase : Page, IConsultarTipoBase
    {
        private PresentadorConsultarTipoBase _presentador;
        private bool _cargada;

        #region IConsultarTipoBase

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object TipoBase
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set { this._txtDescripcion.IsEnabled = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion


        public ConsultarTipoBase(object estado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarTipoBase(this, (TipoBase)estado);
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarTipoBase, "Eliminar TipoBase", MessageBoxButton.YesNo, MessageBoxImage.Question))
                this._presentador.Eliminar();
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