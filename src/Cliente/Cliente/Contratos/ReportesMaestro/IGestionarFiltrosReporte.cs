using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.ReportesMaestro
{
    interface IGestionarFiltrosReporte: IPaginaBase
    {
        
        object ReporteDeMarca { get; set; }

        string TituloReporteDeMarca { get; set; }

        object CamposSeleccionadosReporteDeMarca { get; set; }

        object CampoSeleccionadoReporteDeMarca { get; set; }

        object OperadoresDeReporte { get; set; }

        object OperadorDeReporte { get; set; }

        object FiltrosReporteDeMarca { get; set; }

        object FiltroReporteDeMarca { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
