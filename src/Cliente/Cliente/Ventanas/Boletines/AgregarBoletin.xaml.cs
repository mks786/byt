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
using Trascend.Bolet.Cliente.Presentadores.Boletines;
using Trascend.Bolet.Cliente.Contratos.Boletines;

namespace Trascend.Bolet.Cliente.Ventanas.Boletines
{
    /// <summary>
    /// Interaction logic for AgregarBoletin.xaml
    /// </summary>
    public partial class AgregarBoletin : Page, IAgregarBoletin
    {
        private PresentadorAgregarBoletin _presentador;
        private bool _cargada;

        #region IAgregarBoletin

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object  Boletin
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        #endregion

        public AgregarBoletin()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarBoletin(this);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.AgregarBoletin();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Cancelar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                this._dpkFechaBoletinVence.SelectedDate = System.DateTime.Today;
                this._dpkFechaBoletinVence.Text = string.Empty;
                EstaCargada = true;
            }
        }

        private void _dpkFechaBoletin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EstaCargada)
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
