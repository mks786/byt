using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.EscritosPatente;
using Trascend.Bolet.Cliente.Presentadores.EscritosPatente;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.EscritosPatente
{
    /// <summary>
    /// Interaction logic for ExamenDePatentabilidad.xaml
    /// </summary>
    public partial class ReconsideracionPerimidaVariante : Page, IReconsideracionPerimidaVariante
    {
        private PresentadorReconsideracionPerimidaVariante _presentador;
        private bool _cargada;

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IPerimidaVariante

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtIdAgenteFiltrar.Focus();
        }

        public object Escrito
        {
            get { return this._gridDatosAgente.DataContext; }
            set { this._gridDatosAgente.DataContext = value; }
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

        public string IdAgenteFiltrar
        {
            get { return this._txtIdAgenteFiltrar.Text; }
        }

        public string NombreAgenteFiltrar
        {
            get { return this._txtNombreAgenteFiltrar.Text; }
        }

        public object Resoluciones
        {
            get { return this._cbxResolucion.DataContext; }
            set { this._cbxResolucion.DataContext = value; }
        }

        public object Resolucion
        {
            get { return this._cbxResolucion.SelectedItem; }
            set { this._cbxResolucion.SelectedItem = value; }
        }

        public object Resoluciones2
        {
            get { return this._cbxResolucion2.DataContext; }
            set { this._cbxResolucion2.DataContext = value; }
        }

        public object Resolucion2
        {
            get { return this._cbxResolucion2.SelectedItem; }
            set { this._cbxResolucion2.SelectedItem = value; }
        }

        public object Boletines
        {
            get { return this._cbxBoletin.DataContext; }
            set { this._cbxBoletin.DataContext = value; }
        }

        public object Boletin
        {
            get { return this._cbxBoletin.SelectedItem; }
            set { this._cbxBoletin.SelectedItem = value; }
        }

        public object Modalidades
        {
            get { return this._cbxModalidad.DataContext; }
            set { this._cbxModalidad.DataContext = value; }
        }

        public object Modalidad
        {
            get { return this._cbxModalidad.SelectedItem; }
            set { this._cbxModalidad.SelectedItem = value; }
        }
         public object Boletines2
        {
            get { return this._cbxBoletin2.DataContext; }
            set { this._cbxBoletin2.DataContext = value; }
        }

        public object Boletin2
        {
            get { return this._cbxBoletin2.SelectedItem; }
            set { this._cbxBoletin2.SelectedItem = value; }
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

        public string IdPatenteFiltrar
        {
            get { return this._txtIdPatenteFiltrar.Text; }
        }

        public string NombrePatenteFiltrar
        {
            get { return this._txtNombrePatenteFiltrar.Text; }
        }

        public object Patente
        {
            get { return this._gridDatosPatente.DataContext; }
            set { this._gridDatosPatente.DataContext = value; }
        }

        public string NombrePatente
        {
            set { this._txtNombrePatente.Text = value; }
        }

        public object PatentesFiltrados
        {
            get { return this._lstPatentes.DataContext; }
            set { this._lstPatentes.DataContext = value; }
        }

        public object PatenteFiltrado
        {
            get { return this._lstPatentes.SelectedItem; }
            set { this._lstPatentes.SelectedItem = value; }
        }

        public object PatentesAgregadas
        {
            get { return this._lstPatentesAgregadas.DataContext; }
            set { this._lstPatentesAgregadas.DataContext = value; }
        }

        public object PatenteAgregada
        {
            get { return this._lstPatentesAgregadas.SelectedItem; }
            set { this._lstPatentesAgregadas.SelectedItem = value; }
        }

        public string Fecha
        {
            get { return this._dpkFecha.SelectedDate.ToString(); }
            set { this._dpkFecha.Text = value; }
        }

        public string FechaDeAviso
        {
            get { return this._dpkFecha1.SelectedDate.ToString(); }
        }

        public string BotonModificar
        {
            get { return this._txbAceptar.Text; }
            set { this._txbAceptar.Text = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtNombreAgente.IsEnabled = value;
                this._txtNombreAgenteFiltrar.IsEnabled = value;
                this._txtIdAgenteFiltrar.IsEnabled = value;
                this._btnConsultarAgente.IsEnabled = value;

                this._txtNombrePatente.IsEnabled = value;
                this._txtNombrePatenteFiltrar.IsEnabled = value;
                this._txtIdPatenteFiltrar.IsEnabled = value;
                this._btnConsultarPatente.IsEnabled = value;
                this._btnMas.IsEnabled = value;
                this._btnMenos.IsEnabled = value;
            }
        }

        public void MensajeAlerta(string mensaje)
        {
            MessageBox.Show(mensaje,
            "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion

        public ReconsideracionPerimidaVariante()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorReconsideracionPerimidaVariante(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmacionGenerarEscritoPatente,
                this._lstPatentesAgregadas.Items.Count),
                "Generar Escrito", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _txtNombreAgente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MostrarFiltroAgente(true);
            MostrarFiltroPatente(false);

            this._btnConsultarAgente.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void _lstAgentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAgente())
            {
                MostrarFiltroAgente(false);

                this._btnConsultarAgente.IsDefault = false;
                this._btnAceptar.IsDefault = true;
            }
        }

        private void _OrdenarAgentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAgentes);
        }

        private void _btnConsultarAgente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarAgente();
        }

        private void _txtAgenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarAgente.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void MostrarFiltroAgente(bool filtroVisible)
        {
            if (filtroVisible)
            {
                this._txtIdAgenteFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._txtNombreAgenteFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._btnConsultarAgente.Visibility = System.Windows.Visibility.Visible;
                this._lstAgentes.Visibility = System.Windows.Visibility.Visible;
                this._lblIdAgenteFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._lblNombreAgenteFiltrar.Visibility = System.Windows.Visibility.Visible;

                this._txtNombreAgente.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this._txtIdAgenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._txtNombreAgenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._btnConsultarAgente.Visibility = System.Windows.Visibility.Collapsed;
                this._lstAgentes.Visibility = System.Windows.Visibility.Collapsed;
                this._lblIdAgenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._lblNombreAgenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;

                this._txtNombreAgente.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void _txtNombrePatente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MostrarFiltroPatente(true);
            MostrarFiltroAgente(false);

            this._btnConsultarPatente.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void _lstPatentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPatente())
            {
                _btnMas_Click(sender, e);
                this._btnConsultarPatente.IsDefault = false;
                this._btnAceptar.IsDefault = true;
            }
        }

        private void _OrdenarPatentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPatentes);
        }

        private void _btnConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPatente();
        }

        private void _txtPatenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPatente.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void MostrarFiltroPatente(bool filtroVisible)
        {
            if (filtroVisible)
            {
                this._txtIdPatenteFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._txtNombrePatenteFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._btnConsultarPatente.Visibility = System.Windows.Visibility.Visible;
                this._lstPatentes.Visibility = System.Windows.Visibility.Visible;
                this._lblIdPatenteFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._lblNombrePatenteFiltrar.Visibility = System.Windows.Visibility.Visible;

                this._txtNombrePatente.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this._txtIdPatenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._txtNombrePatenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._btnConsultarPatente.Visibility = System.Windows.Visibility.Collapsed;
                this._lstPatentes.Visibility = System.Windows.Visibility.Collapsed;
                this._lblIdPatenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._lblNombrePatenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;

                this._txtNombrePatente.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarPatente();
        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EliminarPatente();
        }

        private void _lstPatentesAgregadas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _btnMenos_Click(sender, e);
        }

        private void _cbxBoletin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.ActualizarResoluciones();
        }

        private void _cbxBoletin2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.ActualizarResoluciones2();
        }

        public void ActualizarResoluciones()
        {
            throw new System.NotImplementedException();
        }


        public void ActualizarResoluciones2()
        {
            throw new System.NotImplementedException();
        }
    }
}
