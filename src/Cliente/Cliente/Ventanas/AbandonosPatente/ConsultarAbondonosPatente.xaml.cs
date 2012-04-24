using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.AbandonosPatente;
using Trascend.Bolet.Cliente.Presentadores.AbandonosPatente;

namespace Trascend.Bolet.Cliente.Ventanas.AbandonosPatente
{
    /// <summary>
    /// Interaction logic for ConsultarAbandonosPatente.xaml
    /// </summary>
    public partial class ConsultarAbandonosPatente : Page, IConsultarAbandonosPatente
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarAbandonosPatente _presentador;
        private bool _cargada;

        #region IConsultarRenovacionesPatente

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public object AbandonoSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
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

        public object Patentes
        {
            get { return this._lstPatentes.DataContext; }
            set { this._lstPatentes.DataContext = value; }
        }

        public object Patente
        {
            get { return this._lstPatentes.SelectedItem; }
            set { this._lstPatentes.SelectedItem = value; }
        }

        public string IdPatenteFiltrar
        {
            get { return this._txtIdPatenteFiltrar.Text; }
        }

        public string Fecha
        {
            get { return this._dpkFecha.Text; }
            set { this._dpkFecha.Text = value; }
        }

        public string NombrePatenteFiltrar
        {
            get { return this._txtNombrePatenteFiltrar.Text; }
        }

        public string NombrePatente
        {
            set { this._txtPatente.Text = value; }
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }      

        public string PatenteFiltrada
        {
            get { return this._txtPatente.Text; }
            set { this._txtPatente.Text = value; }           
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarAbandonosPatente()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarAbandonosPatente(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();            
            validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarAbandono();
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

        private void _btnConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarPatente();
        }          

        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
            this._btnConsultarPatente.IsDefault = false;           
        }

        private void _btnConsultarPatenteFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarPatente.IsDefault = true;
        }

        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

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

            if (todosCamposVacios)
                this._txtId.Focus();
        }      
      
        private void _txtPatente_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroPatente(true);            
        }        

        private void GestionarVisibilidadFiltroPatente(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtPatente.Visibility = Visibility.Collapsed;

                this._txtIdPatenteFiltrar.Visibility = Visibility.Visible;
                this._txtNombrePatenteFiltrar.Visibility = Visibility.Visible;
                this._lblIdPatente.Visibility = Visibility.Visible;
                this._lblNombrePatente.Visibility = Visibility.Visible;
                this._lstPatentes.Visibility = Visibility.Visible;
                this._btnConsultarPatente.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtPatente.Visibility = Visibility.Visible;

                this._txtIdPatenteFiltrar.Visibility = Visibility.Collapsed;
                this._txtNombrePatenteFiltrar.Visibility = Visibility.Collapsed;
                this._lblIdPatente.Visibility = Visibility.Collapsed;
                this._lblNombrePatente.Visibility = Visibility.Collapsed;
                this._lstPatentes.Visibility = Visibility.Collapsed;
                this._btnConsultarPatente.Visibility = Visibility.Collapsed;
            }
        }     

        private void _lstPatentes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.ElegirPatente())
                GestionarVisibilidadFiltroPatente(false);
        }       
        
    }
}
