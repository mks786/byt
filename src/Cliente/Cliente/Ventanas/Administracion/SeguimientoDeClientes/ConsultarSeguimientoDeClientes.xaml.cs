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
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeClientes;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoDeClientes;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoDeClientes
{
    /// <summary>
    /// Lógica de interacción para ConsultarSeguimientoDeClientes.xaml
    /// </summary>
    public partial class ConsultarSeguimientoDeClientes : Page, IConsultarSeguimientoDeClientes
    {
        private bool _cargada;
        private PresentadorConsultarSeguimientoDeClientes _presentador;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ConsultarSeguimientoDeClientes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarSeguimientoDeClientes(this);
        }

        #region IConsultarSeguimientoDeClientes

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

        public object Monedas
        {
            get { return this._cbxMonedaSegClientes.DataContext; }
            set { this._cbxMonedaSegClientes.DataContext = value; }
        }

        public object Moneda
        {
            get { return this._cbxMonedaSegClientes.SelectedItem; }
            set { this._cbxMonedaSegClientes.SelectedItem = value; }
        }

        public object TiposSaldos
        {
            get { return this._cbxTipoSaldoSegCliente.DataContext; }
            set { this._cbxTipoSaldoSegCliente.DataContext = value; }
        }

        public object TipoSaldo
        {
            get { return this._cbxTipoSaldoSegCliente.SelectedItem; }
            set { this._cbxTipoSaldoSegCliente.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamentoSegCliente.DataContext; }
            set { this._cbxDepartamentoSegCliente.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamentoSegCliente.SelectedItem; }
            set { this._cbxDepartamentoSegCliente.SelectedItem = value; }
        }

        public string RangoInferior
        {
            set { this._txtRangoInfSegCliente.Text = value; }
        }

        public string RangoSuperior
        {
            get { return this._txtRangoSupSegCliente.Text; }
            set { this._txtRangoSupSegCliente.Text = value; }
        }

        public object Ordenamientos
        {
            get { return this._cbxOrdenamientoSegCliente.DataContext; }
            set { this._cbxOrdenamientoSegCliente.DataContext = value; }
        }

        public object Ordenamiento
        {
            get { return this._cbxOrdenamientoSegCliente.SelectedItem; }
            set { this._cbxOrdenamientoSegCliente.SelectedItem = value; }
        }

        public string IdAsociado
        {
            get { return this._txtAsociadoSegCliente.Text; }
            set { this._txtAsociadoSegCliente.Text = value; }
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
            get { return this._cbxEjeXSegClientes.DataContext; }
            set { this._cbxEjeXSegClientes.DataContext = value; }
        }

        public object EjeXSeleccionado
        {
            get { return this._cbxEjeXSegClientes.SelectedItem; }
            set { this._cbxEjeXSegClientes.SelectedItem = value; }
        }

        public object CamposEjeYPivot
        {
            get { return this._cbxEjeYSegClientes.DataContext; }
            set { this._cbxEjeYSegClientes.DataContext = value; }
        }

        public object EjeYSeleccionado
        {
            get { return this._cbxEjeYSegClientes.SelectedItem; }
            set { this._cbxEjeYSegClientes.SelectedItem = value; }
        }

        public object CamposEjeZPivot
        {
            get { return this._cbxEjeZSegClientes.DataContext; }
            set { this._cbxEjeZSegClientes.DataContext = value; }
        }

        public object EjeZSeleccionado
        {
            get { return this._cbxEjeZSegClientes.SelectedItem; }
            set { this._cbxEjeZSegClientes.SelectedItem = value; }
        }

        public string TotalUSD
        {
            get { return this._txtTotalDolares.Text; }
            set { this._txtTotalDolares.Text = value; }
        }

        public string TotalBSF
        {
            get { return this._txtTotalBolivares.Text; }
            set { this._txtTotalBolivares.Text = value; }
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

        public string TotalReportes
        {
            get { return this._txtTotalReportes.Text; }
            set { this._txtTotalReportes.Text = value; }
        }

        public string TotalOtrosDptos
        {
            get { return this._txtTotalOtrosDptos.Text; }
            set { this._txtTotalOtrosDptos.Text = value; }
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

        private void _txtAsociadoSegCliente_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(true);
        }

        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado();
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

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Cuando se genera la tabla pivot y al seleccionar aqui se va a las gestiones de cobranza
        }

        //Evento para generar la data cruda a partir de un filtro
        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.ObtenerResumenGeneralSaldos();
            validarCamposVacios();
        }

        private void _btnVerPivot_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaDatosPivot();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
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
                this._txtAsociadoSegCliente.Visibility = Visibility.Collapsed;
                this._txtIdAsociado.Visibility = Visibility.Visible;
                this._txtNombreAsociado.Visibility = Visibility.Visible;
                this._lblIdAsociado.Visibility = Visibility.Visible;
                this._lblNombreAsociado.Visibility = Visibility.Visible;
                this._lstAsociados.Visibility = Visibility.Visible;
                this._btnConsultarAsociado.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtAsociadoSegCliente.Visibility = Visibility.Visible;
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

            if ((this._cbxMonedaSegClientes.SelectedIndex != 0) && (this._cbxMonedaSegClientes.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxMonedaSegClientes.Focus();
            }

            //if ((this._cbxMesSegCliente.SelectedIndex != 0) && (this._cbxMesSegCliente.SelectedIndex != -1))
            //{
            //    todosCamposVacios = false;
            //    this._cbxMesSegCliente.Focus();
            //}

            if ((this._cbxOrdenamientoSegCliente.SelectedIndex != 0) && (this._cbxOrdenamientoSegCliente.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxOrdenamientoSegCliente.Focus();
            }

            //if (!this._txtAnoSegCliente.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtAnoSegCliente.Focus();
            //}

            if (!this._txtIdAsociado.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdAsociado.Focus();
            }
            

            if (todosCamposVacios)
            {
                this._cbxMonedaSegClientes.Focus();
            }
        }

        public void ActivarEjesPivot()
        {
            this._lblEjeXSegClientes.IsEnabled = true;
            this._lblEjeYSegClientes.IsEnabled = true;
            this._lblEjeZSegClientes.IsEnabled = true;
            this._cbxEjeXSegClientes.IsEnabled = true;
            this._cbxEjeYSegClientes.IsEnabled = true;
            this._cbxEjeZSegClientes.IsEnabled = true;
            this._btnVerPivot.IsEnabled = true;
            this._btnConsultar.IsDefault = false;
            this._btnVerPivot.IsDefault = true;
            this._btnVerPivot.Focus();
            //this._presentador.PredeterminarEjes();
        }

        public void DesactivarEjesPivot()
        {
            this._lblEjeXSegClientes.IsEnabled = false;
            this._lblEjeYSegClientes.IsEnabled = false;
            this._lblEjeZSegClientes.IsEnabled = false;
            this._cbxEjeXSegClientes.IsEnabled = false;
            this._cbxEjeYSegClientes.IsEnabled = false;
            this._cbxEjeZSegClientes.IsEnabled = false;
            this._btnVerPivot.IsEnabled = false;
            this._btnConsultar.IsDefault = true;
            this._btnVerPivot.IsDefault = false;
            this._btnVerPivot.Focus();
        }

        #endregion

        

        

        

        
    }
}
