using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.FechasPatente;
using Trascend.Bolet.Cliente.Presentadores.FechasPatente;

namespace Trascend.Bolet.Cliente.Ventanas.FechasPatente
{
    /// <summary>
    /// Interaction logic for ConsultarInventor.xaml
    /// </summary>
    public partial class ConsultarFecha : Page, IConsultarFecha
    {

        private PresentadorConsultarFecha _presentador;
        private bool _cargada;

        #region IConsultarFecha

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
                this._txtFecha.IsEnabled = value;
                this._txtCorrespondencia.IsEnabled = value;
                //this._cbxTipo.IsEnabled = value;
                this._txtComentario.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public object FechaPatente
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
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

        public string Correspondencia
        {
            get { return this._txtCorrespondencia.Text; }
            set { this._txtCorrespondencia.Text = value; }
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

         public void mensaje(string mensaje)
         {
             MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
         }


        #endregion

        public ConsultarFecha(object fecha)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarFecha(this, fecha);
            
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
