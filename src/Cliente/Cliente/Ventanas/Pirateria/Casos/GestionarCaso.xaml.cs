using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Pirateria.Casos;
using Trascend.Bolet.Cliente.Presentadores.Pirateria.Casos;

namespace Trascend.Bolet.Cliente.Ventanas.Pirateria.Casos
{
    /// <summary>
    /// Lógica de interacción para GestionarCaso.xaml
    /// </summary>
    public partial class GestionarCaso : Page, IGestionarCaso
    {
        private bool _cargada;
        private bool _asociadosCargados;
        private bool _interesadosCargados;
        private PresentadorGestionarCaso _presentador;
        
        #region IGestionarCaso

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object Caso
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string IdCaso
        {
            get { return this._txtIdCaso.Text; }
            set { this._txtIdCaso.Text = value; }
        }

        public string FechaCaso
        {
            get { return this._dpkFechaCaso.Text; }
            set { this._dpkFechaCaso.Text = value; }
        }

        public object OrigenesCaso
        {
            get { return this._cbxOrigenCaso.DataContext; }
            set { this._cbxOrigenCaso.DataContext = value; }
        }

        public object OrigenCaso
        {
            get { return this._cbxOrigenCaso.SelectedItem; }
            set { this._cbxOrigenCaso.SelectedItem = value; }
        }

        public string DescripcionCaso
        {
            get { return this._txtDescripcionCaso.Text; }
            set { this._txtDescripcionCaso.Text = value; }
        }

        public string IdAsociadoCaso
        {
            get { return this._txtIdAsociadoCaso.Text; }
            set { this._txtIdAsociadoCaso.Text = value; }
        }

        public string AsociadoCaso
        {
            get { return this._txtAsociadoCaso.Text; }
            set { this._txtAsociadoCaso.Text = value; }
        }

        public string IdAsociadoConsultar
        {
            get { return this._txtIdAsociadoBuscar.Text; }
            set { this._txtIdAsociadoBuscar.Text = value; }
        }

        public string AsociadoConsultar
        {
            get { return this._txtNombreAsociadoBuscar.Text; }
            set { this._txtNombreAsociadoBuscar.Text = value; }
        }

        public object AsociadosConsultados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public object AsociadoSeleccionado
        {
            get { return this._lstAsociados.SelectedItem; }
            set { this._lstAsociados.SelectedItem = value; }
        }

        public bool AsociadosCargados
        {
            get { return this._asociadosCargados; }
            set { this._asociadosCargados = value; }
        }

        public string IdInteresadoCaso
        {
            get { return this._txtIdInteresado.Text; }
            set { this._txtIdInteresado.Text = value; }
        }

        public string InteresadoCaso
        {
            get { return this._txtInteresado.Text; }
            set { this._txtInteresado.Text = value; }
        }

        public string IdInteresadoConsultar
        {
            get { return this._txtIdInteresadoBuscar.Text; }
            set { this._txtIdInteresadoBuscar.Text = value; }
        }

        public string InteresadoConsultar
        {
            get { return this._txtNombreInteresadoBuscar.Text; }
            set { this._txtNombreInteresadoBuscar.Text = value; }
        }

        public object InteresadosConsultados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object InteresadoSeleccionado
        {
            get { return this._lstInteresados.SelectedItem; }
            set { this._lstInteresados.SelectedItem = value; }
        }

        public string InteresadoCiudad
        {
            get { return this._txtInteresadoCiudad.Text; }
            set { this._txtInteresadoCiudad.Text = value; }
        }

        public bool InteresadosCargados
        {
            get { return this._interesadosCargados; }
            set { this._interesadosCargados = value; }
        }

        public string PrimeraReferencia
        {
            get { return this._txtReferenciaCaso.Text; }
            set { this._txtReferenciaCaso.Text = value; }
        }

        public string ComentariosCaso
        {
            get { return this._txtComentarioCaso.Text; }
            set { this._txtComentarioCaso.Text = value; }
        }

