using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.TraspasosPatentes.FusionesPatentes;
using Trascend.Bolet.Cliente.Presentadores.TraspasosPatentes.FusionesPatentes;

namespace Trascend.Bolet.Cliente.Ventanas.TraspasosPatentes.FusionesPatentes
{
    /// <summary>
    /// Interaction logic for ConsultarTodosPoder.xaml
    /// </summary>
    public partial class ConsultarFusionesPatentes : Page, IConsultarFusionesPatentes
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarFusionesPatentes _presentador;
        private bool _cargada;

        #region IConsultarFusiones

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
            set { this._txtPatenteNombre.Text = value; }
        }
        public object FusionSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string IdPatenteFiltrar
        {
            get { return this._txtIdPatenteFiltrar.Text; }
            set { this._txtIdPatenteFiltrar.Text = value; }
        }

        public string NombrePatenteFiltrar
        {
            get { return this._txtNombrePatenteFiltrar.Text; }
            set { this._txtNombrePatenteFiltrar.Text = value; }
        }

        public string Fecha
        {
            get { return this._dpkFecha.SelectedDate.ToString(); }
            set { this._dpkFecha.Text = value; }
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
        public ConsultarFusionesPatentes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarFusionesPatentes(this);
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
            validarCamposVacios();
        }


        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarFusion();
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


        private void _btnConsultarPatenteFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarPatente.IsDefault = true;
            //this._btnConsultarInteresado.IsDefault = false;
        }


        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
            this._btnConsultarPatente.IsDefault = false;
            //this._btnConsultarInteresado.IsDefault = false;
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

            //if (!this._txtDescripcion.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtDescripcion.Focus();
            //}

            //if (!this._txtFichas.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtFichas.Focus();
            //}

            if (!this._dpkFecha.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFecha.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }


        private void _dpkFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void _txtPatenteNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            GestionarVisibilidadDatosDePatente(Visibility.Collapsed);
            GestionarVisibilidadFiltroPatente(Visibility.Visible);
        }


        private void _lstPatentes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.ElegirPatente())
            {
                GestionarVisibilidadDatosDePatente(Visibility.Visible);
                GestionarVisibilidadFiltroPatente(Visibility.Collapsed);
            }
        }


        private void GestionarVisibilidadFiltroPatente(object value)
        {
            this._txtIdPatenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombrePatenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPatente.Visibility = (System.Windows.Visibility)value;
            this._lstPatentes.Visibility = (System.Windows.Visibility)value;
            this._lblCodigo.Visibility = (System.Windows.Visibility)value;
            this._lblNombre.Visibility = (System.Windows.Visibility)value;
        }


        private void GestionarVisibilidadDatosDePatente(object value)
        {
            this._txtPatenteNombre.Visibility = (System.Windows.Visibility)value;
        }


        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }
    }
}
