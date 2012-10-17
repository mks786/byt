using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Presentadores.Asociados;
using Trascend.Bolet.ControlesByT;
using System.Windows.Media;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Asociados
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class ConsultarAsociado : Page, IConsultarAsociado
    {

        private PresentadorConsultarAsociado _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private object p;
        private IConsultarAsociados iConsultarAsociados;

        #region IconsultarAsociado


        public object Asociado
        {
            get { return this._tbcPestañas.DataContext; }
            set { this._tbcPestañas.DataContext = value; }
        }

        public object Pais
        {
            get { return this._cbxPaisDatos.SelectedItem; }
            set { this._cbxPaisDatos.SelectedItem = value; }
        }

        public object Paises
        {
            get { return this._cbxPaisDatos.DataContext; }
            set { this._cbxPaisDatos.DataContext = value; }
        }

        public object Idioma
        {
            get { return this._cbxIdiomaDatos.SelectedItem; }
            set { this._cbxIdiomaDatos.SelectedItem = value; }
        }

        public object Idiomas
        {
            get { return this._cbxIdiomaDatos.DataContext; }
            set { this._cbxIdiomaDatos.DataContext = value; }
        }

        public object Moneda
        {
            get { return this._cbxMonedaDatos.SelectedItem; }
            set { this._cbxMonedaDatos.SelectedItem = value; }
        }

        public object Monedas
        {
            get { return this._cbxMonedaDatos.DataContext; }
            set { this._cbxMonedaDatos.DataContext = value; }
        }

        public object Descuento
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public object Descuentos
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public object TipoCliente
        {
            get { return this._cbxTipoClienteAdministracion.SelectedItem; }
            set { this._cbxTipoClienteAdministracion.SelectedItem = value; }
        }

        public object TiposClientes
        {
            get { return this._cbxTipoClienteAdministracion.DataContext; }
            set { this._cbxTipoClienteAdministracion.DataContext = value; }
        }

        public object Etiqueta
        {
            get { return this._cbxEtiquetaAdministracion.SelectedItem; }
            set { this._cbxEtiquetaAdministracion.SelectedItem = value; }
        }

        public object Etiquetas
        {
            get { return this._cbxEtiquetaAdministracion.DataContext; }
            set { this._cbxEtiquetaAdministracion.DataContext = value; }
        }

        public object DetallePago
        {
            get { return this._cbxDetallePagoAdministracion.SelectedItem; }
            set { this._cbxDetallePagoAdministracion.SelectedItem = value; }
        }

        public object DetallesPagos
        {
            get { return this._cbxDetallePagoAdministracion.DataContext; }
            set { this._cbxDetallePagoAdministracion.DataContext = value; }
        }

        public object Tarifa
        {
            get { return this._cbxTarifaAdministracion.SelectedItem; }
            set { this._cbxTarifaAdministracion.SelectedItem = value; }
        }

        public object Tarifas
        {
            get { return this._cbxTarifaAdministracion.DataContext; }
            set { this._cbxTarifaAdministracion.DataContext = value; }
        }

        public object TipoPersonas
        {

            get { return this._cbxTipoPersonaDatos.DataContext; }
            set { this._cbxTipoPersonaDatos.DataContext = value; }
        }

        public object TipoPersona
        {

            get { return this._cbxTipoPersonaDatos.SelectedItem; }
            set { this._cbxTipoPersonaDatos.SelectedItem = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNombreDatos.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtNombreDatos.IsEnabled = value;
                this._txtNombreAdministracion.IsEnabled = value;
                this._txtDomicilioDatos.IsEnabled = value;
                this._txtTelefono1Datos.IsEnabled = value;
                this._txtTelefono2Datos.IsEnabled = value;
                this._txtTelefono3Datos.IsEnabled = value;
                this._txtFax1Datos.IsEnabled = value;
                this._txtFax2Datos.IsEnabled = value;
                this._txtFax3Datos.IsEnabled = value;
                this._txtEmailDatos.IsEnabled = value;
                this._txtWebDatos.IsEnabled = value;
                this._txtDescuentoDatos.IsEnabled = value;
                this._txtRifDatos.IsEnabled = value;
                this._txtNitDatos.IsEnabled = value;

                if(this._chkAlertaAdministracion.IsChecked.Value)
                    this._txtAlarmaAdministracion.IsEnabled = value;
                
                this._txtDiasCreditoAdministracion.IsEnabled = value;
                this._txtDescuentoAdministracion.IsEnabled = value;

                this._cbxTipoPersonaDatos.IsEnabled = value;
                this._cbxPaisDatos.IsEnabled = value;
                this._cbxIdiomaDatos.IsEnabled = value;
                this._cbxMonedaDatos.IsEnabled = value;
                this._cbxTipoClienteAdministracion.IsEnabled = value;
                this._cbxTarifaAdministracion.IsEnabled = value;
                this._cbxEtiquetaAdministracion.IsEnabled = value;
                this._cbxDetallePagoAdministracion.IsEnabled = value;

                this._chkContribuyenteDatos.IsEnabled = value;
                this._chkActivoAdministracion.IsEnabled = value;
                this._chkAlertaAdministracion.IsEnabled = value;
                this._chkEdoCuentaAdministracion.IsEnabled = value;
                this._chkEdoCuentaDigitalAdministracion.IsEnabled = value;
                this._chkIsfAdministracion.IsEnabled = value;
                this._chkPendienteStatementAdministracion.IsEnabled = value;
                this._chkContribuyenteDatos.IsEnabled = value;

            }
        }


        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public char GetTipoPersona
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxTipoPersonaDatos.Text))
                    return (this._cbxTipoPersonaDatos.Text)[0];
                else
                    return ' ';
            }
        }

        public string SetTipoPersona
        {
            set { this._cbxTipoPersonaDatos.Text = value; }
        }

        public void ArchivoNoEncontrado()
        {
            MessageBox.Show(Recursos.MensajesConElUsuario.ErrorAsociadoNoEncontrado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        public void pintarJustificacion()
        {

            this._btnJustificacionesAdministracion.Background = Brushes.LightGreen;
            this._btnJustificacionesDatos.Background = Brushes.LightGreen;
        }

        public void pintarContacto()
        {
            this._btnContactosAdministracion.Background = Brushes.LightGreen;
            this._btnContactosDatos.Background = Brushes.LightGreen;
        }

        public void pintarCorrespondencia()
        {
            this._btnCorrespondenciasDatos.Background = Brushes.LightGreen;
            this._btnCorrespondenciasDatos.Background = Brushes.LightGreen;

            this._btnCorrespondenciasAdministracion.Background = Brushes.LightGreen;
            this._btnCorrespondenciasAdministracion.Background = Brushes.LightGreen;
        }

        public void pintarDatosTransferencia()
        {
            this._btnTrasferenciaAdministracion.Background = Brushes.LightGreen;
        }

        public void pintarAuditoria()
        {
            this._btnAuditoriaAdministracion.Background = Brushes.LightGreen;
            this._btnAuditoriaDatos.Background = Brushes.LightGreen;
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

        public object ListaContactos
        {
            get { return this._lstContactos.DataContext; }
            set { this._lstContactos.DataContext = value; }
        }

        public object ContactoSeleccionado 
        {
            get { return this._lstContactos.SelectedItem; }
        }
        #endregion

        public ConsultarAsociado(object asociado, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarAsociado(this, asociado,ventanaPadre);

        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }

        private void _btnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AbrirExpediente();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarAsociado,
                "Eliminar Asociado", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnJustificacionesDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaJustificaciones();
        }

        private void _btnContactosDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaContactos();
        }

        private void _btnTrasferenciaAdministracion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaDatosTransferencia();
        }

        //private void _soloNumero_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(this._txtDiasCreditoAdministracion.Text, "[^0-9]"))
        //    {
        //        this._txtDiasCreditoAdministracion.Text = "";
        //    }
        //}

        //private void _soloNumero_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
        //        e.Handled = true;

        //}

        private void _txtDescuentoAdministracion_KeyDown(object sender, KeyEventArgs e)
        {

            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "^\\$?(\\d{1,3},?(\\d{3},?)*\\d{3}(.\\d{0,3})?|\\d{1,3}(.\\d{2})?)$"))
                e.Handled = true;
            
        }

        private void _chkAlertaAdministracion_Click(object sender, RoutedEventArgs e)
        {
            if (this._chkAlertaAdministracion.IsEnabled)
            {
                if (!this._chkAlertaAdministracion.IsChecked.Value)
                {
                    this._txtAlarmaAdministracion.IsEnabled = false;
                }
                else
                {
                    this._txtAlarmaAdministracion.IsEnabled = true;
                }
            }
        }

        private void _txtCodigoDatos_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _btnCorrespondenciasDatos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrACorrespondencia();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void _btnVerEnviada_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarUltimaCorrespondenciaEnviada();
        }

        private void _btnVerCreacion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCorrespondenciaCreacion();
        }

        private void _btnVerEntrada_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarUltimaCorrespondenciaEntrada();
        }

    }
}

