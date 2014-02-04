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
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional
{
    /// <summary>
    /// Lógica de interacción para ConsultarSeguimientoCxPInternacional.xaml
    /// </summary>
    public partial class ConsultarSeguimientoCxPInternacional : Page, IConsultarSeguimientoCxPInternacional
    {

        private bool _cargada;
        private PresentadorConsultarSeguimientoCxPInternacional _presentador;

        public ConsultarSeguimientoCxPInternacional()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarSeguimientoCxPInternacional(this);
        }

        #region IConsultarSeguimientoCxPInternacional

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object FiltroDataCruda
        {
            get { return this._spFiltro.DataContext; }
            set { this._spFiltro.DataContext = value; }
        }

        public object TiposDeudas
        {
            get { return this._cbxTipoDeudaSegCxPInt.DataContext; }
            set { this._cbxTipoDeudaSegCxPInt.DataContext = value; }
        }

        public object TipoDeuda
        {
            get { return this._cbxTipoDeudaSegCxPInt.SelectedItem; }
            set { this._cbxTipoDeudaSegCxPInt.SelectedItem = value; }
        }

        public string RangoInferior
        {
            set { this._txtRangoInfSegCxPInt.Text = value; }
        }

        public string RangoSuperior
        {
            get { return this._txtRangoSupSegCxPInt.Text; }
            set { this._txtRangoSupSegCxPInt.Text = value; }
        }

        public object Ordenamientos
        {
            get { return this._cbxOrdenamientoSegCxPInt.DataContext; }
            set { this._cbxOrdenamientoSegCxPInt.DataContext = value; }
        }

        public object Ordenamiento
        {
            get { return this._cbxOrdenamientoSegCxPInt.SelectedItem; }
            set { this._cbxOrdenamientoSegCxPInt.SelectedItem = value; }
        }

        public string IdAsociado
        {
            get { return this._txtAsociadoSegCxPInt.Text; }
            set { this._txtAsociadoSegCxPInt.Text = value; }
        }

        public string IdAsociadoFiltrar
        {
            get { return this._txtIdAsociado.Text; }
            set { this._txtIdAsociado.Text = value; }
        }


        public string NombreAsociadoFiltrar
        {
            get { return this._txtNombreAsociado.Text; }
            set { this._txtNombreAsociado.Text = value; }
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

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        //Ejes para la tabla Pivot

        public object CamposEjeXPivot
        {
            get { return this._cbxEjeXSegCxPInt.DataContext; }
            set { this._cbxEjeXSegCxPInt.DataContext = value; }
        }

        public object EjeXSeleccionado
        {
            get { return this._cbxEjeXSegCxPInt.SelectedItem; }
            set { this._cbxEjeXSegCxPInt.SelectedItem = value; }
        }

        public object CamposEjeYPivot
        {
            get { return this._cbxEjeYSegCxPInt.DataContext; }
            set { this._cbxEjeYSegCxPInt.DataContext = value; }
        }

        public object EjeYSeleccionado
        {
            get { return this._cbxEjeYSegCxPInt.SelectedItem; }
            set { this._cbxEjeYSegCxPInt.SelectedItem = value; }
        }

        public object CamposEjeZPivot
        {
            get { return this._cbxEjeZSegCxPInt.DataContext; }
            set { this._cbxEjeZSegCxPInt.DataContext = value; }
        }

        public object EjeZSeleccionado
        {
            get { return this._cbxEjeZSegCxPInt.SelectedItem; }
            set { this._cbxEjeZSegCxPInt.SelectedItem = value; }
        }


        //Totales de la consulta de Resumen General
        
        public string TotalUSD
        {
            get { return this._txtTotalDolares.Text; }
            set { this._txtTotalDolares.Text = value; }
        }

        public string TotalPorVencer
        {
            get { return this._txtTotalPorVencer.Text; }
            set { this._txtTotalPorVencer.Text = value; }
        }

        public string TotalVencido
        {
            get { return this._txtTotalVencido.Text; }
            set { this._txtTotalVencido.Text = value; }
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
            else
                this._presentador.ActualizarTitulo();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado();
        }

        private void _txtAsociadoSegCxPInt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(true);
        }

        private void _btnConsultarAsociadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarAsociado.IsDefault = true;
        }

        private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociado())
                GestionarVisibilidadFiltroAsociado(false);
        }

        /// <summary>
        /// Evento del boton para presentar los totales generales ANTES de generar la data cruda para el pivot
        /// </summary>
        private void _btnVerTotalesGenerales_Click(object sender, RoutedEventArgs e)
        {
            this._btnVerPivot.Focus();
            this._presentador.ObtenerResumenGeneral();
            validarCamposVacios();
        }
        
        /// <summary>
        /// Evento del boton que presenta la ventana con los datos pivot 
        /// </summary>
        private void _btnVerPivot_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaDatosPivot();
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

        private void GestionarVisibilidadFiltroAsociado(bool visibilidad)
        {
            if (visibilidad)
            {
                this._txtAsociadoSegCxPInt.Visibility = Visibility.Collapsed;
                this._txtIdAsociado.Visibility = Visibility.Visible;
                this._txtNombreAsociado.Visibility = Visibility.Visible;
                this._lblIdAsociado.Visibility = Visibility.Visible;
                this._lblNombreAsociado.Visibility = Visibility.Visible;
                this._lstAsociados.Visibility = Visibility.Visible;
                this._btnConsultarAsociado.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtAsociadoSegCxPInt.Visibility = Visibility.Visible;
                this._txtIdAsociado.Visibility = Visibility.Collapsed;
                this._txtNombreAsociado.Visibility = Visibility.Collapsed;
                this._lblIdAsociado.Visibility = Visibility.Collapsed;
                this._lblNombreAsociado.Visibility = Visibility.Collapsed;
                this._lstAsociados.Visibility = Visibility.Collapsed;
                this._btnConsultarAsociado.Visibility = Visibility.Collapsed;
            }
        }


        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;

            //if ((this._cbxMonedaSegCxPInt.SelectedIndex != 0) && (this._cbxMonedaSegCxPInt.SelectedIndex != -1))
            //{
            //    todosCamposVacios = false;
            //    this._cbxMonedaSegCxPInt.Focus();
            //}

            if ((this._cbxOrdenamientoSegCxPInt.SelectedIndex != 0) && (this._cbxOrdenamientoSegCxPInt.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxOrdenamientoSegCxPInt.Focus();
            }

            
            if (!this._txtIdAsociado.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdAsociado.Focus();
            }


            if (todosCamposVacios)
            {
                this._cbxOrdenamientoSegCxPInt.Focus();
            }
        }


        public void ActivarEjesPivot()
        {
            this._lblEjeXSegCxPInt.IsEnabled = true;
            this._lblEjeYSegCxPInt.IsEnabled = true;
            this._lblEjeZSegCxPInt.IsEnabled = true;
            this._cbxEjeXSegCxPInt.IsEnabled = true;
            this._cbxEjeYSegCxPInt.IsEnabled = true;
            this._cbxEjeZSegCxPInt.IsEnabled = true;
            this._btnVerPivot.IsEnabled = true;
            this._btnVerTotalesGenerales.IsDefault = false;
            this._btnVerPivot.IsDefault = true;
            this._btnVerPivot.Focus();
        }

        public void DesactivarEjesPivot()
        {
            this._lblEjeXSegCxPInt.IsEnabled = false;
            this._lblEjeYSegCxPInt.IsEnabled = false;
            this._lblEjeZSegCxPInt.IsEnabled = false;
            this._cbxEjeXSegCxPInt.IsEnabled = false;
            this._cbxEjeYSegCxPInt.IsEnabled = false;
            this._cbxEjeZSegCxPInt.IsEnabled = false;
            this._btnVerPivot.IsEnabled = false;
            this._btnVerTotalesGenerales.IsDefault = true;
            this._btnVerPivot.IsDefault = false;
            this._btnVerPivot.Focus();
        }


        #endregion

        

        

        

        
    }
}
