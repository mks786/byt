using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Interesados;
using Trascend.Bolet.Cliente.Presentadores.Interesados;

namespace Trascend.Bolet.Cliente.Ventanas.Interesados
{
    /// <summary>
    /// Interaction logic for ConsultarTodosPoder.xaml
    /// </summary>
    public partial class ConsultarInteresados : Page, IConsultarInteresados
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarInteresados _presentador;
        private bool _cargada;

        #region IConsultarPoderes

        public object InteresadoFiltrar
        {
            get { return this._splFiltro.DataContext; }
            set { this._splFiltro.DataContext = value; }
        }

        public object InteresadoSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }

        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
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

        public object Nacionalidades
        {
            get { return this._cbxNacionalidad.DataContext; }
            set { this._cbxNacionalidad.DataContext = value; }
        }

        public object Nacionalidad
        {
            get { return this._cbxNacionalidad.SelectedItem; }
            set { this._cbxNacionalidad.SelectedItem = value; }
        }

        public object Corporaciones
        {
            get { return this._cbxCorporacion.DataContext; }
            set { this._cbxCorporacion.DataContext = value; }
        }

        public object Corporacion
        {
            get { return this._cbxCorporacion.SelectedItem; }
            set { this._cbxCorporacion.SelectedItem = value; }
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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarInteresados()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarInteresados(this);
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
            this._presentador.IrConsultarPoder();
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

            if ((this._cbxTipoPersona.SelectedIndex != 0) && (this._cbxTipoPersona.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxTipoPersona.Focus();
            }

            if (!this._txtNombre.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNombre.Focus();
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

            if ((this._cbxPais.SelectedIndex != 0) && (this._cbxPais.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxPais.Focus();
            }

            if ((this._cbxNacionalidad.SelectedIndex != 0) && (this._cbxNacionalidad.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxNacionalidad.Focus();
            }

            if ((this._cbxCorporacion.SelectedIndex != 0) && (this._cbxCorporacion.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxCorporacion.Focus();
            }

            if (!this._txtCi.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtCi.Focus();
            }

            if (!this._txtRMercantil.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtRMercantil.Focus();
            }

            if (!this._txtRegMercantil.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtRegMercantil.Focus();
            }

            if (!this._txtDomicilio.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDomicilio.Focus();
            }

            if (!this._txtAlerta.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtAlerta.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }
    }
}