        public object SituacionesCaso
        {
            get { return this._cbxSituacionCaso.DataContext; }
            set { this._cbxSituacionCaso.DataContext = value; }
        }

        public object SituacionCaso
        {
            get { return this._cbxSituacionCaso.SelectedItem; }
            set { this._cbxSituacionCaso.SelectedItem = value; }
        }

        public string SituacionDescripcion
        {
            get { return this._txtSituacionCaso.Text; }
            set { this._txtSituacionCaso.Text = value; }
        }

        public object TiposDeCaso
        {
            get { return this._cbxTiposCaso.DataContext; }
            set { this._cbxTiposCaso.DataContext = value; }
        }

        public object TipoDeCaso
        {
            get { return this._cbxTiposCaso.SelectedItem; }
            set { this._cbxTiposCaso.SelectedItem = value; }
        }

        public object ListaTiposCaso
        {
            get { return this._lstTiposCasos.DataContext; }
            set { this._lstTiposCasos.DataContext = value; }
        }

        public object ListaTipoCaso
        {
            get { return this._lstTiposCasos.SelectedItem; }
            set { this._lstTiposCasos.SelectedItem = value; }
        }

        public object AccionesCaso
        {
            get { return this._cbxAccionesCaso.DataContext; }
            set { this._cbxAccionesCaso.DataContext = value; }
        }

        public object AccionCaso
        {
            get { return this._cbxAccionesCaso.SelectedItem; }
            set { this._cbxAccionesCaso.SelectedItem = value; }
        }

        public object ListaAccionesCaso
        {
            get { return this._lstAcciones.DataContext; }
            set { this._lstAcciones.DataContext = value; }
        }

        public object ListaAccionCaso
        {
            get { return this._lstAcciones.SelectedItem; }
            set { this._lstAcciones.SelectedItem = value; }
        }

        public object TiposBase
        {
            get { return this._cbxTiposBase.DataContext; }
            set { this._cbxTiposBase.DataContext = value; }
        }

        public object TipoBase
        {
            get { return this._cbxTiposBase.SelectedItem; }
            set { this._cbxTiposBase.SelectedItem = value; }
        }

        public string TextoBotonModificar
        {
            get { return this._txbAceptar.Text; }
            set { this._txbAceptar.Text = value; }
        }

        
        public bool HabilitarCampos
        {
            set
            {
                this._txtIdCaso.IsEnabled = value;
                this._dpkFechaCaso.IsEnabled = value;
                this._cbxOrigenCaso.IsEnabled = value;
                this._txtDescripcionCaso.IsEnabled = value;
                this._btnIrAsociados.IsEnabled = value;
                this._txtIdAsociadoCaso.IsEnabled = value;
                this._txtAsociadoCaso.IsEnabled = value;
                this._txtIdAsociadoBuscar.IsEnabled = value;
                this._txtNombreAsociadoBuscar.IsEnabled = value;
                this._btnConsultarAsociado.IsEnabled = value;
                this._btnIrInteresados.IsEnabled = value;
                this._txtIdInteresado.IsEnabled = value;
                this._txtInteresado.IsEnabled = value;
                this._txtIdInteresadoBuscar.IsEnabled = value;
                this._txtNombreInteresadoBuscar.IsEnabled = value;
                this._btnConsultarInteresado.IsEnabled = value;
                this._txtInteresadoCiudad.IsEnabled = value;
                this._txtReferenciaCaso.IsEnabled = value;
                this._txtComentarioCaso.IsEnabled = value;
                this._cbxSituacionCaso.IsEnabled = value;
                this._txtSituacionCaso.IsEnabled = value;
                this._cbxTiposCaso.IsEnabled = value;
                this._btnAgregarTipoCaso.IsEnabled = value;
                this._btnQuitarTipoCaso.IsEnabled = value;
                this._lstTiposCasos.IsEnabled = value;
                this._cbxAccionesCaso.IsEnabled = value;
                this._btnAgregarAccion.IsEnabled = value;
                this._btnQuitarAccion.IsEnabled = value;
                this._lstAcciones.IsEnabled = value;
                
                this._chkByt.IsEnabled = value;
                this._cbxTiposBase.IsEnabled = value;
                this._txtCodigoCasoBase.IsEnabled = value;
                this._txtITipoCasoBase.IsEnabled = value;
                this._txtClaseInternacionalCasoBase.IsEnabled = value;
                this._txtClaseNacionalCasoBase.IsEnabled = value;
                this._btnAgregarCasoBase.IsEnabled = value;
                this._btnQuitarCasoBase.IsEnabled = value;
                this._txtIdMarcaPatenteFiltrar.IsEnabled = value;
                this._txtNombreMarcaPatenteFiltrar.IsEnabled = value;
                this._btnConsultarMarcaPatente.IsEnabled = value;
                this._lstCasosBases.IsEnabled = value;

            }
        }

