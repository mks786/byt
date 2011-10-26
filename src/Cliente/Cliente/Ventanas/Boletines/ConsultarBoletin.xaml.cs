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
using Trascend.Bolet.Cliente.Contratos.Boletines;
using Trascend.Bolet.Cliente.Presentadores.Boletines;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Boletines
{
    /// <summary>
    /// Interaction logic for ConsultarBoletin.xaml
    /// </summary>
    public partial class ConsultarBoletin : Page, IConsultarBoletin
    {
        private PresentadorConsultarBoletin _presentador;
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

        public object Boletin
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }


        public bool DeshabilitarFecha
        {
            set { this._deshabilitarFecha = value; }
        }

        public bool HabilitarCampos
        {
            set 
            { 
                this._dpkFechaBoletin.IsEnabled = value;
                this._dpkFechaBoletinVence.IsEnabled = value; 
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        #endregion


        public ConsultarBoletin(object boletin)
        {

            InitializeComponent();
            this._presentador = new PresentadorConsultarBoletin(this, (Boletin)boletin);
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

                if (this._deshabilitarFecha)
                {
                    this._presentador.DeshabilitarDias(this._dpkFechaBoletinVence, this._dpkFechaBoletin.SelectedDate.Value.AddDays(-1));
                }
                else
                {
                    this._dpkFechaBoletinVence.SelectedDate = System.DateTime.Today;
                    this._dpkFechaBoletinVence.Text = string.Empty;
                }

                EstaCargada = true;
            }
        }

        private void _dpkFechaBoletin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EstaCargada)
                this._presentador.DeshabilitarDias(this._dpkFechaBoletinVence, this._dpkFechaBoletin.SelectedDate.Value.AddDays(-1));
            
        }

        private void _txtId_KeyUp(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(this._txtId.Text, "[^0-9]"))
            {
                this._txtId.Text = "";
            }
        }

        private void _txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
                e.Handled = true;
        }

    }
}