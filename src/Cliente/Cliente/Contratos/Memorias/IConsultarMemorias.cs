using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Memorias
{
    interface IConsultarMemorias : IPaginaBase
    {
        object MemoriaFiltrar { get; set; }

        object MemoriaSeleccionado { get; }

        object Resultados { get; set; }

        string Id { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
