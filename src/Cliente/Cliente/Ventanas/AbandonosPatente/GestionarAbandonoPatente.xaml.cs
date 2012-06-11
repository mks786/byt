using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.AbandonosPatente;
using Trascend.Bolet.Cliente.Presentadores.AbandonosPatente;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.AbandonosPatente
{
    /// <summary>
    /// Interaction logic for GestionarAbandono.xaml
    /// </summary>
    public partial class GestionarAbandonoPatente : Page, IGestionarAbandonoPatente
    {

        private PresentadorGestionarAbandonoPatente _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IGestionarAbandonoPatente

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Operacion
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

        public string Aplicada
        {
            get { return this._txtAplicada.Text; }
        }

        public object Asociado
        {
            get { return this._gridDatosAsociado.DataContext; }
            set { this._gridDatosAsociado.DataContext = value; }
        }

        public string NombreAsociado
        {
            set { this._txtNombreAsociado.Text = value; }
        }

        public string IdAsociadoFiltrar
        {
            get { return this._txtIdAsociadoFiltrar.Text; }
        }

        public string NombreAsociadoFiltrar
        {
            get { return this._txtNombreAsociadoFiltrar.Text; }
        }

        public object AsociadosFiltrados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public object AsociadoFiltrado
        {
            get { return this._lstAsociados.SelectedItem; }
            set { this._lstAsociados.SelectedItem = value; }
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

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresadoFiltrar.Text; }
        }

        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresadoFiltrar.Text; }
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

        public object Patente
        {
            get { return this._gridDatosPatente.DataContext; }
            set { this._gridDatosPatente.DataContext = value; }
        }

        public string NombrePatente
        {
            set { this._txtNombrePatente.Text = value; }
        }

        public string IdPatenteFiltrar
        {
            get { return this._txtIdPatenteFiltrar.Text; }
        }

        public string NombrePatenteFiltrar
        {
            get { return this._txtNombrePatenteFiltrar.Text; }
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

        public void FocoPredeterminado()
        {
            this._txtIdOperacion.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtIdOperacion.IsEnabled = value;
                this._txtAplicada.IsEnabled = value;               
                //this._dpkFechaOperacion.IsEnabled = value; 


                this._txtNombrePatente.IsEnabled = value;
                this._txtIdPatenteFiltrar.IsEnabled = value;
                this._txtNombrePatenteFiltrar.IsEnabled = value;
                this._btnConsultarPatente.IsEnabled = value;

                this._txtNombreAsociado.IsEnabled = value;
                this._txtIdAsociadoFiltrar.IsEnabled = value;
                this._txtNombreAsociadoFiltrar.IsEnabled = value;
                this._btnConsultarAsociado.IsEnabled = value;

                this._txtNombreInteresado.IsEnabled = value;
                this._txtNombreInteresadoFiltrar.IsEnabled = value;
                this._txtIdInteresadoFiltrar.IsEnabled = value;
                this._txtPaisInteresado.IsEnabled = value;
                this._txtCiudadInteresado.IsEnabled = value;
                this._btnConsultarInteresado.IsEnabled = value;

                //this._cbxBoletin.IsEnabled = value;

                this._txtDescripcionServicio.IsEnabled = value;

                this._txtDescripcionOperacion.IsEnabled = value;                
            }
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

        public GestionarAbandonoPatente(object abandono)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarAbandonoPatente(this, abandono);
        }

                /// <summary>
        /// Constructor para la consulta desde operaciones
        /// </summary>
        /// <param name="abandono">la abandono a mostrar</param>
        /// <param name="visibilidad">parametro que indica la visibilidad de los botones</param>
        public GestionarAbandonoPatente(object abandono, object visibilidad)
        {
            InitializeComponent();
            this._cargada = false;
            this._btnEliminar.Visibility = (System.Windows.Visibility)visibilidad;
            this._presentador = new PresentadorGestionarAbandonoPatente(this, abandono);
        }


        public void PintarAsociado(string tipo)
        {
            SolidColorBrush color;

            if (tipo.Equals("1"))
            {
                color = Brushes.LightGreen;
            }
            else if (tipo.Equals("2"))
            {
                color = Brushes.LightBlue;
            }
            else if (tipo.Equals("3"))
            {
                color = Brushes.LightYellow;
            }
            else if (tipo.Equals("4"))
            {
                color = Brushes.Pink;
            }
            else color = Brushes.White;

            this._txtNombreAsociado.Background = color;
        }


        public void ActivarControlesAlAgregar()
        {
            this.HabilitarCampos = true;

            this._btnAceptar.Visibility = System.Windows.Visibility.Visible;
            this._btnEliminar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdOperacion.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdOperacion.Visibility = System.Windows.Visibility.Collapsed;
            this._dpkFechaOperacion.IsEnabled = false;
            this._cbxBoletin.IsEnabled = true;
            this._lblTipoServicio.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDescripcionServicio.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Agregar();
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarAbandono,
                "Eliminar Abandono", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name.Equals("_btnConsultarPatente"))
                this._presentador.ConsultarPatentes();
            else if (((Button)sender).Name.Equals("_btnConsultarInteresado"))
                this._presentador.ConsultarInteresados();
            else if (((Button)sender).Name.Equals("_btnConsultarAsociado"))
                this._presentador.ConsultarAsociados();            
        }

        #region Eventos Patentes

        private void _lstPatentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPatente())
            {
                GestionarVisibilidadDatosDePatente(Visibility.Visible);
                GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

                this._btnConsultarPatente.IsDefault = false;
                this._btnAceptar.IsDefault = true;
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

            //escondo el filtro de interesado
            GestionarVisibilidadDatosDeInteresado(Visibility.Visible);
            GestionarVisibilidadFiltroInteresado(Visibility.Collapsed);

            //escondo el filtro de Asociado
            GestionarVisibilidadDatosDeAsociado(Visibility.Visible);
            GestionarVisibilidadFiltroAsociado(Visibility.Collapsed);

            this._btnConsultarPatente.IsDefault = false;
            this._btnAceptar.IsDefault = true;
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
            this._btnAceptar.IsDefault = false;
        }

        #endregion

        #region Eventos Interesado

        private void _lstInteresados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresado())
            {
                GestionarVisibilidadDatosDeInteresado(Visibility.Visible);
                GestionarVisibilidadFiltroInteresado(Visibility.Collapsed);
            }
        }

        private void _OrdenarInteresados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresados);
        }

        private void _txtInteresado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeInteresado(Visibility.Collapsed);

            GestionarVisibilidadFiltroInteresado(Visibility.Visible);

            //escondo el filtro de interesado Patente
            GestionarVisibilidadDatosDePatente(Visibility.Visible);
            GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

            //escondo el filtro de Asociado
            GestionarVisibilidadDatosDeAsociado(Visibility.Visible);
            GestionarVisibilidadFiltroAsociado(Visibility.Collapsed);
           
            this._btnConsultarInteresado.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresado(object value)
        {
            this._lblIdInteresado.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresado.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresados.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresado.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresado(object value)
        {
            this._txtNombreInteresado.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresado.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresado.Visibility = (System.Windows.Visibility)value;
        }

        public void GestionarBotonConsultarInteresado(bool value)
        {
            this._btnConsultarInteresado.IsEnabled = value;
        }

        private void _txtInteresadoFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarInteresado.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        #endregion       

        #region Eventos Asociado

        private void _lstAsociados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociado())
            {
                GestionarVisibilidadDatosDeAsociado(Visibility.Visible);
                GestionarVisibilidadFiltroAsociado(Visibility.Collapsed);                
            }
        }

        private void _OrdenarAsociados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresados);
        }

        private void _txtNombreAsociado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeAsociado(Visibility.Collapsed);

            GestionarVisibilidadFiltroAsociado(Visibility.Visible);

            //escondo el filtro de Patente
            GestionarVisibilidadDatosDePatente(Visibility.Visible);
            GestionarVisibilidadFiltroPatente(Visibility.Collapsed);

            //escondo el filtro de interesado
            GestionarVisibilidadDatosDeInteresado(Visibility.Visible);
            GestionarVisibilidadFiltroInteresado(Visibility.Collapsed);

            this._btnConsultarAsociado.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroAsociado(object value)
        {
            this._lblIdAsociado.Visibility = (System.Windows.Visibility)value;
            this._lblNombreAsociado.Visibility = (System.Windows.Visibility)value;
            this._txtNombreAsociadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdAsociadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstAsociados.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarAsociado.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeAsociado(object value)
        {
            this._txtNombreAsociado.Visibility = (System.Windows.Visibility)value;          
        }

        public void GestionarBotonConsultarAsociado(bool value)
        {
            this._btnConsultarAsociado.IsEnabled = value;
        }

        private void _txtAsociadoFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarAsociado.IsDefault = true;
            this._btnAceptar.IsDefault = false;
        }

        #endregion       

    }
}
