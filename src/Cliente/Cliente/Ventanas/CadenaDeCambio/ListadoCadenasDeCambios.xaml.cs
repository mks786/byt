using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.CadenaDeCambio;
using Trascend.Bolet.Cliente.Presentadores.CadenaDeCambio;

namespace Trascend.Bolet.Cliente.Ventanas.CadenaDeCambio
{
    /// <summary>
    /// Lógica de interacción para ListadoCadenasDeCambios.xaml
    /// </summary>
    public partial class ListadoCadenasDeCambios : Page, IListadoCadenasDeCambios
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListadoCadenasDeCambios _presentador;
        private bool _cargada;
        

        public ListadoCadenasDeCambios(object marcaOPatente, string tipoCadenaCambio, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListadoCadenasDeCambios(this, marcaOPatente, tipoCadenaCambio, ventanaPadre);
        }


        #region IListadoCadenasDeCambios

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
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

        public object CadenasDeCambios
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
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

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }
        #endregion

        

        #region Metodos

        #endregion
    }
}
