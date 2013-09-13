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
using Trascend.Bolet.Cliente.Contratos.ReportesMaestro;
using Trascend.Bolet.Cliente.Presentadores.ReportesMaestro;


namespace Trascend.Bolet.Cliente.Ventanas.ReportesMaestro
{
    /// <summary>
    /// Lógica de interacción para GestionarFiltrosReporteDeMarca.xaml
    /// </summary>
    public partial class GestionarFiltrosReporte : Page, IGestionarFiltrosReporte
    {

        private bool _cargada;
        private PresentadorGestionarFiltroReporte _presentador;

        /// <summary>
        /// Constructor predeterminado que solo recibe una ventana padre
        /// </summary>
        public GestionarFiltrosReporte(object reporteDeMarca, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFiltroReporte(this, ventanaPadre, reporteDeMarca);

        }


        #region IGestionarFiltrosReporte

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object ReporteDeMarca
        {
            get { return this._gridReporteMarca.DataContext; }
            set { this._gridReporteMarca.DataContext = value; }
        }

        public string TituloReporteDeMarca
        {
            get { return this._txtDescripcionReporteDeMarca.Text; }
            set { this._txtDescripcionReporteDeMarca.Text = value; }
        }

        public object CamposSeleccionadosReporteDeMarca
        {
            get { return this._cbxCamposReporteDeMarca.DataContext; }
            set { this._cbxCamposReporteDeMarca.DataContext = value; }
        }

        public object CampoSeleccionadoReporteDeMarca
        {
            get { return this._cbxCamposReporteDeMarca.SelectedItem; }
            set { this._cbxCamposReporteDeMarca.SelectedItem = value; }
        }

        public object OperadoresDeReporte
        {
            get { return this._cbxOperadorFiltroReporte.DataContext; }
            set { this._cbxOperadorFiltroReporte.DataContext = value; }
        }

        public object OperadorDeReporte
        {
            get { return this._cbxOperadorFiltroReporte.SelectedItem; }
            set { this._cbxOperadorFiltroReporte.SelectedItem = value; }
        }

        public object FiltrosReporteDeMarca
        {
            get { return this._lstFiltrosReporteMarca.DataContext; }
            set { this._lstFiltrosReporteMarca.DataContext = value; }
        }

        public object FiltroReporteDeMarca
        {
            get { return this._lstFiltrosReporteMarca.SelectedItem; }
            set { this._lstFiltrosReporteMarca.SelectedItem = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnAceptar.Focus();
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

        private void _btnAgregarCampoAFiltro_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarFiltro();
        }

        private void _btnQuitarCampoAFiltro_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.QuitarFiltro();
        }
        
        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarModificacionFiltrosDeReporte),
                    "Filtros de Reporte de Marca", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Aceptar();
            }
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
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        
        #endregion
    }
}
