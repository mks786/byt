using System;
using System.Data;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeCobranzas
{
    interface IListaDatosPivotSeguimientoCobranzas: IPaginaBase
    {
        object Resultados { get; set; }

        object ResultadosDetalle { get; set; }

        string TotalHits { set; }

        string TotalHitsDetalle { set; }

        void Mensaje(string mensaje, int opcion);

        void ExportarDataGrid(String tipo);

        void VisibilidadListaDetalle();
    }
}
