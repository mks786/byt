using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.ReportesMaestro
{
    interface IGestionarValoresFiltrosDeReporte: IPaginaBase
    {
        object Reporte { get; set; }

        string TituloReporte { get; set; }

        string Usuario { get; set; }
        
        object Filtros { get; set; }

        object CamposDelReporte { get; set; }

        object CampoDelReporte { get; set; }

        object TiposDeOrdenamiento { get; set; }

        object TipoDeOrdenamiento { get; set; }

        object OrdenamientosReporte { get; set; }

        object OrdenamientoReporte { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
