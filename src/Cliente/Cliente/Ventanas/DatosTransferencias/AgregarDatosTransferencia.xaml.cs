using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.DatosTransferencias;
using Trascend.Bolet.Cliente.Presentadores.DatosTransferencias;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Ventanas.DatosTransferencias
{
    /// <summary>
    /// Interaction logic for ConsultarPoder.xaml
    /// </summary>
    public partial class AgregarDatosTransferencia : Page, IAgregarDatosTransferencia
    {
        private PresentadorAgregarDatosTransferencia _presentador;
        private bool _cargada;

        #region IAgregarDatosTransferencia

        public object DatosTransferencia
        {
            get{return this._gridDatos.DataContext;}
            set{this._gridDatos.DataContext = value;}
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtBeneficiario.Focus();
        }

        #endregion


        public AgregarDatosTransferencia(object asociado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarDatosTransferencia(this, asociado);
        }

        public AgregarDatosTransferencia(object asociado, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarDatosTransferencia(this, asociado,ventanaPadre,null);
        }


        public AgregarDatosTransferencia(object asociado, object ventanaPadre, object ventanaConsultarAsociado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarDatosTransferencia(this, asociado, ventanaPadre, ventanaConsultarAsociado);
        }



        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //if (true)
            //    this._presentador.Regresar();
            //else
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
