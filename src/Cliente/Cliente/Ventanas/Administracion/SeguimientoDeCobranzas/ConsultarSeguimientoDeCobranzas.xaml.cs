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
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeCobranzas;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoDeCobranzas;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoDeCobranzas
{
    /// <summary>
    /// Lógica de interacción para ConsultarSeguimientoDeCobranzas.xaml
    /// </summary>
    public partial class ConsultarSeguimientoDeCobranzas : Page, IConsultarSeguimientoDeCobranzas
    {

        private bool _cargada;
        private PresentadorConsultarSeguimientoDeCobranzas _presentador;

        
        public ConsultarSeguimientoDeCobranzas()
        {
            InitializeComponent();
            this._cargada = false;
            _presentador = new PresentadorConsultarSeguimientoDeCobranzas(this);
        }

        #region IConsultarSeguimientoDeCobranzas

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        public object FiltroDeSeguimientoCobranzas
        {
            get { return this._spFiltro.DataContext; }
            set { this._spFiltro.DataContext = value; }
        }


        #region Filtros para consulta de Resumen General

        public object Monedas
        {
            get { return this._cbxMonedaSegCobranzas.DataContext; }
            set { this._cbxMonedaSegCobranzas.DataContext = value; }
        }

        public object Moneda
        {
            get { return this._cbxMonedaSegCobranzas.SelectedItem; }
            set { this._cbxMonedaSegCobranzas.SelectedItem = value; }
        }

        public object Usuarios
        {
            get { return this._cbxUsuarioSegCobranza.DataContext; }
            set { this._cbxUsuarioSegCobranza.DataContext = value; }
        }

        public object Usuario
        {
            get { return this._cbxUsuarioSegCobranza.SelectedItem; }
            set { this._cbxUsuarioSegCobranza.SelectedItem = value; }
        }

        public object Medios
        {
            get { return this._cbxMedioSegCobranza.DataContext; }
            set { this._cbxMedioSegCobranza.DataContext = value; }
        }

        public object Medio
        {
            get { return this._cbxMedioSegCobranza.SelectedItem; }
            set { this._cbxMedioSegCobranza.SelectedItem = value; }
        }

        public object Ordenamientos
        {
            get { return this._cbxOrdenamientoSegCobranzas.DataContext; }
            set { this._cbxOrdenamientoSegCobranzas.DataContext = value; }
        }

        public object Ordenamiento
        {
            get { return this._cbxOrdenamientoSegCobranzas.SelectedItem; }
            set { this._cbxOrdenamientoSegCobranzas.SelectedItem = value; }
        }

        #endregion


        #region Informacion de Asociado

        public string IdAsociado
        {
            get { return this._txtAsociadoSegCobranza.Text; }
            set { this._txtAsociadoSegCobranza.Text = value; }
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

        #endregion

        #region Campos Pivot

        public object CamposEjeXPivot
        {
            get { return this._cbxEjeXSegCobranzas.DataContext; }
            set { this._cbxEjeXSegCobranzas.DataContext = value; }
        }

        public object EjeXSeleccionado
        {
            get { return this._cbxEjeXSegCobranzas.SelectedItem; }
            set { this._cbxEjeXSegCobranzas.SelectedItem = value; }
        }

        public object CamposEjeYPivot
        {
            get { return this._cbxEjeYSegCobranzas.DataContext; }
            set { this._cbxEjeYSegCobranzas.DataContext = value; }
        }

        public object EjeYSeleccionado
        {
            get { return this._cbxEjeYSegCobranzas.SelectedItem; }
            set { this._cbxEjeYSegCobranzas.SelectedItem = value; }
        }

        public object CamposEjeZPivot
        {
            get { return this._cbxEjeZSegCobranzas.DataContext; }
            set { this._cbxEjeZSegCobranzas.DataContext = value; }
        }

        public object EjeZSeleccionado
        {
            get { return this._cbxEjeZSegCobranzas.SelectedItem; }
            set { this._cbxEjeZSegCobranzas.SelectedItem = value; }
        }

        #endregion

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
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

        private void _txtAsociadoSegCobranza_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadFiltroAsociado(true);
        }
               

        private void _btnConsultarAsociadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultarAsociado.IsDefault = true;
        }


        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado();
        }


        private void _btnConsultarResumenGeneralGestiones_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultarResumenGeneralGestiones.Focus();
            this._presentador.GenerarResumenGeneralDeGestiones();
            validarCamposVacios();
            
        }
        
        private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociado())
                GestionarVisibilidadFiltroAsociado(false);
        }

        private void _btnVerPivot_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrListaDatosPivot();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
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
                this._txtAsociadoSegCobranza.Visibility = Visibility.Collapsed;
                this._txtIdAsociado.Visibility = Visibility.Visible;
                this._txtNombreAsociado.Visibility = Visibility.Visible;
                this._lblIdAsociado.Visibility = Visibility.Visible;
                this._lblNombreAsociado.Visibility = Visibility.Visible;
                this._lstAsociados.Visibility = Visibility.Visible;
                this._btnConsultarAsociado.Visibility = Visibility.Visible;
            }
            else
            {
                this._txtAsociadoSegCobranza.Visibility = Visibility.Visible;
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

            
            if ((this._cbxMonedaSegCobranzas.SelectedIndex != 0) && (this._cbxMonedaSegCobranzas.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxMonedaSegCobranzas.Focus();
            }

            
            if ((this._cbxUsuarioSegCobranza.SelectedIndex != 0) && (this._cbxUsuarioSegCobranza.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxUsuarioSegCobranza.Focus();
            }

            
            if ((this._cbxMedioSegCobranza.SelectedIndex != 0) && (this._cbxMedioSegCobranza.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxMedioSegCobranza.Focus();
            }

            
            if ((this._cbxOrdenamientoSegCobranzas.SelectedIndex != 0) && (this._cbxOrdenamientoSegCobranzas.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxOrdenamientoSegCobranzas.Focus();
            }

            
            if (!this._txtIdAsociado.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdAsociado.Focus();
            }


            if (todosCamposVacios)
            {
                this._cbxMonedaSegCobranzas.Focus();
            }

        }


        public void ActivarEjesPivot()
        {
            this._lblEjeXSegCobranzas.IsEnabled = true;
            this._lblEjeYSegCobranzas.IsEnabled = true;
            this._lblEjeZSegCobranzas.IsEnabled = true;
            this._cbxEjeXSegCobranzas.IsEnabled = true;
            this._cbxEjeYSegCobranzas.IsEnabled = true;
            this._cbxEjeZSegCobranzas.IsEnabled = true;
            this._btnVerPivot.IsEnabled = true;
            this._btnConsultarResumenGeneralGestiones.IsDefault = false;
            this._btnVerPivot.IsDefault = true;
            this._btnVerPivot.Focus();
            this._presentador.PredeterminarEjes();
        }

        public void DesactivarEjesPivot()
        {
            this._lblEjeXSegCobranzas.IsEnabled = false;
            this._lblEjeYSegCobranzas.IsEnabled = false;
            this._lblEjeZSegCobranzas.IsEnabled = false;
            this._cbxEjeXSegCobranzas.IsEnabled = false;
            this._cbxEjeYSegCobranzas.IsEnabled = false;
            this._cbxEjeZSegCobranzas.IsEnabled = false;
            this._btnVerPivot.IsEnabled = false;
            this._btnConsultarResumenGeneralGestiones.IsDefault = true;
            this._btnVerPivot.IsDefault = false;
            this._btnVerPivot.Focus();
        }

        #endregion
    }
}
