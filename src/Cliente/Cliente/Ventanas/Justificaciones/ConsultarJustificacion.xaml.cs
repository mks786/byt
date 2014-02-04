using System;
using System.Windows;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Justificaciones;
using Trascend.Bolet.Cliente.Presentadores.Justificaciones;
using Trascend.Bolet.Cliente.Ayuda;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Cliente.Ventanas.Justificaciones
{
    /// <summary>
    /// Interaction logic for ConsultarJustificacion.xaml
    /// </summary>
    public partial class ConsultarJustificacion : Page, IConsultarJustificacion
    {
        private PresentadorConsultarJustificacion _presentador;
        private bool _cargada;

        #region IConsultarJustificacion

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public object Justificacion
        {
            get { return this._gridDatos.DataContext; }
            set { this._gridDatos.DataContext = value; }
        }

        public bool HabilitarCampos
        {
            set
            {
                this._dpkFecha.IsEnabled = value;
                this._cbxConcepto.IsEnabled = value;
                this._txtCodigoCarta.IsEnabled = value;
            }
        }

        public string TextoBotonModificar
        {
            get { return this._txbModificar.Text; }
            set { this._txbModificar.Text = value; }
        }

        public object Concepto
        {
            get{return this._cbxConcepto.SelectedItem;}
            set{this._cbxConcepto.SelectedItem = value;}
        }

        public object Conceptos
        {
            get { return this._cbxConcepto.DataContext; }
            set { this._cbxConcepto.DataContext = value; }
        }


        public void FocoPredeterminado()
        {
            this._txtIdAsociado.Focus();
        }

        #endregion

        public ConsultarJustificacion(object justificacion)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarJustificacion(this, justificacion,null);
        }

        public ConsultarJustificacion(object justificacion, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarJustificacion(this, justificacion, ventanaPadre);
        }

        private void _dpkFecha_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            //this._presentador.Regresar();
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            this._btnModificar.Focus();
            this._presentador.Modificar();
        }

        private void _btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show(Recursos.MensajesConElUsuario.ConfirmacionEliminarJustificacion, "Eliminar Justificacion", MessageBoxButton.YesNo, MessageBoxImage.Question))
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

        private void _txtCodigoCarta_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this._presentador.VerCarta();
        }
    }
}
