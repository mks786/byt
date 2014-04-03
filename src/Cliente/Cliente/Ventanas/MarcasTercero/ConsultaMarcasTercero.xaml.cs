using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Presentadores.MarcasTercero;

namespace Trascend.Bolet.Cliente.Ventanas.MarcasTercero
{
    /// <summary>
    /// Interaction logic for ConsultarTodosPoder.xaml
    /// </summary>
    public partial class ConsultaMarcasTercero : Page, IConsultaMarcasTercero
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultaMarcasTercero _presentador;
        private bool _cargada;

        #region IConsultarMarcaTercerosTercero

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

        public string Solicitud
        {
            get { return this._txtSolicitud.Text; }
            set { this._txtSolicitud.Text = value; }
        }

        //public string NombreMarcaTercero
        //{
        //    set { this._txtMarcaTerceroNombre.Text = value; }
        //}

        public object MarcaTerceroSeleccionada
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
            get { return this._dpkFechaPresentacion.SelectedDate.ToString(); }
            set { this._dpkFechaPresentacion.Text = value; }
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

        public string ClaseInternacional
        {
            get { return this._txtClaseInternacional.Text; }
            set { this._txtClaseInternacional.Text = value; }
        }

        public string Distingue
        {
            get { return this._txtDistingue.Text; }
            set { this._txtDistingue.Text = value; }
        }

        public string ClaseNacional
        {
            get { return this._txtClaseNacional.Text; }
            set { this._txtClaseNacional.Text = value; }
        }

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

        public object TiposDeCasos
        {
            get { return this._cbxTipoDeCaso.DataContext; }
            set { this._cbxTipoDeCaso.DataContext = value; }
        }

        public object TipoDeCaso
        {
            get { return this._cbxTipoDeCaso.SelectedItem; }
            set { this._cbxTipoDeCaso.SelectedItem = value; }
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

        public bool NacionalEstaSeleccionado
        {
            get { return this._chkNacional.IsChecked.Value; }
            set { this._chkNacional.IsChecked = value; }
        }

        public bool BoletinesEstaSeleccionado
        {
            get { return this._chkBoletines.IsChecked.Value; }
            set { this._chkBoletines.IsChecked = value; }
        }

        public object MarcaTerceroParaFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public void LimpiarCampos()
        {
            this._txtClaseInternacional.Text = "";
            this._txtClaseNacional.Text = "";
        }

        public object OrigenesMarcaTercero
        {
            get { return this._cbxOrigenMarcaTercero.DataContext; }
            set { this._cbxOrigenMarcaTercero.DataContext = value; }
        }

        public object OrigenMarcaTercero
        {
            get { return this._cbxOrigenMarcaTercero.SelectedItem; }
            set { this._cbxOrigenMarcaTercero.SelectedItem = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultaMarcasTercero()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultaMarcasTercero(this);
        }


        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            this._dpkFechaPresentacion.Text = string.Empty;
            validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarMarcaTercero();
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

            if (!this._dpkFechaPresentacion.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaPresentacion.Focus();
            }

            if ((this._cbxOrigenMarcaTercero.SelectedIndex != 0) && (this._cbxOrigenMarcaTercero.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxOrigenMarcaTercero.Focus();
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

        #region Interesado

        private void _txtInteresado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(false);
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
           
            this._btnConsultarInteresado.IsDefault = true;
        }

        #endregion

        #region Checks



        private void _chkNacional_Click(object sender, RoutedEventArgs e)
        {
            GestionarVisibilidadFiltroNacional(this._chkNacional.IsChecked.Value);

        }

        public void GestionarVisibilidadFiltroNacional(bool visibilidad)
        {
            if (visibilidad)
            {
                this._lblId.Visibility = Visibility.Visible;
                this._txtId.Visibility = Visibility.Visible;

                this._lblSolicitud.Visibility = Visibility.Visible;
                this._txtSolicitud.Visibility = Visibility.Visible;
                this._dpkFechaPresentacion.Visibility = Visibility.Visible;

                this._lblMarcaTercero.Visibility = Visibility.Visible;
                this._txtDescripcion.Visibility = Visibility.Visible;

                this._txtInteresado.Visibility = Visibility.Visible;
                this._txtAsociado.Visibility = Visibility.Visible;

                this._lblAsociado.Visibility = Visibility.Visible;
                this._lblInteresado.Visibility = Visibility.Visible;

                this._nacional.Visibility = Visibility.Visible;
            }
            else
            {
                this._lblId.Visibility = Visibility.Collapsed;
                this._txtId.Visibility = Visibility.Collapsed;

                this._lblSolicitud.Visibility = Visibility.Collapsed;
                this._txtSolicitud.Visibility = Visibility.Collapsed;
                this._dpkFechaPresentacion.Visibility = Visibility.Collapsed;

                this._lblMarcaTercero.Visibility = Visibility.Collapsed;
                this._txtDescripcion.Visibility = Visibility.Collapsed;

                this._txtInteresado.Visibility = Visibility.Collapsed;
                this._txtAsociado.Visibility = Visibility.Collapsed;

                this._lblAsociado.Visibility = Visibility.Collapsed;
                this._lblInteresado.Visibility = Visibility.Collapsed;

                this._nacional.Visibility = Visibility.Collapsed;
            }
        }

        public void _chkBoletines_Click(object sender, RoutedEventArgs e)
        {
            if (this._chkBoletines.IsChecked.Value)
                this._boletines.Visibility = Visibility.Visible;
            else
                this._boletines.Visibility = Visibility.Collapsed;
        }

        private void _btnConsultarMarcaTercero_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarMarcaTercero();
        }

        private void _btnConsultarMarcaTerceroFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            //this._btnConsultarInteresado.IsDefault = false;
        }


        public void GestionarVisibilidadLimpiarFiltros()
        {

            this._boletines.Visibility = Visibility.Collapsed;

        }

        #endregion

        private void _chkInstruccionesDeRenovacion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _chkRenovadaPorOtroTramitante_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _lstCorresponsals_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void _btnConsultarCorresponsal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _chkInternacional_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _dpkFechaPresentacion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
