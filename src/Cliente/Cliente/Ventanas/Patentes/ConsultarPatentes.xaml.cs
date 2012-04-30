using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Presentadores.Patentes;

namespace Trascend.Bolet.Cliente.Ventanas.Patentes
{
    /// <summary>
    /// Interaction logic for ConsultarPatentes.xaml
    /// </summary>
    public partial class ConsultarPatentes : Page, IConsultarPatentes
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarPatentes _presentador;
        private bool _cargada;

        #region IConsultarCesiones

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

        public string NombrePatente
        {
            get { return this._txtNombre.Text; }
            set { this._txtNombre.Text = value; }
        }

        public object Patente
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string Fecha
        {
            get { return this._dpkFecha.SelectedDate.ToString(); }
        }

        public object Patentes
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
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
   
   
        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarPatentes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarPatentes(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            this._dpkFecha.Text = string.Empty;
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrGestionarPatente();
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

        //private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.BuscarAsociado();
        //}

        //private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.BuscarInteresado();
        //}

        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        
        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _btnTransferir_Click(object sender, RoutedEventArgs e)
        {

        }


        public void LimpiarCampos()
        {
            
        }

    }
}
