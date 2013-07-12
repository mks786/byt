using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Lógica de interacción para GestionarCertificadoDeMarca.xaml
    /// </summary>
    public partial class GestionarCertificadoDeMarca : Page, IGestionarCertificadoDeMarca
    {
        private PresentadorGestionarCertificadoDeMarca _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();
        private bool _camposHabilitados = true;

        #region IGestionarCertificadoDeMarca

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnModificar.Focus();
        }

        public object Certificado
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }


        public string ReciboNumero
        {
            get { return this._txtReciboNumero.Text; }
            set { this._txtReciboNumero.Text = value; }
        }

        public string RegistroBs
        {
            get { return this._txtRegistroBs.Text; }
            set { this._txtRegistroBs.Text = value; }
        }

        public string EscrituraBs
        {
            get { return this._txtEscrituraBs.Text; }
            set { this._txtEscrituraBs.Text = value; }
        }

        public string PapelProtocolo
        {
            get { return this._txtPapelProtocolo.Text; }
            set { this._txtPapelProtocolo.Text = value; }
        }

        public string TotalBs
        {
            get { return this._txtTotalBs.Text; }
            set { this._txtTotalBs.Text = value; }
        }

        public string Clases
        {
            get { return this._txtClases.Text; }
            set { this._txtClases.Text = value; }
        }

        public string Comentario
        {
            get { return this._txtComentario.Text; }
            set { this._txtComentario.Text = value; }
        }

        public object Registradores
        {
            get { return this._cbxRegistradores.DataContext; }
            set { this._cbxRegistradores.DataContext = value; }
        }

        public object Registrador
        {
            get { return this._cbxRegistradores.SelectedItem; }
            set { this._cbxRegistradores.SelectedItem = value; }
        }

        #endregion

        #region Constructores

        public GestionarCertificadoDeMarca(object certificado, object marca)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCertificadoDeMarca(this, certificado, marca, null);

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += new System.ComponentModel.DoWorkEventHandler(bgw_DoWork);
            _bgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            _bgw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bgw_ProgressChanged);
        }

        public GestionarCertificadoDeMarca(object certificado, object marca, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCertificadoDeMarca(this, certificado, marca, ventanaPadre);

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
            //this._txtMensaje.Text = "Certificado de Marca modificado exitosamente.";
            this._txtMensaje.Text = string.Format(Recursos.MensajesConElUsuario.ConfirmarModificacionCertificadoMarca);
        }

        void bgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //this._presentador.IrConsultarMarca();
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

        /// <summary>
        /// Evento para visualizar la imagen del certificado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnVerCertificado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerImagenCertificado();
        }

        /// <summary>
        /// Boton para obtener la Auditoria de la tabla de Certificados de Marca
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarModificarCertificadoMarca,
                _presentador.ObtenerIdMarca()),
                "Modificar Certificado de Marca", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                if (this._presentador.Modificar())
                {
                    _bgw.RunWorkerAsync();
                }

            }

        }


        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarEliminarCertificadoMarca,
                _presentador.ObtenerIdMarca()),
                "Eliminar Certificado de Marca", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
        }


        private void _btnFechaMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrFechas();
        }


        #endregion

        #region Metodos

        public void ArchivoNoEncontrado(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void MostrarBotonEliminar(bool mostrar)
        {
            if(mostrar)
                this._btnEliminar.Visibility = System.Windows.Visibility.Visible;
            
               
        }

        #endregion

        

        

        

        


    }
}