        public bool? ByT
        {
            get { return this._chkByt.IsChecked; }
        }

        public string IdCasoBase
        {
            get { return this._txtCodigoCasoBase.Text; }
            set { this._txtCodigoCasoBase.Text = value; }
        }

        public string ITipoCasoBase
        {
            get { return this._txtITipoCasoBase.Text; }
            set { this._txtITipoCasoBase.Text = value; }
        }

        public string ClaseInternacionalCasoBase
        {
            get { return this._txtClaseInternacionalCasoBase.Text; }
            set { this._txtClaseInternacionalCasoBase.Text = value; }
        }

        public string ClaseNacionalCasoBase
        {
            get { return this._txtClaseNacionalCasoBase.Text; }
            set { this._txtClaseNacionalCasoBase.Text = value; }
        }

        public object CasosBases
        {
            get { return this._lstCasosBases.DataContext; }
            set { this._lstCasosBases.DataContext = value; }
        }

        public object CasoBaseSeleccionado
        {
            get { return this._lstCasosBases.SelectedItem; }
            set { this._lstCasosBases.SelectedItem = value; }
        }

        //Campos para las busquedas de Casos Bases

        public string IdMarcaPatenteFiltrar
        {
            get { return this._txtIdMarcaPatenteFiltrar.Text; }
            set { this._txtIdMarcaPatenteFiltrar.Text = value; }
        }

        public string NombreMarcaPatenteFiltrar
        {
            get { return this._txtNombreMarcaPatenteFiltrar.Text; }
            set { this._txtNombreMarcaPatenteFiltrar.Text = value; }
        }

        public object ListaMarcasPatentes
        {
            get { return this._lstMarcasPatentes.DataContext; }
            set { this._lstMarcasPatentes.DataContext = value; }
        }

        public object ListaMarcaOPatente
        {
            get { return this._lstMarcasPatentes.SelectedItem; }
            set { this._lstMarcasPatentes.SelectedItem = value; }
        }

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor predeterminado sin parametros
        /// </summary>
        public GestionarCaso()
        {
            InitializeComponent();
            this._cargada = false;
            this._asociadosCargados = false;
            this._interesadosCargados = false;
            this._presentador = new PresentadorGestionarCaso(this,null,null);
        }


        /// <summary>
        /// Constructor predeterminado que obtiene un caso y una ventana padre
        /// </summary>
        /// <param name="caso">Caso a visualizar</param>
        /// <param name="ventanaPadre">Ventana que antecede a esta ventana </param>
        public GestionarCaso(object caso, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCaso(this, caso,ventanaPadre);
        }

        #endregion


        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _cbxSituacionCaso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((_cbxSituacionCaso.SelectedIndex != 0) && (_cbxSituacionCaso.SelectedIndex != -1))
            {
                this._presentador.VisualizarNombreServicio();
            }
        }

