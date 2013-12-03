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

namespace Trascend.Bolet.Cliente.Ventanas.Plantillas
{
    /// <summary>
    /// Lógica de interacción para ConsultarMaestrosPlantillas.xaml
    /// </summary>
    public partial class ConsultarMaestrosPlantillas : Page, IConsultarMaestrosPlantillas
    {
        private bool _cargada;
        private PresentadorConsultarMaestrosPlantillas _presentador;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;


        public ConsultarMaestrosPlantillas()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarMaestrosPlantillas(this);
        }


        #region IConsultarMaestrosPlantillas

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

        public object MaestroPlantillaFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public string IdMaestroPlantilla
        {
            get { return this._txtIdPlantilla.Text; }
            set { this._txtIdPlantilla.Text = value; }
        }

        public object Idiomas
        {
            get { return this._cbxIdiomaPlantilla.DataContext; }
            set { this._cbxIdiomaPlantilla.DataContext = value; }
        }

        public object Idioma
        {
            get { return this._cbxIdiomaPlantilla.SelectedItem; }
            set { this._cbxIdiomaPlantilla.SelectedItem = value; }
        }

        public object Referidos
        {
            get { return this._cbxReferidoPlantilla.DataContext; }
            set { this._cbxReferidoPlantilla.DataContext = value; }
        }

        public object Referido
        {
            get { return this._cbxReferidoPlantilla.SelectedItem; }
            set { this._cbxReferidoPlantilla.SelectedItem = value; }
        }

        public object Criterios
        {
            get { return this._cbxCriterioPlantilla.DataContext; }
            set { this._cbxCriterioPlantilla.DataContext = value; }
        }

        public object Criterio
        {
            get { return this._cbxCriterioPlantilla.SelectedItem; }
            set { this._cbxCriterioPlantilla.SelectedItem = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuarioPlantilla.DataContext; }
            set { this._cbxUsuarioPlantilla.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuarioPlantilla.SelectedItem; }
            set { this._cbxUsuarioPlantilla.SelectedItem = value; }
        }

        public object Encabezados
        {
            get { return this._cbxEncabezado.DataContext; }
            set { this._cbxEncabezado.DataContext = value; }
        }

        public object Encabezado
        {
            get { return this._cbxEncabezado.SelectedItem; }
            set { this._cbxEncabezado.SelectedItem = value; }
        }

        public object Detalles
        {
            get { return this._cbxDetalle.DataContext; }
            set { this._cbxDetalle.DataContext = value; }
        }

        public object Detalle
        {
            get { return this._cbxDetalle.SelectedItem; }
            set { this._cbxDetalle.SelectedItem = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object MaestroDePlantillaSeleccionado
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

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            validarCamposVacios();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarMaestroDePlantilla();
        }

        #endregion


        #region Metodos

        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filtro
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;

            if (!this._txtIdPlantilla.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdPlantilla.Focus();
            }
            
            if ((this._cbxIdiomaPlantilla.SelectedIndex != 0) && (this._cbxIdiomaPlantilla.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxIdiomaPlantilla.Focus();
            }

            if ((this._cbxReferidoPlantilla.SelectedIndex != 0) && (this._cbxReferidoPlantilla.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxReferidoPlantilla.Focus();
            }

            if ((this._cbxCriterioPlantilla.SelectedIndex != 0) && (this._cbxCriterioPlantilla.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxCriterioPlantilla.Focus();
            }

            
            if ((this._cbxEncabezado.SelectedIndex != 0) && (this._cbxEncabezado.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxEncabezado.Focus();
            }

            if ((this._cbxDetalle.SelectedIndex != 0) && (this._cbxDetalle.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxDetalle.Focus();
            }

            if (todosCamposVacios)
            {
                this._txtIdPlantilla.Focus();
            }
        }

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
