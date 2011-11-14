﻿using System.Windows.Controls;
using Trascend.Bolet.Cliente.Contratos.Cartas;
using Trascend.Bolet.Cliente.Presentadores.Cartas;
using System.Windows;
using System.Windows.Input;

namespace Trascend.Bolet.Cliente.Ventanas.Cartas
{
    /// <summary>
    /// Interaction logic for AgregarAsociado.xaml
    /// </summary>
    public partial class AgregarCarta : Page, IAgregarCarta
    {
        private PresentadorAgregarCarta _presentador;
        private bool _cargada;

        #region IAgregarCarta

        public object Carta
        {
            get { return this._dataCarta.DataContext; }
            set { this._dataCarta.DataContext = value; }
        }

        public void Mensaje(string mensaje)
        {
            throw new System.NotImplementedException();
        }

        public object Asociado
        {
            get { return this._lstAsociados.SelectedItem; }
            set
            {
                this._lstAsociados.SelectedItem = value;
                this._lstAsociados.ScrollIntoView(value);
            }
        }

        public object Asociados
        {
            get { return this._lstAsociados.DataContext; }
            set { this._lstAsociados.DataContext = value; }
        }

        public object Receptor
        {
            get { return this._cbxReceptor.SelectedItem; }
            set { this._cbxReceptor.SelectedItem = value; }
        }

        public object Receptores
        {
            get { return this._cbxReceptor.DataContext; }
            set { this._cbxReceptor.DataContext = value; }
        }

        public object Remitente
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

        public object Remitentes
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

        public object Responsable
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

        public object Responsables
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

        public object Resumen
        {
            get { return this._cbxResumen.SelectedItem; }
            set { this._cbxResumen.SelectedItem = value; }
        }

        public object Resumenes
        {
            get { return this._cbxResumen.DataContext; }
            set { this._cbxResumen.DataContext = value; }
        }

        public object Departamento
        {
            get { return this._cbxDepartamento.SelectedItem; }
            set { this._cbxDepartamento.SelectedItem = value; }
        }

        public object Departamentos
        {
            get { return this._cbxDepartamento.DataContext; }
            set { this._cbxDepartamento.DataContext = value; }
        }

        public object Persona
        {
            get { return this._cbxContacto.SelectedItem; }
            set { this._cbxContacto.SelectedItem = value; }
        }

        public object Personas
        {
            get { return this._cbxContacto.DataContext; }
            set { this._cbxContacto.DataContext = value; }
        }

        public object Medio
        {
            get { return this._cbxMedio.SelectedItem; }
            set { this._cbxMedio.SelectedItem = value; }
        }

        public object Medios
        {
            get { return this._cbxMedio.DataContext; }
            set { this._cbxMedio.DataContext = value; }
        }


        public object MedioTracking
        {
            get { return this._cbxMedioTracking.SelectedItem; }
            set { this._cbxMedioTracking.SelectedItem = value; }
        }

        public object MediosTracking
        {
            get { return this._cbxMedioTracking.DataContext; }
            set { this._cbxMedioTracking.DataContext = value; }
        }

        public object MedioTrackingConfirmacion
        {
            get { return this._cbxMedioTrackingConfirmacion.SelectedItem; }
            set { this._cbxMedioTrackingConfirmacion.SelectedItem = value; }
        }

        public object MediosTrackingConfirmacion
        {
            get { return this._cbxMedioTrackingConfirmacion.DataContext; }
            set { this._cbxMedioTrackingConfirmacion.DataContext = value; }
        }
        public object Anexo
        {
            get { return this._cbxAnexo.SelectedItem; }
            set { this._cbxAnexo.SelectedItem = value; }
        }

        public object Anexos
        {
            get { return this._cbxAnexo.DataContext; }
            set { this._cbxAnexo.DataContext = value; }
        }

        public object AnexoCarta
        {
            get { return this._lstAnexosCarta.SelectedItem; }
            set { this._lstAnexosCarta.SelectedItem = value; }
        }

        public object AnexosCarta
        {
            get { return this._lstAnexosCarta.DataContext; }
            set { this._lstAnexosCarta.DataContext = value; }
        }

        public object AnexoConfirmacion
        {
            get { return this._cbxAnexoConfirmacion.SelectedItem; }
            set { this._cbxAnexoConfirmacion.SelectedItem = value; }
        }

        public object AnexosConfirmacion
        {
            get { return this._cbxAnexoConfirmacion.DataContext; }
            set { this._cbxAnexoConfirmacion.DataContext = value; }
        }

