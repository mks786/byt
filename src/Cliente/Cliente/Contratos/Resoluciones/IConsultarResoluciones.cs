using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Resoluciones
{
    interface IConsultarResoluciones : IPaginaBase
    {
        object ResolucionSeleccionado { get; }

        string Id { get; }

        string FechaResolucion { get; }

        string Volumen { get; }

        string Pagina { get; }

        object Resultados { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }

    }
}
