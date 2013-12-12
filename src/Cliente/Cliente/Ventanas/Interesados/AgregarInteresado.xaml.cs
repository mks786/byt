using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Interesados;
using Trascend.Bolet.Cliente.Presentadores.Interesados;

namespace Trascend.Bolet.Cliente.Ventanas.Interesados
{
    /// <summary>
    /// Interaction logic for ConsultarInteresado.xaml
    /// </summary>
    public partial class AgregarInteresado : Page, IAgregarInteresado
    {
        private PresentadorAgregarInteresado _presentador;
        private bool _cargada;

        #region IAgregarInteresado

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNombre.Focus();
        }

        public object Interesado
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
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

        public string Ciudad
        {
            get { return this._txtCiudad.Text; }
            set { this._txtCiudad.Text = value; }
        }

        public string Estado
        {
            get { return this._txtEstado.Text; }
            set { this._txtEstado.Text = value; }
        }

        public object OrigenesClientes
        {
            get { return this._cbxOrigenCliente.DataContext; }
            set { this._cbxOrigenCliente.DataContext = value; }
        }

        public object OrigenCliente
        {
            get { return this._cbxOrigenCliente.SelectedItem; }
            set { this._cbxOrigenCliente.SelectedItem = value; }
        }

        #endregion

        public AgregarInteresado()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarInteresado(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }
    }
}
