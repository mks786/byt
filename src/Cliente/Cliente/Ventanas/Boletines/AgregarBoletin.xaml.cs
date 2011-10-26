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

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            this._presentador.DeshabilitarDias(this._dpkFechaBoletinVence, this._dpkFechaBoletin.SelectedDate.Value.AddDays(-1));
        }

        private void _txtId_KeyUp(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(this._txtId.Text, "[^0-9]"))
            {
                this._txtId.Text = "";
            }
        }

        private void _txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
                e.Handled = true;
        }

   
    }
}
