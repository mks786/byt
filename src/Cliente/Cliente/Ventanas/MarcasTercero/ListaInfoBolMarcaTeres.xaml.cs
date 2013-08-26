using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.MarcasTercero;
using Trascend.Bolet.Cliente.Presentadores.MarcasTercero;
using System;

namespace Trascend.Bolet.Cliente.Ventanas.MarcasTercero
{
    /// <summary>
    /// Interaction logic for ListaAuditorias.xaml
    /// </summary>
    public partial class ListaInfoBolMarcaTeres : Page, IListaInfoBolMarcaTeres
    {
        
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaInfoBolMarcaTeres _presentador;
        private bool _cargada;


        #region IListaContactos

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object InfoBolMarcaTeres
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object InfoBolMarcaTerSeleccionado
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
        /// Constructor por defecto que recibe una marca 
        /// </summary>
        /// <param name="marca">Marca para ver sus Infoboles</param>
        public ListaInfoBolMarcaTeres(object marca)
        {
            InitializeComponent();
            this._cargada= false;
            this._presentador = new PresentadorListaInfoBolMarcaTeres(this, marca);

        }


        /// <summary>
        /// Constructor por defecto qre recibe una ventana padre
        /// </summary>
        /// <param name="marca">Marca para ver sus Infoboles</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public ListaInfoBolMarcaTeres(object marca, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaInfoBolMarcaTeres(this, marca, ventanaPadre, null);

        }


        /// <summary>
        /// Constructor por defecto que recibe la marca, la ventana padre y la ventana anterior a la ventana padre 
        /// </summary>
        /// <param name="marca">Marca a consultar</param>
        /// <param name="ventanaPadre">Ventana IGestionarMarcaTercero que precede a esta ventana</param>
        /// <param name="ventanaPadreConsultarMarcaTercero">Ventana IConsultarMarcasTercero que precede a la ventana IGestionarMarcaTercero</param>
        public ListaInfoBolMarcaTeres(object marca, object ventanaPadre, object ventanaPadreConsultarMarcaTercero)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaInfoBolMarcaTeres(this, marca, ventanaPadre, ventanaPadreConsultarMarcaTercero);

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
            this._presentador.IrConsultarMarca();
        }

        private void EventoIrGestionarInfoBolMarcaTer(object sender, EventArgs e)
        {
            if (sender.GetType().ToString().Equals("System.Windows.Controls.Button"))
                this._presentador.IrGestionarInfoBolMarcaTer(true);
            else
                this._presentador.IrGestionarInfoBolMarcaTer(false);
        }
    }
}
