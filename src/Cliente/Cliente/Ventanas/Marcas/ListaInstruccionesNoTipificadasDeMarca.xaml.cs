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
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Lógica de interacción para ListaInstruccionesNoTipificadasDeMarca.xaml
    /// </summary>
    public partial class ListaInstruccionesNoTipificadasDeMarca : Page, IListaInstruccionesNoTipificadasDeMarca
    {

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaInstruccionesNoTipificadasDeMarca _presentador;
        private bool _cargada;

        /// <summary>
        /// Constructor por defecto que recibe una marca y una ventana padre
        /// </summary>
        /// <param name="marca">Marca seleccionada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public ListaInstruccionesNoTipificadasDeMarca(object marca, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaInstruccionesNoTipificadasDeMarca(this, marca, ventanaPadre);
        }

        
        #region IListaInstruccionesNoTipificadasDeMarca

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Instrucciones
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object InstruccionSeleccionada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
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


        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        
        private void EventoIrGestionarInstruccionNoTipificadaMarca(object sender, EventArgs e)
        {
            if (sender.GetType().ToString().Equals("System.Windows.Controls.Button"))
            {
                this._presentador.IrGestionarInstruccionNoTipificada(true);
            }
            else
            {
                this._presentador.IrGestionarInstruccionNoTipificada(false);
            }
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        #endregion


        #region Metodos

        #endregion
    }
}
