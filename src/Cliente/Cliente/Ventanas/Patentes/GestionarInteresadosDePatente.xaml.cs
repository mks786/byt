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
    /// Lógica de interacción para GestionarInteresadosDePatente.xaml
    /// </summary>
    public partial class GestionarInteresadosDePatente : Page, IGestionarInteresadosDePatente
    {

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorGestionarInteresadosDePatente _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();
        
        #region IGestionarInteresadosDePatente

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnAceptar.Focus();
        }


        public object Patente
        {
            get { return this._gridDatosPatente.DataContext; }
            set { this._gridDatosPatente.DataContext = value; }
        }

        //Datos de Interesado1

        public string IdInteresado1
        {
            get { return this._txtIdInteresado1.Text; }
            set { this._txtIdInteresado1.Text = value; }
        }

        public string NombreInteresado1
        {
            get { return this._txtInteresado1.Text; }
            set { this._txtInteresado1.Text = value; }
        }

        public string IdInteresado1Filtrar
        {
            get { return this._txtIdInteresado1Filtrar.Text; }
        }

        public string NombreInteresado1Filtrar
        {
            get { return this._txtNombreInteresado1Filtrar.Text; }
        }

        public object Interesados1
        {
            get { return this._lstInteresados1.DataContext; }
            set { this._lstInteresados1.DataContext = value; }
        }


        public object Interesado1
        {
            get { return this._lstInteresados1.SelectedItem; }
            set { this._lstInteresados1.SelectedItem = value;} 
        }

        //Datos de Interesado2

        public string IdInteresado2
        {
            get { return this._txtIdInteresado2.Text; }
            set { this._txtIdInteresado2.Text = value; }
        }

        public string NombreInteresado2
        {
            get { return this._txtInteresado2.Text; }
            set { this._txtInteresado2.Text = value; }
        }

        public string IdInteresado2Filtrar
        {
            get { return this._txtIdInteresado2Filtrar.Text; }
        }

        public string NombreInteresado2Filtrar
        {
            get { return this._txtNombreInteresado2Filtrar.Text; }
        }

        public object Interesados2
        {
            get { return this._lstInteresados2.DataContext; }
            set { this._lstInteresados2.DataContext = value; }
        }


        public object Interesado2
        {
            get { return this._lstInteresados2.SelectedItem; }
            set { this._lstInteresados2.SelectedItem = value; }
        }

        //Datos de Interesado3

        public string IdInteresado3
        {
            get { return this._txtIdInteresado3.Text; }
            set { this._txtIdInteresado3.Text = value; }
        }

        public string NombreInteresado3
        {
            get { return this._txtInteresado3.Text; }
            set { this._txtInteresado3.Text = value; }
        }

        public string IdInteresado3Filtrar
        {
            get { return this._txtIdInteresado3Filtrar.Text; }
        }

        public string NombreInteresado3Filtrar
        {
            get { return this._txtNombreInteresado3Filtrar.Text; }
        }

        public object Interesados3
        {
            get { return this._lstInteresados3.DataContext; }
            set { this._lstInteresados3.DataContext = value; }
        }


        public object Interesado3
        {
            get { return this._lstInteresados3.SelectedItem; }
            set { this._lstInteresados3.SelectedItem = value; }
        }

        public bool Mensaje(string mensaje)
        {
            this._txtMensaje.Text = mensaje;
            return true;
        }

        #endregion


        #region Constructores

        public GestionarInteresadosDePatente(object patente, object ventanaPadre, object ventanaGestionarPatente, object ventanaConsultarPatente)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarInteresadosDePatente
                (this,patente,ventanaPadre,ventanaGestionarPatente,ventanaConsultarPatente);

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }

        public GestionarInteresadosDePatente(object patente, object interesadoPatente, object ventanaPadre, object ventanaGestionarPatente, object ventanaConsultarPatentes)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarInteresadosDePatente
                (this,patente,interesadoPatente,ventanaPadre,ventanaGestionarPatente, ventanaConsultarPatentes);

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }


 
        #endregion

        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _txtInteresado1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresado1.IsDefault = true;
            MostrarLstInteresado1();
        }

        private void _txtInteresado2_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresado2.IsDefault = true;
            MostrarLstInteresado2();
        }

        private void _txtInteresado3_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresado3.IsDefault = true;
            MostrarLstInteresado3();
        }

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            string nombreBotonConsulta = string.Empty;
            Button botonPresionado = (Button)sender;
            nombreBotonConsulta = botonPresionado.Name;
            this._presentador.BuscarInteresadoAdicional(nombreBotonConsulta);
        }

        private void _lstInteresados1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView listaSeleccionada = (ListView)sender;
            string nombreListView = listaSeleccionada.Name;
            this._presentador.CambiarInteresado(nombreListView);
            OcultarLstInteresado1();

            this._btnConsultarInteresado1.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _lstInteresados2_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView listaSeleccionada = (ListView)sender;
            string nombreListView = listaSeleccionada.Name;
            this._presentador.CambiarInteresado(nombreListView);
            OcultarLstInteresado2();

            this._btnConsultarInteresado2.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _lstInteresados3_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView listaSeleccionada = (ListView)sender;
            string nombreListView = listaSeleccionada.Name;
            this._presentador.CambiarInteresado(nombreListView);
            OcultarLstInteresado3();

            this._btnConsultarInteresado3.IsDefault = false;
            this._btnAceptar.IsDefault = true;
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
            this._txtMensaje.Text = "Operación realizada exitosamente";
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this._presentador.irListaInteresadosPatente();
        }
        
        #endregion

        

        

        #region Metodos

        public void ConvertirEnteroMinimoABlanco()
        {
            if (null != this.Interesado1)
            {
                if (!this.IdInteresado1.Equals(""))
                {
                    if (int.Parse(this.IdInteresado1) == int.MinValue)
                    {
                        this.IdInteresado1 = "";
                    }
                }
            }

            if (null != this.Interesado2)
            {
                if (!this.IdInteresado2.Equals(""))
                {
                    if (int.Parse(this.IdInteresado2) == int.MinValue)
                    {
                        this.IdInteresado2 = "";
                    }
                }
            }

            if (null != this.Interesado3)
            {
                if (!this.IdInteresado3.Equals(""))
                {
                    if (int.Parse(this.IdInteresado3) == int.MinValue)
                    {
                        this.IdInteresado3 = "";
                    }
                }
            }
        }

        private void MostrarLstInteresado1()
        {
            this._txtIdInteresado1.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresado1.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados1.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados1.IsEnabled = true;
            this._btnConsultarInteresado1.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresado1Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresado1Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresado1Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresado1Filtrar.Visibility = System.Windows.Visibility.Visible;
        }

        private void MostrarLstInteresado2()
        {
            this._txtIdInteresado2.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresado2.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados2.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados2.IsEnabled = true;
            this._btnConsultarInteresado2.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresado2Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresado2Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresado2Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresado2Filtrar.Visibility = System.Windows.Visibility.Visible;
        }

        private void MostrarLstInteresado3()
        {
            this._txtIdInteresado3.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresado3.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados3.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados3.IsEnabled = true;
            this._btnConsultarInteresado3.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresado3Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresado3Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresado3Filtrar.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresado3Filtrar.Visibility = System.Windows.Visibility.Visible;
        }

        private void OcultarLstInteresado1()
        {
            this._txtIdInteresado1.Visibility = System.Windows.Visibility.Visible;
            this._txtInteresado1.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados1.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados1.IsEnabled = false;
            this._btnConsultarInteresado1.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresado1Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresado1Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdInteresado1Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresado1Filtrar.Visibility = System.Windows.Visibility.Collapsed; 
        }

        private void OcultarLstInteresado2()
        {
            this._txtIdInteresado2.Visibility = System.Windows.Visibility.Visible;
            this._txtInteresado2.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados2.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados2.IsEnabled = false;
            this._btnConsultarInteresado2.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresado2Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresado2Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdInteresado2Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresado2Filtrar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OcultarLstInteresado3()
        {
            this._txtIdInteresado3.Visibility = System.Windows.Visibility.Visible;
            this._txtInteresado3.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados3.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados3.IsEnabled = false;
            this._btnConsultarInteresado3.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresado3Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresado3Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdInteresado3Filtrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresado3Filtrar.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        

        

        

        

        

        

        

        

        
    }
}
