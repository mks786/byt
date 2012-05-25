using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Anualidades;
using Trascend.Bolet.Cliente.Presentadores.Anualidades;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Input;

namespace Trascend.Bolet.Cliente.Ventanas.Anualidades
{
    /// <summary>
    /// Interaction logic for GestionarAnualidades.xaml
    /// </summary>
    public partial class GestionarAnualidades : Page, IGestionarAnualidades
    {

        private PresentadorGestionarAnualidades _presentador;
        private bool _cargada;
        private bool _asociadosCargados;
        private bool _interesadosCargados;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarAnualidades

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Patente
        {
            get
            {
                return this._gridDatos.DataContext;
            }
            set
            {
                this._gridDatos.DataContext = value;
            }
        }

        public string IdPatenteFiltrar
        {
            get { return this._txtIdPatenteFiltrar.Text; }
        }

        public string NombrePatenteFiltrar
        {
            get { return this._txtNombrePatenteFiltrar.Text; }
        }

        //public object Patente
        //{
        //    get { return this._gridDatosPatente.DataContext; }
        //    set { this._gridDatosPatente.DataContext = value; }
        //}

        public string Region
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public bool HabilitarCampos
        {
            set
            {
                
           //this._txtIdPatenteFiltrar.IsEnabled = value;
           //     this._txtNombrePatente.IsEnabled = value;
           //     this._txtNombrePatenteFiltrar.IsEnabled = value;
           //     this._btnConsultarPatente.IsEnabled = value;
           //     this._txtAsociadoSolicitud.IsEnabled = value;
           //     this._txtIdAsociadoSolicitud.IsEnabled = value;
           //     this._txtNombreAsociadoSolicitud.IsEnabled = value;
           //     this._btnConsultarAsociadoSolicitud.IsEnabled = value;
           //     this._btnConsultarInteresadoSolicitud.IsEnabled = value;
           //     this._txtNombreInteresadoSolicitud.IsEnabled = value;
           //     this._txtIdInteresadoSolicitud.IsEnabled = value;
           //     this._txtInteresadoSolicitud.IsEnabled = value;
           //     this._cbxBoletinConcesion.IsEnabled = value;
           //     this._cbxBoletinPublicacion.IsEnabled = value;
           //     this._txtCodigoInscripcion.IsEnabled = value;
           //     this._txtFechaInscripcion.IsEnabled = value;
           //     this._cbxSituacion.IsEnabled = value;
           //     this._txtReferencia.IsEnabled = value;
           //     this._txtCodigoRegistro.IsEnabled = value;
           //     this._txtFechaRegistro.IsEnabled = value;
           //     this._dpkFecha.IsEnabled = value;
                this._dpkFechaAnualidad.IsEnabled = value;
                this._dpkFechaVoucher.IsEnabled = value;
                this._txtRecibo.IsEnabled = value;
                this._txtVoucher.IsEnabled = value;
                this._cbxISituacion.IsEnabled = value;
                this._lstAnualidades.IsEnabled = value;
                this._btnMas.IsEnabled = value;
                this._btnMenos.IsEnabled = value;
                this._btnNuevaAnualidad.IsEnabled = value;
                this._btnModificarSeleccionado.IsEnabled = value;
                this._chkFactura.IsEnabled = value;

            }
        }

        public string NombrePatente
        {
            set { this._txtNombrePatente.Text = value; }
        }

        public object PatentesFiltradas
        {
            get { return this._lstPatentes.DataContext; }
            set { this._lstPatentes.DataContext = value; }
        }

