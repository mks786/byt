using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Interesados;
using Trascend.Bolet.Cliente.Presentadores.Interesados;

namespace Trascend.Bolet.Cliente.Ventanas.Interesados
{
    /// <summary>
    /// Interaction logic for ConsultarPoder.xaml
    /// </summary>
    public partial class ConsultarInteresado : Page, IConsultarInteresado
    {
        private PresentadorConsultarInteresado _presentador;
        private bool _cargada;

        #region IConsultarInteresado

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Interesado
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._cbxTipoPersona.IsEnabled = value;
                this._txtNombre.IsEnabled = value;
                this._txtCiudad.IsEnabled = value;
                this._txtEstado.IsEnabled = value;
                this._cbxPais.IsEnabled = value;
                this._cbxNacionalidad.IsEnabled = value;
                this._cbxCorporacion.IsEnabled = value;
                this._txtRegMercantil.IsEnabled = value;
                this._txtRMercantil.IsEnabled = value;
                this._txtAlerta.IsEnabled = value;
                this._txtDomicilio.IsEnabled = value;
                this._txtCi.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public object TipoPersonas
        {

            get { return this._cbxTipoPersona.DataContext; }
            set { this._cbxTipoPersona.DataContext = value; }
        }

        public object TipoPersona
        {

            get { return this._cbxTipoPersona.SelectedItem; }
            set { this._cbxTipoPersona.SelectedItem = value; }
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

        public object Nacionalidades
        {
            get { return this._cbxNacionalidad.DataContext; }
            set { this._cbxNacionalidad.DataContext = value; }
        }

        public object Nacionalidad
        {
            get { return this._cbxNacionalidad.SelectedItem; }
            set { this._cbxNacionalidad.SelectedItem = value; }
        }

        public object Corporaciones
        {
            get { return this._cbxCorporacion.DataContext; }
            set { this._cbxCorporacion.DataContext = value; }
        }

        public object Corporacion
        {
            get { return this._cbxCorporacion.SelectedItem; }
            set { this._cbxCorporacion.SelectedItem = value; }
        }

        #endregion

        public ConsultarInteresado(object interesado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarInteresado(this, interesado);
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarInteresado, "Eliminar Interesado", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }

        private void _btnPoderes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.VerPoderes();
        }
    }
}
