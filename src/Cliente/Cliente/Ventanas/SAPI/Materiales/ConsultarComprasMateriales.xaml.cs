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
using Trascend.Bolet.Cliente.Contratos.SAPI.Materiales;
using Trascend.Bolet.Cliente.Presentadores.SAPI.Materiales;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales
{
    /// <summary>
    /// Lógica de interacción para ConsultarComprasMateriales.xaml
    /// </summary>
    public partial class ConsultarComprasMateriales : Page, IConsultarComprasMateriales
    {
        private PresentadorConsultarComprasMateriales _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarComprasMateriales

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtIdCompraSapi.Focus();
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

        public string IdCompraSapi
        {
            get { return this._txtIdCompraSapi.Text; }
            set { this._txtIdCompraSapi.Text = value; }
        }

        public string FechaCompraSapi
        {
            get { return this._dpkFechaCompraSapi.Text; }
            set { this._dpkFechaCompraSapi.Text = value; }
        }

        public object MaterialesSapi
        {
            get { return this._cbxMaterialCompraSapi.DataContext; }
            set { this._cbxMaterialCompraSapi.DataContext = value; }
        }

        public object MaterialSapi
        {
            get { return this._cbxMaterialCompraSapi.SelectedItem; }
            set { this._cbxMaterialCompraSapi.SelectedItem = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object CompraSapiSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        #endregion

        #region Constructores

        public ConsultarComprasMateriales()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarComprasMateriales(this);
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

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.VerCompraSAPI();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
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


        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;

            if (!this._txtIdCompraSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdCompraSapi.Focus();
            }

            if (!this._dpkFechaCompraSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaCompraSapi.Focus();
            }

            if ((this._cbxMaterialCompraSapi.SelectedIndex != 0) && (this._cbxMaterialCompraSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxMaterialCompraSapi.Focus();
            }

            if (todosCamposVacios)
            {
                this._txtIdCompraSapi.Focus();
            }
        }

        #endregion

        
    }
}
