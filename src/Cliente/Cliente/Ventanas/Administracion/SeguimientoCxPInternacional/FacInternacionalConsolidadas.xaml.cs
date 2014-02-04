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
using Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional;
using Trascend.Bolet.Cliente.Presentadores.Administracion.SeguimientoCxPInternacional;

namespace Trascend.Bolet.Cliente.Ventanas.Administracion.SeguimientoCxPInternacional
{
    /// <summary>
    /// Lógica de interacción para FacInternacionalConsolidadas.xaml
    /// </summary>
    public partial class FacInternacionalConsolidadas : Page, IFacInternacionalConsolidadas
    {
        private bool _cargada;
        private PresentadorFacInternacionalConsolidadas _presentador;

        /// <summary>
        /// Constructor predeterminado que recibe las facturas aprobadas y que se van a consolidar
        /// </summary>
        /// <param name="listaFacInternacionalesAprobadas">Facturas aprobadas a consolidar</param>
        /// <param name="soloVer">Bit necesario para seleccionar si se va a ver el proceso de consolidacion</param>
        public FacInternacionalConsolidadas(object listaFacInternacionalesAprobadas, bool soloVer)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalConsolidadas(this,listaFacInternacionalesAprobadas, soloVer);
        }

        /// <summary>
        /// Constructor predeterminado que recibe las facturas aprobadas y seleccionadas a consolidar y una ventana padre
        /// </summary>
        /// <param name="listaFacInternacionalesAprobadas">Facturas aprobadas a consolidar</param>
        /// <param name="soloVer">Bit necesario para seleccionar si se va a ver el proceso de consolidacion</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public FacInternacionalConsolidadas(object listaFacInternacionalesAprobadas, bool soloVer, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalConsolidadas(this, listaFacInternacionalesAprobadas, soloVer, ventanaPadre);
        }

        /// <summary>
        /// Constructor por defecto que recibe un grupo de datos ya consolidados
        /// </summary>
        /// <param name="listaFacAsociadoIntCxPInternacional">Datos consolidados previamente guardados</param>
        /// <param name="listaFacInternacionalesAprobadas">Lista de Facturas Internacionales seleccionadas para consolidar</param>
        /// <param name="soloVer">Bit necesario para seleccionar si se va a ver el proceso de consolidacion</param>
        /// <param name="datosConsolidados">Bit para indicar si son datos consolidados previamente guardados</param>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public FacInternacionalConsolidadas(object listaFacAsociadoIntCxPInternacional, object listaFacInternacionalesAprobadas, bool soloVer, bool datosConsolidados, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalConsolidadas(this, listaFacAsociadoIntCxPInternacional, listaFacInternacionalesAprobadas, soloVer, datosConsolidados, ventanaPadre);
        }
                

        #region IFacInternacionalConsolidadas

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
           this._btnRegresar.Focus();
        }

        public object FacturasAprobadas
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object FacturasAprobadasSoloVer
        {
            get { return this._lstResultados1.DataContext; }
            set { this._lstResultados1.DataContext = value; }
        }

        public string TotalMontoConsolidado
        {
            get { return this._txtTotalMontoConsolidado.Text; }
            set { this._txtTotalMontoConsolidado.Text = value; }
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


        private void RegistrarPagoConsolidado_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            object objeto = b.CommandParameter as object;
            this._presentador.RegistrarPagoConsolidado(objeto);

        }

        private void VerDetallePagoConsolidado_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            object objeto = b.CommandParameter as object;
            this._presentador.VerDetalleDeConsolidado(objeto);

        }

        private void _btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == System.Windows.MessageBox.Show(string.Format(Recursos.MensajesConElUsuario.ConfirmarGuardarDatosConsolidacion),
                            "Consolidar CxP Internacional", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                this._presentador.Modificar();
            } 
            
        }

        #endregion

        #region Metodos

        public void HabilitarListaSoloVer()
        {
            this._lstResultados1.Visibility = System.Windows.Visibility.Visible;
            this._lstResultados.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void HabilitarBotonModificar()
        {
            this._btnModificar.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion

        
    }
}
