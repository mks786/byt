using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Trascend.Bolet.Cliente.Contratos.Pirateria.Casos;
using Trascend.Bolet.Cliente.Presentadores.Pirateria.Casos;

namespace Trascend.Bolet.Cliente.Ventanas.Pirateria.Casos
{
    /// <summary>
    /// Lógica de interacción para GestionarInfoTerceros.xaml
    /// </summary>
    public partial class GestionarInfoTerceros : Page, IGestionarInfoTerceros
    {
        private bool _cargada;
        private PresentadorGestionarInfoTerceros _presentador;
        BackgroundWorker _bgw = new BackgroundWorker();
        private string _tab;

        #region IGestionarInfoTerceros

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNombreInteresado.Focus();
        }

        public object Caso
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string NombreInteresado
        {
            get { return this._txtNombreInteresado.Text; }
            set { this._txtNombreInteresado.Text = value; }
        }

        public string NombreAsociado
        {
            get { return this._txtNombreAsociado.Text; }
            set { this._txtNombreAsociado.Text = value; }
        }



        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="caso">Caso a agregarle la informacion adicional</param>
        /// <param name="ventanaPadre">Ventana que antecede a esta ventana</param>
        public GestionarInfoTerceros(object caso, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarInfoTerceros(this, caso, ventanaPadre);
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

        void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _bgw.ReportProgress(1);
            Thread.Sleep(2000);
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //this._presentador.IrConsultarMarca();
        }

        void bgw_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //this._txtMensaje.Text = "Operación realizada exitósamente.";
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.Aceptar())
            {
                //_bgw.RunWorkerAsync();
                this._presentador.RegresarVentanaPadre();

            }
        }

        
        #endregion

        
        #region Metodos

        #endregion

    }
}
