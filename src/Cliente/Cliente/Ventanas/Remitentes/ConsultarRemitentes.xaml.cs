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
using Trascend.Bolet.Cliente.Contratos.Remitentes;
using Trascend.Bolet.Cliente.Presentadores.Remitentes;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Remitentes
{
    /// <summary>
    /// Interaction logic for ConsultarRemitentes.xaml
    /// </summary>
    public partial class ConsultarRemitentes : Page, IConsultarRemitentes
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarRemitentes _presentador;
        private bool _cargada;

        #region IConsultarRemitentes

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

        public object RemitenteSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
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

        public object RemitenteFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public object Paises
        {
            get { return this._cbxPais.DataContext; }
            set { this._cbxPais.DataContext = value; }
        }

        public object Pais
        {
            get { return this._cbxPais.SelectedItem; }
            set { this._cbxPais.SelectedItem = value; }
        }

        public char TipoRemitente
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxTipoRemitente.Text))
                    return ((string)this._cbxTipoRemitente.Text)[0];
                else
                    return ' ';
            }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        #endregion

        public ConsultarRemitentes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarRemitentes(this);

        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            this.validarCamposVacios();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarRemitente();
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

            if (!this._txtDescripcion.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDescripcion.Focus();
            }

            if (!this._txtDireccion.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDireccion.Focus();
            }

            if ((this._cbxTipoRemitente.SelectedIndex != 0) && (this._cbxTipoRemitente.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTipoRemitente.Focus();
            }

            if ((this._cbxPais.SelectedIndex != 0) && (this._cbxPais.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxPais.Focus();
            }

            if (!this._txtCiudad.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtCiudad.Focus();
            }

            if (!this._txtEstado.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtEstado.Focus();
            }

            if (!this._txtTelefono.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtTelefono.Focus();
            }

            if (!this._txtFax.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtFax.Focus();
            }

            if (todosCamposVacios)
            {
                this._txtId.Focus();
            }
        }
    }
}
