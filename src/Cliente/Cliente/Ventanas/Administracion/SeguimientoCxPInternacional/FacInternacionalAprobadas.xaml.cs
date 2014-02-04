using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional;
using System.Collections.Generic;
using System.Collections;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional
{
    /// <summary>
    /// Lógica de interacción para FacInternacionalAprobadas.xaml
    /// </summary>
    public partial class FacInternacionalAprobadas : Page, IFacInternacionalAprobadas
    {

        private bool _cargada;
        private PresentadorFacInternacionalAprobadas _presentador;


        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="proformasAprobadas">Lista de proformas internacionales aprobadas</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public FacInternacionalAprobadas(object proformasAprobadas, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalAprobadas(this, proformasAprobadas, ventanaPadre);
        }

        #region IFacInternacionalAprobadas

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public object FacturasAutorizadas
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object FacturasSeleccionadas
        {
            get { return this._lstResultados.SelectedItems; }
            set { this._lstResultados.SelectedItem = value; }
        }

        public string TotalMontoAprobado
        {
            get { return this._txtTotalMonto.Text; }
            set { this._txtTotalMonto.Text = value; }
        }

        public string TotalHits
        {
            set { this._lblHits.Text = value; }
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

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        }

        private void _btnConsolidar_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            String nombreBoton = boton.Name;
            bool hayDatos = false;

            if (nombreBoton.Equals("_btnConsolidar"))
            {
                if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarConsolidacionCxPInternacional),
                            "Consolidar CxP Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    
                    hayDatos = this._presentador.VerificarFacAsociadoConsolidadoGuardado();

                    if (hayDatos)
                    {
                        if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarHayDatosConsolidacionCxPInternacional),
                            "Consolidar CxP Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question))
                        {
                            this._presentador.CargarDatosConsolidacion();
                        }
                        else
                        {
                            this._presentador.IrConsolidarFacturasSeleccionadas(nombreBoton);
                        }
                    }
                    //this._presentador.IrConsolidarFacturasSeleccionadas(nombreBoton);

                } 
            }
            else
                this._presentador.IrConsolidarFacturasSeleccionadas(nombreBoton);
        }

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.RegistrarPago();
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

        #endregion

        

        

        
    }
}
