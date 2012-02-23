﻿using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Traspasos.Fusiones
{
    /// <summary>
    /// Interaction logic for GestionarFusion.xaml
    /// </summary>
    public partial class GestionarFusion : Page, IGestionarFusion
    {

        private PresentadorGestionarFusion _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarFusion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Fusion
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

        public string IdAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdInteresadoEntreFiltrar
        {
            get { return this._txtIdInteresadoEntreFiltrar.Text; }
        }

        public string NombreInteresadoEntreFiltrar
        {
            get { return this._txtNombreInteresadoEntreFiltrar.Text; }
        }

        public string IdInteresadoSobrevivienteFiltrar
        {
            get { return this._txtIdInteresadoSobrevivienteFiltrar.Text; }
        }

        public string NombreInteresadoSobrevivienteFiltrar
        {
            get { return this._txtNombreInteresadoSobrevivienteFiltrar.Text; }
        }

        public string IdAgenteFiltrar
        {
            get { return this._txtIdApoderadoFiltrar.Text; }
        }

        public string NombreAgenteFiltrar
        {
            get { return this._txtNombreApoderadoFiltrar.Text; }
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

        public object InteresadoEntre
        {
            get { return this._gridDatosInteresadoEntre.DataContext; }
            set { this._gridDatosInteresadoEntre.DataContext = value; }
        }

        public string NombreInteresadoEntre
        {
            set { this._txtNombreInteresadoEntre.Text = value; }
        }

        public object InteresadoSobreviviente
        {
            get { return this._gridDatosInteresadoSobreviviente.DataContext; }
            set { this._gridDatosInteresadoSobreviviente.DataContext = value; }
        }

        public string NombreInteresadoSobreviviente
        {
            set { this._txtNombreInteresadoSobreviviente.Text = value; }
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
                this._btnConsultarMarca.IsEnabled = value;
                this._dpkFechaBoletin.IsEnabled = value;
                this._chkEtiqueta.IsEnabled = value;

                _btnConsultarInteresadoEntre.IsEnabled = value;
                _txtNombreInteresadoEntre.IsEnabled = value;
                _txtNombreInteresadoEntreFiltrar.IsEnabled = value;
                _txtIdInteresadoEntreFiltrar.IsEnabled = value;
                _txtPaisInteresadoEntre.IsEnabled = value;
                _txtCiudadInteresadoEntre.IsEnabled = value;

                _btnConsultarInteresadoSobreviviente.IsEnabled = value;
                _txtNombreInteresadoSobreviviente.IsEnabled = value;
                _txtNombreInteresadoSobrevivienteFiltrar.IsEnabled = value;
                _txtIdInteresadoSobrevivienteFiltrar.IsEnabled = value;
                _txtPaisInteresadoSobreviviente.IsEnabled = value;
                _txtCiudadInteresadoSobreviviente.IsEnabled = value;

                _btnConsultarApoderado.IsEnabled = value;
                _txtNombreApoderado.IsEnabled = value;
                _txtNombreApoderadoFiltrar.IsEnabled = value;
                _txtIdApoderadoFiltrar.IsEnabled = value;

                _lblIdPoder.IsEnabled = value;
                _lblFomento.IsEnabled = value;
                _lblAnexoPoder.IsEnabled = value;
                _lblBoletinPoder.IsEnabled = value;
                _lblFechaPoder.IsEnabled = value;
                _lblFacultadPoder.IsEnabled = value;
                _txtIdPoder.IsEnabled = value;
                _txtFacultadPoder.IsEnabled = value;
                _txtAnexoPoder.IsEnabled = value;
                _txtBoletinPoder.IsEnabled = value;
                _txtFacultadPoder.IsEnabled = value;
                _txtFechaPoder.IsEnabled = value;                

                _btnConsultarPoder.IsEnabled = value;                
                _lblPoderFiltrar.IsEnabled = value;
                _lblIdPoderFiltrar.IsEnabled = value;
                _lblIdPoderFiltrar.IsEnabled = value;
                _txtIdPoderFiltrar.IsEnabled = value;                                
                _dpkFechaPoderFiltrar.IsEnabled = value;

            }
        }

        public string NombreMarca
        {
            set { this._txtNombreMarca.Text = value; }
        }

        public object MarcasFiltradas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object MarcaFiltrada
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }

        public object InteresadosEntreFiltrados
        {
            get { return this._lstInteresadosEntre.DataContext; }
            set { this._lstInteresadosEntre.DataContext = value; }
        }

        public object InteresadoEntreFiltrado
        {
            get { return this._lstInteresadosEntre.SelectedItem; }
            set { this._lstInteresadosEntre.SelectedItem = value; }
        }

        public object InteresadosSobrevivienteFiltrados
        {
            get { return this._lstInteresadosSobreviviente.DataContext; }
            set { this._lstInteresadosSobreviviente.DataContext = value; }
        }

        public object InteresadoSobrevivienteFiltrado
        {
            get { return this._lstInteresadosSobreviviente.SelectedItem; }
            set { this._lstInteresadosSobreviviente.SelectedItem = value; }
        }

        public object AgenteApoderado
        {
            get { return this._gridDatosApoderado.DataContext; }
            set { this._gridDatosApoderado.DataContext = value; }
        }

        public string NombreAgenteApoderado
        {
            set { this._txtNombreApoderado.Text = value; }
        }

        public string IdAgenteApoderadoFiltrar
        {
            get { return this._txtIdApoderadoFiltrar.Text; }
        }

        public string NombreAgenteApoderadoFiltrar
        {
            get { return this._txtNombreApoderadoFiltrar.Text; }
        }

        public object AgenteApoderadoFiltrados
        {
            get { return this._lstApoderados.DataContext; }
            set { this._lstApoderados.DataContext = value; }
        }

        public object AgenteApoderadoFiltrado
        {
            get { return this._lstApoderados.SelectedItem; }
            set { this._lstApoderados.SelectedItem = value; }
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

        public object Poder
        {
            get { return this._gridDatosPoder.DataContext; }
            set { this._gridDatosPoder.DataContext = value; }
        }

        public string IdPoder
        {
            get { return this._txtIdPoder.Text; }
            set { this._txtIdPoder.Text = value; }
        }

        public string IdPoderFiltrar
        {
            get { return this._txtIdPoderFiltrar.Text; }
        }

        public string FechaPoderFiltrar
        {
            get { return this._dpkFechaPoderFiltrar.Text; }
        }

        public object PoderesFiltrados
        {
            get { return this._lstPoderes.DataContext; }
            set { this._lstPoderes.DataContext = value; }
        }

        public object PoderFiltrado
        {
            get { return this._lstPoderes.SelectedItem; }
            set { this._lstPoderes.SelectedItem = value; }
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #endregion

        public GestionarFusion(object fusion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFusion(this, fusion);
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

        public void ConvertirEnteroMinimoABlanco()
    {           
            if (!this.IdPoder.Equals(""))
            {
                if (int.Parse(this.IdPoder) == int.MinValue)
                    this.IdPoder = "";
            }                       
        }

        #region Eventos Marcas

        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMarcas();
        }

        private void _lstMarcas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarMarca())
            {
                GestionarVisibilidadDatosDeMarca(Visibility.Visible);
                GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

                this._btnConsultarMarca.IsDefault = false;
                this._btnModificar.IsDefault = true;
            }
        }

        private void _OrdenarMarcas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstMarcas);
        }

        private void _txtNombreMarca_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeMarca(Visibility.Collapsed);
            GestionarVisibilidadFiltroMarca(Visibility.Visible);

            //escondo el filtro de interesado Entre
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

            //escondo el filtro de interesado Sobreviviente
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

            //escondo el filtro de Agente
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            this._btnConsultarMarca.IsDefault = false;
            this._btnModificar.IsDefault = true;
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

        #endregion

        #region Eventos Interesado Entre

        private void _btnConsultarInteresadoEntre_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadosEntre();
        }

        private void _lstInteresadosEntre_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresadoEntre())
            {
                GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
                GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

                this._btnConsultarInteresadoEntre.IsDefault = false;
                this._btnModificar.IsDefault = true;
            }
        }

        private void _OrdenarInteresadosEntre_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosEntre);
        }

        private void _txtInteresadoEntre_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Collapsed);

            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Visible);

            //escondo el filtro de Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            //escondo el filtro de AgenteApoderado
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //escondo el filtro de InteresadoSobreviviente
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

            this._btnConsultarInteresadoEntre.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresadoEntre(object value)
        {
            this._lblIdInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoEntreFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoEntreFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresadosEntre.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresadoEntre.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresadoEntre(object value)
        {                        
            this._txtNombreInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresadoEntre.Visibility = (System.Windows.Visibility)value;
            this._txtDomicilioMarcaTercero.Visibility = (System.Windows.Visibility)value;
        }

        #endregion

        #region Eventos Interesado Sobreviviente

        private void _btnConsultarInteresadoSobreviviente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadosSobreviviente();
        }

        private void _lstInteresadosSobreviviente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarInteresadoSobreviviente())
            {
                GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
                GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

                if (this._presentador.VerificarCambioInteresado())
                {
                    this._btnConsultarPoder.IsEnabled = false;
                    this._btnModificar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarPoder.IsEnabled = true;
                }
            }
        }

        private void _OrdenarInteresadosSobreviviente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresadosSobreviviente);
        }

        private void _txtInteresadoSobreviviente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Collapsed);

            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Visible);

            //escondo el filtro de interesado Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            //escondo el filtro de interesado Entre
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

            //escondo el filtro de Agente
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //escondo el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            this._btnConsultarInteresadoSobreviviente.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroInteresadoSobreviviente(object value)
        {
            this._lblIdInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._lblNombreInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._txtNombreInteresadoSobrevivienteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdInteresadoSobrevivienteFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstInteresadosSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
        }

        private void GestionarVisibilidadDatosDeInteresadoSobreviviente(object value)
        {
            this._txtNombreInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._txtPaisInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
            this._txtCiudadInteresadoSobreviviente.Visibility = (System.Windows.Visibility)value;
        }

        public void GestionarBotonConsultarInteresado(bool value)
        {                          
            this._btnConsultarInteresadoSobreviviente.IsEnabled = value;           
        }


        #endregion

        #region Eventos Agente Apoderado

        private void _btnConsultarApoderado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarApoderados();
        }       

        private void _lstApoderados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {          
            if (this._presentador.CambiarApoderado())
            {
                GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
                GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

                if (this._presentador.VerificarCambioAgente())
                {
                    this._btnConsultarPoder.IsEnabled = false;
                    this._btnModificar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarPoder.IsEnabled = true;
                }
            }
        }

        private void _OrdenarApoderados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstApoderados);
        }

        private void _txtApoderadoFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarApoderado.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtNombreApoderado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Collapsed);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Visible);

            //escondo el filtro de interesado Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            //escondo el filtro de interesado Entre
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

            //escondo el filtro de interesado Sobreviviente
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

            //escondo el filtro de poder
            GestionarVisibilidadDatosDePoder(Visibility.Visible);
            GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

            this._btnConsultarApoderado.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void GestionarVisibilidadFiltroAgenteApoderado(object value)
        {
            this._lblIdApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblNombreApoderadoFiltrar.Visibility = (System.Windows.Visibility)value; 
            this._txtNombreApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdApoderadoFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstApoderados.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarApoderado.Visibility = (System.Windows.Visibility)value;
            
        }

        private void GestionarVisibilidadDatosDeAgenteApoderado(object value)
        {
            this._txtNombreApoderado.Visibility = (System.Windows.Visibility)value;
        }

        public void GestionarBotonConsultarApoderado(bool value)
        {                           
            this._btnConsultarApoderado.IsEnabled = value;           
        }

        #endregion

        #region Poderes

        private void _btnConsultar(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name.Equals("_btnConsultarMarca"))
                this._presentador.ConsultarMarcas();
            else if (((Button)sender).Name.Equals("_btnConsultarInteresadoEntre"))
                this._presentador.ConsultarInteresadosEntre();
            else if (((Button)sender).Name.Equals("_btnConsultarInteresadoSobreviviente"))
                this._presentador.ConsultarInteresadosSobreviviente();
            else if (((Button)sender).Name.Equals("_btnConsultarApoderado"))
                this._presentador.ConsultarApoderados();
            else if (((Button)sender).Name.Equals("_btnConsultarPoder"))            
                this._presentador.ConsultarPoderes();           
        }

        private void _txtPoderFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarPoder.IsDefault = true;
            this._btnModificar.IsDefault = false;
        }

        private void _txtIdPoder_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            //escondo el filtro de Marca
            GestionarVisibilidadDatosDeMarca(Visibility.Visible);
            GestionarVisibilidadFiltroMarca(Visibility.Collapsed);

            //escondo el filtro de Interesado Entre
            GestionarVisibilidadDatosDeInteresadoEntre(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoEntre(Visibility.Collapsed);

            //escondo el filtro de Interesado Sobreviviente
            GestionarVisibilidadDatosDeInteresadoSobreviviente(Visibility.Visible);
            GestionarVisibilidadFiltroInteresadoSobreviviente(Visibility.Collapsed);

            //escondo el filtro de Agente Apoderado
            GestionarVisibilidadDatosDeAgenteApoderado(Visibility.Visible);
            GestionarVisibilidadFiltroAgenteApoderado(Visibility.Collapsed);

            //Muestro el filtro de Poder
            GestionarVisibilidadDatosDePoder(Visibility.Collapsed);            
            GestionarVisibilidadFiltroPoder(Visibility.Visible);            
           
        }

        private void _OrdenarPoderes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPoderes);
        }

        private void _lstPoderes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarPoder())
            {
                GestionarVisibilidadDatosDePoder(Visibility.Visible);
                GestionarVisibilidadFiltroPoder(Visibility.Collapsed);

                if (this._presentador.VerificarCambioPoder())
                {
                    this._btnConsultarApoderado.IsEnabled = false;
                    this._btnConsultarInteresadoSobreviviente.IsEnabled = false;
                    this._btnModificar.IsDefault = true;
                }
                else
                {
                    this._btnConsultarApoderado.IsEnabled = true;
                    this._btnConsultarInteresadoSobreviviente.IsEnabled = true;
                }
            }
        }       

        public void GestionarBotonConsultarPoder(bool value)
        {                
            this._btnConsultarPoder.IsEnabled = value;          
        }

        private void GestionarVisibilidadFiltroPoder(object value)
        {
            this._lblPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._dpkFechaPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lblIdPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoderFiltrar.Visibility = (System.Windows.Visibility)value;
            this._lstPoderes.Visibility = (System.Windows.Visibility)value;
            this._btnConsultarPoder.Visibility = (System.Windows.Visibility)value;

        }

        private void GestionarVisibilidadDatosDePoder(object value)
        {
            this._lblIdPoder.Visibility = (System.Windows.Visibility)value;
            this._txtIdPoder.Visibility = (System.Windows.Visibility)value;
            this._lblFomento.Visibility = (System.Windows.Visibility)value;
            this._lblFechaPoder.Visibility = (System.Windows.Visibility)value;
            this._lblBoletinPoder.Visibility = (System.Windows.Visibility)value;
            this._lblAnexoPoder.Visibility = (System.Windows.Visibility)value;
            this._lblFacultadPoder.Visibility = (System.Windows.Visibility)value;
            this._txtNumPoder.Visibility = (System.Windows.Visibility)value;
            this._txtFechaPoder.Visibility = (System.Windows.Visibility)value;
            this._txtBoletinPoder.Visibility = (System.Windows.Visibility)value;
            this._txtAnexoPoder.Visibility = (System.Windows.Visibility)value;
            this._txtFacultadPoder.Visibility = (System.Windows.Visibility)value;
        }

        #endregion
    }
}
