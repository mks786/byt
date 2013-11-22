using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System.Data;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeClientes
{
    interface IListaDatosPivotSeguimientoClientes : IPaginaBase
    {
        object Resultados { get; set; }

        object ResultadosDetalle { get; set; }

        string TotalHits { set; }

        string TotalHitsDetalle { set; }

        string TotalDolares { get; set; }

        string TotalBolivares { get; set; }

        string TotalGlobalDolares { get; set; }

        string TotalGlobalBolivares { get; set; }

        string EjesResumen { set; }

        void Mensaje(string mensaje, int opcion);

        void VisibilidadListaDetalle();

        void FormatearDataGrid();

        void ExportarDataGrid(String tipo, DataTable datosResumen);
    }
}
