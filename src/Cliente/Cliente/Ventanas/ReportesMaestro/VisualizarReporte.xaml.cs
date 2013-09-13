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
using Trascend.Bolet.Cliente.Ayuda;
using Trascend.Bolet.Cliente.Contratos.ReportesMaestro;
using Trascend.Bolet.Cliente.Presentadores.ReportesMaestro;

namespace Trascend.Bolet.Cliente.Ventanas.ReportesMaestro
{
    /// <summary>
    /// Lógica de interacción para VisualizarReporte.xaml
    /// </summary>
    public partial class VisualizarReporte : Page, IVisualizarReporte
    {
        private bool _cargada;
        private PresentadorVisualizarReporte _presentador;

        /// <summary>
        /// Constructor por defecto que recibe una ventana padre
        /// </summary>
        /// <param name="ventanaPadre">Ventana que precede a esta ventana</param>
        public VisualizarReporte(object ventanaPadre, object resultado)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorVisualizarReporte(this, resultado, ventanaPadre);
        }


        #region IVisualizarReporte

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnCancelar.Focus();
        }

        public object Resultados
        {
            get { return this._grid.DataContext; }
            set { this._grid.DataContext = value; }
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

        #endregion

        private void _dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