        public object AnexoConfirmacionCarta
        {
            get { return this._lstAnexosCartaConfirmacion.SelectedItem; }
            set { this._lstAnexosCartaConfirmacion.SelectedItem = value; }
        }

        public object AnexosConfirmacionCarta
        {
            get { return this._lstAnexosCartaConfirmacion.DataContext; }
            set { this._lstAnexosCartaConfirmacion.DataContext = value; }
        }

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            //this._txtNombreDatos.Focus();
        }

        public string NombreAsociado
        {
            get { return this._txtAsociado.Text; }
            set { this._txtAsociado.Text = value; }
        }

        public string idAsociadoFiltrar
        {
            get { return this._txtIdAsociado.Text; }
            set { this._txtIdAsociado.Text = value; }
        }

        public string NombreAsociadoFiltrar
        {
            get { return this._txtNombreAsociado.Text; }
            set { this._txtNombreAsociado.Text = value; }
        }

        public string FormatoTracking
        {
            get { return this._lsbFormato.Text; }
            set { this._lsbFormato.Text = value; }
        }

        #endregion

        public AgregarCarta()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorAgregarCarta(this);
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

        private void _txtAsociado_GotFocus(object sender, RoutedEventArgs e)
        {
            this._txtAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._lstAsociados.Visibility = System.Windows.Visibility.Visible;
            this._lstAsociados.IsEnabled = true;
            this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Visible;
            this._txtIdAsociado.Visibility = System.Windows.Visibility.Visible;
            this._txtNombreAsociado.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociado.Visibility = System.Windows.Visibility.Visible;
            this._lblNombreAsociado.Visibility = System.Windows.Visibility.Visible;

        }

        private void _lstAsociados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.CambiarAsociado();
            this._lstAsociados.Visibility = System.Windows.Visibility.Collapsed;
            this._btnConsultarAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtIdAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtNombreAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._txtAsociado.Visibility = System.Windows.Visibility.Visible;
            this._lblIdAsociado.Visibility = System.Windows.Visibility.Collapsed;
            this._lblNombreAsociado.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void _btnConsultarAsociado_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.BuscarAsociado();
        }

        private void _btnMas_Click(object sender, RoutedEventArgs e)
        {

            if ((this._presentador.AgregarAnexoCarta()) && (this._lstAnexosCarta.Visibility == System.Windows.Visibility.Collapsed))
                this._lstAnexosCarta.Visibility = System.Windows.Visibility.Visible;

        }

        private void _btnMenos_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.DeshabilitarAnexosCarta())
            {
                this._lstAnexosCarta.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void _btnMasConfirmacion_Click(object sender, RoutedEventArgs e)
        {

            if ((this._presentador.AgregarAnexoCartaConfirmacion()) && (this._lstAnexosCartaConfirmacion.Visibility == System.Windows.Visibility.Collapsed))
                this._lstAnexosCartaConfirmacion.Visibility = System.Windows.Visibility.Visible;

        }

        private void _btnMenosConfirmacion_Click(object sender, RoutedEventArgs e)
        {
            if (this._presentador.DeshabilitarAnexosCartaConfirmacion())
            {
                this._lstAnexosCartaConfirmacion.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void _cbxMedioTracking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._presentador.CarmbiarFormatoTracking();
        }


        //private void _soloNumero_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(this._txtDiasCreditoAdministracion.Text, "[^0-9]"))
        //    {
        //        this._txtDiasCreditoAdministracion.Text = "";
        //    }
        //}

        //private void _soloNumero_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
        //        e.Handled = true;

        //}

        //private void _txtDescuentoAdministracion_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if ((!string.Equals(e.Key.ToString(),"OemComma"))||(this._txtDescuentoAdministracion.Text.Contains(",")))
        //    {
        //        if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
        //            e.Handled = true;
        //    }

        //if (!System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "\\d+"))
        //    e.Handled = true;


        //private void _chkAlertaAdministracion_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!this._chkAlertaAdministracion.IsChecked.Value)
        //    {
        //        this._txtAlarmaAdministracion.IsEnabled = false;
        //    }
        //    else
        //    {
        //        this._txtAlarmaAdministracion.IsEnabled = true;
        //    }
        //}


        //private void _btnJustificacionesDatos_Click(object sender, RoutedEventArgs e)
        //{
        //    this._presentador.IrListaJustificaciones();
        //}




    }
}
