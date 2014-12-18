using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Presentadores.Patentes;

namespace Trascend.Bolet.Cliente.Ventanas.Patentes
{
    /// <summary>
    /// Interaction logic for ConsultarPatentes.xaml
    /// </summary>
    public partial class ConsultarPatentes : Page, IConsultarPatentes
    {

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarPatentes _presentador;
        private bool _cargada;


        #region IConsultarCesiones

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object PatenteParaFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        #region Patente

        public string Id
        {
            get { return this._txtId.Text; }
            set { this._txtId.Text = value; }
        }

        public string NombrePatente
        {
            get { return this._txtDescripcion.Text; }
            set { this._txtDescripcion.Text = value; }
        }

        public string Solicitud
        {
            get { return this._txtSolicitud.Text; }
            set { this._txtSolicitud.Text = value; }
        }

        public string Observacion
        {
            get { return this._txtObservaciones.Text; }
            set { this._txtObservaciones.Text = value; }
        }

        public object Patente
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string Fecha
        {
            get { return this._dpkFecha.Text; }
        }

        public object Patentes
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        #endregion

        #region Checks

        public bool PrioridadesEstaSeleccionado
        {
            get { return this._chkPrioridad.IsChecked.Value; }
            set { this._chkPrioridad.IsChecked = value; }
        }

        public bool BoletinesEstaSeleccionado
        {
            get { return this._chkBoletines.IsChecked.Value; }
            set { this._chkBoletines.IsChecked = value; }
        }


        #endregion

        #region Asociado

        public string IdAsociadoFiltrar
        {
            get { return this._txtIdAsociado.Text; }
            set { this._txtIdAsociado.Text = value; }
        }

        public string NombreAsociadoFiltrar
        {
            get { return this._txtNombreAsociado.Text; }
            set { this._txtNombreAsociado.Text = value; }
        }

        public object Asociados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public object Asociado
        {
            get { return this._lstAsociados.SelectedItem; }
            set { this._lstAsociados.SelectedItem = value; }
        }

        public string AsociadoFiltro
        {
            set { this._txtAsociado.Text = value; }
        }

        #endregion

        #region AsociadoInt

        public string IdAsociadoIntFiltrar
        {
            get { return this._txtIdAsociadoInt.Text; }
            set { this._txtIdAsociadoInt.Text = value; }
        }

        public string NombreAsociadoIntFiltrar
        {
            get { return this._txtNombreAsociadoInt.Text; }
            set { this._txtNombreAsociadoInt.Text = value; }
        }

        public object AsociadosInt
        {
            get { return this._lstAsociadosInt.DataContext; }
            set { this._lstAsociadosInt.DataContext = value; }
        }

        public object AsociadoInt
        {
            get { return this._lstAsociadosInt.SelectedItem; }
            set { this._lstAsociadosInt.SelectedItem = value; }
        }

        public string AsociadoIntFiltro
        {
            set { this._txtAsociadoInt.Text = value; }
        }

        #endregion

