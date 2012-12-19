using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Corresponsales
{
    interface IConsultarCorresponsales : IPaginaBase
    {
        object CorresponsalFiltrar { get; set; }

        object CorresponsalSeleccionado { get; set; }

        object Resultados { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }

        string GetIdCorresponsal { get; }
    }
}
