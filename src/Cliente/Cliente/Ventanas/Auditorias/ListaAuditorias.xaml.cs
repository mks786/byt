using System.Collections.Generic;
using System.Windows.Controls;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Windows;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Agentes;
using Trascend.Bolet.Cliente.Presentadores.Agentes;

namespace Trascend.Bolet.Cliente.Ventanas.Auditorias
{
    /// <summary>
    /// Interaction logic for ListaAuditorias.xaml
    /// </summary>
    public partial class ListaAuditorias : Page, IListaAuditorias
    {
        
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorListaAuditorias _presentador;
        private bool _cargada;


        #region IConsultarAuditoria

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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Auditorias
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        #endregion


        public ListaAuditorias(IList<Auditoria> auditorias)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorListaAuditorias(this, auditorias);

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

    }
}
