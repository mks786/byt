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
using Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones;
using Trascend.Bolet.Cliente.Presentadores.SAPI.Presentaciones;

namespace Trascend.Bolet.Cliente.Ventanas.SAPI.Presentaciones
{
    /// <summary>
    /// Lógica de interacción para GestionarSolicitudPresentacion.xaml
    /// </summary>
    public partial class GestionarSolicitudPresentacion : Page, IGestionarSolicitudPresentacion
    {

        private bool _cargada;
        private PresentadorGestionarSolicitudPresentacion _presentador;

        #region IGestionarSolicitudPresentacion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object EncabezadoPresentacion
        {
            get { return this._stkPresentacionEnc.DataContext; }
            set { this._stkPresentacionEnc.DataContext = value; }
        }


        public string IdPresentacionSapi
        {
            get { return this._txtIdPresentacion.Text; }
            set { this._txtIdPresentacion.Text = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuarioPresentacion.DataContext; }
            set { this._cbxUsuarioPresentacion.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuarioPresentacion.SelectedItem; }
            set { this._cbxUsuarioPresentacion.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDptoPresentacion.DataContext; }
            set { this._cbxDptoPresentacion.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDptoPresentacion.SelectedItem; }
            set { this._cbxDptoPresentacion.SelectedItem = value; }
        }

        public string FechaPresentacion
        {
            get { return this._dpkFechaPresentacion.Text; }
            set { this._dpkFechaPresentacion.Text = value; }
        }

        public object Documentos
        {
            get { return this._cbxDocumentosPresentacion.DataContext; }
            set { this._cbxDocumentosPresentacion.DataContext = value; }
        }

        public object Documento
        {
            get { return this._cbxDocumentosPresentacion.SelectedItem; }
            set { this._cbxDocumentosPresentacion.SelectedItem = value; }
        }

        public string CodigoExpedientePresentacion
        {
            get { return this._txtCodExpPresentacion.Text; }
            set { this._txtCodExpPresentacion.Text = value; }
        }

        public object DetallePresentacion
        {
            get { return this._lstDetallePresentacion.DataContext; }
            set { this._lstDetallePresentacion.DataContext = value; }
        }

        public object DetallePresentacionSeleccionado
        {
            get { return this._lstDetallePresentacion.SelectedItem; }
            set { this._lstDetallePresentacion.SelectedItem = value; }
        }

        public string CantidadDocumentos
        {
            get { return this._txtCantDocsPresentacion.Text; }
            set { this._txtCantDocsPresentacion.Text = value; }
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
                this._cbxUsuarioPresentacion.IsEnabled = value;
                this._cbxDptoPresentacion.IsEnabled = value;
                this._dpkFechaPresentacion.IsEnabled = value;
                this._cbxDocumentosPresentacion.IsEnabled = value;
                this._txtCodExpPresentacion.IsEnabled = value;
                this._btnMas.IsEnabled = value;
                this._btnMenos.IsEnabled = value;
                this._txtCantDocsPresentacion.IsEnabled = value;
                this._lstDetallePresentacion.IsEnabled = value;
            }
        }

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado sin parametros
        /// </summary>
        public GestionarSolicitudPresentacion()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarSolicitudPresentacion(this,null,null);
        }

        /// <summary>
        /// Constructor predeterminado que recibe el encabezado de la Solicitud de Presentacion y la ventana padre
        /// </summary>
        /// <param name="presentacionSapi">Encabezado de la Presentacion Sapi</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public GestionarSolicitudPresentacion(object presentacionSapi, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarSolicitudPresentacion(this, presentacionSapi, ventanaPadre);
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

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarDocumentoASolicitud();
        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.QuitarDocumentoDeSolicitud();
        }

        #endregion

        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void SeleccionarPrimerItem()
        {
            this._cbxDocumentosPresentacion.SelectedIndex = 0;
            this._txtCodExpPresentacion.Text = String.Empty;
        }

        #endregion

        

        
        

    }
}
