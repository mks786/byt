using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System.Data;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional
{
    interface IListaDatosPivotSeguimientoCxPInternacional : IPaginaBase
    {
        object Resultados { get; set; }

        object ResultadosDetalle { get; set; }

        object ResultadoSeleccionado { get; }

        string TotalHits { set; }

        string TotalHitsDetalle { set; }

        string TotalDolares { get; set; }

        //string TotalBolivares { get; set; }

        string TotalGlobalDolares { get; set; }

        //string TotalGlobalBolivares { get; set; }

        string EjesResumen { set; }

        void Mensaje(string mensaje, int opcion);

        void VisibilidadListaDetalle();

        String ObtenerIdsFacInternacional();

        void ExportarDataGrid(String tipo, DataTable datosResumen);

        void PintarConsolidar();

        void HabilitarBotonVerSeleccion(bool value);
    }
}
