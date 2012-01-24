using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Poderes;
using Trascend.Bolet.Cliente.Presentadores.Poderes;

namespace Trascend.Bolet.Cliente.Ventanas.Poderes
{
    /// <summary>
    /// Interaction logic for ConsultarTodosPoder.xaml
    /// </summary>
    public partial class ConsultarPoderes : Page, IConsultarPoderes
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarPoderes _presentador;
        private bool _cargada;

        #region IConsultarPoderes



        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public string NumPoder
        {
            get { return this._txtNumPoder.Text; }
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

        public object Interesados
        {
            get { return this._lstInteresados.DataContext; }
            set { this._lstInteresados.DataContext = value; }
        }

        public object Interesado
        {
            get { return this._lstInteresados.SelectedItem; }
            set { this._lstInteresados.SelectedItem = value; }
        }

        public string Facultad
        {
            get { return this._txtFacultad.Text; }
        }

        public string Anexo
        {
            get { return this._txtAnexo.Text; }
        }

        public string Observaciones
        {
            get { return this._txtObservaciones.Text; }
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

        public object PoderSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
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

        public string IdInteresadoFiltrar
        {
            get { return this._txtIdInteresado.Text; }
        }
        public string NombreInteresadoFiltrar
        {
            get { return this._txtNombreInteresado.Text; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }


        #endregion

        /// <summary>
        /// Constructor predeterminado
        /// </summary>
        public ConsultarPoderes()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarPoderes(this);
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

        private void _btnConsultarInteresado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarInteresado();
        }

        private void _btnConsultarInteresadoFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = false;
            this._btnConsultarInteresado.IsDefault = true;
        }

        private void _btnConsultarFocus(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.IsDefault = true;
            this._btnConsultarInteresado.IsDefault = false;
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

            if (!this._txtNumPoder.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNumPoder.Focus();
            }

            if ((this._cbxBoletin.SelectedIndex != 0) && (this._cbxBoletin.SelectedIndex != -1))
            {
                todosCamposVacios = false;
                this._cbxBoletin.Focus();
            }

            if (!this._txtFacultad.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtFacultad.Focus();
            }

            if (!this._txtAnexo.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtAnexo.Focus();
            }

            if (!this._txtObservaciones.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtObservaciones.Focus();
            }

            if (todosCamposVacios)
                this._txtId.Focus();
        }

    }
}
