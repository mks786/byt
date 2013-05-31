using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Presentadores.MarcasTercero;
using System;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.MarcasTercero
{
    /// <summary>
    /// Lógica de interacción para InfoAdicionalInteresadosAsociadosMarcaTerceros.xaml
    /// </summary>
    public partial class InfoAdicionalInteresadosAsociadosMarcaTerceros : Page, IInfoAdicionalInteresadosAsociadosMarcaTerceros
    {
        private PresentadorInfoAdicionalInteresadosAsociadosMarcaTerceros _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();
        private string _tab;

        
        /// <summary>
        /// Constructor por defecto de la ventana
        /// </summary>
        /// <param name="marca"></param>
        public InfoAdicionalInteresadosAsociadosMarcaTerceros(object marca)
        {
            InitializeComponent();
            this._cargada = false;
            //this._presentador = new PresentadorGestionarInfoAdicionalMarcaTercero(this, marca);
            this._presentador = new PresentadorInfoAdicionalInteresadosAsociadosMarcaTerceros(this, marca);
            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }


        public InfoAdicionalInteresadosAsociadosMarcaTerceros(object marca, string tab)
            : this(marca)
        {
            this._tab = tab;
            
        }

        /// <summary>
        /// Constructor sobrecargado que recibe una ventana padre
        /// </summary>
        /// <param name="marca"></param>
        /// <param name="tab"></param>
        /// <param name="ventanaPadre"></param>
        public InfoAdicionalInteresadosAsociadosMarcaTerceros(object marca, string tab, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            //this._presentador = new PresentadorGestionarInfoAdicionalMarcaTercero(this, marca);
            this._presentador = new PresentadorInfoAdicionalInteresadosAsociadosMarcaTerceros(this, marca, ventanaPadre);
            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }


        

        #region IInfoAdicionalInteresadosAsociadosMarcaTerceros


        public object MarcaTercero
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }


        public void FocoPredeterminado()
        {
            this._txtNombreInteresadoTercero.Focus();
        }


        public string NombreInteresadoTercero
        {
            get { return this._txtNombreInteresadoTercero.Text; }
            set { this._txtNombreInteresadoTercero.Text = value; }
        }

        public string InteresadoTercero
        {
            get { return this._txtNombreInteresadoTercero.Text; }
            set { this._txtNombreInteresadoTercero.Text = value; }
        }


        public string NombreAsociadoTercero
        {
            get { return this._txtNombreAsociadoTercero.Text; }
            set { this._txtNombreAsociadoTercero.Text = value; }
        }
        
        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public string Tab
        {
            get { return this._tab; }
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

        void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _bgw.ReportProgress(1);
            Thread.Sleep(2000);
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this._presentador.IrConsultarMarca();
        }

        void bgw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //this._txtMensaje.Text = "Operación realizada exitósamente.";
        }

        #endregion

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!this._presentador.Aceptar().Equals(""))
            {
                //_bgw.RunWorkerAsync();
                this._presentador.RegresarVentanaPadre();
               
            }
        }
    }
}
