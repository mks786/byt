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
using Trascend.Bolet.Cliente.Contratos.SAPI.Presentaciones;
using Trascend.Bolet.Cliente.Presentadores.SAPI.Presentaciones;

namespace Trascend.Bolet.Cliente.Ventanas.SAPI.Presentaciones
{
    /// <summary>
    /// Lógica de interacción para ControlarSolicitudesPresentaciones.xaml
    /// </summary>
    public partial class ControlarSolicitudesPresentaciones : Page, IControlarSolicitudesPresentaciones
    {
        private PresentadorControlarSolicitudesPresentaciones _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IControlarSolicitudesPresentaciones

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
            get { return this._dpkFechaSolicitudPresentacionSapi.Text; }
            set { this._dpkFechaSolicitudPresentacionSapi.Text = value; }
        }

        public string FechaPresentacionAnteSAPI
        {
            get { return this._dpkFechaPresentacionAnteSapi.Text; }
            set { this._dpkFechaPresentacionAnteSapi.Text = value; }
        }

        public object Dptos
        {
            get { return this._cbxDptoPresentacionSapi.DataContext; }
            set { this._cbxDptoPresentacionSapi.DataContext = value; }
        }

        public object Dpto
        {
            get { return this._cbxDptoPresentacionSapi.SelectedItem; }
            set { this._cbxDptoPresentacionSapi.SelectedItem = value; }
        }

        public object Documentos
        {
            get { return this._cbxDctoPresentacionSapi.DataContext; }
            set { this._cbxDctoPresentacionSapi.DataContext = value; }
        }

        public object Documento
        {
            get { return this._cbxDctoPresentacionSapi.SelectedItem; }
            set { this._cbxDctoPresentacionSapi.SelectedItem = value; }
        }

        public string CodigoExpPresentacion
        {
            get { return this._txtCodExpPresentacionSapi.Text; }
            set { this._txtCodExpPresentacionSapi.Text = value; }
        }

        public object StatusTodos
        {
            get { return this._cbxStatusPresentSapi.DataContext; }
            set { this._cbxStatusPresentSapi.DataContext = value; }
        }

        public object StatusSeleccionado
        {
            get { return this._cbxStatusPresentSapi.SelectedItem; }
            set { this._cbxStatusPresentSapi.SelectedItem = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuarioPresentSapi.DataContext; }
            set { this._cbxUsuarioPresentSapi.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuarioPresentSapi.SelectedItem; }
            set { this._cbxUsuarioPresentSapi.SelectedItem = value; }
        }

        public object Gestores
        {
            get { return this._cbxGestorPresentSapi.DataContext; }
            set { this._cbxGestorPresentSapi.DataContext = value; }
        }

        public object Gestor
        {
            get { return this._cbxGestorPresentSapi.SelectedItem; }
            set { this._cbxGestorPresentSapi.SelectedItem = value; }
        }

        public object GestoresRegistro
        {
            get { return this._cbxGestoresProceso.DataContext; }
            set { this._cbxGestoresProceso.DataContext = value; }
        }

        public object GestorRegistro
        {
            get { return this._cbxGestoresProceso.SelectedItem; }
            set { this._cbxGestoresProceso.SelectedItem = value; }
        }

        public string FechaConfirmacion
        {
            get { return this._dpkFechaEvento.Text; }
            set { this._dpkFechaEvento.Text = value; }
        }


        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        #endregion

        #region Constructores

        public ControlarSolicitudesPresentaciones()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorControlarSolicitudesPresentaciones(this);
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

        private void _RegistarEvento_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            String nombreBotonPresionado = boton.Name;
            this._presentador.RegistrarControlDePresentaciones(nombreBotonPresionado);
        }
               

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegistrarEventoPresentacionDocumentos();
        }

        private void _btnSuspender_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.SuspenderRegistroEvento();
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

            if (!this._dpkFechaSolicitudPresentacionSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaSolicitudPresentacionSapi.Focus();
            }

            if (!this._dpkFechaPresentacionAnteSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaPresentacionAnteSapi.Focus();
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

            if ((this._cbxGestorPresentSapi.SelectedIndex != 0) && (this._cbxGestorPresentSapi.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxGestorPresentSapi.Focus();
            }

            if (!this._txtCodExpPresentacionSapi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtCodExpPresentacionSapi.Focus();
            }

            if (todosCamposVacios)
            {
                this._dpkFechaSolicitudPresentacionSapi.Focus();
            }
        }


        public void MostrarCamposRegistroEvento(string tipoBandera)
        {
            this._lblGestor.Visibility = System.Windows.Visibility.Visible;
            this._cbxGestoresProceso.Visibility = System.Windows.Visibility.Visible;
            switch (tipoBandera)
            {
                case "1":
                    this._lblFechaRecepcionMaterial.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "2":
                    this._lblFechaPresentacionSAPI.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "3":
                    this._lblFechaRecepcionSAPI.Visibility = System.Windows.Visibility.Visible;
                    break;
                case "4":
                    this._lblFechaRecepcionDpto.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
            
            this._dpkFechaEvento.Visibility = System.Windows.Visibility.Visible;
            this._btnConfirmar.Visibility = System.Windows.Visibility.Visible;
            this._btnSuspender.Visibility = System.Windows.Visibility.Visible;
        }


        public void OcultarCamposRegistroEvento(string tipoBandera)
        {
            this._lblGestor.Visibility = System.Windows.Visibility.Collapsed;
            this._cbxGestoresProceso.Visibility = System.Windows.Visibility.Collapsed;
            switch (tipoBandera)
            {
                case "1":
                    this._lblFechaRecepcionMaterial.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "2":
                    this._lblFechaPresentacionSAPI.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "3":
                    this._lblFechaRecepcionSAPI.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case "4":
                    this._lblFechaRecepcionDpto.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }

            this._dpkFechaEvento.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConfirmar.Visibility = System.Windows.Visibility.Collapsed;
            this._btnSuspender.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion


        

    }
}
