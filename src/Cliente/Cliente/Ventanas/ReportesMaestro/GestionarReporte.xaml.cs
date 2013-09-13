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
    /// Lógica de interacción para GestionarReporteDeMarca.xaml
    /// </summary>
    public partial class GestionarReporte : Page, IGestionarReporte
    {

        private bool _cargada;
        private PresentadorGestionarReporte _presentador;

        /// <summary>
        /// Constructor predeterminado que recibe un Reporte 
        /// </summary>
        /// <param name="reporteMarca">Reporte seleccionado</param>
        public GestionarReporte(object reporte)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarReporte(this, reporte,null);
        }

        /// <summary>
        /// Constructor predeterminado que recibe un Reporte de Marca y una ventana padre
        /// </summary>
        /// <param name="reporteMarca">Reporte de Marca seleccionado</param>
        /// <param name="ventanaPadre">Venta Padre que precede a esta ventana</param>
        public GestionarReporte(object reporteMarca, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarReporte(this, reporteMarca, ventanaPadre);
        }


        #region IGestionarReporte

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnAceptar.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtDescripcionReporte.IsEnabled = value;
                this._txtTituloReporte.IsEnabled = value;
                this._cbxUsuarioReporte.IsEnabled = value;
                this._cbxIdiomaReporte.IsEnabled = value;
            }
            
        }

        public object ReporteDeMarca
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string IdReporte
        {
            get { return this._txtIdReporte.Text; }
            set { this._txtIdReporte.Text = value; }
        }

        public string DescripcionReporte
        {
            get { return this._txtDescripcionReporte.Text; }
            set { this._txtDescripcionReporte.Text = value; }
        }

        public string TituloReporte
        {
            get { return this._txtTituloReporte.Text; }
            set { this._txtTituloReporte.Text = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuarioReporte.DataContext; }
            set { this._cbxUsuarioReporte.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuarioReporte.SelectedItem; }
            set { this._cbxUsuarioReporte.SelectedItem = value; }
        }

        public object Idiomas
        {
            get { return this._cbxIdiomaReporte.DataContext; }
            set { this._cbxIdiomaReporte.DataContext = value; }
        }

        public object Idioma
        {
            get { return this._cbxIdiomaReporte.SelectedItem; }
            set { this._cbxIdiomaReporte.SelectedItem = value; }
        }


        public object TiposDeReporte
        {
            get { return _cbxTipoReporte.DataContext; }
            set { this._cbxTipoReporte.DataContext = value; }
        }

        public object TipoDeReporte
        {
            get { return _cbxTipoReporte.SelectedItem; }
            set { this._cbxTipoReporte.SelectedItem = value; }
        }

        //Campos disponibles para el reporte de marca
        public object CamposReporte
        {
            get { return this._lstCamposReporte.DataContext; }
            set { this._lstCamposReporte.DataContext = value; }
        }

        public object CampoReporte
        {
            get { return this._lstCamposReporte.SelectedItem; }
            set { this._lstCamposReporte.SelectedItem = value; }
        }

        //Campos contenidos en el reporte seleccionado o a ingresar por primera vez
        public object CamposSeleccionados
        {
            get { return this._lstCamposSeleccionadosReporte.DataContext; }
            set { this._lstCamposSeleccionadosReporte.DataContext = value; }
        }

        public object CampoSeleccionado
        {
            get { return this._lstCamposSeleccionadosReporte.SelectedItem; }
            set { this._lstCamposSeleccionadosReporte.SelectedItem = value; }
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
            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarModificarReporte),
                    "Modificar Reporte de Marca", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Aceptar();
            }
            
        }

        private void _btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarCampo();
        }

        private void _btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.QuitarCampo();
        }

        private void _btnSubir_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.SubirCampo();
        }

        private void _btnBajar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BajarCampo();
        }

        private void _btnFiltrosReporte_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarFiltros();
        }

        #endregion


        #region Metodos

        public void PintarFiltros()
        {
            this._btnFiltrosReporte.Background = Brushes.LightGreen;
        }


        public void ActivarBotonFiltros(bool valor)
        {
            this._btnFiltrosReporte.IsEnabled = valor;
        }


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        public void InicializarVistaReporte()
        {
            this._cbxTipoReporte.SelectedIndex = 0;
            this._cbxTipoReporte.SelectedItem = _cbxTipoReporte.Items[0];
        }

        #endregion

        private void _cbxTipoReporte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CargarCamposPorVista();
        }
    }
}
