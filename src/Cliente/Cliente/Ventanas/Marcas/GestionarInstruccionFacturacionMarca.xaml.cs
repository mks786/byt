using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Lógica de interacción para GestionarInstruccionFacturacionMarca.xaml
    /// </summary>
    public partial class GestionarInstruccionFacturacionMarca : Page, IGestionarInstruccionFacturacionMarca
    {

        private PresentadorGestionarInstruccionFacturacionMarca _presentador;
        private bool _cargada;
        BackgroundWorker _bgw = new BackgroundWorker();
        private bool _camposHabilitados = true;

        /// <summary>
        /// Constructor por defecto que recibe las instrucciones definidas para la facturacion
        /// </summary>
        /// <param name="instruccionEnvioEmails">Instruccion de facturacion de envio de emails</param>
        /// <param name="instruccionEnvioOriginales">Instruccion de facturacion de envio de originales</param>
        /// <param name="marca">Marca consultada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        /// <param name="ventanaConsultarMarcas">Ventana padre de la ventana ConsultarMarca</param>
        public GestionarInstruccionFacturacionMarca(object instruccionEnvioEmails, object instruccionEnvioOriginales, object marca, object ventanaPadre, object ventanaConsultarMarcas)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarInstruccionFacturacionMarca(this, instruccionEnvioEmails, instruccionEnvioOriginales, marca, ventanaPadre, ventanaConsultarMarcas);
        }


        #region GestionarInstruccionFacturacionMarca

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        #region Datos de Marca

        public object Marca
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string IdMarca
        {
            get { return this._txtIdMarca.Text; }
            set { this._txtIdMarca.Text = value; }
        }

        public string DescripcionMarca
        {
            get { return this._txtDesripcionMarca.Text; }
            set { this._txtDesripcionMarca.Text = value; }
        }

        public string TipoDeMarca
        {
            get { return this._txtTipoMarca.Text; }
            set { this._txtTipoMarca.Text = value; }
        }

        public string ClaseNacionalMarca
        {
            get { return this._txtMarcaClaseNac.Text; }
            set { this._txtMarcaClaseNac.Text = value; }
        }

        public string ClaseInternacionalMarca
        {
            get { return this._txtMarcaClaseInt.Text; }
            set { this._txtMarcaClaseInt.Text = value; }
        }
        #endregion



        #region Datos Instruccion de Facturacion de Envio de Emails

        public object InstruccionEnvioEmail
        {
            get { return this._gridEnvioDeEmails.DataContext; }
            set { this._gridEnvioDeEmails.DataContext = value; }
        }

        public string IdInstruccionEnvioEmails
        {
            get { return this._txtCodigoInstruccion.Text; }
            set { this._txtCodigoInstruccion.Text = value; }
        }

        public object TiposDeInstruccionEnvioEmails
        {
            get { return this._cbxTiposInstruccion.DataContext; }
            set { this._cbxTiposInstruccion.DataContext = value; }
        }

        public object TipoInstruccionEnvioEmails
        {
            get { return this._cbxTiposInstruccion.SelectedItem; }
            set { this._cbxTiposInstruccion.SelectedItem = value; }
        }

        public string IdCorrespondenciaEnvioEmails
        {
            get { return this._txtIdCorrespondenciaInstruccion.Text; }
            set { this._txtIdCorrespondenciaInstruccion.Text = value; }
        }

        public string NombreEmailEnvioEmails
        {
            get { return this._txtNombreInstruccion.Text; }
            set { this._txtNombreInstruccion.Text = value; }
        }

        public string EmailParaEnvioEmails
        {
            get { return this._txtParaEmailInstruccion.Text; }
            set { this._txtParaEmailInstruccion.Text = value; }
        }

        public string EmailCCEnvioEmails
        {
            get { return this._txtCCEmailInstruccion.Text; }
            set { this._txtCCEmailInstruccion.Text = value; }
        }

        #endregion


        #region Datos Instruccion de Facturacion de Envio de Originales

        public object InstruccionEnvioOriginales
        {
            get { return this._gridEnvioOriginales.DataContext; }
            set { this._gridEnvioOriginales.DataContext = value; }
        }

        public object AlertaEOriginales
        {
            get { return this._grdAlerta.DataContext; }
            set { this._grdAlerta.DataContext = value; }
        }

        public string IdInstruccionEnvioOriginales
        {
            get { return this._txtCodigoInstruccionEOriginales.Text; }
            set { this._txtCodigoInstruccionEOriginales.Text = value; }
        }

        public string OtraDireccion
        {
            get { return this._txtOtraDireccion.Text; }
            set { this._txtOtraDireccion.Text = value; }
        }

        public string NombreInstruccionEnvioOriginales
        {
            get { return this._txtNombreEOriginales.Text; }
            set { this._txtNombreEOriginales.Text = value; }
        }

        public string AlertaEnvioOriginales
        {
            get { return this._txtAlerta.Text; }
            set { this._txtAlerta.Text = value; }
        }

        //Datos del Asociado 

        public string IdAsociadoEnvioOriginales
        {
            get { return this._txtIdAsociadoEOriginal.Text; }
            set { this._txtIdAsociadoEOriginal.Text = value; }
        }

        public string AsociadoEnvioOriginales
        {
            get { return this._txtAsociadoEOriginal.Text; }
            set { this._txtAsociadoEOriginal.Text = value; }
        }

        public string IdCorrespondencia_Asociado
        {
            get { return this._txtIdCorrespondenciaAsociado.Text; }
            set { this._txtIdCorrespondenciaAsociado.Text = value; }
        }

        public string Domicilio_Asociado
        {
            get { return this._txtDomicilioAsociadoEOriginal.Text; }
            set { this._txtDomicilioAsociadoEOriginal.Text = value; }
        }

        //Datos del Asociado para Filtrar

        public string IdAsociadoFiltrar
        {
            get { return this._txtIdAsociadoFiltrar.Text; }
            set { this._txtIdAsociadoFiltrar.Text = value; }
        }

        public string NombreAsociadoFiltrar
        {
            get { return this._txtNombreAsociadoFiltrar.Text; }
            set { this._txtNombreAsociadoFiltrar.Text = value; }
        }

        public object Asociados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public object Asociado
        {
            get { return this._lstAsociados.SelectedItem; }
            set { this._lstAsociados.SelectedItem = value; }
        }

        //Datos del Interesado

        public string IdInteresadoEnvioOriginales
        {
            get { return this._txtIdInteresadoEOriginal.Text; }
            set { this._txtIdInteresadoEOriginal.Text = value; }
        }

        public string InteresadoEnvioOriginales
        {
            get { return this._txtInteresadoEOriginal.Text; }
            set { this._txtInteresadoEOriginal.Text = value; }
        }

        public string IdCorrespondencia_Interesado
        {
            get { return this._txtIdCorrespondenciaInteresado.Text; }
            set { this._txtIdCorrespondenciaInteresado.Text = value; }
        }

        public string Domicilio_Interesado
        {
            get { return this._txtDomicilioInteresadoEOriginal.Text; }
            set { this._txtDomicilioInteresadoEOriginal.Text = value; }
        }


        //Datos del Interesado para Filtrar

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresadoFiltrar.Text; }
            set { this._txtIdInteresadoFiltrar.Text = value; }
        }

        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresadoFiltrar.Text; }
            set { this._txtNombreInteresadoFiltrar.Text = value; }
        }

        public object Interesados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._lstInteresados.SelectedItem; }
            set { this._lstInteresados.SelectedItem = value; }
        }


        #endregion

        #endregion


        #region Eventos

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


        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Button boton = new Button();
            boton = (Button)sender;
            String botonPresionado = boton.Name;

            if (botonPresionado.Equals("_btnModificar"))
            {

                if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarModificarInstruccionFacturacionMarca,
                    _presentador.ObtenerIdMarca()),
                    "Modificar Instruccion de Facturacion Envio de Emails", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    if (this._presentador.Modificar(botonPresionado))
                    {
                        _bgw.RunWorkerAsync();
                    }

                }
            }
            else if (botonPresionado.Equals("_btnModificarInstEnvioOriginales"))
            {
                if (MessageBoxResult.Yes == MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarModificarInstruccionFacturacionMarca,
                    _presentador.ObtenerIdMarca()),
                    "Modificar Instruccion de Facturacion Envio de Originales", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    if (this._presentador.Modificar(botonPresionado))
                    {
                        _bgw.RunWorkerAsync();
                    }

                }
            }
        }


        private void _btnConsultarCorrespondencia_Click(object sender, RoutedEventArgs e)
        {
            Button boton = new Button();
            boton = (Button)sender;
            String botonPresionado = boton.Name;

            this._presentador.ConsultarCorrespondencia(botonPresionado);
        }


        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _txtAsociadoEOriginal_GotFocus(object sender, RoutedEventArgs e)
        {

            this._btnRegresar.IsDefault = false;
            this._btnConsultarAsociado.IsDefault = true;

            mostrarLstAsociados();
        }


        private void _txtInteresadoEOriginal_GotFocus(object sender, RoutedEventArgs e)
        {

            this._btnRegresar.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = true;

            mostrarLstInteresados();
        }


        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado();
        }


        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado();
        }


        private void _lstAsociados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociado();
            ocularLstAsociados();

            this._btnConsultarAsociado.IsDefault = false;
            this._btnRegresar.IsDefault = true;
        }


        private void _lstInteresados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._presentador.CambiarInteresado();
            ocularLstInteresados();

            this._btnConsultarInteresado.IsDefault = false;
            this._btnRegresar.IsDefault = true;
        }


        private void _btnIrAsociadosEOriginal_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaAsociado();
        }

        private void _btnIrInteresadosEOriginal_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVentanaInteresado();
        }

        #endregion


        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if(opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        public void ocularLstAsociados()
        {
            this._lstAsociados.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociadoEOriginal.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoEOriginal.Visibility = System.Windows.Visibility.Visible;
            this._txtDomicilioAsociadoEOriginal.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void mostrarLstAsociados()
        {
            this._lstAsociados.ScrollIntoView(this.Asociado);
            this._txtAsociadoEOriginal.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociadoEOriginal.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDomicilioAsociadoEOriginal.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociados.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociados.IsEnabled = true;
            this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociadoFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociadoFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociadoFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociadoFiltrar.Visibility = System.Windows.Visibility.Visible;
        }


        private void mostrarLstInteresados()
        {
            this._lstInteresados.ScrollIntoView(this.Interesado);
            this._txtInteresadoEOriginal.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoEOriginal.Visibility = System.Windows.Visibility.Collapsed;
            this._txtDomicilioInteresadoEOriginal.Visibility = System.Windows.Visibility.Collapsed;
            this._lstInteresados.Visibility = System.Windows.Visibility.Visible;
            this._lstInteresados.IsEnabled = true;
            this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreInteresadoFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoFiltrar.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreInteresadoFiltrar.Visibility = System.Windows.Visibility.Visible;
        }


        public void ocularLstInteresados()
        {
            this._lstInteresados.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarInteresado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdInteresadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreInteresadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._txtInteresadoEOriginal.Visibility = System.Windows.Visibility.Visible;
            this._txtIdInteresadoEOriginal.Visibility = System.Windows.Visibility.Visible;
            this._txtDomicilioInteresadoEOriginal.Visibility = System.Windows.Visibility.Visible;
            this._lblIdInteresadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreInteresadoFiltrar.Visibility = System.Windows.Visibility.Collapsed;
        }


        public void ConvertirEnteroMinimoABlanco()
        {


            #region Asociados

            if (null != this.Asociado)
            {
                if (!this.IdAsociadoEnvioOriginales.Equals(""))
                {
                    if (int.Parse(this.IdAsociadoEnvioOriginales) == int.MinValue)
                    {
                        this.IdAsociadoEnvioOriginales = "";
                    }
                }

            }


            #endregion




        }

        #endregion


    }
}
