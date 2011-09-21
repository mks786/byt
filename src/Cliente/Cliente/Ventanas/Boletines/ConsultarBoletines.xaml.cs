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
using Trascend.Bolet.Cliente.Contratos.Boletines;
using Trascend.Bolet.Cliente.Presentadores.Boletines;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Boletines
{
    /// <summary>
    /// Interaction logic for ConsultarBoletines.xaml
    /// </summary>
    public partial class ConsultarBoletines : Page, IConsultarBoletines
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarBoletines _presentador;
        private bool _cargada;

        #region IConsultarBoletines

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

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object BoletinSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
        }

        public string Id
        {
            get { return this._txtId.Text; }
        }

        public string FechaBoletin
        {
            get { return this._dpkFechaBoletin.SelectedDate.ToString(); }
        }

        public string FechaBoletinVence
        {
            get { return this._dpkFechaBoletinVence.SelectedDate.ToString(); }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public ListView ListaResultados
        {
            get { return this._lstResultados; }
            set { this._lstResultados = value; }
        }

        #endregion

        public ConsultarBoletines()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarBoletines(this);

        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            this._dpkFechaBoletinVence.BlackoutDates.Clear();
            this._dpkFechaBoletin.Text = string.Empty;
            this._dpkFechaBoletinVence.Text = string.Empty;
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarBoletin();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this._presentador.CargarPagina();
        }

        private void _dpkFechaBoletin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(this._dpkFechaBoletin.Text))
                this._presentador.DeshabilitarDias(this._dpkFechaBoletinVence, this._dpkFechaBoletin.SelectedDate.Value.AddDays(-1));
        }

        private void _txtId_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Key == 2)
                e.Handled = false;
            else if ((int)e.Key >= 43 || (int)e.Key <= 34)
                e.Handled = true;
            else
                e.Handled = false;
        }

    }
}
