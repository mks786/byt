using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.CadenaDeCambio
{
    interface IListadoCadenasDeCambios : IPaginaBase
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        object CadenasDeCambios { get; set; }

        string TotalHits { set; }
    }
}
