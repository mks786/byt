using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Anualidades;
using Trascend.Bolet.Cliente.Presentadores.Anualidades;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Anualidades
{
    /// <summary>
    /// Interaction logic for GestionarAnualidades.xaml
    /// </summary>
    public partial class GestionarAnualidades : Page, IGestionarAnualidades
    {

        private PresentadorGestionarAnualidades _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarAnualidades

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object FusionPatente
        {
            get
            {
                return this._gridDatos.DataContext;
            }
            set
            {
                this._gridDatos.DataContext = value;
            }
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

        public string Region
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

        public bool HabilitarCampos
        {
            set
            {
                
           this._txtIdPatenteFiltrar.IsEnabled = value;
                this._txtNombrePatente.IsEnabled = value;
                this._txtNombrePatenteFiltrar.IsEnabled = value;

                this._btnConsultarPatente.IsEnabled = value;

            }
        }

        public string NombrePatente
        {
            set { this._txtNombrePatente.Text = value; }
        }

        public object PatentesFiltradas
        {
            get { return this._lstPatentes.DataContext; }
            set { this._lstPatentes.DataContext = value; }
        }

        public object PatenteFiltrada
        {
            get { return this._lstPatentes.SelectedItem; }
            set { this._lstPatentes.SelectedItem = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public string TextoBotonRegresar
        {
            get { return this._txbRegresar.Text; }
            set { this._txbRegresar.Text = value; }
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

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion

        public GestionarAnualidades(object fusion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarAnualidades(this, fusion);
        }

        public void ActivarControlesAlAgregar()
        {
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            //this._btnCarpeta.Visibility = System.Windows.Visibility.Collapsed;          
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {            
            if (this.TextoBotonRegresar == Recursos.Etiquetas.btnRegresar)
                this._presentador.Regresar();
            else if (this.TextoBotonRegresar == Recursos.Etiquetas.btnCancelar)
                this._presentador.Cancelar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarFusion,
                "Eliminar FusionPatente", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
        }

        private void _btnPlanilla_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnPlanillaVan_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnPlanillaVienen_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnAnexo_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void _btnCarpeta_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrImprimir(((Button)sender).Name);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }


        #region Eventos Patentes

        private void _btnConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPatentes();
        }

        private void _lstPatentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPatente())
            {
                GestionarVisibilidadDatosDePatente(Visibility.Visible);
                GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

                this._btnConsultarPatente.IsDefault = false;
                this._btnModificar.IsDefault = true;
            }
        }

        private void _OrdenarPatentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPatentes);
        }

        private void _txtNombrePatente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDePatente(Visibility.Collapsed);
            GestionarVisibilidadFiltroPatente(Visibility.Visible);
            this._btnConsultarPatente.IsDefault = false;
            this._btnModificar.IsDefault = true;
        }

        private void GestionarVisibilidadFiltroPatente(object value)
        {
            this._lblNombrePatente.Visibility = (System.Windows.Visibility)value;
            this._txtNombrePatenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPatente.Visibility = (System.Windows.Visibility)value;
            this._txtIdPatenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPatentes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPatente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDePatente(object value)
        {
            this._txtNombrePatente.Visibility = (System.Windows.Visibility)value;

        }

        private void _txtPatenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPatente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        #endregion

        public void FocoPredeterminado()
        {
            this._txtIdPatenteFiltrar.Focus();
        }

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name.Equals("_btnConsultarPatente"))
                this._presentador.ConsultarPatentes();

        }
    }
}
