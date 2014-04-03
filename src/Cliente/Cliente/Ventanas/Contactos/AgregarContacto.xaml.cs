using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Contactos;
using Trascend.Bolet.Cliente.Presentadores.Contactos;

namespace Trascend.Bolet.Cliente.Ventanas.Contactos
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class AgregarContacto : Page, IAgregarContacto
    {

        private PresentadorAgregarContacto _presentador;
        private bool _cargada;
        private bool _regresarRefresca;

        #region IAgregarContacto

        public void borrarId()
        {
            this._txtNumero.Text = string.Empty;
        }

        public object Contacto
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
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

        public void AsignarAsociado(int id, string nombre)
        {
            this._txtIdAsociado.Text = id.ToString();
            this._txtNombreAsociado.Text = nombre;
        }

        #endregion

        public AgregarContacto(object asociado, object ventanaPadre, bool regresarRefresca)
        {
            InitializeComponent();
            this._cargada = false;
            this._regresarRefresca = regresarRefresca;
            this._presentador = new PresentadorAgregarContacto(this, asociado, ventanaPadre, null, regresarRefresca);

        }

        public AgregarContacto(object asociado, object ventanaPadre, object ventanaPrevia, bool regresarRefresca)
        {
            InitializeComponent();
            this._cargada = false;
            this._regresarRefresca = regresarRefresca;
            this._presentador = new PresentadorAgregarContacto(this, asociado, ventanaPadre, ventanaPrevia, regresarRefresca);

        }

        #region Eventos

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadreContacto();
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

        private void _btnIrCorrespondencia_Click(object sender, RoutedEventArgs e)
        {
            if (!this._txtCorrespondencia.Text.Equals(string.Empty))
            {
                this._presentador.ConsultarCarta();
            }
        }


        #endregion

        



    }
}
