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
                this._txtUbicacion.IsEnabled = value;                                
                this._btnConsultarMarca.IsEnabled = value;
                
                this._txtNombreCedente.IsEnabled = value;
                this._txtPaisCedente.IsEnabled = value;
                this._txtNacionalidadCedente.IsEnabled = value;
                this._txtIdCedenteFiltrar.IsEnabled = value;
                this._txtNombreCedenteFiltrar.IsEnabled = value;
                this._txtNombreApoderadoCedente.IsEnabled = value;
                this._txtNombreApoderadoCedenteFiltrar.IsEnabled = value;
                this._txtIdApoderadoCedenteFiltrar.IsEnabled = value;                                
                this._txtNombrePoderCedente.IsEnabled = value;
                this._txtNombrePoderCedenteFiltrar.IsEnabled = value;
                this._txtIdPoderCedenteFiltrar.IsEnabled = value;
                this._txtAnexoCedente.IsEnabled = value;
                this._txtBoletinCedente.IsEnabled = value;
                this._txtFacultadCedente.IsEnabled = value;
                this._txtNumPoderCedente.IsEnabled = value;
                this._txtFechaPoderCedente.IsEnabled = value;

                this._txtNombreCesionario.IsEnabled = value;
                this._txtIdCesionarioFiltrar.IsEnabled = value;
                this._txtNombreCesionarioFiltrar.IsEnabled = value;
                this._txtNombreApoderadoCesionario.IsEnabled = value;
                this._txtIdApoderadoCesionarioFiltrar.IsEnabled = value;
                this._txtNombreApoderadoCesionarioFiltrar.IsEnabled = value;                               
                this._txtNombrePoderCesionario.IsEnabled = value;
                this._txtIdPoderCesionarioFiltrar.IsEnabled = value;
                this._txtNombrePoderCesionarioFiltrar.IsEnabled = value;          
                this._txtAnexoCesionario.IsEnabled = value;               
                this._txtBoletinCesionario.IsEnabled = value;               
                this._txtFacultadCesionario.IsEnabled = value;                
                this._txtNumPoderCesionario.IsEnabled = value;
                this._txtFechaPoderCesionario.IsEnabled = value;  
                this._txtPaisCesionario.IsEnabled = value;
                this._txtNacionalidadCesionario.IsEnabled = value;

                this._txtObservacionCesion.IsEnabled = value;
                this._txtOtrosCesion.IsEnabled = value;
                this._txtReferenciaCesion.IsEnabled = value;                                
                this._txtBoletinCesion.IsEnabled = value;
                this._txtAnexoCesion.IsEnabled = value;                              
                this._txtComentarioCesion.IsEnabled = value;                                                                               
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

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {           
            if (((Button)sender).Name.Equals("_btnConsultarMarca"))
                this._presentador.ConsultarMarcas();
            else if (((Button)sender).Name.Equals("_btnConsultarCedente"))
                this._presentador.ConsultarCedentes();  
            else if (((Button)sender).Name.Equals("_btnConsultarApoderadoCedente"))
                this._presentador.ConsultarApoderadosCedente();  
            else if (((Button)sender).Name.Equals("_btnConsultarPoderCedente"))
                this._presentador.ConsultarPoderesCedente();
            else if (((Button)sender).Name.Equals("_btnConsultarCesionario"))
                this._presentador.ConsultarCesionarios();
            else if (((Button)sender).Name.Equals("_btnConsultarApoderadoCesionario"))
                this._presentador.ConsultarApoderadosCesionario();
            else if (((Button)sender).Name.Equals("_btnConsultarPoderCesionario"))
                this._presentador.ConsultarPoderesCedente();            
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
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstCesionarios);
        }
        
        private void _OrdenarApoderadosCedente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstApoderadosCedente);
        }

        private void _OrdenarApoderadosCesionario_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstApoderadosCesionario);
        }
        
        private void _OrdenarPoderesCedente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesCedente);
        }

        private void _OrdenarPoderesCesionario_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderesCesionario);
        }
        
        private void _txtNombreMarca_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeMarca(Visibility.Collapsed);                      

            GestionarVisibilidadFiltroCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCesionario(Visibility.Visible);
            
            GestionarVisibilidadDatosDePoderCedente(Visibility.Visible);                        
        }

        private void _txtNombreCedente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeCedente(Visibility.Collapsed);
                    
            GestionarVisibilidadFiltroCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCesionario(Visibility.Visible);
            
            GestionarVisibilidadDatosDePoderCedente(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCesionario(Visibility.Visible);
        }

        private void _txtNombreCesionario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCedente(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCesionario(Visibility.Visible);
        }

        private void _txtNombreApoderadoCedente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeApoderadoCedente(Visibility.Collapsed);                 

            GestionarVisibilidadFiltroCesionario(Visibility.Collapsed);            

            GestionarVisibilidadFiltroApoderadoCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeCesionario(Visibility.Visible);            

            GestionarVisibilidadDatosDeApoderadoCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCedente(Visibility.Visible);
        }              

        private void _txtNombreApoderadoCesionario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {           
            GestionarVisibilidadDatosDeApoderadoCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroCesionario(Visibility.Collapsed);            
            
            GestionarVisibilidadFiltroCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCesionario(Visibility.Collapsed);                        

            GestionarVisibilidadFiltroApoderadoCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeCesionario(Visibility.Visible);
            
            GestionarVisibilidadDatosDeApoderadoCedente(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCedente(Visibility.Visible);
        }

        private void _txtNombrePoderCedente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {            
            GestionarVisibilidadDatosDePoderCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCesionario(Visibility.Visible);            

            GestionarVisibilidadDatosDePoderCesionario(Visibility.Visible);            
        }


        private void _txtNombrePoderCesionario_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDePoderCesionario(Visibility.Collapsed);            
                       
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            GestionarVisibilidadFiltroCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCedente(Visibility.Collapsed);

            GestionarVisibilidadFiltroCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroApoderadoCesionario(Visibility.Collapsed);

            GestionarVisibilidadFiltroPoderCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDeMarca(Visibility.Visible);

            GestionarVisibilidadDatosDeCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCedente(Visibility.Visible);

            GestionarVisibilidadDatosDeApoderadoCesionario(Visibility.Visible);

            GestionarVisibilidadDatosDePoderCedente(Visibility.Visible);
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

        private void GestionarVisibilidadDatosDeCedente(object value)
        {
            this._lblNombreCedente.Visibility = (System.Windows.Visibility)value;
            this._txtNombreCedente.Visibility = (System.Windows.Visibility)value;
            this._txtPaisCedente.Visibility = (System.Windows.Visibility)value;
            this._txtNacionalidadCedente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeCesionario(object value)
        {
            this._lblNombreCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtNombreCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtPaisCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtNacionalidadCesionario.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeApoderadoCedente(object value)
        {
            this._lblNombreApoderadoCedente.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoCedente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeApoderadoCesionario(object value)
        {
            this._lblNombreApoderadoCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoCesionario.Visibility = (System.Windows.Visibility)value;
        }  

        private void GestionarVisibilidadDatosDePoderCedente(object value)
        {
            this._lblNombrePoderCedente.Visibility = (System.Windows.Visibility)value;
            this._txtNombrePoderCedente.Visibility = (System.Windows.Visibility)value;
            this._lblFomentoCedente.Visibility = (System.Windows.Visibility)value;
            this._lblFechaPoderCedente.Visibility = (System.Windows.Visibility)value;
            this._lblBoletinCedente.Visibility = (System.Windows.Visibility)value;
            this._lblAnexoCedente.Visibility = (System.Windows.Visibility)value;
            this._lblFacultadCedente.Visibility = (System.Windows.Visibility)value;
            this._txtNumPoderCedente.Visibility = (System.Windows.Visibility)value;
            this._txtFechaPoderCedente.Visibility = (System.Windows.Visibility)value;
            this._txtBoletinCedente.Visibility = (System.Windows.Visibility)value;
            this._txtAnexoCedente.Visibility = (System.Windows.Visibility)value;
            this._txtFacultadCedente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDePoderCesionario(object value)
        {
            this._lblNombrePoderCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtNombrePoderCesionario.Visibility = (System.Windows.Visibility)value;
            this._lblFomentoCesionario.Visibility = (System.Windows.Visibility)value;
            this._lblFechaPoderCesionario.Visibility = (System.Windows.Visibility)value;
            this._lblBoletinCesionario.Visibility = (System.Windows.Visibility)value;
            this._lblAnexoCesionario.Visibility = (System.Windows.Visibility)value;
            this._lblFacultadCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtNumPoderCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtFechaPoderCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtBoletinCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtAnexoCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtFacultadCesionario.Visibility = (System.Windows.Visibility)value;
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

        private void GestionarVisibilidadFiltroCedente(object value)
        {
            this._lblCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstCedentes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarCedente.Visibility = (System.Windows.Visibility)value;
     
        }

        private void GestionarVisibilidadFiltroCesionario(object value)
        {
            this._lblCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdCesionario.Visibility = (System.Windows.Visibility)value;
            this._txtIdCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstCesionarios.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarCesionario.Visibility = (System.Windows.Visibility)value;
        }  

        private void GestionarVisibilidadFiltroApoderadoCedente(object value)
        {
            this._lblApoderadoCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreApoderadoCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdApoderadoCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstApoderadosCedente.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarApoderadoCedente.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadFiltroApoderadoCesionario(object value)
        {
            this._lblApoderadoCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreApoderadoCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombreApoderadoCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdApoderadoCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstApoderadosCesionario.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarApoderadoCesionario.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadFiltroPoderCedente(object value)
        {
            this._lblPoderCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombrePoderCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombrePoderCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPoderCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderCedenteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPoderesCedente.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPoderCedente.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadFiltroPoderCesionario(object value)
        {
            this._lblPoderCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombrePoderCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtNombrePoderCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPoderCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderCesionarioFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPoderesCesionario.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPoderCesionario.Visibility = (System.Windows.Visibility)value;

        }

        private void _txtMarcaFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarMarca.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtCedenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarCedente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtApoderadoCedenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarApoderadoCedente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtPoderCedenteFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPoderCedente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtCesionarioFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarCesionario.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtApoderadoCesionarioFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarApoderadoCesionario.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtPoderCesionarioFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPoderCesionario.IsDefault = true;
            this._btnModificar.IsDefault = false;
        } 
    }
}
