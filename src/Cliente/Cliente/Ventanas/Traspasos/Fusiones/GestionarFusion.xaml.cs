using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Presentadores.Traspasos.Fusiones;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.Traspasos.Fusiones
{
    /// <summary>
    /// Interaction logic for GestionarFusion.xaml
    /// </summary>
    public partial class GestionarFusion : Page, IGestionarFusion
    {

        private PresentadorGestionarFusion _presentador;
        private bool _cargada;
        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;

        #region IConsultarFusion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Fusion
        {
            get
            {
                return this._gridDatos.DataContext;
            }
            set
            {
                this._gridDatos.DataContext = value;
            }
        }

        public string IdAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreAsociadoFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdInteresadoEntreFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreInteresadoEntreFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdInteresadoSobrevivienteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreInteresadoSobrevivienteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdAgenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreAgenteFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string IdMarcaFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string NombreMarcaFiltrar
        {
            get { throw new System.NotImplementedException(); }
        }

        public string Region
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public bool HabilitarCampos
        {
            set
            {
                //this._txtCodigo.IsEnabled = value;
                //this._txtNombreIngles.IsEnabled = value;
                //this._txtNombreEspanol.IsEnabled = value;
                //this._txtNacionalidad.IsEnabled = value;
                //this._cbxRegion.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
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

        #endregion

        public GestionarFusion(object fusion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorGestionarFusion(this, fusion);
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarPais,
                "Eliminar Pais", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Eliminar();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnConsultarMarca_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _lstMarcas_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void _OrdenarMarcas_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.OrdenarColumna(sender as GridViewColumnHeader, this._lstMarcas);
        }
    }
}
