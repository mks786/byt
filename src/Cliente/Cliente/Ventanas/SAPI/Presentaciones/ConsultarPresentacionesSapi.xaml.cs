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
using Trascend.Bolet.Cliente.Ayuda;


namespace Trascend.Bolet.Cliente.Ventanas.SAPI.Presentaciones
{
    /// <summary>
    /// Lógica de interacción para ConsultarPresentacionesSapi.xaml
    /// </summary>
    public partial class ConsultarPresentacionesSapi : Page, IConsultarPresentacionesSapi
    {

        private PresentadorConsultarPresentacionesSapi _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarPresentacionesSapi

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

        public string FechaSolicitudPresentacion
        {
            get { return this._dpkFechaPresentacionSapi.Text; }
            set { this._dpkFechaPresentacionSapi.Text = value; }
        }

        public object DptosSolicitudPresentacion
        {
            get { return this._cbxDptoPresentacionSapi.DataContext; }
            set { this._cbxDptoPresentacionSapi.DataContext = value; }
        }

        public object DptoSolicitudPresentacion
        {
            get { return this._cbxDptoPresentacionSapi.SelectedItem; }
            set { this._cbxDptoPresentacionSapi.SelectedItem = value; }
        }

        public object DctosPresentacion
        {
            get { return this._cbxDctoPresentacionSapi.DataContext; }
            set { this._cbxDctoPresentacionSapi.DataContext = value; }
        }

        public object DctoPresentacion
        {
            get { return this._cbxDctoPresentacionSapi.SelectedItem; }
            set { this._cbxDctoPresentacionSapi.SelectedItem = value; }
        }

        public string CodigoExpPresentacion
        {
            get { return this._txtCodExpPresentacionSapi.Text; }
            set { this._txtCodExpPresentacionSapi.Text = value; }
        }

        public object StatusPresentacion
        {
            get { return this._cbxStatusPresentSapi.DataContext; }
            set { this._cbxStatusPresentSapi.DataContext = value; }
        }

        public object StatusPresentacionSeleccionado
        {
            get { return this._cbxStatusPresentSapi.SelectedItem; }
            set { this._cbxStatusPresentSapi.SelectedItem = value; }
        }

        public object UsuariosPresentacion
        {
            get { return this._cbxUsuarioPresentSapi.DataContext; }
            set { this._cbxUsuarioPresentSapi.DataContext = value; }
        }

        public object UsuarioPresentacion
        {
            get { return this._cbxUsuarioPresentSapi.SelectedItem; }
            set { this._cbxUsuarioPresentSapi.SelectedItem = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object SolicitudPresentacionSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        #endregion

        #region Constructores

        public ConsultarPresentacionesSapi()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarPresentacionesSapi(this);
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

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            validarCamposVacios();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.VerSolicitudPresentacionSAPI();
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

            if (!this._dpkFechaPresentacionSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaPresentacionSapi.Focus();
            }

            if ((this._cbxDptoPresentacionSapi.SelectedIndex != 0) && (this._cbxDptoPresentacionSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxDptoPresentacionSapi.Focus();
            }

            if ((this._cbxDctoPresentacionSapi.SelectedIndex != 0) && (this._cbxDctoPresentacionSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxDctoPresentacionSapi.Focus();
            }

            if ((this._cbxStatusPresentSapi.SelectedIndex != 0) && (this._cbxStatusPresentSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxStatusPresentSapi.Focus();
            }

            if ((this._cbxUsuarioPresentSapi.SelectedIndex != 0) && (this._cbxUsuarioPresentSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxUsuarioPresentSapi.Focus();
            }

            if (!this._txtCodExpPresentacionSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtCodExpPresentacionSapi.Focus();
            }

            if (todosCamposVacios)
            {
                this._dpkFechaPresentacionSapi.Focus();
            }
        }

        #endregion

        
    }
}
