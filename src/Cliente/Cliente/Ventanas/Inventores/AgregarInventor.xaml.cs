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

        public void borrarId()
        {
            this._txtNumero.Text = string.Empty;
        }

        public object Inventor
        {
            get{return this._gridDatos.DataContext;}
            set{this._gridDatos.DataContext = value;}
        }

        public string getDepartamento
        {
            get
            {
                if (!string.Equals("", this._cbxDepartamento.Text))
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
                this._cbxDepartamento.Text = value;
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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNumero.Focus();
        }

        public void mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarInventor(object asociado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarInventor(this, asociado);
            
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarAgente,
                "Eliminar Agente", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
