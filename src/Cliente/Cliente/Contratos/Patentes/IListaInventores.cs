using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Patentes
{
    interface IListaInventores : IPaginaBase
    {
        object InventorSeleccionado { get; }

        object Inventores { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
