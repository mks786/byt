using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Recordatorios;
using Trascend.Bolet.Cliente.Presentadores.Recordatorios;
using System;
using System.Collections.Generic;

namespace Trascend.Bolet.Cliente.Ventanas.Recordatorios
{
    /// <summary>
    /// Interaction logic for ConsultarRecordatorios.xaml
    /// </summary>
    public partial class ConsultarRecordatorios : Page, IConsultarRecordatorios
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarRecordatorios _presentador;
        private bool _cargada;

        #region IConsultarRecordatorios

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object Resultado
        {
            get { return this._lstResultados.SelectedItems; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string MesFiltro
        {
            get { return this._txtMes.Text; }
            set { this._txtMes.Text = value; }
        }

        public string AnoFiltro
        {
            get { return this._txtAno.Text; }
            set { this._txtAno.Text = value; }
        }

        public bool? AutomaticoFiltro
        {
            get { return this._chkAutomatico.IsChecked; }
            set { this._chkAutomatico.IsChecked = value; }
        }

        public bool? EmailFiltro
        {
            get { return this._chkEmailPorEnviar.IsChecked; }
        }

        public bool? FaxFiltro
        {
            get { return this._chkFaxPorEnviar.IsChecked; }
        }

        public bool? TodosFiltro
        {
            get { return this._chkTodos.IsChecked; }
        }

        public DateTime? FechaDesdeFiltro
        {
            get { return this._dpkFechaDesde.SelectedDate; }
            set { this._dpkFechaDesde.SelectedDate = value; }
        }

        public DateTime? FechaHastaFiltro
        {
            get { return this._dpkFechaHasta.SelectedDate; }
            set { this._dpkFechaHasta.SelectedDate = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._lstResultados.Focus();
        }

        public object Recordatorios
        {
            get { return this._cbxRecordatorio.DataContext; }
            set { this._cbxRecordatorio.DataContext = value; }
        }

        public object Recordatorio
        {
            get { return this._cbxRecordatorio.SelectedItem; }
            set { this._cbxRecordatorio.SelectedItem = value; }
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
            get { return this._lstResultados; }
            set { this._lstResultados = value; }
        }

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show(mensaje, "Operación Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool MensajeAlerta(string mensaje)
        {
            bool retorno = false;

            if (MessageBoxResult.Yes == MessageBox.Show(mensaje,
                "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question))
                retorno = true;

            return retorno;
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        public object TiposBusqueda
        {
            get { return this._cbxTipoBusqueda.DataContext; }
            set
            {
                this._cbxTipoBusqueda.DataContext = value;
                //this._cbxTipoBusqueda.SelectedIndex = 1;
            }
        }


        public object TipoBusqueda
        {
            get { return this._cbxTipoBusqueda.SelectedItem; }
            set { this._cbxTipoBusqueda.SelectedItem = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarRecordatorios()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarRecordatorios(this);
        }

        public void LimpiarFiltros()
        {
            this._chkEmailPorEnviar.IsChecked = false;
            this._chkFaxPorEnviar.IsChecked = false;

            this._dpkFechaDesde.Text = string.Empty;
            this._dpkFechaHasta.Text = string.Empty;

            this._txtAno.Text = string.Empty;
            this._txtMes.Text = string.Empty;
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

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

        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
        }

        private void _cbxRecordatorio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EstaCargada)
            {
                this._presentador.ActualizarMarcasRecordatorio();
            }
        }

        public void GestionarEnableChecksFiltro(bool value)
        {
            this._chkEmailPorEnviar.IsEnabled = value;
            this._chkFaxPorEnviar.IsEnabled = value;
            this._chkTodos.IsChecked = !value;
        }

        private void GestionarCheckedChecksFiltro(bool value)
        {
            this._chkEmailPorEnviar.IsChecked = value;
            this._chkFaxPorEnviar.IsChecked = value;
        }

        private void _chkTodos_Click(object sender, RoutedEventArgs e)
        {
            if (this._chkTodos.IsChecked.Value)
            {
                GestionarEnableChecksFiltro(false);
                GestionarCheckedChecksFiltro(false);
            }
            else
                GestionarEnableChecksFiltro(true);
        }

        private void _btnGenerarInformacion_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GenerarInformacion();
        }

        private void _chkAutomatico_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ActualizarMarcasRecordatorio();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        public void SeleccionarTodos(int longitud)
        {
            ////foreach (ListViewItem item in ((ListView)_lstResultados.DataContext).Items)
            ////{
            ////    item.IsSelected = true;
            ////}
            //this._lstResultados = ((ListView)this._lstResultados.DataContext).SelectAll();

        }

        private void _cbxTipoBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this._presentador.RecordatoriosCargados())
                this._presentador.Consultar();
        }


        public bool HayCamposVacios()
        {
            bool retorno = true;

            if (!this._txtAno.Text.Equals(string.Empty))
                retorno = false;

            if (!this._txtMes.Text.Equals(string.Empty))
                retorno = false;

            if (!this._dpkFechaDesde.Text.Equals(string.Empty))
                retorno = false;

            if (!this._dpkFechaHasta.Text.Equals(string.Empty))
                retorno = false;

            return retorno;
        }
    }
}
