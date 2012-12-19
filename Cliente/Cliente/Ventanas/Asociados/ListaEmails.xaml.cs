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
    /// Interaction logic for ListaEmails.xaml
    /// </summary>
    public partial class ListaEmails : Page, IListaEmails
    {
        
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaEmails _presentador;
        private bool _cargada;


        #region IListaDatosTransferencia

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Emails
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object EmailSeleccionado
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

        public ListaEmails(object asociado)
        {
            InitializeComponent();
            this._cargada= false;
            this._presentador = new PresentadorListaEmails(this, asociado);

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
            this._presentador.Regresar();
        }

        private void _btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.IrAgregarEmail();
        }

        private void _lstResultados_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarEmailAsociado();
        }

        public void RefrescarPagina() {
            this._presentador.CargarPagina();
        }
    }
}
