using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Trascend.Bolet.Cliente.Contratos.ReportesMaestro
{
    interface IVisualizarReporte: IPaginaBase
    {
        object Reporte { get; set; }

        string TituloReporte { get; set; }

        string AutorReporte { get; set; }

        object FiltrosReporte { get; set; }

        string TotalHits { set; }

        object Resultados { get; set; }

        void ExportarDataGrid();

        void Mensaje(string mensaje, int opcion);

        void LlenarDataGrid(DataTable datos);
    }
}
