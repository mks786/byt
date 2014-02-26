using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Asociados;
using Trascend.Bolet.Cliente.Presentadores.Asociados;

namespace Trascend.Bolet.Cliente.Ventanas.Asociados
{
    /// <summary>
    /// Interaction logic for ListaAuditorias.xaml
    /// </summary>
    public partial class ListaDatosTransferencias : Page, IListaDatosTransferencia
    {
        
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaDatosTransferencia _presentador;
        private bool _cargada;


        #region IListaDatosTransferencia

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object DatosTransferencias
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object DatosTransferenciaSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
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

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion

        /// <summary>
        /// Constructor predeterminado que recibe solo un Asociado
        /// </summary>
        /// <param name="asociado">Asociado</param>
        public ListaDatosTransferencias(object asociado)
        {
            InitializeComponent();
            this._cargada= false;
            this._presentador = new PresentadorListaDatosTransferencia(this, asociado);

        }

        /// <summary>
        /// Constructor predeterminado que recibe un asociado y una ventana padre
        /// </summary>
        /// <param name="asociado">Asociado</param>
        /// <param name="ventanaPadre">Ventana padre</param>
        public ListaDatosTransferencias(object asociado, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaDatosTransferencia(this, asociado, ventanaPadre);

        }

        /// <summary>
        /// Constructor predeterminado que se llama desde la ventana de listado de Datos de Consolidacion para cambiar los datos de 
        /// transferencia de un Asociado Internacional. 
        /// </summary>
        /// <param name="asociado">Asociado Internacional</param>
        /// <param name="datosConsolidados">Datos de consolidacion</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        /// <param name="consolida">Bandera para indicar si viene de la ventana de consolidacion</param>
        /// <param name="soloVerConsolidado">Bandera que indica que viene de la ventana que muestra los datos de consolidacion mas no lo ejecuta</param>
        /// <param name="ventanaFacAprobadas">Ventana anterior a la ventana de Consolidacion</param>
        public ListaDatosTransferencias(object asociado, object datosConsolidados, object ventanaPadre, bool consolida, bool soloVerConsolidado, object ventanaFacAprobadas)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaDatosTransferencia(this, asociado, datosConsolidados, ventanaPadre,consolida, soloVerConsolidado,ventanaFacAprobadas);

        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Regresar();
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrAgregarDatosTransferencia();
        }

        private void _lstResultados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarDatosTransferencia();
        }


        public void PresentarBotonSeleccionarDatos()
        {
            this._btnSeleccionarDatosTransferencia.Visibility = System.Windows.Visibility.Visible;
        }

        private void _btnSeleccionarDatosTransferencia_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.SeleccionarDatosTransferenciaConsolidacion();
        }
    }
}
