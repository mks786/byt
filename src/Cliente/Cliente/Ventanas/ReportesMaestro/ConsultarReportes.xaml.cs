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
    /// Lógica de interacción para ConsultarReportesDeMarca.xaml
    /// </summary>
    public partial class ConsultarReportes : Page, IConsultarReportes
    {

        private bool _cargada;
        private PresentadorConsultarReportes _presentador;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        
        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarReportes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarReportes(this);

        }



        #region IConsultarReportes

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }


        public GridViewColumnHeader CurSortCol
        {
            get { return _CurSortCol; }
            set { _CurSortCol = value; }
        }

        public SortAdorner CurAdorner
        {
            get { return _CurAdorner; }
            set { _CurAdorner = value; }
        }

        public ListView ListaResultados
        {
            get { return this._lstResultados; }
            set { this._lstResultados = value; }
        }


        public object ReporteDeMarcaFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
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

        public string TituloEnIngles
        {
            get { return this._txtTituloReporteIng.Text; }
            set { this._txtTituloReporteIng.Text = value; }
        }

        public string TituloEnEspanol
        {
            get { return this._txtTituloReporteEsp.Text; }
            set { this._txtTituloReporteEsp.Text = value; }
        }

        public object TiposDeReporte
        {
            get { return this._cbxTipoReporte.DataContext; }
            set { this._cbxTipoReporte.DataContext = value; }
        }

        public object TipoDeReporte
        {
            get { return this._cbxTipoReporte.SelectedItem; }
            set { this._cbxTipoReporte.SelectedItem = value; }
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

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object ReporteDeMarcaSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
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

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            validarCamposVacios();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarReporteDeMarca();
        }

        private void _btnEjecutarReporte_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GestionarValoresParaFiltros();
        }

        #endregion

        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;

            if (!this._txtIdReporte.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdReporte.Focus();
            }

            if (!this._txtDescripcionReporte.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDescripcionReporte.Focus();
            }

            if (!this._txtTituloReporteEsp.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtTituloReporteEsp.Focus();
            }

            if (!this._txtTituloReporteIng.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtTituloReporteIng.Focus();
            }

            if ((this._cbxIdiomaReporte.SelectedIndex != 0) && (this._cbxIdiomaReporte.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxIdiomaReporte.Focus();
            }

            if ((this._cbxUsuarioReporte.SelectedIndex != 0) && (this._cbxUsuarioReporte.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxUsuarioReporte.Focus();
            }

            if ((this._cbxTipoReporte.SelectedIndex != 0) && (this._cbxTipoReporte.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTipoReporte.Focus();
            }

            if (todosCamposVacios)
            {
                this._txtIdReporte.Focus();
            }
        }


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        
        
    }
}
