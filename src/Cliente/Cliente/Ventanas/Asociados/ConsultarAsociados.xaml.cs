﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Presentadores.Asociados;

namespace Trascend.Bolet.Cliente.Ventanas.Asociados
{
    /// <summary>
    /// Interaction logic for ConsultarObjetos.xaml
    /// </summary>
    public partial class ConsultarAsociados : Page, IConsultarAsociados
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarAsociados _presentador;
        private bool _cargada;

        #region IConsultarAsociados

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

        public string Id
        {
            get { return this._txtId.Text; }
            set { this._txtId.Text = value; }
        }

        public string NombreAsociado
        {
            get { return this._txtNombre.Text; }
            set { this._txtNombre.Text = value; }
        }

        public string DomicilioAsociado
        {
            get { return this._txtDomicilio.Text; }
            set { this._txtDomicilio.Text = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object AsociadoFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public object AsociadoSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }

        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }


        public object Pais
        {
            get { return this._cbxPais.SelectedItem; }
            set { this._cbxPais.SelectedItem = value; }
        }

        public object Paises
        {
            get { return this._cbxPais.DataContext; }
            set { this._cbxPais.DataContext = value; }
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

        public object Moneda
        {
            get { return this._cbxMoneda.SelectedItem; }
            set { this._cbxMoneda.SelectedItem = value; }
        }

        public object Monedas
        {
            get { return this._cbxMoneda.DataContext; }
            set { this._cbxMoneda.DataContext = value; }
        }

        public object Descuento
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public object Descuentos
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public object TipoCliente
        {
            get { return this._cbxTipoCliente.SelectedItem; }
            set { this._cbxTipoCliente.SelectedItem = value; }
        }

        public object TiposClientes
        {
            get { return this._cbxTipoCliente.DataContext; }
            set { this._cbxTipoCliente.DataContext = value; }
        }

        public object Etiqueta
        {
            get { return this._cbxEtiqueta.SelectedItem; }
            set { this._cbxEtiqueta.SelectedItem = value; }
        }

        public object Etiquetas
        {
            get { return this._cbxEtiqueta.DataContext; }
            set { this._cbxEtiqueta.DataContext = value; }
        }

        public object DetallePago
        {
            get { return this._cbxDetallePago.SelectedItem; }
            set { this._cbxDetallePago.SelectedItem = value; }
        }

        public object DetallesPagos
        {
            get { return this._cbxDetallePago.DataContext; }
            set { this._cbxDetallePago.DataContext = value; }
        }

        public object Tarifa
        {
            get { return this._cbxTarifa.SelectedItem; }
            set { this._cbxTarifa.SelectedItem = value; }
        }

        public object Tarifas
        {
            get { return this._cbxTarifa.DataContext; }
            set { this._cbxTarifa.DataContext = value; }
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

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        public ConsultarAsociados()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarAsociados(this);

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

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarAsociado();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;
            if (!this._txtId.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtId.Focus();
            }

            if (!this._txtNombre.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNombre.Focus();
            }

            if (!this._txtDomicilio.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDomicilio.Focus();
            }

            if ((this._cbxTipoPersona.SelectedIndex != 0) && (this._cbxTipoPersona.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTipoPersona.Focus();
            }

            if ((this._cbxPais.SelectedIndex != 0) && (this._cbxPais.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxPais.Focus();
            }

            if ((this._cbxIdioma.SelectedIndex != 0) && (this._cbxIdioma.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxIdioma.Focus();
            }

            if ((this._cbxMoneda.SelectedIndex != 0) && (this._cbxMoneda.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxMoneda.Focus();
            }

            if ((this._cbxTipoCliente.SelectedIndex != 0) && (this._cbxTipoCliente.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTipoCliente.Focus();
            }

            if ((this._cbxTarifa.SelectedIndex != 0) && (this._cbxTarifa.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTarifa.Focus();
            }

            if ((this._cbxEtiqueta.SelectedIndex != 0) && (this._cbxEtiqueta.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxEtiqueta.Focus();
            }

            if ((this._cbxDetallePago.SelectedIndex != 0) && (this._cbxDetallePago.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxDetallePago.Focus();
            }

            if (todosCamposVacios)
            {
                this._txtId.Focus();
            }
        }
       
        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }
    }
}
