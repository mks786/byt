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
using Trascend.Bolet.Cliente.Contratos.Resoluciones;
using Trascend.Bolet.Cliente.Presentadores.Resoluciones;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Resoluciones
{
    /// <summary>
    /// Interaction logic for ConsultarResoluciones.xaml
    /// </summary>
    public partial class ConsultarResoluciones : Page, IConsultarResoluciones
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarResoluciones _presentador;
        private bool _cargada;

        #region IConsultarResoluciones

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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object ResolucionSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public string FechaResolucion
        {
            get { return this._dpkFechaResolucion.SelectedDate.ToString(); }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public ListView ListaResultados
        {
            get { return this._lstResultados; }
            set { this._lstResultados = value; }
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

        public string Volumen
        {
            get { return this._txtVolumen.Text; }
        }

        public string Pagina
        {
            get { return this._txtPagina.Text; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        public ConsultarResoluciones()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarResoluciones(this);

        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            this._dpkFechaResolucion.Text = string.Empty;
            validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarResolucion();
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


        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;
            if (!this._txtId.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtId.Focus();
            }

            if (!this._dpkFechaResolucion.SelectedDate.ToString().Equals(""))
            {
                todosCamposVacios = false;
                this._dpkFechaResolucion.Focus();
            }

            if ((this._cbxBoletin.SelectedIndex != 0) && (this._cbxBoletin.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxBoletin.Focus();
            }

            if (!this._txtVolumen.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtVolumen.Focus();
            }

            if (!this._txtPagina.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtPagina.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }
    }
}
