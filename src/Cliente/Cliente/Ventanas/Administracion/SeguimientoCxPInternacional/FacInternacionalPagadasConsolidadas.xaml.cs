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
    /// Lógica de interacción para FacInternacionalPagadasConsolidadas.xaml
    /// </summary>
    public partial class FacInternacionalPagadasConsolidadas : Page, IFacInternacionalPagadasConsolidadas 
    {
        private bool _cargada;
        private PresentadorFacInternacionalPagadasConsolidadas _presentador;


        public FacInternacionalPagadasConsolidadas(object facAsociadoConsolidadas, object ventanaPadre)
        {
            InitializeComponent();
            this._cargada = false;
            this._presentador = new PresentadorFacInternacionalPagadasConsolidadas(this, facAsociadoConsolidadas, ventanaPadre);
        }

        #region IFacInternacionalPagadasConsolidadas

        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }

        public void FocoPredeterminado()
        {
            this._btnRegresar.Focus();
        }

        public object FacturasConsolidadas
        {
            get { return this._lstResultados.DataContext; }
            set { this._lstResultados.DataContext = value; }
        }

        public object FacturaConsolidada
        {
            get { return this._lstResultados.SelectedItem; }
            set { this._lstResultados.SelectedItem = value; }
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

        private void _lstResultados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this._presentador.IrConsultarFacturaProforma();
        }

        private void _btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.RegresarVentanaPadre();
        } 

        #endregion

        

        

        #region Metodos

        #endregion
    }
}
