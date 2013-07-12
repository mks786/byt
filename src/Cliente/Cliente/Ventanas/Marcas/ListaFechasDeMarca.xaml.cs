using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Marcas;
using Trascend.Bolet.Cliente.Presentadores.Marcas;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Marcas
{
    /// <summary>
    /// Lógica de interacción para ListaFechasDeMarca.xaml
    /// </summary>
    public partial class ListaFechasDeMarca : Page, IListaFechasDeMarca
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaFechasDeMarca _presentador;
        private bool _cargada;
        private object _marca;


        #region IListaFechasDeMarca
        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public object Fechas
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object FechaSeleccionada
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
        /// Constructor predeterminado que recibe una marca
        /// </summary>
        /// <param name="marca"></param>
        public ListaFechasDeMarca(object marca)
        {
            InitializeComponent();
            
            this._cargada = false;

            this._presentador = new PresentadorListaFechasDeMarca(this, marca);
        }


        /// <summary>
        /// Constructor predeterminado que recibe una marca y una ventana padre
        /// </summary>
        /// <param name="marca"></param>
        /// <param name="ventanaPadre"></param>
        public ListaFechasDeMarca(object marca, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaFechasDeMarca(this, marca, ventanaPadre);
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

        private void EventoIrGestionarFechaDeMarca(object sender, EventArgs e)
        {
            if (sender.GetType().ToString().Equals("System.Windows.Controls.Button"))
                this._presentador.IrGestionarFechaDeMarca(true);
                //this._presentador.IrGestionarInfoBol(true);
            else
                this._presentador.IrGestionarFechaDeMarca(false);
                //this._presentador.IrGestionarInfoBol(false);
        }

       
        #endregion

        

        
    }
}
