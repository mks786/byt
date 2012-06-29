using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Renovaciones;
using Trascend.Bolet.Cliente.Presentadores.Renovaciones;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.CambiosDeNombre;

namespace Trascend.Bolet.Cliente.Ventanas.Renovaciones
{
    /// <summary>
    /// Interaction logic for CambiosDeDomicilio.xaml
    /// </summary>
    public partial class GestionarRenovacion : Page, IGestionarRenovacion
    {

        private PresentadorGestionarRenovacion _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarFusion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Renovacion
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

        public string IdAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresadoFiltrar.Text; }
        }

        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresadoFiltrar.Text; }
        }

        public string IdAgenteFiltrar
        {
            get { return this._txtIdAgenteFiltrar.Text; }
        }

        public string NombreAgenteFiltrar
        {
            get { return this._txtNombreAgenteFiltrar.Text; }
        }

        public string IdMarcaFiltrar
        {
            get { return this._txtIdMarcaFiltrar.Text; }
        }

        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
        }

        public DatePicker FechaRenovacion
        {
            get { return this._dpkFechaRenovacion; }
            set { _dpkFechaRenovacion = value; }
    }

        public string IdMarca
        {
            get { return this._txtIdMarca.Text; }
            set { this._txtIdMarca.Text = value; }
        }

        public string IdInteresado
        {
            get { return this._txtIdInteresado.Text; }
            set { this._txtIdInteresado.Text = value; }
        }

        public string IdAgente
        {
            get { return this._txtIdAgente.Text; }
            set { this._txtIdAgente.Text = value; }
        }

        public string Tipo
        {
            get { return this._txtTipo.Text; }
            set { this._txtTipo.Text = value; }
        }

        public string PeriodoDeGracia
        {
            get { return this._txtObservacion.Text; }
            set { this._txtObservacion.Text = value; }
        }

        public object Marca
        {
            get { return this._gridDatosMarca.DataContext; }
            set { this._gridDatosMarca.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._gridDatosInteresado.DataContext; }
            set { this._gridDatosInteresado.DataContext = value; }
        }

        public string NombreInteresado
        {
            set { this._txtNombreInteresado.Text = value; }
        }

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

        public void FocoPredeterminado()
        {
            this._txtIdRenovacion.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
              //  this._txtAsociado.IsEnabled = value;
               // this._txtClaseInternacional.IsEnabled = value;
                //this._txtClaseNacional.IsEnabled = value;
                //this._txtTipo.IsEnabled = value;
                //this._chkEtiqueta.IsEnabled = value;
                // this._txtEtiquetaDescripcion.IsEnabled = value;
                //this._txtNumRegistro.IsEnabled = value;
               // this._txtIdRenovacion.IsEnabled = value;
                this._txtIdMarcaFiltrar.IsEnabled = value;
                this._txtNombreMarca.IsEnabled = value;
                this._txtNombreMarcaFiltrar.IsEnabled = value;
                this._btnConsultarMarca.IsEnabled = value;
                this._dpkFechaRenovacion.IsEnabled = value;

                this._txtIdMarca.IsEnabled = value;
                this._txtIdInteresado.IsEnabled = value;
                this._txtIdAgente.IsEnabled = value;

               // this._txtFechaProximaRenovacionMarca.IsEnabled = value;
               // this._txtFechaRegistro.IsEnabled = value;
               // this._txtEstatus.IsEnabled = value;

                this._txtNombreInteresado.IsEnabled = value;
                this._txtNombreInteresadoFiltrar.IsEnabled = value;
                this._txtIdInteresadoFiltrar.IsEnabled = value;
                this._txtPaisInteresado.IsEnabled = value;
                this._txtCiudadInteresado.IsEnabled = value;

                this._txtNombreAgente.IsEnabled = value;
                this._txtNombreAgenteFiltrar.IsEnabled = value;
                this._txtIdAgenteFiltrar.IsEnabled = value;

                this._lblIdPoder.IsEnabled = value;
                this._lblFomento.IsEnabled = value;
                this._txtIdPoder.IsEnabled = value;
                this._txtNumPoder.IsEnabled = value;

                this._lblPoderFiltrar.IsEnabled = value;
                this._lblIdPoderFiltrar.IsEnabled = value;
                this._lblIdPoderFiltrar.IsEnabled = value;
                this._txtIdPoderFiltrar.IsEnabled = value;
                this._dpkFechaPoderFiltrar.IsEnabled = value;

                this._txtObservacion.IsEnabled = value;
                this._txtOtros.IsEnabled = value;
                this._txtProximaRenovacion.IsEnabled = value;
            }
        }

        public string NombreMarca
        {
            set { this._txtNombreMarca.Text = value; }
        }

        public object MarcasFiltradas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object MarcaFiltrada
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }

        public object InteresadosFiltrados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object InteresadoFiltrado
        {
            get { return this._lstInteresados.SelectedItem; }
            set { this._lstInteresados.SelectedItem = value; }
        }

        public object Agente
        {
            get { return this._gridDatosAgente.DataContext; }
            set { this._gridDatosAgente.DataContext = value; }
        }

        public string NombreAgente
        {
            set { this._txtNombreAgente.Text = value; }
        }

        public string IdAgenteAgenteFiltrar
        {
            get { return this._txtIdAgenteFiltrar.Text; }
        }

        public string NombreAgenteAgenteFiltrar
        {
            get { return this._txtNombreAgenteFiltrar.Text; }
        }

        public object AgentesFiltrados
        {
            get { return this._lstAgentes.DataContext; }
            set { this._lstAgentes.DataContext = value; }
        }

        public object AgenteFiltrado
        {
            get { return this._lstAgentes.SelectedItem; }
            set { this._lstAgentes.SelectedItem = value; }
        }

        public string TextoBotonRegresar
        {
            get { return this._txbRegresar.Text; }
            set { this._txbRegresar.Text = value; }
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

        public object Poder
        {
            get { return this._gridDatosPoder.DataContext; }
            set { this._gridDatosPoder.DataContext = value; }
        }

        public string IdPoder
        {
            get { return this._txtIdPoder.Text; }
            set { this._txtIdPoder.Text = value; }
        }

        public string IdPoderFiltrar
        {
            get { return this._txtIdPoderFiltrar.Text; }
        }

        public string FechaPoderFiltrar
        {
            get { return this._dpkFechaPoderFiltrar.Text; }
        }

        public string Otros
        {
            get { return this._txtOtros.Text; }
            set { this._txtOtros.Text = value; }
        }        

        public string ProximaRenovacion
        {
            get { return this._txtProximaRenovacion.Text; }
            set { this._txtProximaRenovacion.Text = value; }
        }

        public object PoderesFiltrados
        {
            get { return this._lstPoderes.DataContext; }
            set { this._lstPoderes.DataContext = value; }
        }

        public object PoderFiltrado
        {
            get { return this._lstPoderes.SelectedItem; }
            set { this._lstPoderes.SelectedItem = value; }
        }

        public object TiposRenovaciones
        {
            get { return this._cbxTipoR.DataContext; }
            set { this._cbxTipoR.DataContext = value; }
        }

        public object TipoRenovacion
        {
            get { return this._cbxTipoR.SelectedItem; }
            set { this._cbxTipoR.SelectedItem = value; }
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion

        public GestionarRenovacion(object renovacion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarRenovacion(this, renovacion);
        }

        /// <summary>
        /// Constructor para la consulta desde operaciones
        /// </summary>
        /// <param name="renovacion">la renovacion a mostrar</param>
        /// <param name="visibilidad">parametro que indica la visibilidad de los botones</param>
        public GestionarRenovacion(object renovacion, object visibilidad)
        {
            InitializeComponent();
            this._cargada = false;
            this._btnEliminar.Visibility = (System.Windows.Visibility)visibilidad;
            this._presentador = new PresentadorGestionarRenovacion(this, renovacion);
        }

        public void ActivarControlesAlAgregar()
        {
            this.HabilitarCampos = true;

            this._btnAceptar.Visibility = System.Windows.Visibility.Visible;
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdRenovacion.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdRenovacion.Visibility = System.Windows.Visibility.Collapsed;
            this._dpkFechaRenovacion.IsEnabled = true;
            this._cbxTipoR.IsEnabled = false;
            this._chkAsientoEnLibro.IsEnabled = true;
            this._btnCarpeta.Visibility = System.Windows.Visibility.Collapsed;
            this._btnSolicitud.Visibility = System.Windows.Visibility.Collapsed;
            this._btnSolicitudVan.Visibility = System.Windows.Visibility.Collapsed;
            this._btnAnexo.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Agregar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextoBotonRegresar == Recursos.Etiquetas.btnRegresar)
                this._presentador.Regresar();
            else if (this.TextoBotonRegresar == Recursos.Etiquetas.btnCancelar)
                this._presentador.Cancelar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarRenovacion,
                "Eliminar Renovacion", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
        }

        private void _btnAnexo_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnSolicitud_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnSolicitudVan_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnCarpeta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnCopiarDistingue_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CopiarDistingue();
        }

        private void _btnPeriodoDeGracia_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EscribirPeriodoDeGracia();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        public void ConvertirEnteroMinimoABlanco()
        {
            if (!this.IdPoder.Equals(""))
            {
                if (int.Parse(this.IdPoder) == int.MinValue)
                    this.IdPoder = "";
            }

            if (!this.IdInteresado.Equals(""))
            {
                if (int.Parse(this.IdInteresado) == int.MinValue)
                    this.IdInteresado = "";
            }

            if (!this.IdMarca.Equals(""))
            {
                if (int.Parse(this.IdMarca) == int.MinValue)
                    this.IdMarca = "";
            }

        }

        #region Eventos Marcas

        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMarcas();
        }

        private void _lstMarcas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarMarca())
            {
                GestionarVisibilidadDatosDeMarca(Visibility.Visible);
                GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

                this._btnConsultarMarca.IsDefault = false;
                this._btnAceptar.IsDefault = true;
            }
        }

        private void _OrdenarMarcas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstMarcas);
        }

        private void _txtNombreMarca_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeMarca(Visibility.Collapsed);
            GestionarVisibilidadFiltroMarca(Visibility.Visible);

            //escondo el filtro de interesado
            GestionarVisibilidadDatosDeInteresado(Visibility.Visible);
            GestionarVisibilidadFiltroInteresado(Visibility.Collapsed);

            //escondo el filtro de Agente
            GestionarVisibilidadDatosDeAgente(Visibility.Visible);
            GestionarVisibilidadFiltroAgente(Visibility.Collapsed);

            //escondo el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            this._btnConsultarMarca.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void GestionarVisibilidadFiltroMarca(object value)
        {
            this._lblNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._txtNombreMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdMarca.Visibility = (System.Windows.Visibility)value;
            this._txtIdMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstMarcas.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarMarca.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeMarca(object value)
        {
            this._txtNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._txtIdMarca.Visibility = (System.Windows.Visibility)value;
            this._chkEtiqueta.Visibility = (System.Windows.Visibility)value;
            this._lblNoRegistro.Visibility = (System.Windows.Visibility)value;
            this._txtNumRegistro.Visibility = (System.Windows.Visibility)value;
            this._lblTipo.Visibility = (System.Windows.Visibility)value;
            this._txtTipo.Visibility = (System.Windows.Visibility)value;
            this._lblClaseNacional.Visibility = (System.Windows.Visibility)value;
            this._txtClaseNacional.Visibility = (System.Windows.Visibility)value;
            this._lblClaseInternacional.Visibility = (System.Windows.Visibility)value;
            this._txtClaseInternacional.Visibility = (System.Windows.Visibility)value;
            this._lblAsociado.Visibility = (System.Windows.Visibility)value;
            this._txtAsociado.Visibility = (System.Windows.Visibility)value;
            this._lblFechaProximaRenovacionMarca.Visibility = (System.Windows.Visibility)value;
            this._txtFechaProximaRenovacionMarca.Visibility = (System.Windows.Visibility)value;
            this._lblEstatus.Visibility = (System.Windows.Visibility)value;
            this._txtEstatus.Visibility = (System.Windows.Visibility)value;
            this._txtEtiquetaDescripcion.Visibility = (System.Windows.Visibility)value;
            this._lblFechaRegistro.Visibility = (System.Windows.Visibility)value;
            this._txtFechaRegistro.Visibility = (System.Windows.Visibility)value;
        }

        private void _txtMarcaFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarMarca.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        #endregion

        #region Eventos Interesado

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresados();
        }

        private void _lstInteresados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresado())
            {
                GestionarVisibilidadDatosDeInteresado(Visibility.Visible);
                GestionarVisibilidadFiltroInteresado(Visibility.Collapsed);

                if (this._presentador.VerificarCambioInteresado())
                {
                    this._btnConsultarPoder.IsEnabled = false;
                    this._btnAceptar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarPoder.IsEnabled = true;
                }
            }
        }

        private void _OrdenarInteresados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresados);
        }

        private void _txtInteresado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeInteresado(Visibility.Collapsed);

            GestionarVisibilidadFiltroInteresado(Visibility.Visible);

            //escondo el filtro de interesado Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            //escondo el filtro de Agente
            GestionarVisibilidadDatosDeAgente(Visibility.Visible);
            GestionarVisibilidadFiltroAgente(Visibility.Collapsed);

            //Escondo el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            this._btnConsultarInteresado.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresado(object value)
        {
            this._lblIdInteresado.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresado.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresados.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresado.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresado(object value)
        {
            this._txtNombreInteresado.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresado.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresado.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresado.Visibility = (System.Windows.Visibility)value;
        }

        public void GestionarBotonConsultarInteresado(bool value)
        {
            this._btnConsultarInteresado.IsEnabled = value;
        }

        #endregion

        #region Eventos Agente

        private void _btnConsultarAgente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarAgentes();
        }

        private void _lstAgentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAgente())
            {
                GestionarVisibilidadDatosDeAgente(Visibility.Visible);
                GestionarVisibilidadFiltroAgente(Visibility.Collapsed);

                if (this._presentador.VerificarCambioAgente())
                {
                    this._btnConsultarPoder.IsEnabled = false;
                    this._btnAceptar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarPoder.IsEnabled = true;
                }
            }
        }

        private void _OrdenarAgentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAgentes);
        }

        private void _txtAgenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarAgente.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void _txtNombreAgente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeAgente(Visibility.Collapsed);
            GestionarVisibilidadFiltroAgente(Visibility.Visible);

            //escondo el filtro de interesado Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            //escondo el filtro de interesado
            GestionarVisibilidadDatosDeInteresado(Visibility.Visible);
            GestionarVisibilidadFiltroInteresado(Visibility.Collapsed);

            //Escondo el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            this._btnConsultarAgente.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroAgente(object value)
        {
            this._lblIdAgenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreAgenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreAgenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdAgenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstAgentes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarAgente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeAgente(object value)
        {
            this._txtNombreAgente.Visibility = (System.Windows.Visibility)value;
            this._txtIdAgente.Visibility = (System.Windows.Visibility)value;
        }

        public void GestionarBotonConsultarAgente(bool value)
        {
            this._btnConsultarAgente.IsEnabled = value;
        }

        #endregion

        #region Poderes

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name.Equals("_btnConsultarMarca"))
                this._presentador.ConsultarMarcas();
            else if (((Button)sender).Name.Equals("_btnConsultarInteresado"))
                this._presentador.ConsultarInteresados();
            else if (((Button)sender).Name.Equals("_btnConsultarAgente"))
                this._presentador.ConsultarAgentes();
            else if (((Button)sender).Name.Equals("_btnConsultarPoder"))
                this._presentador.ConsultarPoderes();
        }

        private void _txtPoderFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPoder.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void _txtIdPoder_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            //escondo el filtro de Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            //escondo el filtro de Interesado
            GestionarVisibilidadDatosDeInteresado(Visibility.Visible);
            GestionarVisibilidadFiltroInteresado(Visibility.Collapsed);

            //escondo el filtro de Agente Apoderado
            GestionarVisibilidadDatosDeAgente(Visibility.Visible);
            GestionarVisibilidadFiltroAgente(Visibility.Collapsed);

            //Muestro el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Collapsed);
            GestionarVisibilidadFiltroPoder(Visibility.Visible);

        }

        private void _OrdenarPoderes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderes);
        }

        private void _lstPoderes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPoder())
            {
                GestionarVisibilidadDatosDePoder(Visibility.Visible);
                GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

                if (this._presentador.VerificarCambioPoder())
                {
                    this._btnConsultarAgente.IsEnabled = false;
                    this._btnConsultarInteresado.IsEnabled = false;
                    this._btnAceptar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarAgente.IsEnabled = true;
                    this._btnConsultarInteresado.IsEnabled = true;
                }
            }
        }

        public void GestionarBotonConsultarPoder(bool value)
        {
            this._btnConsultarPoder.IsEnabled = value;
        }

        private void GestionarVisibilidadFiltroPoder(object value)
        {
            this._lblPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._dpkFechaPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPoderes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPoder.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadDatosDePoder(object value)
        {
            this._lblIdPoder.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoder.Visibility = (System.Windows.Visibility)value;
            this._lblFomento.Visibility = (System.Windows.Visibility)value;
            this._txtNumPoder.Visibility = (System.Windows.Visibility)value; ;
        }

        #endregion

        private void _dpkFechaRenovacion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //this._presentador.ActualizarFechaProxima();
        }

    }
}
