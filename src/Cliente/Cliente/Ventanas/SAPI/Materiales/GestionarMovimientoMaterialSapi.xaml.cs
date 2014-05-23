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

namespace Trascend.Bolet.Cliente.Ventanas.SAPI.Materiales
{
    /// <summary>
    /// Lógica de interacción para GestionarMovimientoMaterialSapi.xaml
    /// </summary>
    public partial class GestionarMovimientoMaterialSapi : Page, IGestionarMovimientoMaterialSapi
    {

        private PresentadorGestionarMovimientoMaterialSapi _presentador;
        private bool _cargada;
        private int _tipo;

        #region IGestionarMovimientoMaterialSapi

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        //public object MovimientoMaterialesSapi
        //{
        //    get { return this._stkMovimientoMaterialSapi.DataContext; }
        //    set { this._stkMovimientoMaterialSapi.DataContext = value; }
        //}

        public string IdSolicitudMaterial
        {
            get { return this._txtIdMovimientoMaterial.Text; }
            set { this._txtIdMovimientoMaterial.Text = value; }
        }

        public string FechaSolicitudMaterial
        {
            get { return this._dpkFechaMovimientoMaterial.Text; }
            set { this._dpkFechaMovimientoMaterial.Text = value; }
        }

        public object UsuariosSolicitantes
        {
            get { return this._cbxSolicitanteMaterial.DataContext; }
            set { this._cbxSolicitanteMaterial.DataContext = value; }
        }

        public object UsuarioSolicitante
        {
            get { return this._cbxSolicitanteMaterial.SelectedItem; }
            set { this._cbxSolicitanteMaterial.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
        }

        public object MaterialesSAPI
        {
            get { return this._cbxMaterialesSapi.DataContext; }
            set { this._cbxMaterialesSapi.DataContext = value; }
        }

        public object MaterialSAPI
        {
            get { return this._cbxMaterialesSapi.SelectedItem; }
            set { this._cbxMaterialesSapi.SelectedItem = value; }
        }

        public object DetallesSolicitudMaterial
        {
            get { return this._lstDetalleSolicitud.DataContext; }
            set { this._lstDetalleSolicitud.DataContext = value; }
        }

        public object DetalleSolicitudMaterial
        {
            get { return this._lstDetalleSolicitud.SelectedItem; }
            set { this._lstDetalleSolicitud.SelectedItem = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtIdMovimientoMaterial.IsEnabled = value;
                this._dpkFechaMovimientoMaterial.IsEnabled = value;
                this._cbxDepartamento.IsEnabled = value;
                this._cbxSolicitanteMaterial.IsEnabled = value;
                this._cbxMaterialesSapi.IsEnabled = value;
                //this._cbxTipoMovimiento.IsEnabled = value;
                this._lstDetalleSolicitud.IsEnabled = value;
                this._btnMas.IsEnabled = value;
                this._btnMenos.IsEnabled = value;
            }
        }

        //public object TiposMovimientosMaterial
        //{
        //    get { return this._cbxTipoMovimiento.DataContext; }
        //    set { this._cbxTipoMovimiento.DataContext = value; }
        //}

        //public object TipoMovimientoMaterial
        //{
        //    get { return this._cbxTipoMovimiento.SelectedItem; }
        //    set { this._cbxTipoMovimiento.SelectedItem = value; }
        //}

        public string TextoBotonModificar
        {
            get { return this._txbAceptar.Text; }
            set { this._txbAceptar.Text = value; }
        }

        #endregion

        #region Constructores

        public GestionarMovimientoMaterialSapi()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarMovimientoMaterialSapi(this,null,null);
        }

        public GestionarMovimientoMaterialSapi(object solicitudSapi, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarMovimientoMaterialSapi(this, solicitudSapi, ventanaPadre);
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

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMaterial();
        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.QuitarMaterial();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmarEliminarSolicitudSapi, "Eliminar Solicitud de Material",
                MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
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
            this._cbxMaterialesSapi.SelectedIndex = 0;
        }

        public void MostrarBotonEliminar(bool flag)
        {
            if (flag)
                this._btnEliminar.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion

        

        

        
    }
}
