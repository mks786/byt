using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using System.ComponentModel;
using System.Threading;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for AgregarObjeto.xaml
    /// </summary>
    public partial class GestionarInstruccionDeRenovacion : Page, IGestionarInstruccionDeRenovacion
    {
        private PresentadorGestionarIntruccionRenovacion _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();

        #region IGestionarInstruccionDeRenovacion

        public void OcultarControlesAlAgregar()
        {
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            //this._btnVerCorrespondencia.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void FocoPredeterminado()
        {
            this._dpkFecha.Focus();
        }

        public object InstruccionDeRenovacion
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string IdCorrespondencia
        {
            get { return this._txtIdCorrespondencia.Text; }
            set { this._txtIdCorrespondencia.Text = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbAceptar.Text; }
            set { this._txbAceptar.Text = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                //this._txtIdMarca.IsEnabled = value;
                this._txtIdCorrespondencia.IsEnabled = value;

                this._dpkFecha.IsEnabled = value;

            }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }


        public bool Mensaje(string mensaje)
        {
            this._txtMensaje.Text = mensaje;
            return true;
        }

        public void BorrarValorMinimo()
        {
            this._txtIdCorrespondencia.Text = "";
        }

        public void Alerta()
        {
            MessageBox.Show(Recursos.MensajesConElUsuario.ErrorCartaNoEncontrada,
                "Carta Inexistente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        #endregion

        public GestionarInstruccionDeRenovacion(object instruccion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarIntruccionRenovacion(this, instruccion);

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            if (this._presentador.Aceptar())
            {
                _bgw.RunWorkerAsync();
            }
        }

        void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _bgw.ReportProgress(1);
            Thread.Sleep(2000);
        }

        void bgw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this._txtMensaje.Text = "Operación realizada exitósamente.";
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this._presentador.IrListaInstruccionesDeRenovacion();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaInstruccionesDeRenovacion();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarInstruccion,
                "Eliminar Instrucción de Renovación", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                if (this._presentador.Eliminar())
                {
                    _bgw.RunWorkerAsync();
                }
            }
        }

        private void _dpkFechaBusquedaPalabra_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _btnVerCorrespondencia_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCarta();
        }

    }
}
