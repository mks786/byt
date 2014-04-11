using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using System;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for AgregarObjeto.xaml
    /// </summary>
    public partial class GestionarInfoAdicional : Page, IGestionarInfoAdicional
    {
        private PresentadorGestionarInfoAdicional _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();
        private string _tab;

        #region IAgregarInfoAdicional

        public void OculatarControlesAlAgregar()
        {
            this._btnAuditoria.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void FocoPredeterminado()
        {
            this._txtNombre.Focus();
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
                this._txtNombre.IsEnabled = value;
                this._txtEmail.IsEnabled = value;
                this._txtInfo.IsEnabled = value;
            }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object InfoAdicional
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void PintarAuditoria()
        {
            this._btnAuditoria.Background = Brushes.LightGreen;
        }

        public string Tab
        {
            get { return this._tab; }
        }

        #endregion

        /// <summary>
        /// Constructor por defecto que recibe una marca 
        /// </summary>
        /// <param name="marca">Marca seleccionada</param>
        public GestionarInfoAdicional(object marca)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarInfoAdicional(this, marca,null);

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }

        /// <summary>
        /// Constructor por defecto que recibe una marca y la ventana padre ConsultarMarcas
        /// </summary>
        /// <param name="marca">Marca seleccionada</param>
        /// <param name="ventanaPadreConsultarMarcas">Ventana ConsultarMarcas</param>
        public GestionarInfoAdicional(object marca, object ventanaPadreConsultarMarcas)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarInfoAdicional(this, marca,ventanaPadreConsultarMarcas);

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }

        /// <summary>
        /// Referencia a constructor por defecto con una marca y el nombre del tab de la ventana padre
        /// </summary>
        /// <param name="marca">Marca consultada</param>
        /// <param name="tab">Nombre del tab de la ventana anterior</param>
        public GestionarInfoAdicional(object marca, string tab)
            : this(marca)
        {
            this._tab = tab;
        }

        /// <summary>
        /// Referencia a constructor por defecto con una marca, el nombre del tab de la ventana padre y la ventana ConsultarMarcas
        /// </summary>
        /// <param name="marca">Marca consultada</param>
        /// <param name="tab">Nombre del tab de la ventana anterior</param>
        /// <param name="ventanaPadreConsultarMarcas">Ventana padre ConsultarMarcas</param>
        public GestionarInfoAdicional(object marca, string tab, object ventanaPadreConsultarMarcas)
            : this(marca, ventanaPadreConsultarMarcas)
        {
            this._tab = tab;
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
            this._txtMensaje.Text = "Operación realizada exitosamente.";
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this._presentador.IrConsultarMarca();
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

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }
    }
}
