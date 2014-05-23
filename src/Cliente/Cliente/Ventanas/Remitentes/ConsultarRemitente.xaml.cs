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
using Trascend.Bolet.Cliente.Contratos.Remitentes;
using Trascend.Bolet.Cliente.Presentadores.Remitentes;

namespace Trascend.Bolet.Cliente.Ventanas.Remitentes
{
    /// <summary>
    /// Interaction logic for ConsultarRemitente.xaml
    /// </summary>
    public partial class ConsultarRemitente : Page, IConsultarRemitente
    {
        private PresentadorConsultarRemitente _presentador;
        private bool _cargada;
        private bool _deshabilitarFecha = false;

        #region IConsultarRemitente

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            //this._txtId.Focus();
            this._txtDescripcion.Focus();
        }

        public object Remitente
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool DeshabilitarFecha
        {
            set { this._deshabilitarFecha = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtDescripcion.IsEnabled = value;
                this._txtDireccion.IsEnabled = value;
                this._txtCiudad.IsEnabled = value;
                this._txtEstado.IsEnabled = value;
                this._txtTelefono.IsEnabled = value;
                this._txtFax.IsEnabled = value;
                this._cbxTipoRemitente.IsEnabled = value;
                this._cbxPais.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public char GetTipoRemitente
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxTipoRemitente.Text))
                    return ((string)this._cbxTipoRemitente.Text)[0];
                else
                    return ' ';
            }
        }

        public string SetTipoRemitente
        {
            set { this._cbxTipoRemitente.Text = value; }
        }

        public object Paises
        {
            get { return this._cbxPais.DataContext; }
            set { this._cbxPais.DataContext = value; }
        }

        public object Pais
        {
            get { return this._cbxPais.SelectedItem; }
            set { this._cbxPais.SelectedItem = value; }
        }

        #endregion


        public ConsultarRemitente(object remitente, object ventanaPadre)
        {

            InitializeComponent();
            this._presentador = new PresentadorConsultarRemitente(this, remitente, ventanaPadre);
            this._cargada = false;
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarRemitente, "Eliminar Remitente", MessageBoxButton.YesNo, MessageBoxImage.Question))
                this._presentador.Eliminar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();

                EstaCargada = true;
            }
        }

        private void _btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.SeleccionarRemitente();
        }

        public void MostrarBotonSeleccionarRemitente()
        {
            this._btnSeleccionar.Visibility = System.Windows.Visibility.Visible;
        }
    }
}