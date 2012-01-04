using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using System.Threading;
using System.ComponentModel;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for GestionarAnaqua.xaml
    /// </summary>
    public partial class GestionarAnaqua : Page, IGestionarAnaqua
    {
        private PresentadorGestionarAnaqua _presentador;
        private bool _cargada;
        BackgroundWorker bgw = new BackgroundWorker();

        #region IGestionarAnaqua
        public void FocoPredeterminado()
        {
            this._txtIdAnaqua.Focus();
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
                this._txtIdAnaqua.IsEnabled = value;
                this._txtRegistro.IsEnabled = value;
                this._txtSolicitud.IsEnabled = value;
                this._txtComentario.IsEnabled = value;
                this._txtBKId.IsEnabled = value;
                this._txtDistingueIngles.IsEnabled = value;
            }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }
        
        public object Anaqua
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public GestionarAnaqua(object marca)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarAnaqua(this, marca);

            bgw.WorkerReportsProgress = true;
            bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }
        void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bgw.ReportProgress(1);
            Thread.Sleep(2000);
        }

        void bgw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this._txtMensaje.Text = "Operación realizada exitósamente.";
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void ejecutarTransaccion()
        {
            bgw.RunWorkerAsync();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.Aceptar())
            {
                ejecutarTransaccion();
            }
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
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
