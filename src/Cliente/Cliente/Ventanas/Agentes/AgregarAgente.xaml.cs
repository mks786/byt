using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Agentes;
using Trascend.Bolet.Cliente.Presentadores.Agentes;

namespace Trascend.Bolet.Cliente.Ventanas.Agentes
{
    /// <summary>
    /// Interaction logic for ConsultarUsuario.xaml
    /// </summary>
    public partial class AgregarAgente : Page, IAgregarAgente
    {
        private PresentadorAgregarAgente _presentador;
        private bool _cargada;

        #region IConsultarAgente

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Agente
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public char EstadoCivil
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxEstadoCivil.Text))
                    return ((string)this._cbxEstadoCivil.Text)[0];
                else
                    return ' ';
            }
        }

        public char Sexo
        {
            get
            {
                if (!string.IsNullOrEmpty(this._cbxSexo.Text))
                    return (this._cbxSexo.Text)[0];
                else
                    return ' ';
            }
        }

        #endregion

        public AgregarAgente()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarAgente(this);
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
