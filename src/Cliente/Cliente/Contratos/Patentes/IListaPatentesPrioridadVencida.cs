using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;
using System.Data;

namespace Trascend.Bolet.Cliente.Contratos.Patentes
{
    interface IListaPatentesPrioridadVencida : IPaginaBase
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        object PatentesPorVencerPrioridad { get; set; }

        object PatenteSeleccionada { get; set; }

        string TotalHits { set; }

        void Mensaje(string mensaje, int opcion);

        void ExportarListadoDePatentesPorVencer(DataTable datos);

        void DeshabilitarBotonReportes();
    }
}
