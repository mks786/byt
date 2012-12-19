using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Memorias;
using Trascend.Bolet.Cliente.Presentadores.Memorias;

namespace Trascend.Bolet.Cliente.Ventanas.Memorias
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class ConsultarMemoria : Page, IConsultarMemoria
    {

        private PresentadorConsultarMemoria _presentador;
        private bool _cargada;

        #region IConsultarMemoria

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Memoria
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object TipoMensaje
        {
            get { return this._cbxTipo.SelectedItem; }
            set { this._cbxTipo.SelectedItem = value; }
        }

        public object TiposMensajes
        {
            get { return this._cbxTipo.DataContext; }
            set { this._cbxTipo.DataContext = value; }
        }

        public object FormatoDocumento
        {
            get { return this._cbxTipoFinal.SelectedItem; }
            set { this._cbxTipoFinal.SelectedItem = value; }
        }

        public object FormatosDocumentos
        {
            get { return this._cbxTipoFinal.DataContext; }
            set { this._cbxTipoFinal.DataContext = value; }
        }

        public string SetPatente
        {
            set { this._txtPatente.Text = value; }
        }

        public void ArchivoNoEncontrado()
        {
            MessageBox.Show(Recursos.MensajesConElUsuario.ErrorArchivoMemoriaNoEncontrado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool HabilitarCampos
        {
            set
            {
                this._cbxTipoFinal.IsEnabled = value;
                this._cbxTipo.IsEnabled = value;
                this._txtRuta.IsEnabled = value;
                this._dpkFecha.IsEnabled = value;
            }
        }


        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion

        public ConsultarMemoria(object memoria, object patente)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarMemoria(this, memoria, patente);

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
                "Eliminar Memoria", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _btnVerMemoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerMemoria();
        }
    }
}
