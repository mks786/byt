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
using Trascend.Bolet.Cliente.Contratos.Resoluciones;
using Trascend.Bolet.Cliente.Presentadores.Resoluciones;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Resoluciones
{
    /// <summary>
    /// Interaction logic for ConsultarResolucion.xaml
    /// </summary>
    public partial class ConsultarResolucion : Page, IConsultarResolucion
    {
        private PresentadorConsultarResolucion _presentador;
        private bool _cargada;
        private bool _deshabilitarFecha = false;

        #region IConsultarResolucion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._txtId.Focus();
        }

        public object Resolucion
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
                this._dpkFechaResolucion.IsEnabled = value;
                this._cbxBoletin.IsEnabled = value;
                this._txtVolumen.IsEnabled = value;
                this._txtPagina.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public object Boletines
        {
            get { return this._cbxBoletin.DataContext; }
            set { this._cbxBoletin.DataContext = value; }
        }

        public object Boletin
        {
            get { return this._cbxBoletin.SelectedItem; }
            set { this._cbxBoletin.SelectedItem = value; }
        }

        #endregion


        public ConsultarResolucion(object resolucion)
        {

            InitializeComponent();
            this._presentador = new PresentadorConsultarResolucion(this, (Resolucion)resolucion);
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
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarResolucion, "Eliminar Resolucion", MessageBoxButton.YesNo, MessageBoxImage.Question))
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
    }
}