        private void _btnIrAsociados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerAsociadoCaso();
        }

        private void _txtAsociado_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._asociadosCargados)
            {
                this._presentador.CargarAsociados();
            }
                        
            this._btnAceptar.IsDefault = false;
            this._btnConsultarAsociado.IsDefault = true;

            mostrarLstAsociado();

        }

        private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociado();
            ocultarLstAsociado();
            this._btnConsultarAsociado.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarAsociados();
        }

        private void _btnIrInteresados_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerInteresadoCaso();
        }

        private void _txtInteresado_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this._interesadosCargados)
            {
                this._presentador.CargarInteresados();
            }

            this._btnAceptar.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = true;

            mostrarLstInteresado();

        }

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresados();
        }

        private void _lstInteresados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarInteresado();
            ocultarLstInteresado();
            this._btnConsultarInteresado.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _btnAgregarTipoCaso_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarTipoCaso();
        }

        private void _btnQuitarTipoCaso_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.QuitarTipoCaso();
        }

        private void _btnAgregarAccion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarAccionCaso();
        }

        private void _btnQuitarAccion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.QuitarAccionCaso();
        }

        private void _btnAgregarCasoBase_Click(object sender, RoutedEventArgs e)
        {
            if (!this._txtCodigoCasoBase.Text.Equals(String.Empty))
            {
                this._presentador.AgregarCasoBase();
                mostrarListaCasosBase(); 
            }
        }

        

        private void _btnQuitarCasoBase_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.QuitarCasoBase();
        }

        private void _chkByt_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)!this._chkByt.IsChecked)
            {
                this._txtClaseInternacionalCasoBase.IsEnabled = true;
                this._txtClaseNacionalCasoBase.IsEnabled = true;
                this._txtITipoCasoBase.IsEnabled = true;
            }
            else
            {
                this._txtClaseInternacionalCasoBase.IsEnabled = false;
                this._txtClaseNacionalCasoBase.IsEnabled = false;
                this._txtITipoCasoBase.IsEnabled = false;
            }
        }

        private void _txtCodigoTipoBase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if((bool)this._chkByt.IsChecked)
            {
                if (this._presentador.ValidarCasoBaseMarcaPatente())
                {
                    if (this._lstCasosBases.IsVisible)
                        ocultarListaCasosBase();
                    mostrarCamposBusquedaMarcaOPatente();
                    this._btnConsultarMarcaPatente.IsDefault = true;
                    this._btnAceptar.IsDefault = false; 
                }
            }
        }
        
        private void _btnConsultarMarcaPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ObtenerCasoBaseMarcaOPatente();
        }

        private void _lstMarcasPatentes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CapturarDatosCasoBaseSeleccionado();
            ocultarCamposBusquedaMarcaOPatente();
            this._btnConsultarMarcaPatente.IsDefault = false;
            this._btnAceptar.IsDefault = true;
        }

        private void _btnInfoAdicionalTerceros_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerInfoAdicionalTerceros();
        }

        private void _btnDocumentos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerDocumentos();
        }

        private void _btnExpediente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerExpedienteCaso();
        }

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerAuditoria();
        }

        
        #endregion

        
        
        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if(opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if(opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void mostrarLstAsociado()
        {
            this._lstAsociados.ScrollIntoView(this.AsociadoSeleccionado);
            this._txtAsociadoCaso.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoCaso.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociados.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociados.IsEnabled = true;
            this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoBuscar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociadoBuscar.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoBuscar.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoBuscar.Visibility = System.Windows.Visibility.Visible;
        }


        private void mostrarLstInteresado()
        {
            this._lstInteresados.ScrollIntoView(this.InteresadoSeleccionado);
            this._txtInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados.IsEnabled = true;
            this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoBuscar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresadoBuscar.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoBuscar.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresadoBuscar.Visibility = System.Windows.Visibility.Visible;
        }


        private void ocultarLstAsociado()
        {
            this._txtAsociadoCaso.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoCaso.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociados.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociados.IsEnabled = false;
            this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdAsociadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ocultarLstInteresado()
        {
            this._txtInteresado.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresado.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados.IsEnabled = false;
            this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdInteresadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoBuscar.Visibility = System.Windows.Visibility.Collapsed;
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
                color = Brushes.DeepSkyBlue;
            }
            else if (tipo.Equals("3"))
            {
                color = Brushes.Red;
            }
            else if (tipo.Equals("4"))
            {
                color = Brushes.OrangeRed;
            }
            else color = Brushes.White;

            this._txtAsociadoCaso.Background = color;
            this._txtIdAsociadoCaso.Background = color;

        }

        public void ConvertirEnteroMinimoABlanco()
        {
            if (null != this.AsociadoSeleccionado)
            {
                if (!this.IdAsociadoCaso.Equals(""))
                {
                    if (int.Parse(this.IdAsociadoCaso) == int.MinValue)
                    {
                        this.IdAsociadoCaso = "";
                    }
                }
            }

            if (null != this.InteresadoSeleccionado)
            {
                if (!this.IdInteresadoCaso.Equals(""))
                {
                    if (int.Parse(this.IdInteresadoCaso) == int.MinValue)
                    {
                        this.IdInteresadoCaso = "";
                    }
                }
            }
        }

        private void mostrarCamposBusquedaMarcaOPatente()
        {
            this._cbxTiposBase.IsEnabled = false;
            this._lblIdCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._txtCodigoCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._lblTipoCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._txtITipoCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._lblClaseNacCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._txtClaseNacionalCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._lblClaseIntCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._txtClaseInternacionalCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._btnAgregarCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._btnQuitarCasoBase.Visibility = System.Windows.Visibility.Collapsed;
            this._lblIdMarcaPatente.Visibility = System.Windows.Visibility.Visible;
            this._txtIdMarcaPatenteFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreMarcaPatente.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreMarcaPatenteFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._lstMarcasPatentes.Visibility = System.Windows.Visibility.Visible;
            this._lstMarcasPatentes.IsEnabled = true;
            this._btnConsultarMarcaPatente.Visibility = System.Windows.Visibility.Visible;
        }

        private void ocultarCamposBusquedaMarcaOPatente()
        {
            this._cbxTiposBase.IsEnabled = true;
            this._lblIdCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._txtCodigoCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._lblTipoCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._txtITipoCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._lblClaseNacCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._txtClaseNacionalCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._lblClaseIntCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._txtClaseInternacionalCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._btnAgregarCasoBase.Visibility = System.Windows.Visibility.Visible;
            this._btnQuitarCasoBase.Visibility = System.Windows.Visibility.Visible;

            this._lblIdMarcaPatente.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdMarcaPatenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreMarcaPatente.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreMarcaPatenteFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lstMarcasPatentes.Visibility = System.Windows.Visibility.Collapsed;
            this._lstMarcasPatentes.IsEnabled = false;
            this._btnConsultarMarcaPatente.Visibility = System.Windows.Visibility.Collapsed;
        }


        public void mostrarListaCasosBase()
        {
            this._lstCasosBases.Visibility = System.Windows.Visibility.Visible;
            this._lstCasosBases.IsEnabled = true;
            this._cbxTiposBase.SelectedIndex = 0;
            this._txtITipoCasoBase.Text = String.Empty;
            this._txtCodigoCasoBase.Text = String.Empty;
            this._txtClaseInternacionalCasoBase.Text = String.Empty;
            this._txtClaseNacionalCasoBase.Text = String.Empty;
        }

        public void ocultarListaCasosBase()
        {
            this._lstCasosBases.Visibility = System.Windows.Visibility.Collapsed;
            this._lstCasosBases.IsEnabled = false;
        }

        public void ActivarBotones(bool flag)
        {
            this._btnInfoAdicionalTerceros.IsEnabled = flag;
            this._btnDocumentos.IsEnabled = flag;
            this._btnAuditoria.IsEnabled = flag;
            this._btnExpediente.IsEnabled = flag;
        }

        public void PintarBotonExpediente()
        {
            this._btnExpediente.Background = Brushes.LightGreen;
        }

        public void PintarBotonDocumentos()
        {
            this._btnDocumentos.Background = Brushes.LightGreen;
        }

        public void PintarBotonAuditoria()
        {
            this._btnAuditoria.Background = Brushes.LightGreen;
        }

        #endregion

         
        
    }
}
