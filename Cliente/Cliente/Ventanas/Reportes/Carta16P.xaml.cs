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
    public partial class Carta16P : Page, ICarta16P
    {
        private PresentadorCarta16P _presentador;
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
            this._txtIdPatenteFiltrar.Focus();
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

        public bool RadioMuchasPatentes()
        {
            return this._radioGrupal.IsChecked.Value;
        }

        public bool RadioUnicaPatente()
        {
            return this._radioIndividual.IsChecked.Value;
        }

        public object Patente
        {
            get { return this._lstPatentes.SelectedItem; }
            set { this._lstPatentes.SelectedItem = value; }
        }

        public object Patentes
        {
            get { return this._lstPatentes.DataContext; }
            set { this._lstPatentes.DataContext = value; }
        }

        public object PatenteAgregada
        {
            get { return this._lstPatentesAgregadas.SelectedItem; }
            set { this._lstPatentesAgregadas.SelectedItem = value; }
        }

        public object PatentesAgregadas
        {
            get { return this._lstPatentesAgregadas.DataContext; }
            set { this._lstPatentesAgregadas.DataContext = value; }
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

        public string IdPatenteFiltrar
        {
            get { return this._txtIdPatenteFiltrar.Text; }
        }

        public string NombrePatenteFiltrar
        {
            get { return this._txtNombrePatenteFiltrar.Text; }
        }

        public object PatenteGeneral
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
            this._txtIdPatente.Text = "";
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

        public Carta16P()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorCarta16P(this);
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

        private void _OrdenarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstPatentes);
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
            this._btnConsultarPatente.IsDefault = false;
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

        private void _btnConsultarPatente_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarPatente();
        }

        private void _txtIdPatente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._lblIdFiltrar.Visibility = Visibility.Visible;
            this._lblNombreFiltrar.Visibility = Visibility.Visible;
            this._txtIdPatenteFiltrar.Visibility = Visibility.Visible;
            this._txtNombrePatenteFiltrar.Visibility = Visibility.Visible;
            this._btnConsultarPatente.Visibility = Visibility.Visible;
            this._lstPatentes.Visibility = Visibility.Visible;

            this._txtIdPatente.Visibility = Visibility.Collapsed;
            this._txtPatente.Visibility = Visibility.Collapsed;

            this.FilaExpandible.Height = new GridLength(120);

            this._btnConsultar.IsDefault = false;
            this._btnAceptar.IsDefault = false;
            this._btnConsultarPatente.IsDefault = true;
        }

        private void _lstPatentes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_radioIndividual.IsChecked.Value)
            {
                if (this._presentador.CambiarPatente())
                {

                    this._lblIdFiltrar.Visibility = Visibility.Collapsed;
                    this._lblNombreFiltrar.Visibility = Visibility.Collapsed;
                    this._txtIdPatenteFiltrar.Visibility = Visibility.Collapsed;
                    this._txtNombrePatenteFiltrar.Visibility = Visibility.Collapsed;
                    this._btnConsultarPatente.Visibility = Visibility.Collapsed;
                    this._lstPatentes.Visibility = Visibility.Collapsed;

                    this._txtIdPatente.Visibility = Visibility.Visible;
                    this._txtPatente.Visibility = Visibility.Visible;

                    this.FilaExpandible.Height = new GridLength(30);

                    this._btnConsultar.IsDefault = true;
                    this._btnAceptar.IsDefault = false;
                    this._btnConsultarPatente.IsDefault = false;
                }
            }
            else if (_radioGrupal.IsChecked.Value) 
            {
                this._presentador.AgregarPatente();
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
                this._txtDepartamento.Visibility = Visibility.Visible;
                this._txtInteresado.Visibility = Visibility.Visible;
                this._txtNombreFiltrar.Visibility = Visibility.Visible;
                this._txtIdFiltrar.Visibility = Visibility.Visible;
                this._txtNoRegistroPatente.Visibility = Visibility.Visible;
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
                this._lstPatentesAgregadas.Visibility = Visibility.Collapsed;
                this._lblPatentesAgregadas.Visibility = Visibility.Collapsed;
                this.FilaExpandiblePatentesAgregadas.Height = new GridLength(30);
            }
        }

        private void _radioGrupal_Checked(object sender, RoutedEventArgs e)
        {
            this._lblANombreDeInteresado.Visibility = Visibility.Collapsed;
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
            this._txtDepartamento.Visibility = Visibility.Collapsed;
            this._txtInteresado.Visibility = Visibility.Collapsed;
            this._txtNombreFiltrar.Visibility = Visibility.Collapsed;
            this._txtIdFiltrar.Visibility = Visibility.Collapsed;
            this._txtNoRegistroPatente.Visibility = Visibility.Collapsed;
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
            this._lstPatentesAgregadas.Visibility = Visibility.Visible;
            this._lblPatentesAgregadas.Visibility = Visibility.Visible;

            this.FilaExpandiblePatentesAgregadas.Height = new GridLength(120);
        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarPatente();
        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.EliminarPatente();
        }

        private void _lstPatentesAgregadas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._presentador.EliminarPatente();
        }
    }
}
