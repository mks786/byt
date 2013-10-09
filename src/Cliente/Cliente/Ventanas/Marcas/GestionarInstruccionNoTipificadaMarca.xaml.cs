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
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Lógica de interacción para GestionarInstruccionNoTipificadaMarca.xaml
    /// </summary>
    public partial class GestionarInstruccionNoTipificadaMarca : Page, IGestionarInstruccionNoTipificadaMarca
    {

        private PresentadorGestionarInstruccionNoTipificadaMarca _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();

        
        /// <summary>
        /// Constructor por defecto que recibe una instruccion no tipificada y la ventana padre
        /// </summary>
        /// <param name="instruccionNoTipificada">Instruccion No Tipificada a consultar o a agregar</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public GestionarInstruccionNoTipificadaMarca(object instruccionNoTipificada, object marca, object ventanaPadre, object ventanaPadreListaInstrucciones)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarInstruccionNoTipificadaMarca(this, instruccionNoTipificada, marca, ventanaPadre, ventanaPadreListaInstrucciones);
            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
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
            this._presentador.IrListaInstruccionesNoTipificadas();
        }


        #region IGestionarInstruccionNoTipificadaMarca

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnAceptar.Focus();
        }

        public object InstruccionNoTipificada
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string CodigoOperacion
        {
            get { return this._txtCodigoOperacion.Text; }
            set { this._txtCodigoOperacion.Text = value; }
        }

        public string Descripcion
        {
            get { return this._txtDescripcion.Text; }
            set { this._txtDescripcion.Text = value; }
        }

        public string IdCorrespondencia
        {
            get { return this._txtCorrespondencia.Text; }
            set { this._txtCorrespondencia.Text = value; }
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


        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.Aceptar())
            {
                _bgw.RunWorkerAsync();
            }
        }

        private void _btnCorrespondencia_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCorrespondencia();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        #endregion


        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if(opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if(opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        } 

        #endregion

        

        

        
    }
}
