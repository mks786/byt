using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Memorias;
using Trascend.Bolet.Cliente.Presentadores.Memorias;

namespace Trascend.Bolet.Cliente.Ventanas.Memorias
{
    /// <summary>
    /// Interaction logic for AgregarMemoria.xaml
    /// </summary>
    public partial class AgregarMemoria : Page, IAgregarMemoria
    {
        private PresentadorAgregarMemoria _presentador;
        private bool _cargada;

        #region IAgregarMemoria

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

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarMemoria(object patente)
        {
            InitializeComponent();
            this._cargada = false;
            this._dpkFecha.Text = string.Empty;
            this._presentador = new PresentadorAgregarMemoria(this, patente);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMemoria();
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

        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }



    }
}
