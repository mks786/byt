using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.EscritosMarca;
using Trascend.Bolet.Cliente.Presentadores.EscritosMarca;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.EscritosMarca
{
    /// <summary>
    /// Interaction logic for ConsultarUsuario.xaml
    /// </summary>
    public partial class ContinuacionDeTramite : Page, IContinuacionDeTramite
    {
        private PresentadorContinuacionDeTramite _presentador;
        private bool _cargada;

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IContinuacionDeTramite

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public string String
        {
            set { this._txtString.Text = value; }
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

        public string IdMarcaFiltrar
        {
            get { return this._txtIdMarcaFiltrar.Text; }
        }

        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
        }

        public object Marca
        {
            get { return this._gridDatosMarca.DataContext; }
            set { this._gridDatosMarca.DataContext = value; }
        }

        public string NombreMarca
        {
            set { this._txtNombreMarca.Text = value; }
        }

        public object MarcasFiltrados
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object MarcaFiltrado
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }

        public object MarcasAgregadas
        {
            get { return this._lstMarcasAgregadas.DataContext; }
            set { this._lstMarcasAgregadas.DataContext = value; }
        }

        public object MarcaAgregada
        {
            get { return this._lstMarcasAgregadas.SelectedItem; }
            set { this._lstMarcasAgregadas.SelectedItem = value; }
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

                this._txtNombreMarca.IsEnabled = value;
                this._txtNombreMarcaFiltrar.IsEnabled = value;
                this._txtIdMarcaFiltrar.IsEnabled = value;
                this._btnConsultarMarca.IsEnabled = value;
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

        public ContinuacionDeTramite()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorContinuacionDeTramite(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GenerarString();
            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmacionGenerarEscrito,
                this._lstMarcasAgregadas.Items.Count),
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
            MostrarFiltroMarca(false);

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

        private void _txtNombreMarca_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MostrarFiltroMarca(true);
            MostrarFiltroAgente(false);

            this._btnConsultarMarca.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void _lstMarcas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarMarca())
            {
                _btnMas_Click(sender, e);
                this._btnConsultarMarca.IsDefault = false;
                this._btnAceptar.IsDefault = true;
            }
        }

        private void _OrdenarMarcas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstMarcas);
        }

        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMarca();
        }

        private void _txtMarcaFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarMarca.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void MostrarFiltroMarca(bool filtroVisible)
        {
            if (filtroVisible)
            {
                this._txtIdMarcaFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._txtNombreMarcaFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._btnConsultarMarca.Visibility = System.Windows.Visibility.Visible;
                this._lstMarcas.Visibility = System.Windows.Visibility.Visible;
                this._lblIdMarcaFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._lblNombreMarcaFiltrar.Visibility = System.Windows.Visibility.Visible;

                this._txtNombreMarca.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this._txtIdMarcaFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._txtNombreMarcaFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._btnConsultarMarca.Visibility = System.Windows.Visibility.Collapsed;
                this._lstMarcas.Visibility = System.Windows.Visibility.Collapsed;
                this._lblIdMarcaFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._lblNombreMarcaFiltrar.Visibility = System.Windows.Visibility.Collapsed;

                this._txtNombreMarca.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMarca();
        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EliminarMarca();
        }

        private void _lstMarcasAgregadas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _btnMenos_Click(sender, e);
        }
    }
}
