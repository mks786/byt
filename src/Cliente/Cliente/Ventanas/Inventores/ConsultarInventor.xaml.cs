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
                this._txtTelefono.IsEnabled = value;
                this._txtFax.IsEnabled = value;
                this._txtCargo.IsEnabled = value;
                this._txtCorrespondencia.IsEnabled = value;
                this._cbxDepartamento.IsEnabled = value;
                this._txtEmail.IsEnabled = value;
                this._cbxUso.IsEnabled = value;
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

        public string getDepartamento
        {
            get
            {
                if (!string.Equals("",this._cbxDepartamento.Text)) 
                {
                    return ((string)this._cbxDepartamento.Text);
                }
                return "";
            }
        }

         public string setDepartamento
        {
            set
            {
                this._cbxDepartamento.Text = value ; 
            }
        }

         public string setFuncion
         {
             set
             {
                 this._cbxUso.Text = value;
             }
         }

         public string getFuncion
         {
             get
             {
                 if (!string.Equals("", this._cbxUso.Text))
                 {
                     return ((string)this._cbxUso.Text);
                 }
                 return "";
             }
         }

         public string getCorrespondencia
         {
             get { return this._txtCorrespondencia.Text; }
         }

         public string setCorrespondencia
         {
             set { this._txtCorrespondencia.Text = value; }
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
