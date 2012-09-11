using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Reportes;
using Trascend.Bolet.Cliente.Presentadores.Reportes;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Reportes
{
    /// <summary>
    /// Interaction logic for ConsultarUsuario.xaml
    /// </summary>
    public partial class Carta2 : Page, ICarta2
    {
        private PresentadorCarta2 _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region ICarta1

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtIdMarcaFiltrar.Focus();
        }

        public object Idioma
        {
            get { return this._cbxIdioma.SelectedItem; }
            set { this._cbxIdioma.SelectedItem = value; }
        }

        public object Idiomas
        {
            get { return this._cbxIdioma.DataContext; }
            set { this._cbxIdioma.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._lstInteresados.SelectedItem; }
            set { this._lstInteresados.SelectedItem = value; }
        }

        public object Interesados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object Asociado
        {
            get { return this._lstAsociados.SelectedItem; }
            set { this._lstAsociados.SelectedItem = value; }
        }

        public object Asociados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public bool RadioConsultarAsociado()
        {
            return this._radioAsociado.IsChecked.Value;
        }

        public bool RadioConsultarInteresado()
        {
            return this._radioInteresado.IsChecked.Value;
        }

        public bool RadioMuchasMarcas()
        {
            return this._radioGrupal.IsChecked.Value;
        }

        public bool RadioUnicaMarca()
        {
            return this._radioIndividual.IsChecked.Value;
        }

        public object Marca
        {
            get { return this._lstMarcas.SelectedItem; }
            set { this._lstMarcas.SelectedItem = value; }
        }

        public object Marcas
        {
            get { return this._lstMarcas.DataContext; }
            set { this._lstMarcas.DataContext = value; }
        }

        public object MarcaAgregada
        {
            get { return this._lstMarcasAgregadas.SelectedItem; }
            set { this._lstMarcasAgregadas.SelectedItem = value; }
        }

        public object MarcasAgregadas
        {
            get { return this._lstMarcasAgregadas.DataContext; }
            set { this._lstMarcasAgregadas.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuario.SelectedItem; }
            set { this._cbxUsuario.SelectedItem = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuario.DataContext; }
            set { this._cbxUsuario.DataContext = value; }
        }

        public string IdFiltrar
        {
            get { return this._txtIdFiltrar.Text; }
        }

        public string Fecha
        {
            get { return this._dpkFecha.Text; }
            set { this._dpkFecha.Text = value; }
        }

        public string NombreFiltrar
        {
            get { return this._txtNombreFiltrar.Text; }
        }

        public string IdMarcaFiltrar
        {
            get { return this._txtIdMarcaFiltrar.Text; }
        }

        public string NombreMarcaFiltrar
        {
            get { return this._txtNombreMarcaFiltrar.Text; }
        }

        public object MarcaGeneral
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public void Departamento(string texto)
        {
            this._txtDepartamento.Text = texto;
        }

        public void BorrarCeros()
        {
            this._txtIdMarca.Text = "";
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

        public void MensajeAlerta(string mensaje)
        {
            MessageBox.Show(mensaje,
                "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
        }

        public void MensajeExito(string mensaje)
        {
            MessageBox.Show(mensaje,
                   "Reporte exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        #endregion

        public Carta2()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorCarta2(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _cbxIdioma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadoOAsociado();
            this._btnAceptar.IsDefault = true;
            this._btnConsultar.IsDefault = false;
        }

        private void _lstAsociados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void _OrdenarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAsociados);
        }

        private void _OrdenarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstMarcas);
        }

        private void _OrdenarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstInteresados);
        }

        private void _lstInteresados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void _txtIdFiltrar_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            this._btnAceptar.IsDefault = false;
            this._btnConsultarMarca.IsDefault = false;
            this._btnConsultar.IsDefault = true;
        }

        private void _radioAsociado_Checked(object sender, RoutedEventArgs e)
        {
            if (_cargada)
            {
                this._lstInteresados.Visibility = Visibility.Collapsed;
                this._lstAsociados.Visibility = Visibility.Visible;
            }
        }

        private void _radioInteresado_Checked(object sender, RoutedEventArgs e)
        {
            if (_cargada)
            {
                this._lstAsociados.Visibility = Visibility.Collapsed;
                this._lstInteresados.Visibility = Visibility.Visible;
            }
        }

        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarMarca();
        }

        private void _txtIdMarca_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._lblIdFiltrar.Visibility = Visibility.Visible;
            this._lblNombreFiltrar.Visibility = Visibility.Visible;
            this._txtIdMarcaFiltrar.Visibility = Visibility.Visible;
            this._txtNombreMarcaFiltrar.Visibility = Visibility.Visible;
            this._btnConsultarMarca.Visibility = Visibility.Visible;
            this._lstMarcas.Visibility = Visibility.Visible;

            this._txtIdMarca.Visibility = Visibility.Collapsed;
            this._txtMarca.Visibility = Visibility.Collapsed;

            this.FilaExpandible.Height = new GridLength(120);

            this._btnConsultar.IsDefault = false;
            this._btnAceptar.IsDefault = false;
            this._btnConsultarMarca.IsDefault = true;
        }

        private void _lstMarcas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_radioIndividual.IsChecked.Value)
            {
                if (this._presentador.CambiarMarca())
                {

                    this._lblIdFiltrar.Visibility = Visibility.Collapsed;
                    this._lblNombreFiltrar.Visibility = Visibility.Collapsed;
                    this._txtIdMarcaFiltrar.Visibility = Visibility.Collapsed;
                    this._txtNombreMarcaFiltrar.Visibility = Visibility.Collapsed;
                    this._btnConsultarMarca.Visibility = Visibility.Collapsed;
                    this._lstMarcas.Visibility = Visibility.Collapsed;

                    this._txtIdMarca.Visibility = Visibility.Visible;
                    this._txtMarca.Visibility = Visibility.Visible;

                    this.FilaExpandible.Height = new GridLength(30);

                    this._btnConsultar.IsDefault = true;
                    this._btnAceptar.IsDefault = false;
                    this._btnConsultarMarca.IsDefault = false;
                }
            }
            else if (_radioGrupal.IsChecked.Value)
            {
                this._presentador.AgregarMarca();
            }
        }

        private void _cbxUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CambiarUsuario();
        }

        private void _radioIndividual_Checked(object sender, RoutedEventArgs e)
        {
            if (_cargada)
            {
                this._lblANombreDeInteresado.Visibility = Visibility.Visible;
                this._lblClaseInternacional.Visibility = Visibility.Visible;
                this._lblIdAsoIntFiltrar.Visibility = Visibility.Visible;
                //this._lblIdFiltrar.Visibility = Visibility.Visible;
                this._lblNombreAsoIntFiltrar.Visibility = Visibility.Visible;
                this._lblNoRegistro.Visibility = Visibility.Visible;
                //this._lblNombreFiltrar.Visibility = Visibility.Visible;
                this._lblPara.Visibility = Visibility.Visible;
                this._lblProxRenovacion.Visibility = Visibility.Visible;
                this._lblReferencia.Visibility = Visibility.Visible;
                this._lblUsuario.Visibility = Visibility.Visible;
                this._lblIdioma.Visibility = Visibility.Visible;

                this._txtCiudadInteresado.Visibility = Visibility.Visible;
                this._txtClaseInternacional.Visibility = Visibility.Visible;
                this._txtDepartamento.Visibility = Visibility.Visible;
                this._txtInteresado.Visibility = Visibility.Visible;
                this._txtNombreFiltrar.Visibility = Visibility.Visible;
                this._txtIdFiltrar.Visibility = Visibility.Visible;
                this._txtNoRegistroMarca.Visibility = Visibility.Visible;
                this._txtPaisInteresado.Visibility = Visibility.Visible;
                this._txtReferencia.Visibility = Visibility.Visible;

                this._cbxIdioma.Visibility = Visibility.Visible;
                this._cbxUsuario.Visibility = Visibility.Visible;

                this._radioAsociado.Visibility = Visibility.Visible;
                this._radioInteresado.Visibility = Visibility.Visible;

                this._dpkFechaProxRenovacion.Visibility = Visibility.Visible;

                this._lstAsociados.Visibility = Visibility.Visible;
                this._lstInteresados.Visibility = Visibility.Visible;

                this._btnConsultar.Visibility = Visibility.Visible;
                this._btnMas.Visibility = Visibility.Collapsed;
                this._btnMenos.Visibility = Visibility.Collapsed;
                this._lstMarcasAgregadas.Visibility = Visibility.Collapsed;
                this._lblMarcasAgregadas.Visibility = Visibility.Collapsed;
                this.FilaExpandibleMarcasAgregadas.Height = new GridLength(30);
            }
        }

        private void _radioGrupal_Checked(object sender, RoutedEventArgs e)
        {
            this._lblANombreDeInteresado.Visibility = Visibility.Collapsed;
            this._lblClaseInternacional.Visibility = Visibility.Collapsed;
            this._lblIdAsoIntFiltrar.Visibility = Visibility.Collapsed;
            //this._lblIdFiltrar.Visibility = Visibility.Collapsed;
            this._lblNombreAsoIntFiltrar.Visibility = Visibility.Collapsed;
            this._lblNoRegistro.Visibility = Visibility.Collapsed;
            //this._lblNombreFiltrar.Visibility = Visibility.Collapsed;
            this._lblPara.Visibility = Visibility.Collapsed;
            this._lblProxRenovacion.Visibility = Visibility.Collapsed;
            this._lblReferencia.Visibility = Visibility.Collapsed;
            this._lblUsuario.Visibility = Visibility.Collapsed;
            //this._lblIdioma.Visibility = Visibility.Collapsed;

            this._txtCiudadInteresado.Visibility = Visibility.Collapsed;
            this._txtClaseInternacional.Visibility = Visibility.Collapsed;
            this._txtDepartamento.Visibility = Visibility.Collapsed;
            this._txtInteresado.Visibility = Visibility.Collapsed;
            this._txtNombreFiltrar.Visibility = Visibility.Collapsed;
            this._txtIdFiltrar.Visibility = Visibility.Collapsed;
            this._txtNoRegistroMarca.Visibility = Visibility.Collapsed;
            this._txtPaisInteresado.Visibility = Visibility.Collapsed;
            this._txtReferencia.Visibility = Visibility.Collapsed;

            //this._cbxIdioma.Visibility = Visibility.Collapsed;
            this._cbxUsuario.Visibility = Visibility.Collapsed;

            this._radioAsociado.Visibility = Visibility.Collapsed;
            this._radioInteresado.Visibility = Visibility.Collapsed;

            this._dpkFechaProxRenovacion.Visibility = Visibility.Collapsed;

            this._lstAsociados.Visibility = Visibility.Collapsed;
            this._lstInteresados.Visibility = Visibility.Collapsed;

            this._btnConsultar.Visibility = Visibility.Collapsed;
            this._btnMas.Visibility = Visibility.Visible;
            this._btnMenos.Visibility = Visibility.Visible;
            this._lstMarcasAgregadas.Visibility = Visibility.Visible;
            this._lblMarcasAgregadas.Visibility = Visibility.Visible;

            this.FilaExpandibleMarcasAgregadas.Height = new GridLength(120);
        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarMarca();
        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EliminarMarca();
        }

        private void _lstMarcasAgregadas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._presentador.EliminarMarca();
        }
    }
}
