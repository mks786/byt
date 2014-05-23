using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.EntradasAlternas;
using Trascend.Bolet.Cliente.Presentadores.EntradasAlternas;

namespace Trascend.Bolet.Cliente.Ventanas.EntradasAlternas
{
    /// <summary>
    /// Interaction logic for AgregarEntradaAlterna.xaml
    /// </summary>
    public partial class AgregarEntradaAlterna : Page, IAgregarEntradaAlterna
    {
        private PresentadorAgregarEntradaAlterna _presentador;
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

        public char TipoDestinatario
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxTipoDestinatario.Text))
                    return ((string)this._cbxTipoDestinatario.Text)[0];
                else
                    return ' ';
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

        public void BorrarCeros() 
        {
            this._txtId.Text = string.Empty;
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

        public string Destinatario
        {
            get { return this._txtDestinatario.Text; }
            set { this._txtDestinatario.Text = value; }
        }

        #endregion

        public AgregarEntradaAlterna()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarEntradaAlterna(this);
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
                this._dpkFecha.SelectedDate = System.DateTime.Today;
                EstaCargada = true;
            }
        }

        private void _cbxTipoDestinatario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EstaCargada)
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

        private void _btnRemitente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaRemitente();
        }

        private void _btnMedio_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaMedio();

        }

        private void _btnTipoEntrada_Categoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaTipoDeEntrada_Categoria();
        }

        public void RefrescarMedio(object medio)
        {
            this._presentador.RefrescarMedio(medio);
        }

        public void RefrescarRemitente(object remitente)
        {
            this._presentador.RefrescarRemitente(remitente);
        }


        public void RefrescarTipoDeEntradaCategoria(object tipoEntradaCategoria)
        {
            this._presentador.RefrescarTipoEntradaCategoria(tipoEntradaCategoria);
        }

        private void _btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarReiniciarEntradaAlterna),
                    "Reiniciar Entrada Alterna", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.LimpiarPantalla();
            }
            
        }

        public void MostrarCampoDestinatario()
        {
            this._btnRemitente.Visibility = System.Windows.Visibility.Collapsed;
            this._cbxRemitente.Visibility = System.Windows.Visibility.Collapsed;
            this._lblTipoDestinatario.Visibility = System.Windows.Visibility.Collapsed;
            this._cbxTipoDestinatario.Visibility = System.Windows.Visibility.Collapsed;
            this._lblDestinatario.Visibility = System.Windows.Visibility.Visible;
            this._txtDestinatario.Visibility = System.Windows.Visibility.Visible;
        }

        public void OcultarCampoDestinatario()
        {
            this._btnRemitente.Visibility = System.Windows.Visibility.Visible;
            this._cbxRemitente.Visibility = System.Windows.Visibility.Visible;
            this._lblTipoDestinatario.Visibility = System.Windows.Visibility.Visible;
            this._cbxTipoDestinatario.Visibility = System.Windows.Visibility.Visible;
            this._lblDestinatario.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDestinatario.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void _cbxAcuse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._cbxAcuse.SelectedIndex == 0 || this._cbxAcuse.SelectedIndex == 1)
                OcultarCampoDestinatario();
            if (this._cbxAcuse.SelectedIndex == 2)
                MostrarCampoDestinatario();
        }

        
    }
}
