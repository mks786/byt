using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.TiposBase
{
    interface IConsultarTiposBase : IPaginaBase
    {
        object TipoBaseSeleccionado { get; }

        object TipoBaseFiltrar { get; set; }

        object Resultados { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
