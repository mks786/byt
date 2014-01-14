using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Memorias;
using Trascend.Bolet.Cliente.Presentadores.Memorias;

namespace Trascend.Bolet.Cliente.Ventanas.Patentes
{
    /// <summary>
    /// Interaction logic for ConsultarObjetos.xaml
    /// </summary>
    public partial class ListaMemorias : Page, IListaMemorias
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaMemorias _presentador;
        private bool _cargada;

        #region IConsultarMemorias

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            //this._txtId.Focus();
            this._btnCancelar.Focus();
        }


        public object MemoriaSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
        }

        public object Memorias
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        //public string Id
        //{
        //    get { return this._txtId.Text; }
        //    set { this._txtId.Text = value; }
        //}

        //public object TipoMensaje
        //{
        //    get { return this._cbxTipo.SelectedItem; }
        //    set { this._cbxTipo.SelectedItem = value; }
        //}

        //public object TiposMensajes
        //{
        //    get { return this._cbxTipo.DataContext; }
        //    set { this._cbxTipo.DataContext = value; }
        //}

        //public object FormatoDocumento
        //{
        //    get { return this._cbxTipoFinal.SelectedItem; }
        //    set { this._cbxTipoFinal.SelectedItem = value; }
        //}

        //public object FormatosDocumentos
        //{
        //    get { return this._cbxTipoFinal.DataContext; }
        //    set { this._cbxTipoFinal.DataContext = value; }
        //}

        //public string IdMemoria
        //{
        //    get { return this._txtId.Text; }
        //}


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

        public object ListaResultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        public ListaMemorias(object patente)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaMemorias(this, patente, null);

        }

        public ListaMemorias(object patente, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaMemorias(this, patente, ventanaPadre);

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

        //private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        //{
        //    //this._btnConsultar.Focus();
        //    //this._presentador.Consultar();
        //}

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //this._presentador.IrConsultarMemoria();
            this._presentador.AbrirArchivoMemoria();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }


        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (opcion == 2)
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        //private void validarCamposVacios()
        //{
            //bool todosCamposVacios = true;
            //if (!this._txtId.Text.Equals(""))
            //{
            //    todosCamposVacios = false;
            //    this._txtId.Focus();
            //}

            //if (todosCamposVacios)
            //    this._txtId.Focus();
        //}

        //private void _btnNuevaMemoria_Click(object sender, RoutedEventArgs e)
        //{
        //    //this._presentador.IrAgregarMemoria();
        //}
    }
}
