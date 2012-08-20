using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Inventores;
using Trascend.Bolet.Cliente.Presentadores.Inventores;

namespace Trascend.Bolet.Cliente.Ventanas.Inventores
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class AgregarInventor : Page, IAgregarInventor
    {

        private PresentadorAgregarInventor _presentador;
        private bool _cargada;

        #region IAgregarInventor

        //public void borrarId()
        //{
        //    this._txtId.Text = string.Empty;
        //}

        public object Inventor
        {
            get{return this._gridDatos.DataContext;}
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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNombre.Focus();
        }

        public void mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarInventor(object inventor)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarInventor(this, inventor);
            
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
                //this._presentador.Eliminar();
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

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._btnAceptar.Focus();
            this._presentador.Aceptar();
        }



    }
}
