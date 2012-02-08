using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Traspasos.Cesiones;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.Cesiones;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Traspasos.Cesiones
{
    /// <summary>
    /// Interaction logic for GestionarFusion.xaml
    /// </summary>
    public partial class GestionarCesion : Page, IGestionarCesion
    {

        private PresentadorGestionarCesion _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarFusion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }
      

        public string IdAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }       

        public string IdCedenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreCedenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdCesionarioFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreCesionarioFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdPoderCesionarioFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombrePoderCesionarioFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdPoderCedenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombrePoderCedenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdApoderadoCesionarioFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreApoderadoCesionarioFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdApoderadoCedenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreApoderadoCedenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
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

        public object InteresadoCedente
        {
            get { return this._gridDatosCedente.DataContext; }
            set { this._gridDatosCedente.DataContext = value; }            
        }
        
        public object InteresadoCesionario
        {
            get { return this._gridDatosCesionario.DataContext; }
            set { this._gridDatosCesionario.DataContext = value; }            
        }

        public object ApoderadoCedente
        {
            get { return this._gridDatosApoderadoCedente.DataContext; }
            set { this._gridDatosApoderadoCedente.DataContext = value; }
        }

        public object ApoderadoCesionario
        {
            get { return this._gridDatosApoderadoCesionario.DataContext; }
            set { this._gridDatosApoderadoCesionario.DataContext = value; }            
        }
    
        public object PoderCedente
        {
            get { return this._gridDatosPoderCedente.DataContext; }
            set { this._gridDatosPoderCedente.DataContext = value; }            
        }

        public object PoderCesionario
        {
            get { return this._gridDatosPoderCesionario.DataContext; }
            set { this._gridDatosPoderCesionario.DataContext = value; }
        }  

        public object MarcasFiltradas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object CedentesFiltrados
        {
            get { return this._lstCedentes.DataContext; }
            set { this._lstCedentes.DataContext = value; }
        }

        public object CesionariosFiltrados
        {
            get { return this._lstCesionarios.DataContext; }
            set { this._lstCesionarios.DataContext = value; }
        }

        public object ApoderadosCedenteFiltrados
        {
            get { return this._lstApoderadosCedente.DataContext; }
            set { this._lstApoderadosCedente.DataContext = value; }
        }

        public object ApoderadosCesionarioFiltrados
        {
            get { return this._lstApoderadosCesionario.DataContext; }
            set { this._lstApoderadosCesionario.DataContext = value; }
        }

        public object PoderesCedenteFiltrados
        {
            get { return this._lstPoderesCedente.DataContext; }
            set { this._lstPoderesCedente.DataContext = value; }
        }

        public object PoderesCesionarioFiltrados
        {
            get { return this._lstPoderesCesionario.DataContext; }
            set { this._lstPoderesCesionario.DataContext = value; }
        }
         
        public object Cesion
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }
        
        public object MarcaFiltrada
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }

        public object CedenteFiltrado
        {
            get { return this._lstCedentes.SelectedItem; }
            set { this._lstCedentes.SelectedItem = value; }
        }

        public object CesionarioFiltrado
        {
            get { return this._lstCesionarios.SelectedItem; }
            set { this._lstCesionarios.SelectedItem = value; }
        }

        public object ApoderadoCedenteFiltrado
        {
            get { return this._lstApoderadosCedente.SelectedItem; }
            set { this._lstApoderadosCedente.SelectedItem = value; }
        }

        public object ApoderadoCesionarioFiltrado
        {
            get { return this._lstApoderadosCesionario.SelectedItem; }
            set { this._lstApoderadosCesionario.SelectedItem = value; }
        }

        public object PoderCedenteFiltrado
        {
            get { return this._lstPoderesCedente.SelectedItem; }
            set { this._lstPoderesCedente.SelectedItem = value; }
        }

        public object PoderCesionarioFiltrado
        {
            get { return this._lstPoderesCesionario.SelectedItem; }
            set { this._lstPoderesCesionario.SelectedItem = value; }
        }

        public string Region
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtAsociado.IsEnabled = value;
                this._txtClaseInternacional.IsEnabled = value;
                this._txtClaseNacional.IsEnabled = value;
                this._txtExpediente.IsEnabled = value;
                this._txtId.IsEnabled = value;
                this._txtIdMarcaFiltrar.IsEnabled = value;
                this._txtNombreMarca.IsEnabled = value;
                this._txtNombreMarcaFiltrar.IsEnabled = value;
                this._txtNumInscripcion.IsEnabled = value;
                this._txtNumRegistro.IsEnabled = value;
                this._txtTipo.IsEnabled = value;
                this._txtNombreCedente.IsEnabled = value;
                this._txtPaisCedente.IsEnabled = value;
                this._txtNacionalidadCedente.IsEnabled = value;
                this._btnConsultarMarca.IsEnabled = value;
            }
        }

        public string NombreMarca
        {
            set { this._txtNombreMarca.Text = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
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

        #endregion

        public GestionarCesion(object cesion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCesion(this, cesion);
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarPais,
                "Eliminar Pais", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMarcas();
        }

        private void _btnConsultarCedente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnConsultarCesionario_Click(object sender, RoutedEventArgs e)
        {

        }
              
        private void _btnConsultarApoderadoCedente_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void _btnConsultarApoderadoCesionario_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void _btnPlanillaVienen_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void _btnPlanillaVan_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void _btnPlanilla_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void _btnCarpeta_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void _btnAnexo_Click(object sender, RoutedEventArgs e)
        {
            
        }                         

        private void _lstMarcas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarMarca())
            {
                GestionarVisibilidadDatosDeMarca(Visibility.Visible);
                GestionarVisibilidadFiltroMarca(Visibility.Collapsed);
            }
        }

        private void _lstCedentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void _lstCesionarios_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }        
        
        private void _lstPoderesCedente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
        private void _lstPoderesCesionario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }        

        private void _lstApoderadosCedente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void _lstApoderadosCesionario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
                
        private void _OrdenarMarcas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstMarcas);
        }

        private void _OrdenarCedentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCedentes);
        }

        private void _OrdenarCesionarios_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCedentes);
        }
        
        private void _OrdenarApoderadosCedente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCedentes);
        }

        private void _OrdenarApoderadosCesionario_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCedentes);
        }
        

        private void _OrdenarPoderesCedente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCedentes);
        }

        private void _OrdenarPoderesCesionario_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCedentes);
        }
        
        private void _txtNombreMarca_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Visible);
        }

        private void GestionarVisibilidadFiltroMarca(object value)
        {
            this._lblNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._txtNombreMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdMarca.Visibility = (System.Windows.Visibility)value;
            this._txtIdMarcaFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstMarcas.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarMarca.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeMarca(object value)
        {
            this._txtNombreMarca.Visibility = (System.Windows.Visibility)value;
            this._chkEtiqueta.Visibility = (System.Windows.Visibility)value;
            this._lblNoInscripcion.Visibility = (System.Windows.Visibility)value;
            this._txtNumInscripcion.Visibility = (System.Windows.Visibility)value;
            this._lblNoRegistro.Visibility = (System.Windows.Visibility)value;
            this._txtNumRegistro.Visibility = (System.Windows.Visibility)value;
            this._lblTipo.Visibility = (System.Windows.Visibility)value;
            this._txtTipo.Visibility = (System.Windows.Visibility)value;
            this._lblClaseNacional.Visibility = (System.Windows.Visibility)value;
            this._txtClaseNacional.Visibility = (System.Windows.Visibility)value;
            this._lblClaseInternacional.Visibility = (System.Windows.Visibility)value;
            this._txtClaseInternacional.Visibility = (System.Windows.Visibility)value;
            this._lblAsociado.Visibility = (System.Windows.Visibility)value;
            this._txtAsociado.Visibility = (System.Windows.Visibility)value;            
        }

        private void _txtMarcaFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarMarca.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }
    

    


    }
}
