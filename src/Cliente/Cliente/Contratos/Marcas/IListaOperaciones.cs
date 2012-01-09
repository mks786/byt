using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IListaOperaciones : IPaginaBase
    {
        object OperacionSeleccionado { get; }

        object Operaciones { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
