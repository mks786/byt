using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Inventores;
using Trascend.Bolet.Cliente.Presentadores.Inventores;

namespace Trascend.Bolet.Cliente.Ventanas.Inventores
{
    /// <summary>
    /// Interaction logic for ConsultarInventor.xaml
    /// </summary>
    public partial class ConsultarInventor : Page, IConsultarInventor
    {

        private PresentadorConsultarInventor _presentador;
        private bool _cargada;

        #region IConsultarInventor

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnModificar.Focus();
        }

        public bool HabilitarCampos
        {
            set 
            {
                this._txtNombre.IsEnabled = value;
                this._cbxNacionalidad.IsEnabled = value;
                this._cbxPais.IsEnabled = value;
                this._txtDomicilio.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public object Inventor
        {
            get { return this._gridDatos.DataContext; }
            set{this._gridDatos.DataContext = value;}
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

        public object Nacionalidades
        {
            get { return this._cbxNacionalidad.DataContext; }
            set { this._cbxNacionalidad.DataContext = value; }
        }

        public object Nacionalidad
        {
            get { return this._cbxNacionalidad.SelectedItem; }
            set { this._cbxNacionalidad.SelectedItem = value; }
        }

         public void mensaje(string mensaje)
         {
             MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
         }


        #endregion

        public ConsultarInventor(object contacto)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarInventor(this, contacto);
            
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarInventor,
                "Eliminar Inventor", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
