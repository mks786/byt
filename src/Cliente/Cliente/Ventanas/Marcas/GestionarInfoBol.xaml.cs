using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for AgregarObjeto.xaml
    /// </summary>
    public partial class GestionarInfoBol : Page
    {
        //private PresentadorGestionarInfoAdicional _presentador;
        private bool _cargada;

        //#region IAgregarInfoAdicional


        //public void FocoPredeterminado()
        //{
        //    this._txtNombre.Focus();
        //}

        //public string TextoBotonModificar
        //{
        //    get { return this._txbAceptar.Text; }
        //    set { this._txbAceptar.Text = value; }
        //}

        //public bool HabilitarCampos
        //{
        //    set
        //    {
        //        this._txtNombre.IsEnabled = value;
        //        this._txtEmail.IsEnabled = value;
        //        this._txtInfo.IsEnabled = value;
        //    }
        //}

        //public bool EstaCargada
        //{
        //    get { return this._cargada; }
        //    set { this._cargada = value; }
        //}

        //public object InfoAdicional
        //{
        //    get { return this._gridDatos.DataContext; }
        //    set { this._gridDatos.DataContext = value; }
        //}

        //public bool Mensaje(string mensaje)
        //{
        //    this._txtMensaje.Text = mensaje;
        //    return true;
        //}

        //#endregion

        public GestionarInfoBol(object marca)
        {
            InitializeComponent();
            this._cargada = false;
            //this._presentador = new PresentadorGestionarInfoAdicional(this, marca);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Aceptar();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Regresar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //if (!EstaCargada)
            //{
            //    this._presentador.CargarPagina();
            //    EstaCargada = true;
            //}
        }
    }
}
