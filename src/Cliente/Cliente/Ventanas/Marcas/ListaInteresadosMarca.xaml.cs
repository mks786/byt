using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
    /// Lógica de interacción para ListaInteresadosMarca.xaml
    /// </summary>
    public partial class ListaInteresadosMarca : Page, IListaInteresadosMarca
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaInteresadosMarca _presentador;
        private bool _cargada;

        #region IListaInteresadosMarca

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public object InteresadosDeMarca
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object InteresadoSeleccionado
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

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
        }

        #endregion


        #region Constructores

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="marca">Marca consultada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        /// <param name="ventanaConsultarMarcas">Ventana ConsultarMarcas</param>
        public ListaInteresadosMarca(object marca, object ventanaPadre, object ventanaConsultarMarcas)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaInteresadosMarca(this, marca, ventanaPadre, ventanaConsultarMarcas);
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
            this._presentador.IrVentanaConsultarMarca();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrVerInteresadoSeleccionado();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrGestionarInteresadosDeMarca();
        }

        #endregion

        #region Metodos

        #endregion
    }
}
