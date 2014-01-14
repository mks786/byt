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
using Trascend.Bolet.Cliente.Contratos.Patentes;
using Trascend.Bolet.Cliente.Presentadores.Patentes;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Patentes
{
    /// <summary>
    /// Lógica de interacción para ListaInteresadosPatente.xaml
    /// </summary>
    public partial class ListaInteresadosPatente : Page, IListaInteresadosPatente
    {

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaInteresadosPatente _presentador;
        private bool _cargada;

        /// <summary>
        /// Constructor por defecto que recibe una patente
        /// </summary>
        /// <param name="patente">Patente consultada</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public ListaInteresadosPatente(object patente, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaInteresadosPatente(this,patente,ventanaPadre,null);
        }

        public ListaInteresadosPatente(object patente, object ventanaPadre, object ventanaConsultarPatentes)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaInteresadosPatente(this, patente, ventanaPadre, ventanaConsultarPatentes);
        }

        #region IListaInteresadosPatente

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public object InteresadosDePatente
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
            //this._presentador.RegresarVentanaPadre();
            this._presentador.IrVentanaGestionarPatente();
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrVerInteresadoSeleccionado();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrGestionarInteresadosDePatente();
        }

        #endregion

        


        #region Metodos

        #endregion

    }
}
