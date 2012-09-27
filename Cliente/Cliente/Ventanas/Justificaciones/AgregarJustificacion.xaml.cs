using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Justificaciones;
using Trascend.Bolet.Cliente.Presentadores.Justificaciones;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Justificaciones
{
    /// <summary>
    /// Interaction logic for ConsultarPoder.xaml
    /// </summary>
    public partial class AgregarJustificacion : Page, IAgregarJustificacion
    {
        private PresentadorAgregarJustificacion _presentador;
        private bool _cargada;

        #region IAgregarJustificacion
        
        public object Justificacion
        {
            get{return this._gridDatos.DataContext;}
            set{this._gridDatos.DataContext = value;}
        }

        public void BorrarId()
        {
            this._txtCarta.Text = string.Empty;
        }

        public object Conceptos
        {
            get { return this._cbxConcepto.DataContext; }
            set { this._cbxConcepto.DataContext = value; }
        }

        public object Concepto
        {
            get { return this._cbxConcepto.SelectedItem; }
            set { this._cbxConcepto.SelectedItem = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtCarta.Focus();
        }


        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion


        public AgregarJustificacion(object asociado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarJustificacion(this,asociado);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (true)
                this._presentador.Regresar();
            else
                this._presentador.Cancelar();
        }

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this._btnAceptar.Focus();
            this._presentador.Aceptar();
        }

        private void _dpkFecha_SelectedDateChanged(object sender, RoutedEventArgs e) 
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }
    }
}
