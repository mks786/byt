﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.Administracion.Gestiones_Automaticas;
using Trascend.Bolet.Cliente.Presentadores.Administracion.Gestiones_Automaticas;



namespace Trascend.Bolet.Cliente.Ventanas.Administracion.Gestiones_Automaticas
{
    /// <summary>
    /// Lógica de interacción para ConsultarCorreosParaGestionAutomatica.xaml
    /// </summary>
    public partial class ConsultarCorreosParaGestionAutomatica : Page, IConsultarCorreosParaGestionAutomatica
    {

        private GridViewColumnHeader _CurSortCol = null;
        private SortAdorner _CurAdorner = null;
        private PresentadorConsultarCorreosParaGestionAutomatica _presentador;
        private bool _cargada;

        public ConsultarCorreosParaGestionAutomatica()
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorConsultarCorreosParaGestionAutomatica(this);
        }


        #region IConsultarCorreosParaGestionAutomatica

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }
        
        public object Resultados
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object Resultado
        {
            get { return this._lstResultados.SelectedItems; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public object Medios
        {
            get { return this._cbxMedio.DataContext; }
            set { this._cbxMedio.DataContext = value; }
        }

        public object Medio
        {
            get { return this._cbxMedio.SelectedItem; }
            set { this._cbxMedio.SelectedItem = value; }
        }


        public object Conceptos
        {
            get { return this._cbxConcepto.DataContext; }
            set { this._cbxConcepto.DataContext = value; }
        }

        public object Concepto
        {
            get { return this._cbxConcepto.SelectedItem; }
            set { this._cbxConcepto.SelectedItem = value; }
        }


        public string IdentificacionDeUsuario
        {
            get { return this._txtUsuarioGestion.Text; }
            set { this._txtUsuarioGestion.Text = value; }
        }

        public object Carpetas
        {
            get { return this._cbxCarpetaGestion.DataContext; }
            set { this._cbxCarpetaGestion.DataContext = value; }
        }


        public object Carpeta
        {
            get { return this._cbxCarpetaGestion.SelectedItem; }
            set { this._cbxCarpetaGestion.SelectedItem = value; }
        }


        public string IdAsociado
        {
            get { return this._txtAsociadoGestion.Text; }
            set { this._txtAsociadoGestion.Text = value; }
        }


        public string DetalleGestion
        {
            get { return this._txtDetalleGestion.Text; }
            set { this._txtDetalleGestion.Text = value; }
        }


        public void FocoPredeterminado()
        {
            this._lstResultados.Focus();
        }

        #endregion

        #region Eventos

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
            else
                this._presentador.ActualizarTitulo();
        }

        private void _btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnRecargarCorreos_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.CargarCorreosOutlook();
            this._btnGenerarGestiones.Focus();
        }

        private void _btnGenerarGestiones_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.GenerarGestiones();
        } 

        #endregion



        #region Metodos de la clase

        public void Mensaje(string mensaje, int opcion)
        {
            if (opcion == 0)
                MessageBox.Show(mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (opcion == 1)
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                MessageBox.Show(mensaje, "Operación Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool MensajeAlerta(string mensaje)
        {
            bool retorno = false;

            if (MessageBoxResult.Yes == MessageBox.Show(mensaje,
                "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question))
                retorno = true;

            return retorno;
        } 

        #endregion

        private void _btnLimpiarTodo_Click(object sender, RoutedEventArgs e)
        {
            this._txtAsociadoGestion.Text = "";
            this._txtDetalleGestion.Text = "";
            this._lstResultados = null;
            this._lblHits.Text = "0";
        }
    }
}