        #region Interesado

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresado.Text; }
            set { this._txtIdInteresado.Text = value; }
        }

        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresado.Text; }
            set { this._txtNombreInteresado.Text = value; }
        }

        public object Interesados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._lstInteresados.SelectedItem; }
            set { this._lstInteresados.SelectedItem = value; }
        }

        public string InteresadoFiltro
        {
            set { this._txtInteresado.Text = value; }
        }

        #endregion

        #region Boletin

        public object BoletinesOrdenPublicacion
        {
            get { return this._cbxBolOrdPublicacion.DataContext; }
            set { this._cbxBolOrdPublicacion.DataContext = value; }
        }

        public object BoletinOrdenPublicacion
        {
            get { return this._cbxBolOrdPublicacion.SelectedItem; }
            set { this._cbxBolOrdPublicacion.SelectedItem = value; }
        }

        public object BoletinesPublicacion
        {
            get { return this._cbxBolPublicacion.DataContext; }
            set { this._cbxBolPublicacion.DataContext = value; }
        }

        public object BoletinPublicacion
        {
            get { return this._cbxBolPublicacion.SelectedItem; }
            set { this._cbxBolPublicacion.SelectedItem = value; }
        }

        public object BoletinesConcesion
        {
            get { return this._cbxBolConcesion.DataContext; }
            set { this._cbxBolConcesion.DataContext = value; }
        }

        public object BoletinConcesion
        {
            get { return this._cbxBolConcesion.SelectedItem; }
            set { this._cbxBolConcesion.SelectedItem = value; }
        }

        #endregion

        #region Prioridad

        public string FechaPrioridad
        {
            get { return this._dpkPrioridadFecha.Text; }
        }

        public string IdPrioridad
        {
            get { return this._txtPrioridadCodigo.Text; }
            set { this._txtPrioridadCodigo.Text = value; }
        }

        #endregion

        #region Combobox

        public object Servicios
        {
            get { return this._cbxSituacion.DataContext; }
            set { this._cbxSituacion.DataContext = value; }
        }

        public object Servicio
        {
            get { return this._cbxSituacion.SelectedItem; }
            set { this._cbxSituacion.SelectedItem = value; }
        }

        public object Detalles
        {
            get { return this._cbxDetalleDatos.DataContext; }
            set { this._cbxDetalleDatos.DataContext = value; }
        }

        public object Detalle
        {
            get { return this._cbxDetalleDatos.SelectedItem; }
            set { this._cbxDetalleDatos.SelectedItem = value; }
        }

        public object Paises
        {
            get { return this._cbxPrioridadPais.DataContext; }
            set { this._cbxPrioridadPais.DataContext = value; }
        }

        public object PaisPrioridad
        {
            get { return this._cbxPrioridadPais.SelectedItem; }
            set { this._cbxPrioridadPais.SelectedItem = value; }
        }

        #endregion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
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


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        #region Filtro Internacional


        public string IdInternacional
        {
            get { return this._txtCodigoInternacional.Text; }
        }


        public string IdCorrelativoInternacional
        {
            get { return this._txtCodigoInternacional2.Text; }
        }


        public string ReferenciaInteresado
        {
            get { return this._txtReferenciaInteresado.Text; }
        }


        public string ReferenciaAsociado
        {
            get { return this._txtReferenciaAsociado.Text; }
        }


        public object AsociadosInternacionales
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }


        public object AsociadoInternacional
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }


        public object TiposBusqueda
        {
            get { return this._cbxTipoBusqueda.DataContext; }
            set
            {
                this._cbxTipoBusqueda.DataContext = value;
                //this._cbxTipoBusqueda.SelectedIndex = 1;
            }
        }


        public object TipoBusqueda
        {
            get { return this._cbxTipoBusqueda.SelectedItem; }
            set { this._cbxTipoBusqueda.SelectedItem = value; }
        }


        public object PaisesInt
        {
            get { return this._cbxPaisInternacional.DataContext; }
            set { this._cbxPaisInternacional.DataContext = value; }
        }


        public object PaisInt
        {
            get { return this._cbxPaisInternacional.SelectedItem; }
            set { this._cbxPaisInternacional.SelectedItem = value; }
        }


        #endregion

        public object OrigenesAsociados
        {
            get { return this._cbxOrigenAsociado.DataContext; }
            set { this._cbxOrigenAsociado.DataContext = value; }
        }

        public object OrigenAsociado
        {
            get { return this._cbxOrigenAsociado.SelectedItem; }
            set { this._cbxOrigenAsociado.SelectedItem = value; }
        }

        public object OrigenesInteresados
        {
            get { return this._cbxOrigenInteresado.DataContext; }
            set { this._cbxOrigenInteresado.DataContext = value; }
        }

        public object OrigenInteresado
        {
            get { return this._cbxOrigenInteresado.SelectedItem; }
            set { this._cbxOrigenInteresado.SelectedItem = value; }
        }

        public object OrigenesPatente
        {
            get { return this._cbxOrigenPatente.DataContext; }
            set { this._cbxOrigenPatente.DataContext = value; }
        }

        public object OrigenPatente
        {
            get { return this._cbxOrigenPatente.SelectedItem; }
            set { this._cbxOrigenPatente.SelectedItem = value; }
        }

        #endregion


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarPatentes()
        {
            InitializeComponent();
            this._cargada = false;

            this._presentador = new PresentadorConsultarPatentes(this);
        }


        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }


        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            //this._dpkFecha.Text = string.Empty;
            validarCamposVacios();
        }


        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrGestionarPatente();
        }


        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }


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


        private void _btnConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarPatente();
        }


        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void _btnTransferir_Click(object sender, RoutedEventArgs e)
        {

        }


        public void LimpiarCampos()
        {

        }


        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;
            if (!this._txtId.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtId.Focus();
            }

            if (!this._txtDescripcion.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDescripcion.Focus();
            }

            if (!this._dpkFecha.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFecha.Focus();
            }

            if (!this._txtObservaciones.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtObservaciones.Focus();
            }

            if (!this._txtSolicitud.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtSolicitud.Focus();
            }

            if (!this._txtRegistroTYR.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtRegistroTYR.Focus();
            }

            if ((this._cbxOrigenPatente.SelectedIndex != 0) && (this._cbxOrigenPatente.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxOrigenPatente.Focus();
            }


            if (todosCamposVacios)
                this._txtId.Focus();
        }


        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
            this._btnConsultarAsociado.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = false;
        }


        #region Asociado


        private void _txtAsociado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(true);
            GestionarVisibilidadFiltroInteresado(false);
        }


        private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociado())
                GestionarVisibilidadFiltroAsociado(false);
        }


        private void GestionarVisibilidadFiltroAsociado(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtAsociado.Visibility = Visibility.Collapsed;

                this._txtIdAsociado.Visibility = Visibility.Visible;
                this._txtNombreAsociado.Visibility = Visibility.Visible;
                this._lblIdAsociado.Visibility = Visibility.Visible;
                this._lblNombreAsociado.Visibility = Visibility.Visible;
                this._lstAsociados.Visibility = Visibility.Visible;
                this._btnConsultarAsociado.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtAsociado.Visibility = Visibility.Visible;

                this._txtIdAsociado.Visibility = Visibility.Collapsed;
                this._txtNombreAsociado.Visibility = Visibility.Collapsed;
                this._lblIdAsociado.Visibility = Visibility.Collapsed;
                this._lblNombreAsociado.Visibility = Visibility.Collapsed;
                this._lstAsociados.Visibility = Visibility.Collapsed;
                this._btnConsultarAsociado.Visibility = Visibility.Collapsed;
            }
        }


        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado();
        }


        private void _btnConsultarAsociadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarAsociado.IsDefault = true;
            this._btnConsultarInteresado.IsDefault = false;
        }


        #endregion

        #region AsociadoInt


        private void _txtAsociadoInt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociadoInt(true);
            GestionarVisibilidadFiltroAsociado(false);
            GestionarVisibilidadFiltroInteresado(false);
        }


        private void _lstAsociadosInt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociadoInt())
                GestionarVisibilidadFiltroAsociadoInt(false);
        }


        private void GestionarVisibilidadFiltroAsociadoInt(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtAsociadoInt.Visibility = Visibility.Collapsed;

                this._txtIdAsociadoInt.Visibility = Visibility.Visible;
                this._txtNombreAsociadoInt.Visibility = Visibility.Visible;
                this._lblIdAsociadoInt.Visibility = Visibility.Visible;
                this._lblNombreAsociadoInt.Visibility = Visibility.Visible;
                this._lstAsociadosInt.Visibility = Visibility.Visible;
                this._btnConsultarAsociadoInt.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtAsociadoInt.Visibility = Visibility.Visible;

                this._txtIdAsociadoInt.Visibility = Visibility.Collapsed;
                this._txtNombreAsociadoInt.Visibility = Visibility.Collapsed;
                this._lblIdAsociadoInt.Visibility = Visibility.Collapsed;
                this._lblNombreAsociadoInt.Visibility = Visibility.Collapsed;
                this._lstAsociadosInt.Visibility = Visibility.Collapsed;
                this._btnConsultarAsociadoInt.Visibility = Visibility.Collapsed;
            }
        }


        private void _btnConsultarAsociadoInt_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociadoInt();
        }


        private void _btnConsultarAsociadoIntFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarAsociadoInt.IsDefault = true;
            this._btnConsultarInteresado.IsDefault = false;
        }


        #endregion


        #region Interesado


        private void _txtInteresado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(false);
            GestionarVisibilidadFiltroAsociadoInt(false);
            GestionarVisibilidadFiltroInteresado(true);
        }


        private void GestionarVisibilidadFiltroInteresado(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtInteresado.Visibility = Visibility.Collapsed;

                this._txtIdInteresado.Visibility = Visibility.Visible;
                this._txtNombreInteresado.Visibility = Visibility.Visible;
                this._lblIdInteresado.Visibility = Visibility.Visible;
                this._lblNombreInteresado.Visibility = Visibility.Visible;
                this._lstInteresados.Visibility = Visibility.Visible;
                this._btnConsultarInteresado.Visibility = Visibility.Visible;

            }
            else
            {
                this._txtInteresado.Visibility = Visibility.Visible;

                this._txtIdInteresado.Visibility = Visibility.Collapsed;
                this._txtNombreInteresado.Visibility = Visibility.Collapsed;
                this._lblIdInteresado.Visibility = Visibility.Collapsed;
                this._lblNombreInteresado.Visibility = Visibility.Collapsed;
                this._lstInteresados.Visibility = Visibility.Collapsed;
                this._btnConsultarInteresado.Visibility = Visibility.Collapsed;
            }
        }


        private void _lstInteresados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresado())
                GestionarVisibilidadFiltroInteresado(false);
        }


        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado();
        }


        private void _btnConsultarInteresadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarAsociado.IsDefault = false;
            this._btnConsultarAsociadoInt.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = true;
        }


        #endregion


        #region Checks


        public void _chkBoletines_Click(object sender, RoutedEventArgs e)
        {
            if (this._chkBoletines.IsChecked.Value)
                this._boletines.Visibility = Visibility.Visible;
            else
                this._boletines.Visibility = Visibility.Collapsed;
        }


        public void _chkPrioridad_Click(object sender, RoutedEventArgs e)
        {
            if (this._chkPrioridad.IsChecked.Value)
                this._prioridad.Visibility = Visibility.Visible;
            else
                this._prioridad.Visibility = Visibility.Collapsed;
        }


        #endregion


        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            {
                this._presentador.Consultar();
            }
        }


        private void _cbxTipoBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._cbxTipoBusqueda.SelectedIndex == 0)
            {
                HabilitarZonaInternacional(true);
            }
            else if (this._cbxTipoBusqueda.SelectedIndex == 1)
            {
                HabilitarZonaInternacional(false);
            }
        }


        private void HabilitarZonaInternacional(bool valor)
        {
            this._txtCodigoInternacional.IsEnabled = valor;
            this._txtCodigoInternacional2.IsEnabled = valor;
            this._cbxPaisInternacional.IsEnabled = valor;
            this._txtReferenciaInteresado.IsEnabled = valor;
            this._txtReferenciaAsociado.IsEnabled = valor;

        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Navegar(new ConsultarPatentes());
        }



        #region TYR



        public string NumeroCodigoRegistro
        {
            get { return this._txtRegistroTYR.Text; }
            set { this._txtRegistroTYR.Text = value; }
        }
        
        
        public bool TYREstaSeleccionado
        {
            get { return this._chkTYR.IsChecked.Value; }
            set { this._chkTYR.IsChecked = value; }
        }

        

        

        public string FechaRegistro
        {
            get { return this._dpkFechaTYR.Text; }
            set { this._dpkFechaTYR.Text = value; }
        }

        public string ExpCambioPendiente
        {
            get { return this._txtCambioPendiente.Text; }
            set { this._txtCambioPendiente.Text = value; }
        }

        public void _chkTYR_Click(object sender, RoutedEventArgs e)
        {
            if (this._chkTYR.IsChecked.Value)
                this._TYR.Visibility = Visibility.Visible;
            else
                this._TYR.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
