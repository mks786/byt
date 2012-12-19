using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Corresponsales;
using Trascend.Bolet.Cliente.Presentadores.Corresponsales;

namespace Trascend.Bolet.Cliente.Ventanas.Corresponsales
{
    /// <summary>
    /// Interaction logic for ConsultarUsuario.xaml
    /// </summary>
    public partial class AgregarCorresponsal : Page, IAgregarCorresponsal
    {
        private PresentadorAgregarCorresponsal _presentador;
        private bool _cargada;

        #region IConsultarCorresponsal

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Corresponsal
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object Paises
        {

            get { return this._cbxPais.DataContext; }
            set { this._cbxPais.DataContext = value; }
        }

        public object Pais
        {

            get { return this._cbxPais.SelectedItem; }
            set { this._cbxPais.SelectedItem = value; }
        }

        public object Idioma
        {
            get { return this._cbxIdioma.SelectedItem; }
            set { this._cbxIdioma.SelectedItem = value; }
        }

        public object Idiomas
        {
            get { return this._cbxIdioma.DataContext; }
            set { this._cbxIdioma.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void MostrarBotones(string texto) 
        {
            this._btnAuditoria.Visibility = Visibility.Visible;
            this._btnContactos.Visibility = Visibility.Visible;
            this._txbCancelar.Text = texto;
        }

        #endregion

        public AgregarCorresponsal(object ventanaPadre,object corresponsal)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarCorresponsal(this, ventanaPadre, corresponsal);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
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

        private void _btnContactos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Eliminar();
        }
    }
}
