using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Plantillas;
using Trascend.Bolet.Cliente.Presentadores.Plantillas;
using System.Threading;
using System.ComponentModel;


namespace Trascend.Bolet.Cliente.Ventanas.Plantillas
{
    /// <summary>
    /// Lógica de interacción para GestionarFiltroDePlantilla.xaml
    /// </summary>
    public partial class GestionarFiltroDePlantilla : Page, IGestionarFiltroDePlantilla
    {
        
        private bool _cargada;
        private PresentadorGestionarFiltroDePlantilla _presentador;
        BackgroundWorker _bgw = new BackgroundWorker();
        
        /// <summary>
        /// Constructor por defecto que recibe una ventana padre
        /// </summary>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public GestionarFiltroDePlantilla(object filtro, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFiltroDePlantilla(this, filtro, ventanaPadre);

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);

        }


        public GestionarFiltroDePlantilla(object filtro, object ventanaPadre, object ventanaPadreMaestroPlantillas)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFiltroDePlantilla(this, filtro, ventanaPadre, ventanaPadreMaestroPlantillas);

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);

        }


        #region IGestionarFiltroPlantilla

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
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
                //this._txtIdPlantilla.IsEnabled = value;
                this._txtNombreFiltroPlantilla.IsEnabled = value;
                this._txtNombreVariableFiltro.IsEnabled = value;
                this._cbxTipoDeFiltro.IsEnabled = value;
                this._cbxTipoCampoFiltro.IsEnabled = value;
                
            }
        }


        public object FiltroPlantilla
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string CodigoDePlantilla
        {
            get { return this._txtIdPlantilla.Text; }
            set { this._txtIdPlantilla.Text = value; }
        }

        public string NombreFiltro
        {
            get { return this._txtNombreFiltroPlantilla.Text; }
            set { this._txtNombreFiltroPlantilla.Text = value; }
        }

        public string NombreVariableFiltro
        {
            get { return this._txtNombreVariableFiltro.Text; }
            set { this._txtNombreVariableFiltro.Text = value; }
        }

        public object TiposDeDatosFiltro
        {
            get { return this._cbxTipoCampoFiltro.DataContext; }
            set { this._cbxTipoCampoFiltro.DataContext = value; }
        }

        public object TipoDeDatosFiltro
        {
            get { return this._cbxTipoCampoFiltro.SelectedItem; }
            set { this._cbxTipoCampoFiltro.SelectedItem = value; }
        }

        public object TiposDeFiltro
        {
            get { return this._cbxTipoDeFiltro.DataContext; }
            set { this._cbxTipoDeFiltro.DataContext = value; }
        }

        public object TipoDeFiltro
        {
            get { return this._cbxTipoDeFiltro.SelectedItem; }
            set { this._cbxTipoDeFiltro.SelectedItem = value; }
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
            else
                this._presentador.ActualizarTitulo();
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
            this._presentador.irListaListaFiltros();
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarFiltroPlantilla,
                "Eliminar Filtro Plantilla", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                if (this._presentador.Eliminar())
                {
                    _bgw.RunWorkerAsync();
                }
            }
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        #endregion


        #region Metodos

        public void MensajeAlerta(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion
    }
}
