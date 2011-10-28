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
using Trascend.Bolet.Cliente.Contratos.Resumenes;
using Trascend.Bolet.Cliente.Presentadores.Resumenes;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Resumenes
{
    /// <summary>
    /// Interaction logic for ConsultarBoletin.xaml
    /// </summary>
    public partial class ConsultarResumen : Page, IConsultarResumen
    {
        private PresentadorConsultarResumen _presentador;
        private bool _cargada;
        private bool _deshabilitarFecha = false;

        #region IConsultarBoletin

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Resumen
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set 
            {
                this._txtDescripcion.IsEnabled = value;
                this._txtDias.IsEnabled = value;
                this._cbxSeg.IsEnabled = value; 
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion


        public ConsultarResumen(object boletin)
        {

            InitializeComponent();
            this._presentador = new PresentadorConsultarResumen(this, (Resumen)boletin);
            this._cargada = false;
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarBoletin, "Eliminar Boletin", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _txtDias_KeyUp(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(this._txtDias.Text, "[^0-9]"))
            {
                this._txtDias.Text = "";
            }
        }

        private void _txtDias_KeyDown(object sender, KeyEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
                e.Handled = true;
        }

    }
}