        public object PatenteFiltrada
        {
            get { return this._lstPatentes.SelectedItem; }
            set { this._lstPatentes.SelectedItem = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public string TextoBotonRegresar
        {
            get { return this._txbRegresar.Text; }
            set { this._txbRegresar.Text = value; }
        }

        public string Referencia
        {
            get { return this._txtReferencia.Text; }
            set { this._txtReferencia.Text = value; }
        }

        public string RegistroCodigo
        {
            get { return this._txtCodigoRegistro.Text; }
            set { this._txtCodigoRegistro.Text = value; }
        }

        public string RegistroFecha
        {
            get { return this._txtFechaRegistro.Text; }
            set { this._txtFechaRegistro.Text = value; }
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

        #region Asociados

        public string IdAsociadoSolicitudFiltrar
        {
            get { return this._txtIdAsociadoSolicitud.Text; }
        }

        public string NombreAsociadoSolicitud
        {
            get { return this._txtAsociadoSolicitud.Text; }
            set { this._txtAsociadoSolicitud.Text = value; }
        }

        public string IdAsociadoSolicitud
        {
            set { this._txtIdAsociadoSolicitud.Text = value; }
        }

        public string NombreAsociadoSolicitudFiltrar
        {
            get { return this._txtNombreAsociadoSolicitud.Text; }
        }

        public string NombreAsociadoDatosFiltrar
        {
            get { return this._txtNombreAsociadoSolicitud.Text; }
        }

        public object AsociadosSolicitud
        {
            get { return this._lstAsociadosSolicitud.DataContext; }
            set { this._lstAsociadosSolicitud.DataContext = value; }
        }

        public object AsociadoSolicitud
        {
            get { return this._lstAsociadosSolicitud.SelectedItem; }
            set { this._lstAsociadosSolicitud.SelectedItem = value; }
        }

        public bool AsociadosEstanCargados
        {
            get { return this._asociadosCargados; }
            set { this._asociadosCargados = value; }
        }

        #endregion

        #region Interesados

        public bool InteresadosEstanCargados
        {
            get { return this._interesadosCargados; }
            set { this._interesadosCargados = value; }
        }

        public string IdInteresadoSolicitudFiltrar
        {
            get { return this._txtIdInteresadoSolicitud.Text; }
        }

        public string NombreInteresadoSolicitudFiltrar
        {
            get { return this._txtNombreInteresadoSolicitud.Text; }
        }

        public string NombreInteresadoDatosFiltrar
        {
            get { return this._txtNombreInteresadoSolicitud.Text; }
        }

        public string NombreInteresadoSolicitud
        {
            get { return this._txtInteresadoSolicitud.Text; }
            set { this._txtInteresadoSolicitud.Text = value; }
        }

        public object InteresadosSolicitud
        {
            get { return this._lstInteresadosSolicitud.DataContext; }
            set { this._lstInteresadosSolicitud.DataContext = value; }
        }

        public object InteresadoSolicitud
        {
            get { return this._lstInteresadosSolicitud.SelectedItem; }
            set
            {
                this._lstInteresadosSolicitud.SelectedItem = value;
                //this._lstInteresadosSolicitud.ScrollIntoView(value);
            }
        }
        #endregion

        #region Boletines y Situacion

        public object BoletinesPublicacion
        {
            get { return this._cbxBoletinPublicacion.DataContext; }
            set { this._cbxBoletinPublicacion.DataContext = value; }
        }

        public object BoletinPublicacion
        {
            get { return this._cbxBoletinPublicacion.SelectedItem; }
            set { this._cbxBoletinPublicacion.SelectedItem = value; }
        }

        public object BoletinesConcesion
        {
            get { return this._cbxBoletinConcesion.DataContext; }
            set { this._cbxBoletinConcesion.DataContext = value; }
        }

        public object BoletinConcesion
        {
            get { return this._cbxBoletinConcesion.SelectedItem; }
            set { this._cbxBoletinConcesion.SelectedItem = value; }
        }

        public object Situaciones
        {
            get { return this._cbxSituacion.DataContext; }
            set { this._cbxSituacion.DataContext = value; }
        }

        public object Situacion
        {
            get { return this._cbxSituacion.SelectedItem; }
            set { this._cbxSituacion.SelectedItem = value; }
        }


        #endregion

        #region Anualidades

        public object Anualidad
        {
            get { return this._lstAnualidades.SelectedItem; }
            set { this._lstAnualidades.SelectedItem = value; }
        }

        public object Anualidades
        {
            get { return this._lstAnualidades.DataContext; }
            set { this._lstAnualidades.DataContext = value; }
        }

        public string FechaAnualidad
        {
            get { return this._dpkFechaAnualidad.Text; }
            set { this._dpkFechaAnualidad.Text = value; }
        }

        public string Recibo
        {
            get { return this._txtRecibo.Text; }
            set { this._txtRecibo.Text = value; }
        }

        public string FechaFactura
        {
            get { return this._dpkFechaFactura.Text; }
            set { this._dpkFechaFactura.Text = value; }
        }

        public string Factura
        {
            get { return this._txtFactura.Text; }
            set { this._txtFactura.Text = value; }
        }

        public object ISituaciones
        {
            get { return this._cbxISituacion.DataContext; }
            set { this._cbxISituacion.DataContext = value; }
        }

        public object ISituacion
        {
            get { return this._cbxISituacion.SelectedItem; }
            set { this._cbxISituacion.SelectedItem = value; }
        }

        public string FechaVoucher
        {
            get { return this._dpkFechaVoucher.Text; }
            set { this._dpkFechaVoucher.Text = value; }
        }

        public string Voucher
        {
            get { return this._txtVoucher.Text; }
            set { this._txtVoucher.Text = value; }
        }

        public CheckBox ChkFactura
        {
            get { return this._chkFactura; }
        }
        #endregion
        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion

        public GestionarAnualidades(object fusion)
        {
            InitializeComponent();
            this._cargada = false;
            this._asociadosCargados = false;
            this._interesadosCargados = false;
            this._presentador = new PresentadorGestionarAnualidades(this, fusion);
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {            
            if (this.TextoBotonRegresar == Recursos.Etiquetas.btnRegresar)
                this._presentador.Regresar();
            else if (this.TextoBotonRegresar == Recursos.Etiquetas.btnCancelar)
                this._presentador.Cancelar();
        }


        private void _btnPlanilla_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnPlanillaVan_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnPlanillaVienen_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnAnexo_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnCarpeta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                if (this._presentador.CargarAnualidad())
                    this._lstAnualidades.Visibility = System.Windows.Visibility.Visible;
                EstaCargada = true;
            }
        }


        #region Eventos Patentes

        private void _btnConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPatentes();
        }

