using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Inventores;
using Trascend.Bolet.Cliente.Presentadores.Inventores;


namespace Trascend.Bolet.Cliente.Ventanas.Inventores
{
    /// <summary>
    /// Lógica de interacción para QueryInventor.xaml
    /// </summary>
    public partial class QueryInventor : Page, IQueryInventor
    {

        private PresentadorQueryInventor _presentador;
        private bool _cargada;

        #region IQueryInventor
        /* En esta porcion del codigo van los metodos de la interfaz (Contrato) 
         * IQueryInventor. Estos metodos son getters y setters y aqui se implementan
         * para darles funcionalidad */

        public bool EstaCargada
        //Prpiedad para saber si la ventana esta cargada cuando es llamada 
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        //Propiedad para mantener el foco del boton Modificar de la ventana
        public void FocoPredeterminado()
        {
            this._btnModificar.Focus();
        }

        public bool HabilitarCampos
        //Propiedad que hace un set del valor de como esten los campos en el formulario XAML
        {
            set
            {
                this._txtNombre.IsEnabled = value;
                this._cbxNacionalidad.IsEnabled = value;
                this._cbxPais.IsEnabled = value;
                this._txtDomicilio.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public object Inventor
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

        public object Nacionalidades
        {
            get { return this._cbxNacionalidad.DataContext; }
            set { this._cbxNacionalidad.DataContext = value; }
        }

        public object Nacionalidad
        {
            get { return this._cbxNacionalidad.SelectedItem; }
            set { this._cbxNacionalidad.SelectedItem = value; }
        }

        public void mensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }



        #endregion

        #region Constructor de la Ventana QueryInventor
        /// <summary>
        /// Constructor de la clase QueryInventor que inicializar la carga de la ventana en false
        /// y el objeto del presentador
        /// </summary>
        /// <param name="inventor"></param>
        public QueryInventor(object inventor)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorQueryInventor(this, inventor);
        }

        #endregion

        #region Eventos de la Ventana

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;                
            }
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        

        

        


    }
}
