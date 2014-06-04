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
using Trascend.Bolet.Cliente.Contratos.CadenaDeCambio;
using Trascend.Bolet.Cliente.Presentadores.CadenaDeCambio;

namespace Trascend.Bolet.Cliente.Ventanas.CadenaDeCambio
{
    /// <summary>
    /// Lógica de interacción para ConsultarCadenasDeCambios.xaml
    /// </summary>
    public partial class ConsultarCadenasDeCambios : Page, IConsultarCadenasDeCambios 
    {

        private bool _cargada;
        private PresentadorConsultarCadenasDeCambios _presentador;

        public ConsultarCadenasDeCambios()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarCadenasDeCambios(this);
        }


        #region IConsultarCadenasDeCambios

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object CadenaDeCambios
        {
            get { return this._spFiltro.DataContext; }
            set { this._spFiltro.DataContext = value; }
        }

        public object TiposCadenasDeCambios
        {
            get { return this._cbxTipoCadenaCambio.DataContext; }
            set { this._cbxTipoCadenaCambio.DataContext = value; }
        }

        public object TipoCadenaDeCambios
        {
            get { return this._cbxTipoCadenaCambio.SelectedItem; }
            set { this._cbxTipoCadenaCambio.SelectedItem = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object CadenaCambioSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string IdCadenaCambios
        {
            get { return this._txtIdCadenaCambios.Text; }
            set { this._txtIdCadenaCambios.Text = value; }
        }

        public string CodigoOperacionCadenaCambios
        {
            get { return this._txtCodOperacionCadenaCambios.Text; }
            set { this._txtCodOperacionCadenaCambios.Text = value; }
        }

        public string FechaCadenaCambios
        {
            get { return this._txtFechaCadenaCambios.Text; }
            set { this._txtFechaCadenaCambios.Text = value; }
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
        }

        private void _btnConsultarCadenaCambio_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultarCadenaCambio.Focus();
            this._presentador.Consultar();
            validarCamposVacios();
        }


        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrVerCadenaDeCambios();
        }

        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
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

        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;

            if ((this._cbxTipoCadenaCambio.SelectedIndex != 0) && (this._cbxTipoCadenaCambio.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTipoCadenaCambio.Focus();
            }

            if (!this._txtIdCadenaCambios.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtIdCadenaCambios.Focus();
            }

            if (!this._txtCodOperacionCadenaCambios.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtCodOperacionCadenaCambios.Focus();
            }

            if (this._txtFechaCadenaCambios.Text != null)
            {
                if (!this._txtFechaCadenaCambios.Text.Equals(""))
                {
                    todosCamposVacios = false;
                    this._txtFechaCadenaCambios.Focus();
                } 
            }

            if (todosCamposVacios)
            {
                this._txtIdCadenaCambios.Focus();
            }

        }

        #endregion

        

        
    }
}
