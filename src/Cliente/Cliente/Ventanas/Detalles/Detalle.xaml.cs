using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Recursos;
using Trascend.Bolet.Cliente.Presentadores.Detalles;
using Trascend.Bolet.Cliente.Contratos.Detalles;
using Trascend.Bolet.Cliente.Ventanas.Principales;

namespace Trascend.Bolet.Cliente.Ventanas.Detalles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Detalle : Window, IVentanaDetalle
    {

        private bool _cargada;
        private PresentadorVentanaDetalle _presentador;

        #region Singleton

        private static Detalle _ventanaDetalle;

        public static Detalle ObtenerInstancia
        {
            get
            {
                if (null == _ventanaDetalle)
                    _ventanaDetalle = new Detalle();
                return _ventanaDetalle;
            }
        }

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        private Detalle()
        {
            InitializeComponent();
            this._presentador = new PresentadorVentanaDetalle(this);

        }

        #endregion

        #region IVentanaDetalle

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txbAceptar.Focus();
        }


        public object Data
        {
            get
            {
                return null;
            }
            set
            {  
            }
        }
        #endregion

        #region Eventos del menú

        //#region Historia

        //private void _menuItemAgenteAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarAgente();
        //}

        //private void _menuItemAgenteConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarAgentes();
        //}

        //private void _menuItemBoletinConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarBoletines();
        //}

        //private void _menuItemBoletinAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarBoletin();
        //}

        //private void _menuItemEstadoConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarEstados();
        //}

        //private void _menuItemEstadoAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarEstados();
        //}

        //private void _menuItemInternacionalConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarInternacionales();
        //}

        //private void _menuItemInternacionalAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarInternacional();
        //}

        //private void _menuItemNacionalConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarNacionales();
        //}

        //private void _menuItemNacionalAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarNacionales();
        //}

        //private void _menuItemObjetoConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarObjetos();
        //}

        //private void _menuItemObjetoAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarObjeto();
        //}

        //private void _menuItemGestionarObjetosXRoles_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.GestionarObjetosXRoles();
        //}

        //private void _menuItemPaisConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarPais();
        //}

        //private void _menuItemPaisAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarPais();
        //}

        //private void _menuItemResolucionConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarResoluciones();
        //}

        //private void _menuItemResolucionAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarResolucion();
        //}

        //private void _menuItemRolConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarRoles();
        //}

        //private void _menuItemRolAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarRol();
        //}

        //private void _menuItemTipoInfobolConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarTipoInfoboles();
        //}

        //private void _menuItemTipoInfobolAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarTipoInfobol();
        //}

        //private void _menuItemUsuarioConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarUsuarios();
        //}

        //private void _menuItemUsuarioAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarUsuarios();
        //} 
        
        //private void _menuItemTipoFechaConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarTipoFechas();
        //}

        //private void _menuItemTipoFechaAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarTipoFecha();
        //}

        //private void _menuItemEstatusAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarEstatus();
        //}

        //private void _menuItemEstatusConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarEstatuses();
        //}

        //private void _menuItemPoderConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarPoderes();

        //}

        //private void _menuItemPoderAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarPoder();
        //}

        //private void _menuItemGestionarPoderesXAgentes_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.GestionarPoderXAgente();
        //}

        //private void _menuItemInteresadoConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarInteresados();
        //}

        //private void _menuItemInteresadoAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarInteresado();
        //}

        //private void _menuItemAsociadoConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarAsociados();
        //}

        //private void _menuItemAsociadoAgregar_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.AgregarAsociado();
        //}

        //#endregion

        #endregion

        #region Eventos de la ventana

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            //Al ejecutar la siClienteente línea se dispara el evento "Window_Closing"

            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VentanaPrincipal.ObtenerInstancia.IsEnabled = true;
            _ventanaDetalle = null;

        }

        #endregion

        #region Métodos

        //public void AplicarPermisologia()
        //{
        //    this._presentador.AplicarPermisologia();
        //}

        #endregion

    }
}
