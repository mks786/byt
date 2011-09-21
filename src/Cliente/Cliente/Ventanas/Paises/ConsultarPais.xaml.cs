using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Paises;
using Trascend.Bolet.Cliente.Presentadores.Paises;

namespace Trascend.Bolet.Cliente.Ventanas.Paises
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class ConsultarPais : Page, IConsultarPais
    {

        private PresentadorConsultarPais _presentador;
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

        public bool HabilitarCampos
        {
            set 
            {
                this._txtCodigo.IsEnabled = value;
                this._txtNombreIngles.IsEnabled = value;
                this._txtNombreEspanol.IsEnabled = value;
                this._txtNacionalidad.IsEnabled = value;
                this._txtRegion.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion

        public ConsultarPais(object pais)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarPais(this, pais);
            
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarPais,
                "Eliminar Pais", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
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
