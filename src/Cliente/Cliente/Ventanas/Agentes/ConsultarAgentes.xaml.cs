using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Agentes;
using Trascend.Bolet.Cliente.Presentadores.Agentes;

namespace Trascend.Bolet.Cliente.Ventanas.Agentes
{
    /// <summary>
    /// Interaction logic for ConsultarObjetos.xaml
    /// </summary>
    public partial class ConsultarAgentes : Page, IConsultarAgentes
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarAgentes _presentador;
        private bool _cargada;

        #region IConsultarAgentes

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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object AgenteFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public object AgenteSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }

        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object EstadosCivil
        {

            get { return this._cbxEstadoCivil.DataContext; }
            set { this._cbxEstadoCivil.DataContext = value; }
        }

        public object EstadoCivil
        {

            get { return this._cbxEstadoCivil.SelectedItem; }
            set { this._cbxEstadoCivil.SelectedItem = value; }
        }

        public object Sexo
        {
            get { return this._cbxSexo.SelectedItem; }
            set { this._cbxSexo.SelectedItem = value; }
        }

        public object Sexos
        {
            get { return this._cbxSexo.DataContext; }
            set { this._cbxSexo.DataContext = value; }
        }

        #endregion

        public ConsultarAgentes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarAgentes(this);

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
            this._presentador.IrConsultarAgente();
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

            if (!this._txtTelefono.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtTelefono.Focus();
            }

            if ((this._cbxEstadoCivil.SelectedIndex != 0) && (this._cbxEstadoCivil.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxEstadoCivil.Focus();
            }

            if ((this._cbxSexo.SelectedIndex != 0) && (this._cbxSexo.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxSexo.Focus();
            }

            if (!this._txtNumeroImpresoAbogado.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNumeroImpresoAbogado.Focus();
            }

            if (!this._txtNumeroAbogado.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNumeroAbogado.Focus();
            }

            if (!this._txtNumeroPropiedad.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNumeroPropiedad.Focus();
            }

            if (!this._txtCCI.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtCCI.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }
    }
}
