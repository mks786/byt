using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.EmailsAsociado;
using Trascend.Bolet.Cliente.Presentadores.EmailsAsociado;
using Trascend.Bolet.Cliente.Ayuda;
using System.Windows.Media;

namespace Trascend.Bolet.Cliente.Ventanas.EmailsAsociado
{
    /// <summary>
    /// Interaction logic for ConsultarPoder.xaml
    /// </summary>
    public partial class ConsultarEmailAsociado : Page, IConsultarEmailAsociado
    {

        private PresentadorConsultarEmailAsociado _presentador;
        private bool _cargada;

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;


        #region IConsultarEmailAsociado

        public object DatosTransferencia
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void PintarAuditoria()
        {
            this._btnAuditoria.Visibility = Visibility.Visible;
            this._btnAuditoria.Background = Brushes.LightGreen;
        }

        public void FocoPredeterminado()
        {
            this._txtEmail.Focus();
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
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


        public object EmailAsociado
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public object TiposEmail
        {
            get { return this._lstTiposEmail.DataContext; }
            set { this._lstTiposEmail.DataContext = value; }
        }

        public object TipoEmail
        {
            get { return this._lstTiposEmail.SelectedItem; }
            set { this._lstTiposEmail.SelectedItem = value; }
        }

        public string Funcion
        {
            set { this._txtFuncion.Text = value; }
        }

        public string Descripcion
        {
            set { this._txtDescripcion.Text = value; }
        }

        public void Mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void MostrarBotones()
        {
            this._btnAceptar.Visibility = Visibility.Visible;
            this._btnEliminar.Visibility = Visibility.Visible;
            this._btnAuditoria.Visibility = Visibility.Visible;
        }

        #endregion


        public ConsultarEmailAsociado(object email, object asociado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarEmailAsociado(this, email, asociado);
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
            this._presentador.Aceptar();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }


        private void _txtTipoEmail_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GestionarVisibilidadListaTiposEmail(true);
        }


        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstTiposEmail);
        }


        private void _lstTiposEmail_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarTipoEmail())
                GestionarVisibilidadListaTiposEmail(false);

        }


        private void GestionarVisibilidadListaTiposEmail(bool visible)
        {
            if (visible)
            {
                this._lstTiposEmail.Visibility = Visibility.Visible;

                this._lblDepartamento.Visibility = Visibility.Collapsed;
                this._lblFuncion.Visibility = Visibility.Collapsed;
                this._lblDescripcion.Visibility = Visibility.Collapsed;
                this._txtDescripcion.Visibility = Visibility.Collapsed;
                this._txtFuncion.Visibility = Visibility.Collapsed;
                this._cbxDepartamento.Visibility = Visibility.Collapsed;
            }
            else
            {
                this._lstTiposEmail.Visibility = Visibility.Collapsed;

                this._lblDepartamento.Visibility = Visibility.Visible;
                this._lblFuncion.Visibility = Visibility.Visible;
                this._lblDescripcion.Visibility = Visibility.Visible;
                this._txtDescripcion.Visibility = Visibility.Visible;
                this._txtFuncion.Visibility = Visibility.Visible;
                this._cbxDepartamento.Visibility = Visibility.Visible;
            }
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Eliminar();
        }

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Auditoria();
        }
    }
}
