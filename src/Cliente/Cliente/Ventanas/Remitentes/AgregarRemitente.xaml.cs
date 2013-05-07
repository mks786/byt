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
using Trascend.Bolet.Cliente.Presentadores.Remitentes;
using Trascend.Bolet.Cliente.Contratos.Remitentes;

namespace Trascend.Bolet.Cliente.Ventanas.Remitentes
{
    /// <summary>
    /// Interaction logic for AgregarRemitente.xaml
    /// </summary>
    public partial class AgregarRemitente : Page, IAgregarRemitente
    {
        private PresentadorAgregarRemitente _presentador;
        private bool _cargada;

        #region IAgregarRemitente

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object  Remitente
        {
	        get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object Paises
        {
            get { return this._cbxPais.DataContext; }
            set { this._cbxPais.DataContext = value; }
        }

        public object Pais
        {
            get { return this._cbxPais.SelectedItem; }
            set { this._cbxPais.SelectedItem = value; }
        }

        public char TipoRemitente
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxTipoRemitente.Text))
                    return ((string)this._cbxTipoRemitente.Text)[0];
                else
                    return ' ';
            }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        public AgregarRemitente(object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarRemitente(this, ventanaPadre);
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Aceptar();
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
                EstaCargada = true;
            }
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

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }
    }
}