        private void _lstPatentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPatente())
            {
                GestionarVisibilidadDatosDePatente(Visibility.Visible);
                GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

                this._btnConsultarPatente.IsDefault = false;
                this._btnModificar.IsDefault = true;
            }
        }

        private void _OrdenarPatentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPatentes);
        }

        private void _txtNombrePatente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDePatente(Visibility.Collapsed);
            GestionarVisibilidadFiltroPatente(Visibility.Visible);
            this._btnConsultarPatente.IsDefault = false;
            this._btnModificar.IsDefault = true;
        }

        private void GestionarVisibilidadFiltroPatente(object value)
        {
            this._lblNombrePatente.Visibility = (System.Windows.Visibility)value;
            this._txtNombrePatenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPatente.Visibility = (System.Windows.Visibility)value;
            this._txtIdPatenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPatentes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPatente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDePatente(object value)
        {
            this._txtNombrePatente.Visibility = (System.Windows.Visibility)value;

        }

        private void _txtPatenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPatente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        #endregion

        #region Asociados


        private void _txtAsociadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._asociadosCargados)
            {
                this._presentador.CargarAsociados();
            }

            this._btnModificar.IsDefault = false;
            this._btnConsultarAsociadoSolicitud.IsDefault = true;

            mostrarLstAsociadoSolicitud();

        }

        private void _OrdenarAsociadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAsociadosSolicitud);
        }

        private void _btnConsultarAsociadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado(0);
        }

        private void _lstAsociadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociadoSolicitud();
            ocultarLstAsociadoSolicitud();
            //ocultarLstAsociadoDatos();
            this._btnConsultarAsociadoSolicitud.IsDefault = false;
            this._btnModificar.IsDefault = true;
        }


        private void mostrarLstAsociadoSolicitud()
        {
            this._lstAsociadosSolicitud.ScrollIntoView(this.AsociadoSolicitud);
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociadosSolicitud.IsEnabled = true;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        private void ocultarLstAsociadoSolicitud()
        {
            this._lstAsociadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }


        #endregion

        #region Interesados


        private void mostrarLstInteresadoSolicutud()
        {
            this._lstInteresadosSolicitud.ScrollIntoView(this.InteresadoSolicitud);
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresadosSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresadosSolicitud.IsEnabled = true;
            this._btnConsultarInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
        }

        private void ocultarLstInteresadoSolicutud()
        {
            this._presentador.CambiarInteresadoSolicitud();
            this._lstInteresadosSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoSolicitud.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoSolicitud.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void _txtInteresadoSolicitud_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._interesadosCargados)
            {
                this._presentador.CargarInteresados();
            }

            //ocultarLstCorresponsalSolicutud();
            ocultarLstAsociadoSolicitud();

            this._btnModificar.IsDefault = false;
            this._btnConsultarInteresadoSolicitud.IsDefault = true;

            mostrarLstInteresadoSolicutud();
        }

        private void _btnConsultarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado(0);
        }

        private void _lstInteresadosSolicitud_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarInteresadoSolicitud();
            ocultarLstInteresadoSolicutud();
            //ocultarLstInteresadoDatos();

            this._btnConsultarInteresadoSolicitud.IsDefault = false;
            this._btnModificar.IsDefault = true;
        }

        private void _OrdenarInteresadoSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosSolicitud);
        }

        #endregion


        #region Anualidades

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {

            if ((this._presentador.AgregarAnualidad()) && (this._lstAnualidades.Visibility == System.Windows.Visibility.Collapsed))
                this._lstAnualidades.Visibility = System.Windows.Visibility.Visible;
            this._gridCamposAnualidad.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.DeshabilitarAnualidad())
            {
                this._lstAnualidades.Visibility = System.Windows.Visibility.Collapsed;
            }
            _presentador.CargarAnualidadSeleccionada(true);
            _gridCamposAnualidad.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void _btnModificarSeleccionado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ModificarAnualidad();
            
        }


        private void _btnNuevaAnualidad_Click(object sender, RoutedEventArgs e)
        {
            this._gridCamposAnualidad.Visibility = System.Windows.Visibility.Visible;
            this._presentador.CargarAnualidadSeleccionada(true);
        }

        private void _lstAnualidades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._gridCamposAnualidad.Visibility = System.Windows.Visibility.Visible;
            this._presentador.CargarAnualidadSeleccionada(false);

        }

        private void _chkFactura_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)this._chkFactura.IsChecked)
            {
                this._dpkFechaFactura.IsEnabled = true;
                this._txtFactura.IsEnabled = true;
            }
            else {
                this._dpkFechaFactura.IsEnabled = false;
                this._txtFactura.IsEnabled = false;
            }

        }
        #endregion

        public void FocoPredeterminado()
        {
            this._txtIdPatenteFiltrar.Focus();
        }

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name.Equals("_btnConsultarPatente"))
                this._presentador.ConsultarPatentes();

        }

        public void AgregarAnualidad()
        {
            throw new System.NotImplementedException();
        }

        public void CargarAnualidad()
        {
            throw new System.NotImplementedException();
        }

        public void DeshabilitarAnualidad()
        {
            throw new System.NotImplementedException();
        }

        public void CargarAnualidadSeleccionada()
        {
            throw new System.NotImplementedException();
        }





    }
}
