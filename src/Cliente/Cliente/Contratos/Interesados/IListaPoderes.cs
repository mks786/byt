using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Interesados
{
    interface IListaPoderes : IPaginaBase
    {
        object PoderSeleccionado { get; }

        object Poderes { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
