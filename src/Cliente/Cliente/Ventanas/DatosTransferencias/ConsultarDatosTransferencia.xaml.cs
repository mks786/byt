using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.DatosTransferencias;
using Trascend.Bolet.Cliente.Presentadores.DatosTransferencias;
using Trascend.Bolet.Cliente.Ayuda;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.DatosTransferencias
{
    /// <summary>
    /// Interaction logic for ConsultarJustificacion.xaml
    /// </summary>
    public partial class ConsultarDatosTransferencia : Page, IConsultarDatosTransferencia
    {
        private PresentadorConsultarDatosTransferencia _presentador;
        private bool _cargada;

        #region IConsultarDatosTransferencia

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object DatosTransferencia
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._txtAba.IsEnabled = value;
                this._txtBancoBenef.IsEnabled = value;
                this._txtBancoInt.IsEnabled = value;
                this._txtBeneficiario.IsEnabled = value;
                this._txtCuenta.IsEnabled = value;
                this._txtDireccion.IsEnabled = value;
                this._txtSwif.IsEnabled = value;
                this._txtSwiftInt.IsEnabled = value;
                this._txtIban.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtIdAsociado.Focus();
        }

        #endregion

        public ConsultarDatosTransferencia(object datosTransferencia)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarDatosTransferencia(this, datosTransferencia);
        }

        private void _dpkFecha_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Regresar();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.Modificar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarDatosTransferencia, "Eliminar Datos de la transferencia", MessageBoxButton.YesNo, MessageBoxImage.Question))
                this._presentador.Eliminar();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }

        private void _btnAuditoria_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Auditoria();
        }

        private void _btnVerPoder_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.AbrirPoder();
        }

        private void _Ordenar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.OrdenarColumna(sender as GridViewColumnHeader);
        }
    }
}
