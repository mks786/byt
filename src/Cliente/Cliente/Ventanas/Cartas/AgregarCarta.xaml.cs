using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Cartas;
using Trascend.Bolet.Cliente.Presentadores.Cartas;
using System.Windows;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ventanas.Base;

namespace Trascend.Bolet.Cliente.Ventanas.Cartas
{
    /// <summary>
    /// Interaction logic for AgregarAsociado.xaml
    /// </summary>
    public partial class AgregarCarta : Page, IAgregarCarta
    {
        private PresentadorAgregarCarta _presentador;
        private bool _cargada;
        private bool _acuseSeleccionado;

        #region IAgregarCarta

        public object Carta
        {
            get { return this._dataCarta.DataContext; }
            set { this._dataCarta.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public object Asociado
        {
            get { return this._lstAsociados.SelectedItem; }
            set
            {
                this._lstAsociados.SelectedItem = value;
                this._lstAsociados.ScrollIntoView(value);
            }
        }

        public object Asociados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public object Receptor
        {
            get { return this._cbxReceptor.SelectedItem; }
            set { this._cbxReceptor.SelectedItem = value; }
        }

        public object Receptores
        {
            get { return this._cbxReceptor.DataContext; }
            set { this._cbxReceptor.DataContext = value; }
        }

        public object Remitente
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

        public object Remitentes
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

        public object Responsable
        {
            get { return this._cbxResponsable.SelectedItem; }
            set { this._cbxResponsable.SelectedItem = value; }
        }

        public object Responsables
        {
            get { return this._cbxResponsable.DataContext; }
            set { this._cbxResponsable.DataContext = value; }
        }

        public object ResponsableList
        {
            get { return this._lstResponsables.SelectedItem; }
            set { this._lstResponsables.SelectedItem = value; }
        }

        public object ResponsablesList
        {
            get { return this._lstResponsables.DataContext; }
            set { this._lstResponsables.DataContext = value; }
        }

        public object Resumen
        {
            get { return this._cbxResumen.SelectedItem; }
            set { this._cbxResumen.SelectedItem = value; }
        }

        public object Resumenes
        {
            get { return this._cbxResumen.DataContext; }
            set { this._cbxResumen.DataContext = value; }
        }

        public object Persona
        {
            get { return this._cbxContacto.SelectedItem; }
            set { this._cbxContacto.SelectedItem = value; }
        }

        public object Personas
        {
            get { return this._cbxContacto.DataContext; }
            set { this._cbxContacto.DataContext = value; }
        }

        public object Acuse
        {
            get { return this._cbxAcuse.SelectedItem; }
            set { this._cbxAcuse.SelectedItem = value; }
        }

        public object AcuseLista
        {
            get { return this._cbxAcuse.DataContext; }
            set { this._cbxAcuse.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public object Medio
        {
            get { return this._cbxMedio.SelectedItem; }
            set { this._cbxMedio.SelectedItem = value; }
        }

        public object Medios
        {
            get { return this._cbxMedio.DataContext; }
            set { this._cbxMedio.DataContext = value; }
        }

        public object MedioTrackingConfirmacion
        {
            get { return this._cbxMedioTrackingConfirmacion.SelectedItem; }
            set { this._cbxMedioTrackingConfirmacion.SelectedItem = value; }
        }

        public object MediosTrackingConfirmacion
        {
            get { return this._cbxMedioTrackingConfirmacion.DataContext; }
            set { this._cbxMedioTrackingConfirmacion.DataContext = value; }
        }
        public object Anexo
        {
            get { return this._cbxAnexo.SelectedItem; }
            set { this._cbxAnexo.SelectedItem = value; }
        }

        public object Anexos
        {
            get { return this._cbxAnexo.DataContext; }
            set { this._cbxAnexo.DataContext = value; }
        }

        public object AnexoCarta
        {
            get { return this._lstAnexosCarta.SelectedItem; }
            set { this._lstAnexosCarta.SelectedItem = value; }
        }

        public object AnexosCarta
        {
            get { return this._lstAnexosCarta.DataContext; }
            set { this._lstAnexosCarta.DataContext = value; }
        }

        public object AnexoConfirmacion
        {
            get { return this._cbxAnexoConfirmacion.SelectedItem; }
            set { this._cbxAnexoConfirmacion.SelectedItem = value; }
        }

        public object AnexosConfirmacion
        {
            get { return this._cbxAnexoConfirmacion.DataContext; }
            set { this._cbxAnexoConfirmacion.DataContext = value; }
        }

        public object AnexoConfirmacionCarta
        {
            get { return this._lstAnexosCartaConfirmacion.SelectedItem; }
            set { this._lstAnexosCartaConfirmacion.SelectedItem = value; }
        }

        public object AnexosConfirmacionCarta
        {
            get { return this._lstAnexosCartaConfirmacion.DataContext; }
            set { this._lstAnexosCartaConfirmacion.DataContext = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            //this._txtNombreDatos.Focus();
        }

        public string NombreAsociado
        {
            get { return this._txtAsociado.Text; }
            set { this._txtAsociado.Text = value; }
        }

        public string CodigoAsociado
        {
            get { return this._txtCodigoAsociado.Text; }
            set { this._txtCodigoAsociado.Text = value; }
        }

        public string idAsociadoFiltrar
        {
            get { return this._txtIdAsociado.Text; }
            set { this._txtIdAsociado.Text = value; }
        }

        public string idCarta
        {
            get { return this._txtIdCarta.Text; }
            set { this._txtIdCarta.Text = value; }
        }

        public string NombreAsociadoFiltrar
        {
            get { return this._txtNombreAsociado.Text; }
            set { this._txtNombreAsociado.Text = value; }
        }

        public string FormatoTracking
        {
            get { return this._lsbFormato.Text; }
            set { this._lsbFormato.Text = value; }
        }

        public string FormatoTrackingConfirmacion
        {
            get { return this._lsbFormatoConfirmacion.Text; }
            set { this._lsbFormatoConfirmacion.Text = value; }
        }


        //------

        //public bool AcuseSeleccionado
        //{
        //    get { return this._acuseSeleccionado; }
        //    set { this._acuseSeleccionado = value; }
        //}

        //------

        public bool HabilitarCampos
        {
            set
            {
                this._txtAsociado.IsEnabled = value;
                this._txtCodigoAsociado.IsEnabled = value;
                this._cbxAcuse.IsEnabled = value;
                this._txtNombreDepartamento.IsEnabled = value;
                this._txtReferencia.IsEnabled = value;
                this._txtResumen.IsEnabled = value;
                this._txtTipoAnexo.IsEnabled = value;
                this._txtTracking.IsEnabled = value;
                this._txtTrackingConfirmacion.IsEnabled = value;
                this._cbxAnexo.IsEnabled = value;
                this._cbxAnexoConfirmacion.IsEnabled = value;
                this._cbxContacto.IsEnabled = value;
                this._cbxDepartamento.IsEnabled = value;
                this._cbxMedio.IsEnabled = value;
                //this._cbxMedioTracking.IsEnabled = value;
                this._cbxResumen.IsEnabled = value;
                this._cbxReceptor.IsEnabled = value;
                this._lstAnexosCarta.IsEnabled = value;
                this._lstAnexosCartaConfirmacion.IsEnabled = value;
                this._btnMas.IsEnabled = value;
                this._btnMasConfirmacion.IsEnabled = value;
                this._btnMenos.IsEnabled = value;
                this._btnMenosConfirmacion.IsEnabled = value;
                this._dpkFecha.IsEnabled = value;
                this._dpkFechaAlternativa.IsEnabled = value;
                this._dpkFechaAnexo.IsEnabled = value;
                this._dpkFechaConfirmacion.IsEnabled = value;
                this._dpkFechaReal.IsEnabled = value;
                //this._txtMedio.IsEnabled = value;
                this._cbxMedioTrackingConfirmacion.IsEnabled = value;
                this._lstResponsables.IsEnabled = value;
                this._btnMasResponsable.IsEnabled = value;
                this._btnMenosResponsable.IsEnabled = value;
                this._cbxResponsable.IsEnabled = value;
                this._txtIdAsociado.IsEnabled = value;
                this._txtNombreAsociado.IsEnabled = value;
            }
        }


        #endregion

        public AgregarCarta()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarCarta(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _txtAsociado_GotFocus(object sender, RoutedEventArgs e)
        {
            this._txtAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtCodigoAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociados.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociados.IsEnabled = true;
            this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociado.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociado.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociado.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociado.Visibility = System.Windows.Visibility.Visible;

            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociado.IsDefault = true;

        }

        private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociado())
            {
                this._lstAsociados.Visibility = System.Windows.Visibility.Collapsed;
                this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Collapsed;
                this._txtIdAsociado.Visibility = System.Windows.Visibility.Collapsed;
                this._txtNombreAsociado.Visibility = System.Windows.Visibility.Collapsed;
                this._txtAsociado.Visibility = System.Windows.Visibility.Visible;
                this._txtCodigoAsociado.Visibility = System.Windows.Visibility.Visible;
                this._lblIdAsociado.Visibility = System.Windows.Visibility.Collapsed;
                this._lblNombreAsociado.Visibility = System.Windows.Visibility.Collapsed;

                this._btnAceptar.IsDefault = true;
                this._btnConsultarAsociado.IsDefault = false;

            }
        }

        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado();
        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {

            if ((this._presentador.AgregarAnexoCarta()) && (this._lstAnexosCarta.Visibility == System.Windows.Visibility.Collapsed))
                this._lstAnexosCarta.Visibility = System.Windows.Visibility.Visible;

        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.DeshabilitarAnexosCarta())
            {
                this._lstAnexosCarta.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void _btnMasConfirmacion_Click(object sender, RoutedEventArgs e)
        {

            if ((this._presentador.AgregarAnexoCartaConfirmacion()) && (this._lstAnexosCartaConfirmacion.Visibility == System.Windows.Visibility.Collapsed))
                this._lstAnexosCartaConfirmacion.Visibility = System.Windows.Visibility.Visible;

        }

        private void _btnMenosConfirmacion_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.DeshabilitarAnexosCartaConfirmacion())
            {
                this._lstAnexosCartaConfirmacion.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void _cbxMedio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CarmbiarFormatoTracking();
        }

        private void _cbxMedioTrackingConfirmacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CarmbiarFormatoTrackingConfirmacion();
        }

        private void _btnMasResponsable_Click(object sender, RoutedEventArgs e)
        {
            if ((this._presentador.AgregarResponsable()) && (this._lstResponsables.Visibility == System.Windows.Visibility.Collapsed))
                this._lstResponsables.Visibility = System.Windows.Visibility.Visible;
        }

        private void _btnMenosResponsable_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.DeshabilitarResponsable())
            {
                this._lstResponsables.Visibility = System.Windows.Visibility.Collapsed;
            }
        }



        private void _btnPersona_Click(object sender, RoutedEventArgs e)
        {
            Comodin ventana = new Comodin(new ConsultarCartas());
            ventana.ShowDialog();
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

        //private void _txtDescuentoAdministracion_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if ((!string.Equals(e.Key.ToString(),"OemComma"))||(this._txtDescuentoAdministracion.Text.Contains(",")))
        //    {
        //        if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
        //            e.Handled = true;
        //    }

        //if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
        //    e.Handled = true;


        //private void _chkAlertaAdministracion_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!this._chkAlertaAdministracion.IsChecked.Value)
        //    {
        //        this._txtAlarmaAdministracion.IsEnabled = false;
        //    }
        //    else
        //    {
        //        this._txtAlarmaAdministracion.IsEnabled = true;
        //    }
        //}


        //private void _btnJustificacionesDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.IrListaJustificaciones();
        //}






    }
}
