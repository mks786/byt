using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Presentadores.Patentes;
using System.ComponentModel;
using System.Threading;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Patentes
{
    /// <summary>
    /// Interaction logic for AgregarObjeto.xaml
    /// </summary>
    public partial class GestionarInfoBol : Page, IGestionarInfoBol
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorGestionarInfoBol _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();

        #region IAgregarInfoBol

        public void OculatarControlesAlAgregar()
        {
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            this._cbxTipo.IsEnabled = true;
        }

        public void FocoPredeterminado()
        {
            this._cbxTipo.Focus();
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
                this._cbxBoletin.IsEnabled = value;
                this._txtComentario.IsEnabled = value;
                this._txtPagina.IsEnabled = value;
                this._txtResolucion.IsEnabled = value;
                this._cbxTomo.IsEnabled = value;
                this._dpkFecha.IsEnabled = value;
            }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object InfoBol
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object Boletines
        {
            get { return this._cbxBoletin.DataContext; }
            set { this._cbxBoletin.DataContext = value; }
        }

        public object Boletin
        {
            get { return this._cbxBoletin.SelectedItem; }
            set { this._cbxBoletin.SelectedItem = value; }
        }

        public object Tomos
        {
            get { return this._cbxTomo.DataContext; }
            set { this._cbxTomo.DataContext = value; }
        }

        public object Tomo
        {
            get { return this._cbxTomo.SelectedItem; }
            set { this._cbxTomo.SelectedItem = value; }
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

        public bool Mensaje(string mensaje)
        {
            this._txtMensaje.Text = mensaje;
            return true;
        }

        public GridViewColumnHeader CurSortCol
        {
            get { return _CurSortCol; }
            set { _CurSortCol = value; }
        }

        public SortAdorner CurAdorner
        {
            get { return _CurAdorner; }
            set { _CurAdorner = value; }
        }

        #endregion

        public GestionarInfoBol(object infoBol)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarInfoBol(this, infoBol);

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
            this._presentador.irListaInfoBol();
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

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarInfoBol,
                "Eliminar InfoBol", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                if (this._presentador.Eliminar())
                {
                    _bgw.RunWorkerAsync();
                }
            }
        }

        private void _cbxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CargarCambio();
        }

    }
}
