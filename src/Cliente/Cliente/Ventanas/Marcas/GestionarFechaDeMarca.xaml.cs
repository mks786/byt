using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using System.ComponentModel;
using System.Threading;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Lógica de interacción para GestionarFechaDeMarca.xaml
    /// </summary>
    public partial class GestionarFechaDeMarca : Page, IGestionarFechaDeMarca
    {

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorGestionarFechaDeMarca _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();
        
        #region Constructores

        public GestionarFechaDeMarca(object fechaMarca, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFechaDeMarca(this, fechaMarca, ventanaPadre);

            
            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
           
        }

        public GestionarFechaDeMarca(object fechaMarca, object ventanaPadre, object ventanaPadreLista)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFechaDeMarca(this, fechaMarca, ventanaPadre, ventanaPadreLista);


            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);

        } 

        #endregion



        #region IGestionarFechaDeMarca

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object FechaMarca
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object TiposDeFechas
        {
            get { return this._cbxTipoFecha.DataContext; }
            set { this._cbxTipoFecha.DataContext = value; }
        }

        public object TipoDeFecha
        {
            get { return this._cbxTipoFecha.SelectedItem; }
            set { this._cbxTipoFecha.SelectedItem = value; }
        }

        public string IdCorrespondencia
        {
            get { return this._txtCorrespondencia.Text; }
            set { this._txtCorrespondencia.Text = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnAceptar.Focus();
        }

        #endregion



        #region Eventos

        void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _bgw.ReportProgress(1);
            Thread.Sleep(2000);
            
        }

        void bgw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this._txtMensaje.Text = "Operacion realizada exitosamente";
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //this._presentador.irListaInfoBol();
            this._presentador.irListaFechasDeMarca();
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
            if (this._presentador.Aceptar())
            {
                _bgw.RunWorkerAsync();
            }
        }


        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarFechaMarca,
                "Eliminar Fecha de Marca", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                if (this._presentador.Eliminar())
                {
                    _bgw.RunWorkerAsync();
                }
            }
        }


        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }
        
        #endregion

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        

        
    }
}
