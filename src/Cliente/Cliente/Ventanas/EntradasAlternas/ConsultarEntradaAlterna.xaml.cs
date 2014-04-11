using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Trascend.Bolet.Cliente.Contratos.EntradasAlternas;
using Trascend.Bolet.Cliente.Presentadores.EntradasAlternas;

namespace Trascend.Bolet.Cliente.Ventanas.EntradasAlternas
{
    /// <summary>
    /// Interaction logic for ConsultarObjeto.xaml
    /// </summary>
    public partial class ConsultarEntradaAlterna : Page, IConsultarEntradaAlterna
    {

        private PresentadorConsultarEntradaAlterna _presentador;
        private bool _cargada;

        #region IConsultarEntradaAlterna

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public bool HabilitarCampos
        {
            set 
            {
                this._txtDescripcion.IsEnabled = value;
                this._cbxCategoria.IsEnabled = value;
                this._cbxDepartamento.IsEnabled = value;
                this._cbxHoras.IsEnabled = value;
                this._cbxMedio.IsEnabled = value;

                if (this._cbxMedio.SelectedIndex != 0)
                    this._cbxMinutos.IsEnabled = value;

                this._cbxPersona.IsEnabled = value;
                this._cbxReceptor.IsEnabled = value;
                this._cbxRemitente.IsEnabled = value;
                this._cbxTipoDestinatario.IsEnabled = value;
                this._cbxAcuse.IsEnabled = value;
                this._dpkFecha.IsEnabled = value;
            }
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

        public object Persona
        {
            get { return this._cbxPersona.SelectedItem; }
            set { this._cbxPersona.SelectedItem = value; }
        }

        public object Personas
        {
            get { return this._cbxPersona.DataContext; }
            set { this._cbxPersona.DataContext = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }
        public object EntradaAlterna
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
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
            get { return this._cbxRemitente.SelectedItem; }
            set { this._cbxRemitente.SelectedItem = value; }
        }

        public object Remitentes
        {
            get { return this._cbxRemitente.DataContext; }
            set { this._cbxRemitente.DataContext = value; }
        }

        public object Categoria
        {
            get { return this._cbxCategoria.SelectedItem; }
            set { this._cbxCategoria.SelectedItem = value; }
        }

        public object Categorias
        {
            get { return this._cbxCategoria.DataContext; }
            set { this._cbxCategoria.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public object Horas
        {
            get { return this._cbxHoras; }
        }

        public object Hora
        {
            get { return this._cbxHoras.SelectedItem; }
            set { this._cbxPersona.SelectedItem = value; }
        }

        public object Minutos
        {
            get { return this._cbxMinutos; }
        }

        public object Minuto
        {
            get { return this._cbxMinutos.SelectedItem; }
            set { this._cbxPersona.SelectedItem = value; }
        }

        public char GetTipoDestinatario
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxTipoDestinatario.Text))
                    return ((string)this._cbxTipoDestinatario.Text)[0];
                else
                    return ' ';
            }
        }

        public string SetTipoDestinatario
        {
            set { this._cbxTipoDestinatario.Text = value; }
        }

        public string SetHora
        {
            set { this._cbxHoras.Text = value; }
        }

        public string SetMinuto
        {
            set { this._cbxMinutos.Text = value; }
        }

        public object TiposAcuse
        {
            get { return this._cbxAcuse.DataContext; }
            set { this._cbxAcuse.DataContext = value; }
        }

        public object TipoAcuse
        {
            get { return this._cbxAcuse.SelectedItem; }
            set { this._cbxAcuse.SelectedItem = value; }
        }

        #endregion

        public ConsultarEntradaAlterna(object entradaAlterna)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarEntradaAlterna(this, entradaAlterna);

        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarAgente,
                "Eliminar Agente", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _cbxTipoDestinatario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string texto = (sender as ComboBox).SelectedItem.ToString();

            if (texto.Length != 36)
            {
                texto = texto.Substring(38);
            }
            else
            {
                texto = "";
            }

            if (EstaCargada)
            {
                if (texto.Equals(Recursos.Etiquetas.cbiNinguno))
                {
                    this._cbxDepartamento.Visibility = System.Windows.Visibility.Collapsed;
                    this._cbxPersona.Visibility = System.Windows.Visibility.Collapsed;
                    this._wplCodigoDestinatario.Visibility = System.Windows.Visibility.Collapsed;
                }
                else if (texto.Equals(Recursos.Etiquetas.cbiDepartamento))
                {
                    this._cbxDepartamento.Visibility = System.Windows.Visibility.Visible;
                    this._cbxPersona.Visibility = System.Windows.Visibility.Collapsed;
                    this._wplCodigoDestinatario.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    this._cbxPersona.Visibility = System.Windows.Visibility.Visible;
                    this._cbxDepartamento.Visibility = System.Windows.Visibility.Collapsed;
                    this._wplCodigoDestinatario.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                if (texto.Equals(Recursos.Etiquetas.cbiDepartamento))
                {
                    this._cbxDepartamento.Visibility = System.Windows.Visibility.Visible;
                    this._wplCodigoDestinatario.Visibility = System.Windows.Visibility.Visible;
                }
                else if (texto.Equals(Recursos.Etiquetas.cbiPersona))
                {
                    this._cbxPersona.Visibility = System.Windows.Visibility.Visible;
                    this._wplCodigoDestinatario.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void _cbxHoras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EstaCargada)
            {
                string texto = (sender as ComboBox).SelectedItem.ToString();

                if (!texto.Equals(""))
                {
                    this._cbxMinutos.IsEnabled = true;
                }
                else
                {
                    this._cbxMinutos.SelectedIndex = 0;
                    this._cbxMinutos.IsEnabled = false;
                }
            }
        }

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }

        public void MensajeConfirmacion(bool flag)
        {
            if (flag)
                this._txtMensaje.Text = "Operacion realizada exitosamente";
            else
                this._txtMensaje.Text = null;
        }

        
        public void PintarAuditoria()
        {
            this._btnAuditoria.Background = Brushes.LightGreen;
        }
    }
}
