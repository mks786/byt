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
using System.Windows.Shapes;
using Trascend.Bolet.Cliente.Presentadores.ReportesPatente;
using Trascend.Bolet.Cliente.Contratos.ReportesPatente;

namespace Trascend.Bolet.Cliente.Ventanas.ReportesPatentes
{
    /// <summary>
    /// Interaction logic for Reportes.xaml
    /// </summary>
    public partial class ReportesBotones : Window, IReportePatente
    {

        private PresentadorReportePatente _presentador;
        private bool _cargada;


        #region IReportePatente


        public bool EstaCargada
        {
            get { return this._cargada; }
            set { this._cargada = value; }
        }


        public void FocoPredeterminado()
        {
        }


        public void MensajeAlerta(string mensaje)
        {
            MessageBox.Show(mensaje,
                "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }

        public void MensajeExito(string mensaje)
        {
            MessageBox.Show(mensaje,
                   "Reporte exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        #endregion


        public ReportesBotones(object patente)
        {
            InitializeComponent();
            _cargada = false;
            _presentador = new PresentadorReportePatente(this, patente);

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!EstaCargada)
            {
                this._presentador.CargarPagina();
                EstaCargada = true;
            }
        }


        private void _btnSolicitudVan_Click(object sender, RoutedEventArgs e)
        {
            this._presentador.ImprimirSolicitudVan();
        }


        private void _btnSolicitudVienen_Click(object sender, RoutedEventArgs e)
        {
            _presentador.ImprimirSolicitudVienen();
        }


        private void _btnDatosVan_Click(object sender, RoutedEventArgs e)
        {
            //_presentador.ImprimirDatosVan();
            _presentador.ImprimirPlanilla();
        }


        private void _btnDatosVienen_Click(object sender, RoutedEventArgs e)
        {
            _presentador.ImprimirDatosVienen();
        }


        private void _btnPlanilla_Click(object sender, RoutedEventArgs e)
        {
            //_presentador.ImprimirPlanilla();
            _presentador.ImprimirDatosVan();
        }


    }
}
