using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using Trascend.Bolet.ControlesByT;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Interaction logic for ConsultarTodosPoder.xaml
    /// </summary>
    public partial class ConsultarMarcas : Page, IConsultarMarcas
    {

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarMarcas _presentador;
        private bool _cargada;


        #region IConsultarMarcas


        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }


        public string Id
        {
            get { return this._txtId.Text; }
            set { this._txtId.Text = value; }
        }


        public string NombreMarca
        {
            set { this._txtMarcaNombre.Text = value; }
        }


        public object MarcaSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
        }


        //public string FichasFiltrar
        //{
        //    get { return this._txtFichas.Text; }
        //}


        public string DescripcionFiltrar
        {
            get { return this._txtDescripcion.Text; }
            set { this._txtDescripcion.Text = value; }
        }


        public string Fecha
        {
            get { return this._dpkFecha.SelectedDate.ToString(); }
            set { this._dpkFecha.Text = value; }
        }


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


        public string IdCorresponsalFiltrar
        {
            get { return this._txtIdCorresponsal.Text; }
            set { this._txtIdCorresponsal.Text = value; }
        }


        public string NombreCorresponsalFiltrar
        {
            get { return this._txtNombreCorresponsal.Text; }
            set { this._txtNombreCorresponsal.Text = value; }
        }


        public object Corresponsales
        {
            get { return this._lstCorresponsals.DataContext; }
            set { this._lstCorresponsals.DataContext = value; }
        }


        public object Corresponsal
        {
            get { return this._lstCorresponsals.SelectedItem; }
            set { this._lstCorresponsals.SelectedItem = value; }
        }


        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresado.Text; }
            set { this._txtIdInteresado.Text = value; }
        }


        public string Solicitud
        {
            get { return this._txtSolicitud.Text; }
            set { this._txtSolicitud.Text = value; }
        }


        public string Distingue
        {
            get { return this._txtDistingue.Text; }
            set { this._txtDistingue.Text = value; }
        }


        public string DistingueEnIngles
        {
            get { return this._txtDistingueIngles.Text; }
            set { this._txtDistingueIngles.Text = value; }
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


        public string Otros
        {
            get { return this._txtOtros.Text; }
            set { this._txtOtros.Text = value; }
        }


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        public string AsociadoFiltro
        {
            set { this._txtAsociado.Text = value; }
        }


        public string InteresadoFiltro
        {
            set { this._txtInteresado.Text = value; }
        }


        public string CorresponsalFiltro
        {
            set { this._txtCorresponsal.Text = value; }
        }


        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


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
            get { return this._cbxPaisInternacional.DataContext; }
            set { this._cbxPaisInternacional.DataContext = value; }
        }


        public object Pais
        {
            get { return this._cbxPaisInternacional.SelectedItem; }
            set { this._cbxPaisInternacional.SelectedItem = value; }
        }


        public object PaisesPrioridad
        {
            get { return this._cbxPrioridadPais.DataContext; }
            set { this._cbxPrioridadPais.DataContext = value; }
        }


        public object PaisPrioridad
        {
            get { return this._cbxPrioridadPais.SelectedItem; }
            set { this._cbxPrioridadPais.SelectedItem = value; }
        }


        public object Condiciones
        {
            get { return this._cbxCondicion.DataContext; }
            set { this._cbxCondicion.DataContext = value; }
        }


        public object Condicion
        {
            get { return this._cbxCondicion.SelectedItem; }
            set { this._cbxCondicion.SelectedItem = value; }
        }


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


        public object CambioDeDomicilioSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
        }


        public string IdMarcaFiltrar
        {
            get { return this._txtIdMarcaFiltrar.Text; }
            set { this._txtIdMarcaFiltrar.Text = value; }
        }


        public string ClaseInternacional
        {
            get { return this._txtClaseInternacional.Text; }
            set { this._txtClaseInternacional.Text = value; }
        }


        public string ClaseNacional
        {
            get { return this._txtClaseNacional.Text; }
            set { this._txtClaseNacional.Text = value; }
        }


        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
            set { this._txtNombreMarcaFiltrar.Text = value; }
        }


        public string PrioridadCodigo
        {
            get { return this._txtPrioridadCodigo.Text; }
            set { this._txtPrioridadCodigo.Text = value; }
        }


        public object Marcas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }


        public object Marca
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }


        public bool InternacionalEstaSeleccionado
        {
            get { return true; }
            set { }
        }


        public bool NacionalEstaSeleccionado
        {
            get { return this._chkNacional.IsChecked.Value; }
            set { this._chkNacional.IsChecked = value; }
        }


        public bool TYREstaSeleccionado
        {
            get { return this._chkTYR.IsChecked.Value; }
            set { this._chkTYR.IsChecked = value; }
        }


        public bool IndicadoresEstaSeleccionado
        {
            get { return this._chkIndicadores.IsChecked.Value; }
            set { this._chkIndicadores.IsChecked = value; }
        }


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


        public string CodigoRegistro
        {
            get { return this._txtRegistroTYR.Text; }
            set { this._txtRegistroTYR.Text = value; }
        }


        public string FechaRegistro
        {
            get { return this._dpkFechaTYR.Text; }
            set { this._dpkFechaTYR.Text = value; }
        }


        public string PrioridadFecha
        {
            get { return this._dpkPrioridadFecha.Text; }
            set { this._dpkPrioridadFecha.Text = value; }
        }


        public bool RenovadoPorOtroTramitante
        {
            get { return this._chkRenovadaPorOtroTramitante.IsChecked.Value; }
            set { this._chkRenovadaPorOtroTramitante.IsChecked = value; }
        }


        public bool InstruccionesDeRenovacion
        {
            get { return this._chkInstruccionesDeRenovacion.IsChecked.Value; }
            set { this._chkInstruccionesDeRenovacion.IsChecked = value; }
        }


        public string ExpCambioPendiente
        {
            get { return this._txtCambioPendiente.Text; }
            set { this._txtCambioPendiente.Text = value; }
        }


        public object MarcaParaFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }


        #region Filtro Internacional


        public string IdInternacional
        {
            get { return this._txtCodigoInternacional.Text; }
            set { this._txtCodigoInternacional.Text = value; }
        }


        public string IdCorrelativoInternacional
        {
            get { return this._txtCodigoInternacional2.Text; }
            set { this._txtCodigoInternacional2.Text = value; }
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


        #endregion


        public void LimpiarCampos()
        {
            this._txtClaseInternacional.Text = "";
            this._txtClaseNacional.Text = "";
            //this._txtIdInternacional.Text = "";
        }


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


        #endregion


        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarMarcas()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarMarcas(this);
        }


        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }


        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            this._dpkFecha.Text = string.Empty;
            validarCamposVacios();
        }


        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarMarca();
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


        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
            this._btnConsultarAsociado.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = false;
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

            //if (!this._txtFichas.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtFichas.Focus();
            //}

            if (!this._dpkFecha.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFecha.Focus();
            }

            if (!this._txtSolicitud.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtSolicitud.Focus();
            }

            if (!_txtRegistroTYR.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtRegistroTYR.Focus();
            }

            if (!_txtOtros.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtOtros.Focus();
            }


            if (todosCamposVacios)
                this._txtId.Focus();
        }


        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }


        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void _btnTransferir_Click(object sender, RoutedEventArgs e)
        {

        }


        #region Asociado

        private void _txtAsociado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(true);
            GestionarVisibilidadFiltroInteresado(false);
            GestionarVisibilidadFiltroCorresponsal(false);
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
            this._btnConsultarCorresponsal.IsDefault = false;
        }


        #endregion


        #region Interesado

        private void _txtInteresado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(false);
            GestionarVisibilidadFiltroCorresponsal(false);
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
            this._btnConsultarCorresponsal.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = true;
        }


        #endregion


        #region Corresponsal

        private void _txtCorresponsal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroCorresponsal(true);
            GestionarVisibilidadFiltroInteresado(false);
            GestionarVisibilidadFiltroAsociado(false);
        }


        private void _lstCorresponsals_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarCorresponsal())
                GestionarVisibilidadFiltroCorresponsal(false);
        }


        private void GestionarVisibilidadFiltroCorresponsal(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtCorresponsal.Visibility = Visibility.Collapsed;

                this._txtIdCorresponsal.Visibility = Visibility.Visible;
                this._txtNombreCorresponsal.Visibility = Visibility.Visible;
                this._lblIdCorresponsal.Visibility = Visibility.Visible;
                this._lblNombreCorresponsal.Visibility = Visibility.Visible;
                this._lstCorresponsals.Visibility = Visibility.Visible;
                this._btnConsultarCorresponsal.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtCorresponsal.Visibility = Visibility.Visible;

                this._txtIdCorresponsal.Visibility = Visibility.Collapsed;
                this._txtNombreCorresponsal.Visibility = Visibility.Collapsed;
                this._lblIdCorresponsal.Visibility = Visibility.Collapsed;
                this._lblNombreCorresponsal.Visibility = Visibility.Collapsed;
                this._lstCorresponsals.Visibility = Visibility.Collapsed;
                this._btnConsultarCorresponsal.Visibility = Visibility.Collapsed;
            }
        }


        private void _btnConsultarCorresponsal_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarCorresponsal();
        }


        private void _btnConsultarCorresponsalFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarAsociado.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = false;
            this._btnConsultarCorresponsal.IsDefault = true;
        }


        #endregion


        #region Checks

        private void _chkInternacional_Click(object sender, RoutedEventArgs e)
        {
            //GestionarVisibilidadFiltroInternacional(this._chkInternacional.IsChecked.Value);
            //if (this._chkInternacional.IsChecked.Value)
            //{
            //    this._chkNacional.IsChecked = !this._chkInternacional.IsChecked.Value;
            //    GestionarVisibilidadFiltroNacional(!this._chkInternacional.IsChecked.Value);
            //}
        }


        public void GestionarVisibilidadFiltroInternacional(bool visibilidad)
        {
            if (visibilidad)
            {
                //this._lblIdInternacional.Visibility = Visibility.Visible;
                //this._txtIdInternacional.Visibility = Visibility.Visible;

                this._lblCodigoInternacional.Visibility = Visibility.Visible;
                this._txtCodigoInternacional.Visibility = Visibility.Visible;
                this._txtCodigoInternacional2.Visibility = Visibility.Visible;

                this._lblPaisInternacional.Visibility = Visibility.Visible;
                this._cbxPaisInternacional.Visibility = Visibility.Visible;

                this._lblReferenciaInteresado.Visibility = Visibility.Visible;
                this._txtReferenciaInteresado.Visibility = Visibility.Visible;

                this._lblReferenciaAsociado.Visibility = Visibility.Visible;
                this._txtReferenciaAsociado.Visibility = Visibility.Visible;
            }
            else
            {
                //this._lblIdInternacional.Visibility = Visibility.Collapsed;
                //this._txtIdInternacional.Visibility = Visibility.Collapsed;

                this._lblCodigoInternacional.Visibility = Visibility.Collapsed;
                this._txtCodigoInternacional.Visibility = Visibility.Collapsed;
                this._txtCodigoInternacional2.Visibility = Visibility.Collapsed;

                this._lblPaisInternacional.Visibility = Visibility.Collapsed;
                this._cbxPaisInternacional.Visibility = Visibility.Collapsed;

                this._lblReferenciaInteresado.Visibility = Visibility.Collapsed;
                this._txtReferenciaInteresado.Visibility = Visibility.Collapsed;

                this._lblReferenciaAsociado.Visibility = Visibility.Collapsed;
                this._txtReferenciaAsociado.Visibility = Visibility.Collapsed;
            }
        }


        private void _chkNacional_Click(object sender, RoutedEventArgs e)
        {
            GestionarVisibilidadFiltroNacional(this._chkNacional.IsChecked.Value);

            if (this._chkNacional.IsChecked.Value)
            {
                //this._chkInternacional.IsChecked = !this._chkNacional.IsChecked.Value;
                //GestionarVisibilidadFiltroInternacional(!this._chkNacional.IsChecked.Value);
            }
        }


        public void GestionarVisibilidadFiltroNacional(bool visibilidad)
        {
            if (visibilidad)
            {
                this._lblId.Visibility = Visibility.Visible;
                this._txtId.Visibility = Visibility.Visible;

                this._lblSolicitud.Visibility = Visibility.Visible;
                this._txtSolicitud.Visibility = Visibility.Visible;
                this._dpkFecha.Visibility = Visibility.Visible;

                this._lblMarca.Visibility = Visibility.Visible;
                this._txtDescripcion.Visibility = Visibility.Visible;

                this._txtInteresado.Visibility = Visibility.Visible;
                this._txtAsociado.Visibility = Visibility.Visible;
                this._txtCorresponsal.Visibility = Visibility.Visible;

                this._lblAsociado.Visibility = Visibility.Visible;
                this._lblCorresponsal.Visibility = Visibility.Visible;
                this._lblInteresado.Visibility = Visibility.Visible;

                this._nacional.Visibility = Visibility.Visible;

                this._lstInteresados.Visibility = Visibility.Collapsed;
                this._btnConsultarInteresado.Visibility = Visibility.Collapsed;
                this._txtIdInteresado.Visibility = Visibility.Collapsed;
                this._txtNombreInteresado.Visibility = Visibility.Collapsed;
                this._lblNombreInteresado.Visibility = Visibility.Collapsed;
                this._lblIdInteresado.Visibility = Visibility.Collapsed;

                this._lstAsociados.Visibility = Visibility.Collapsed;
                this._btnConsultarAsociado.Visibility = Visibility.Collapsed;
                this._txtIdAsociado.Visibility = Visibility.Collapsed;
                this._txtNombreAsociado.Visibility = Visibility.Collapsed;
                this._lblNombreAsociado.Visibility = Visibility.Collapsed;
                this._lblIdAsociado.Visibility = Visibility.Collapsed;


                this._indicadores.Visibility = Visibility.Collapsed;
                this._prioridad.Visibility = Visibility.Collapsed;
                this._boletines.Visibility = Visibility.Collapsed;
                this._TYR.Visibility = Visibility.Collapsed;

                this.GestionarVisibilidadFiltroMarca(Visibility.Collapsed);
                this.GestionarVisibilidadDatosDeMarca(Visibility.Visible);

                //this._lblIdInternacional.Visibility = Visibility.Visible;
                //this._txtIdInternacional.Visibility = Visibility.Visible;

                this._lblCodigoInternacional.Visibility = Visibility.Visible;
                this._txtCodigoInternacional.Visibility = Visibility.Visible;
                this._txtCodigoInternacional2.Visibility = Visibility.Visible;

                this._lblPaisInternacional.Visibility = Visibility.Visible;
                this._cbxPaisInternacional.Visibility = Visibility.Visible;

                this._lblReferenciaInteresado.Visibility = Visibility.Visible;
                this._txtReferenciaInteresado.Visibility = Visibility.Visible;

                this._lblReferenciaAsociado.Visibility = Visibility.Visible;
                this._txtReferenciaAsociado.Visibility = Visibility.Visible;


                this.GestionarVisibilidadFiltroCorresponsal(false);
            }
            else
            {
                this._lblId.Visibility = Visibility.Collapsed;
                this._txtId.Visibility = Visibility.Collapsed;

                this._lblSolicitud.Visibility = Visibility.Collapsed;
                this._txtSolicitud.Visibility = Visibility.Collapsed;
                this._dpkFecha.Visibility = Visibility.Collapsed;

                this._lblMarca.Visibility = Visibility.Collapsed;
                this._txtDescripcion.Visibility = Visibility.Collapsed;

                this._txtInteresado.Visibility = Visibility.Collapsed;
                this._txtAsociado.Visibility = Visibility.Collapsed;
                this._txtCorresponsal.Visibility = Visibility.Collapsed;

                this._lblAsociado.Visibility = Visibility.Collapsed;
                this._lblCorresponsal.Visibility = Visibility.Collapsed;
                this._lblInteresado.Visibility = Visibility.Collapsed;

                this._nacional.Visibility = Visibility.Collapsed;


                //this._lblIdInternacional.Visibility = Visibility.Collapsed;
                //this._txtIdInternacional.Visibility = Visibility.Collapsed;

                this._lblCodigoInternacional.Visibility = Visibility.Collapsed;
                this._txtCodigoInternacional.Visibility = Visibility.Collapsed;
                this._txtCodigoInternacional2.Visibility = Visibility.Collapsed;

                this._lblPaisInternacional.Visibility = Visibility.Collapsed;
                this._cbxPaisInternacional.Visibility = Visibility.Collapsed;

                this._lblReferenciaInteresado.Visibility = Visibility.Collapsed;
                this._txtReferenciaInteresado.Visibility = Visibility.Collapsed;

                this._lblReferenciaAsociado.Visibility = Visibility.Collapsed;
                this._txtReferenciaAsociado.Visibility = Visibility.Collapsed;
            }
        }


        public void _chkTYR_Click(object sender, RoutedEventArgs e)
        {
            if (this._chkTYR.IsChecked.Value)
                this._TYR.Visibility = Visibility.Visible;
            else
                this._TYR.Visibility = Visibility.Collapsed;
        }


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


        public void _chkIndicadores_Click(object sender, RoutedEventArgs e)
        {
            if (this._chkIndicadores.IsChecked.Value)
                this._indicadores.Visibility = Visibility.Visible;
            else
                this._indicadores.Visibility = Visibility.Collapsed;
        }


        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarMarca();
        }


        private void _btnConsultarMarcaFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarMarca.IsDefault = true;
            //this._btnConsultarInteresado.IsDefault = false;
        }


        private void _txtMarcaNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            GestionarVisibilidadDatosDeMarca(Visibility.Collapsed);
            GestionarVisibilidadFiltroMarca(Visibility.Visible);
        }


        private void _lstMarcas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.ElegirMarca())
            {
                GestionarVisibilidadDatosDeMarca(Visibility.Visible);
                GestionarVisibilidadFiltroMarca(Visibility.Collapsed);
            }
        }


        public void GestionarVisibilidadFiltroMarca(object value)
        {
            this._txtIdMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarMarca.Visibility = (System.Windows.Visibility)value;
            this._lstMarcas.Visibility = (System.Windows.Visibility)value;
            this._lblCodigo.Visibility = (System.Windows.Visibility)value;
            this._lblNombre.Visibility = (System.Windows.Visibility)value;
        }


        private void GestionarVisibilidadDatosDeMarca(object value)
        {
            this._txtMarcaNombre.Visibility = (System.Windows.Visibility)value;
        }


        public void GestionarVisibilidadLimpiarFiltros()
        {
            this._TYR.Visibility = Visibility.Collapsed;
            this._boletines.Visibility = Visibility.Collapsed;
            this._prioridad.Visibility = Visibility.Collapsed;
            this._indicadores.Visibility = Visibility.Collapsed;

        }


        #endregion


        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            { this._presentador.Consultar(); }

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


        public bool ConfirmarConsultaPorLocalidad(string localidadAConsultar)
        {
            bool retorno = false;
            //MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarConsultarMarcasPorLocalidad,
            //        localidadAConsultar,
            //        "Modificar Marca", MessageBoxButton.YesNoCancel, MessageBoxImage.Question));


            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarConsultarMarcasPorLocalidad, localidadAConsultar), "Consultar Marcas", MessageBoxButton.YesNo))
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }
                

            return retorno;
        }
    }
}
