using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.EscritosMarca;
using Trascend.Bolet.Cliente.Presentadores.EscritosMarca;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.EscritosMarca
{
    /// <summary>
    /// Interaction logic for NumeracionDePoderPorInteresado.xaml
    /// </summary>
    public partial class NumeracionDePoderPorInteresado : Page, INumeracionDePoderPorInteresado
    {
        private PresentadorNumeracionDePoderPorInteresado _presentador;
        private bool _cargada;

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region INumeracionDePoderPorInteresado

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

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresadoFiltrar.Text; }
        }

        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresadoFiltrar.Text; }
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

        public object InteresadosAgregados
        {
            get { return this._lstInteresadosAgregados.DataContext; }
            set { this._lstInteresadosAgregados.DataContext = value; }
        }

        public object InteresadoAgregado
        {
            get { return this._lstInteresadosAgregados.SelectedItem; }
            set { this._lstInteresadosAgregados.SelectedItem = value; }
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

                this._txtNombreInteresado.IsEnabled = value;
                this._txtNombreInteresadoFiltrar.IsEnabled = value;
                this._txtIdInteresadoFiltrar.IsEnabled = value;
                this._btnConsultarInteresado.IsEnabled = value;
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

        public NumeracionDePoderPorInteresado()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorNumeracionDePoderPorInteresado(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmacionGenerarEscritoInteresado,
                this._lstInteresadosAgregados.Items.Count),
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
            MostrarFiltroInteresado(false);

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

        private void _txtNombreInteresado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MostrarFiltroInteresado(true);
            MostrarFiltroAgente(false);

            this._btnConsultarInteresado.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void _lstInteresados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresado())
            {
                _btnMas_Click(sender, e);
                this._btnConsultarInteresado.IsDefault = false;
                this._btnAceptar.IsDefault = true;
            }
        }

        private void _OrdenarInteresados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresados);
        }

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresado();
        }

        private void _txtInteresadoFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarInteresado.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void MostrarFiltroInteresado(bool filtroVisible)
        {
            if (filtroVisible)
            {
                this._txtIdInteresadoFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._txtNombreInteresadoFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Visible;
                this._lstInteresados.Visibility = System.Windows.Visibility.Visible;
                this._lblIdInteresadoFiltrar.Visibility = System.Windows.Visibility.Visible;
                this._lblNombreInteresadoFiltrar.Visibility = System.Windows.Visibility.Visible;

                this._txtNombreInteresado.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this._txtIdInteresadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._txtNombreInteresadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Collapsed;
                this._lstInteresados.Visibility = System.Windows.Visibility.Collapsed;
                this._lblIdInteresadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
                this._lblNombreInteresadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;

                this._txtNombreInteresado.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarInteresado();
        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EliminarInteresado();
        }

        private void _lstInteresadosAgregados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _btnMenos_Click(sender, e);
        }
    }
}
