using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System.Data;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional
{
    interface IFacInternacionalConsolidadas : IPaginaBase
    {
        object FacturasAprobadas { get; set; }

        object FacturasAprobadasSoloVer { get; set; }

        string TotalMontoConsolidado { get; set; }

        string TotalHits { set; }

        void HabilitarListaSoloVer();

        void HabilitarBotonModificar();

        void Mensaje(string mensaje, int opcion);

        void ExportarDatosConsolidadosExcel(String tipo, DataTable datosResumen);
    }
}
