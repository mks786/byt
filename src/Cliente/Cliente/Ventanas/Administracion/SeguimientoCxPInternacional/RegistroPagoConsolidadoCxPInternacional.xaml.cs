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
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional
{
    /// <summary>
    /// Lógica de interacción para RegistroPagoConsolidadoCxPInternacional.xaml
    /// </summary>
    public partial class RegistroPagoConsolidadoCxPInternacional : Window, IRegistroPagoConsolidadoCxPInternacional
    {
        private bool _cargada;
        private PresentadorRegistroPagoConsolidadoCxPInternacional _presentador;

        /// <summary>
        /// Constructor predeterminado que recibe las facturas consolidadas de un Asociado Internacional especifico
        /// </summary>
        /// <param name="facConsolidadaAsociado">Estructura de datos que posee las proformas del Asociado internacional</param>
        public RegistroPagoConsolidadoCxPInternacional(object facConsolidadaAsociado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorRegistroPagoConsolidadoCxPInternacional(this, facConsolidadaAsociado);

        }
        
        #region IRegistroPagoConsolidadoCxPInternacional

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public String FechaPago
        {
            get { return this._dpkFechaPago.Text; }
            set { this._dpkFechaPago.Text = value; }
        }

        public object TiposPago
        {
            get { return this._cbxTipoPago.DataContext; }
            set { this._cbxTipoPago.DataContext = value; }
        }

        public object TipoPago
        {
            get { return this._cbxTipoPago.SelectedItem; }
            set { this._cbxTipoPago.SelectedItem = value; }
        }

        public object Bancos
        {
            get { return this._cbxBanco.DataContext; }
            set { this._cbxBanco.DataContext = value; }
        }

        public object Banco
        {
            get { return this._cbxBanco.SelectedItem; }
            set { this._cbxBanco.SelectedItem = value; }
        }

        public String DescripcionPago
        {
            get { return this._txtDescripcionPago.Text; }
            set { this._txtDescripcionPago.Text = value; }
        }

        #endregion
        
        #region Eventos

        private void _btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarPagoConsolidadoCxPInt),
                    "Confirmar Pago Consolidado CxP Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Aceptar();
            }
            this.Close();
            //this._presentador.Aceptar();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Metodos

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void CerrarVentanaException()
        {
            this.Close();
        }

        #endregion

    }
}
