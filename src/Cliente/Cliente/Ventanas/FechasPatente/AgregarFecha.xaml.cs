using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.FechasPatente;
using Trascend.Bolet.Cliente.Presentadores.FechasPatente;

namespace Trascend.Bolet.Cliente.Ventanas.FechasPatente
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class AgregarFecha : Page, IAgregarFecha
    {

        private PresentadorAgregarFecha _presentador;
        private bool _cargada;

        #region IAgregarFecha

        //public void borrarId()
        //{
        //    this._txtId.Text = string.Empty;
        //}

        public object FechaPatente
        {
            get{return this._gridDatos.DataContext;}
            set{this._gridDatos.DataContext = value;}
        }

        public object Tipos
        {
            get { return this._cbxTipo.DataContext; }
            set { this._cbxTipo.DataContext = value; }
        }

        public object Tipo
        {
            get { return this._cbxTipo.SelectedItem; }
            set { this._cbxTipo.SelectedItem = value; }
        }

        public object Correspondencias
        {
            get { return this._cbxCorrespondencia.DataContext; }
            set { this._cbxCorrespondencia.DataContext = value; }
        }

        public object Correspondencia
        {
            get { return this._cbxCorrespondencia.SelectedItem; }
            set { this._cbxCorrespondencia.SelectedItem = value; }
        }

        public string Comentario
        {
            get { return this._txtComentario.Text; }
            set { this._txtComentario.Text = value; }
        }

        public string TimeStamp
        {
            get { return this._dpkTimeStamp.Text; }
            set { this._dpkTimeStamp.Text = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtFecha.Focus();
        }

        public void mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarFecha(object fecha)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarFecha(this, fecha);
            
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
