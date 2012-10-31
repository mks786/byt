using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Presentadores.Poderes;
using Trascend.Bolet.Cliente.Ayuda;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Poderes
{
    /// <summary>
    /// Interaction logic for ConsultarPoder.xaml
    /// </summary>
    public partial class ConsultarPoder : Page, IConsultarPoder
    {
        private PresentadorConsultarPoder _presentador;
        private GridViewColumnHeader _CurSortCol = null;
        private bool _cargada;
        private SortAdorner _CurAdorner = null;

        #region IConsultarPoder

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtNumSAPI.Focus();
        }

        public object Poder
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtNumSAPI.IsEnabled = value;
                this._txtFacultad.IsEnabled = value;
                this._cbxBoletin.IsEnabled = value;
                this._lstInteresados.IsEnabled = value;
                this._txtAnexo.IsEnabled = value;
                this._txtObservaciones.IsEnabled = value;
                this._txtNombreInteresado.IsEnabled = value;
                this._cbxAgente.IsEnabled = value;
                this._btnAgregarAgente.IsEnabled = value;
                this._btnQuitarAgente.IsEnabled = value;
                this._lstAgentes.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }


        public object Boletines
        {
            get { return this._cbxBoletin.DataContext; }
            set { this._cbxBoletin.DataContext = value; }
        }

        public object Boletin
        {
            get { return this._cbxBoletin.SelectedItem; }
            set { this._cbxBoletin.SelectedItem = value; }
        }

        public object Agentes
        {
            get { return this._cbxAgente.DataContext; }
            set { this._cbxAgente.DataContext = value; }
        }

        public object Agente
        {
            get { return this._cbxAgente.SelectedItem; }
            set { this._cbxAgente.SelectedItem = value; }
        }

        public object Apoderados
        {
            get { return this._lstAgentes.DataContext; }
            set { this._lstAgentes.DataContext = value; }
        }

        public object Apoderado
        {
            get { return this._lstAgentes.SelectedItem; }
            set { this._lstAgentes.SelectedItem = value; }
        }

        public object Interesados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._lstInteresados.SelectedItem; }
            set
            {
                this._lstInteresados.SelectedItem = value;
                this._lstInteresados.ScrollIntoView(value);
            }
        }

        public string IdInteresadoConsultar
        {
            get { return this._txtIdInteresadoFiltrar.Text; }
        }

        public string NombreInteresadoConsultar
        {
            get { return this._txtNombreInteresadoFiltrar.Text; }
        }

        public void ArchivoNoEncontrado()
        {
            MessageBox.Show(Recursos.MensajesConElUsuario.ErrorPoderNoEncontrado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public ListView ListaResultados
        {
            get { return this._lstInteresados; }
            set { this._lstInteresados = value; }
        }

        public string NombreInteresado
        {
            set { this._txtNombreInteresado.Text = value; }
        }

        public bool ConfirmarModificacion(string mensaje)
        {
            bool retorno = false;

            if (MessageBoxResult.Yes == MessageBox.Show(mensaje,
                "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question))
                retorno = true;

            return retorno;
        }

        #endregion

        //public ConsultarPoder(object poder, object Boletines, object Interesados,object ventanaPadre)
        //{
        //    InitializeComponent();
        //    this._cargada = false;
        //    this._presentador = new PresentadorConsultarPoder(this, poder, Boletines, Interesados);
        //}

        public ConsultarPoder(object poder, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarPoder(this, poder,ventanaPadre);
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarPoder, "Eliminar Poder", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _btnVerPoder_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AbrirPoder();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        //private void _txtInteresado_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    this._txtInteresado.Visibility = System.Windows.Visibility.Hidden;
        //    this._lstInteresados.Visibility = System.Windows.Visibility.Visible;
        //    this._lstInteresados.Height = 240;
        //}

        //private void _lstInteresados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    this._txtInteresado.Visibility = System.Windows.Visibility.Visible;
        //    this._lstInteresados.Visibility = System.Windows.Visibility.Hidden;
        //    this._lstInteresados.Height = 30;
        //    this._presentador.cambiarInteresado();
        //}

        //private void _lstInteresados_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    this._txtInteresado.Visibility = System.Windows.Visibility.Visible;
        //    this._lstInteresados.Visibility = System.Windows.Visibility.Hidden;
        //    this._lstInteresados.Height = 30;
        //    this._presentador.cambiarInteresado();
        //}

        private void _txtNombreInteresado_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._txtIdInteresadoFiltrar.Focus();
            this._btnConsultarInteresado.IsDefault = true;
            this._btnAceptar.IsDefault = false;

            MostrarListInteresados();
        }

        private void _OrdenarAgentes_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstAgentes);
        }

        private void MostrarListInteresados()
        {
            this._txtNombreInteresado.Visibility = Visibility.Collapsed;
            this._lblNombreInteresadoFiltrar.Visibility = Visibility.Visible;
            this._txtNombreInteresadoFiltrar.Visibility = Visibility.Visible;
            this._lblIdInteresadoFiltrar.Visibility = Visibility.Visible;
            this._txtIdInteresadoFiltrar.Visibility = Visibility.Visible;
            this._btnConsultarInteresado.Visibility = Visibility.Visible;
            this._lstInteresados.Visibility = Visibility.Visible;
            this._colInteresados.Height = new System.Windows.GridLength(180);
        }

        private void MostrarNombreInteresado()
        {
            this._txtNombreInteresado.Visibility = Visibility.Visible;
            this._lblNombreInteresadoFiltrar.Visibility = Visibility.Collapsed;
            this._txtNombreInteresadoFiltrar.Visibility = Visibility.Collapsed;
            this._lblIdInteresadoFiltrar.Visibility = Visibility.Collapsed;
            this._txtIdInteresadoFiltrar.Visibility = Visibility.Collapsed;
            this._btnConsultarInteresado.Visibility = Visibility.Collapsed;
            this._lstInteresados.Visibility = Visibility.Collapsed;
            this._colInteresados.Height = new System.Windows.GridLength(30);
        }

        private void _lstInteresados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._lstInteresados.SelectedItem != null)
            {
                _presentador.CambiarInteresado();
                MostrarNombreInteresado();
            }
        }

        private void _txtIdInteresadoFiltrar_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarInteresadoFiltro();
        }

        private void _btnAgregarAgente_Click(object sender, RoutedEventArgs e)
        {
            _presentador.AgregarAgente();
        }

        private void _btnQuitarAgente_Click(object sender, RoutedEventArgs e)
        {
            _presentador.EliminarAgente();

        }

        private void _btnVerPoder_Click_1(object sender, RoutedEventArgs e)
        {

        }

    }
}
