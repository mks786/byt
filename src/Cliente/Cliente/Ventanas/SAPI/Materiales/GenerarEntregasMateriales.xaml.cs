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
    /// Lógica de interacción para GenerarEntregasMateriales.xaml
    /// </summary>
    public partial class GenerarEntregasMateriales : Page, IGenerarEntregasMateriales
    {

        private bool _cargada;
        private PresentadorGenerarEntregasMateriales _presentador;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IGenerarEntregasMateriales

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

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        public string FechaSolicitudSapi
        {
            get { return this._dpkFechaSolicitudSapi.Text; }
            set { this._dpkFechaSolicitudSapi.Text = value; }
        }

        
        public object Departamentos
        {
            get { return this._cbxDptoSolicitudSapi.DataContext; }
            set { this._cbxDptoSolicitudSapi.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDptoSolicitudSapi.SelectedItem; }
            set { this._cbxDptoSolicitudSapi.SelectedItem = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuarioSolicitudSapi.DataContext; }
            set { this._cbxUsuarioSolicitudSapi.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuarioSolicitudSapi.SelectedItem; }
            set { this._cbxUsuarioSolicitudSapi.SelectedItem = value; }
        }

        public object StatusSolicitudesSapi
        {
            get { return this._cbxStatusSolicitudSapi.DataContext; }
            set { this._cbxStatusSolicitudSapi.DataContext = value; }
        }

        public object StatusSolicitudSapi
        {
            get { return this._cbxStatusSolicitudSapi.SelectedItem; }
            set { this._cbxStatusSolicitudSapi.SelectedItem = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object SolicitudSapiSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public object SolicitudSapiFiltro
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        #endregion


        #region Constructores

        public GenerarEntregasMateriales()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGenerarEntregasMateriales(this,null);
        }

        public GenerarEntregasMateriales(object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGenerarEntregasMateriales(this,ventanaPadre);
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

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void _btnEntregarMaterial_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GenerarEntregaMateriales();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpíarCampos();
        }

        private void _btnRecibirMaterial_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GenerarRecepcionMateriales();
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

            if (!this._dpkFechaSolicitudSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaSolicitudSapi.Focus();
            }

            if ((this._cbxDptoSolicitudSapi.SelectedIndex != 0) && (this._cbxDptoSolicitudSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxDptoSolicitudSapi.Focus();
            }

            if ((this._cbxUsuarioSolicitudSapi.SelectedIndex != 0) && (this._cbxUsuarioSolicitudSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxUsuarioSolicitudSapi.Focus();
            }

            if ((this._cbxStatusSolicitudSapi.SelectedIndex != 0) && (this._cbxStatusSolicitudSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxStatusSolicitudSapi.Focus();
            }

            if (todosCamposVacios)
            {
                this._dpkFechaSolicitudSapi.Focus();
            }
        }


        public void MostrarBotonRecepcionMateriales()
        {
            this._btnRecibirMaterial.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion

        

        

        
    }
}
