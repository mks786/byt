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
using Trascend.Bolet.Cliente.Contratos.CadenaDeCambio;
using Trascend.Bolet.Cliente.Presentadores.CadenaDeCambio;

namespace Trascend.Bolet.Cliente.Ventanas.CadenaDeCambio
{
    /// <summary>
    /// Lógica de interacción para GestionarCadenasDeCambios.xaml
    /// </summary>
    public partial class GestionarCadenaDeCambios : Page, IGestionarCadenaDeCambios
    {

        private PresentadorGestionarCadenaDeCambios _presentador;
        private bool _cargada;

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        /// <param name="cadenaCambios">Cadena de Cambios a mostrar en pantalla</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public GestionarCadenaDeCambios(object cadenaCambios, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarCadenaDeCambios(this, cadenaCambios, ventanaPadre);
        }

        #region IGestionarCadenasDeCambios

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public object CadenaDeCambios
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public string IdCadenaDeCambios
        {
            get { return this._txtIdCadenaCambios.Text; }
            set { this._txtIdCadenaCambios.Text = value; }
        }

        public string CodigoOperacionCadenaCambios
        {
            get { return this._txtAplicaCadenaCambios.Text; }
            set { this._txtAplicaCadenaCambios.Text = value; }
        }

        public object TiposCadenaCambios
        {
            get { return this._cbxTipoCadenaCambios.DataContext; }
            set { this._cbxTipoCadenaCambios.DataContext = value; }
        }

        public object TipoCadenaCambios
        {
            get { return this._cbxTipoCadenaCambios.SelectedItem; }
            set { this._cbxTipoCadenaCambios.SelectedItem = value; }
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
                //this._txtIdCadenaCambios.IsEnabled = value;
                this._txtAplicaCadenaCambios.IsEnabled = value;
                this._btnConsultarCodigoOperacion.IsEnabled = value;
                this._cbxTipoCadenaCambios.IsEnabled = value;
                this._txtIdCorrespondencia.IsEnabled = value;
                this._btnVerCorrespondencia.IsEnabled = value;
            }
        }

        public string IdCarta
        {
            get { return this._txtIdCorrespondencia.Text; }
            set { this._txtIdCorrespondencia.Text = value; }
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
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.AlertaAgregarOModificarCadenaCambios),
                    "Registro de Cadena de Cambios", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Aceptar();
            }
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnConsultarCodigoOperacion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrConsultarCodigoOperacion();
        }

        private void _btnOperaciones_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrVerOperaciones();
        }

        private void _btnVerCorrespondencia_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarCarta();
        }

        #endregion

        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void MostarBotonOperaciones()
        {
            this._btnOperaciones.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion


    }
}
