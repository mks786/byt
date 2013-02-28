using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Cartas;
using Trascend.Bolet.Cliente.Presentadores.Cartas;
using Trascend.Bolet.Cliente.Contratos.Marcas;

namespace Trascend.Bolet.Cliente.Ventanas.Cartas
{
    /// <summary>
    /// Interaction logic for ConsultarObjetos.xaml
    /// </summary>
    public partial class ConsultarContactosPorAsociado : Page, IConsultarContactosPorAsociado
    {
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarContactosPorAsociado _presentador;
        private bool _cargada;

        #region Implementación de IConsultarContactosPorAsociado


        public string IdAsociado
        {
            get { return this._txtId.Text; }
            set { this._txtId.Text = value; }
        }

        public string NombreAsociado
        {
            get { return this._txtNombre.Text; }
            set { this._txtNombre.Text = value; }
        }

        public string TelefonoAsociado
        {
            get { return this._txtTelefono.Text; }
            set { this._txtTelefono.Text = value; }
        }

        public string FaxAsociado
        {
            get { return this._txtFax.Text; }
            set { this._txtFax.Text = value; }
        }

        public string DomicilioAsociado
        {
            get { return this._txtDomicilio.Text; }
            set { this._txtDomicilio.Text = value; }
        }

        public string WebAsociado
        {
            get { return this._txtWeb.Text; }
            set { this._txtWeb.Text = value; }
        }

        public string EmailAsociado
        {
            get { return this._txtEmail.Text; }
            set { this._txtEmail.Text = value; }
        }

        public string NombreContacto
        {
            get { return this._txtNombreContacto.Text; }
            set { this._txtNombreContacto.Text = value; }
        }

        public string TelefonoContacto
        {
            get { return this._txtTelefonoContacto.Text; }
            set { this._txtTelefonoContacto.Text = value; }
        }

        public string FaxContacto
        {
            get { return this._txtFaxContacto.Text; }
            set { this._txtFaxContacto.Text = value; }
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

        public string EmailContacto
        {
            get { return this._txtEmailContacto.Text; }
            set { this._txtEmailContacto.Text = value; }
        }

        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
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

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        public string TotalHits
        {
            set { this._lblHits.Text = value; }
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


        public object Asociados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public object Asociado
        {
            get { return this._lstAsociados.SelectedItem; }
            set { this._lstAsociados.SelectedItem = value; }
        }

        //public object Contactos
        //{
        //    get { return this._lstContactos.DataContext; }
        //    set { this._lstContactos.DataContext = value; }
        //}

        //public object Contacto
        //{
        //    get { return this._lstContactos.SelectedItem; }
        //    set { this._lstContactos.SelectedItem = value; }
        //}

        public string AsociadoFiltrar
        {
            set { this._txtAsociado.Text = value; }
        }

        


        public object ContactoSeleccionado
        {
            get { return this._lstResultados.SelectedItem; }
        }

        #endregion

        public ConsultarContactosPorAsociado(object ventanaPadre, object asociado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarContactosPorAsociado(this, ventanaPadre, asociado);

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
            else
                this._presentador.ActualizarTitulo();
        }


        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }


        private void _btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            this._btnConsultar.Focus();
            this._presentador.Consultar();
            validarCamposVacios();
        }


        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarAsociado();
        }


        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }


        /// <summary>
        /// Método que se encarga de posicionar el cursor en los campos del filto
        /// </summary>
        private void validarCamposVacios()
        {
            bool todosCamposVacios = true;
            if (!this._txtId.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtId.Focus();
            }

            if (!this._txtNombre.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtNombre.Focus();
            }

            if (!this._txtDomicilio.Text.Equals(""))
            {
                todosCamposVacios = false;
                this._txtDomicilio.Focus();
            }

            if (todosCamposVacios)
            {
                this._txtId.Focus();
            }
        }


        private void _btnLimpiarCampos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.LimpiarCampos();
        }


        private void _txtContacto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadAsociado(Visibility.Collapsed);
            //GestionarVisibilidadContacto(Visibility.Visible);
        }


        private void _txtAsociado_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GestionarVisibilidadAsociado(Visibility.Visible);

            this._btnConsultarAsociado.IsDefault = true;
            this._btnConsultar.IsDefault = false;
        }


        private void GestionarVisibilidadAsociado(object visibilidad)
        {
            this._IdAsociado.Visibility = ((Visibility)visibilidad);
            this._NombreAsociado.Visibility = ((Visibility)visibilidad);
            this._EmailAsociado.Visibility = ((Visibility)visibilidad);
            this._TelefonoAsociado.Visibility = ((Visibility)visibilidad);
            this._FaxAsociado.Visibility = ((Visibility)visibilidad);
            this._DomicilioAsociado.Visibility = ((Visibility)visibilidad);
            this._WebAsociado.Visibility = ((Visibility)visibilidad);

            this._lstAsociados.Visibility = ((Visibility)visibilidad);
            this._btnConsultarAsociado.Visibility = ((Visibility)visibilidad);
        }


        //private void GestionarVisibilidadContacto(object visibilidad)
        //{
        //    this._NombreContacto.Visibility = ((Visibility)visibilidad);
        //    this._EmailContacto.Visibility = ((Visibility)visibilidad);
        //    this._TelefonoContacto.Visibility = ((Visibility)visibilidad);
        //    this._FaxContacto.Visibility = ((Visibility)visibilidad);
        //    this._DepartamentoContacto.Visibility = ((Visibility)visibilidad);

        //    this._lstContactos.Visibility = ((Visibility)visibilidad);
        //    this._btnConsultarContacto.Visibility = ((Visibility)visibilidad);
        //}


        //private void _lstContactos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (this._presentador.CambiarContacto())
        //        GestionarVisibilidadContacto(Visibility.Collapsed);
        //}


        private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._presentador.CambiarAsociado())
            {
                GestionarVisibilidadAsociado(Visibility.Collapsed);


                this._btnConsultarAsociado.IsDefault = false;
                this._btnConsultar.IsDefault = true;
            }
        }


        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ConsultarAsociado();
        }


        //private void _btnConsultarContacto_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.ConsultarContacto();

        //}


        public void Refrescar() {
            this._presentador.Consultar();
        }


        private void _btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.SeleccionarContacto();
        }

        private void _btnNuevoContacto_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CrearNuevoContacto();
        }

    }
}
