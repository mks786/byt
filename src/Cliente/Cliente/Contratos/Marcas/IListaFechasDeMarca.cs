using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IListaFechasDeMarca : IPaginaBase
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        object Fechas { get; set; }

        object FechaSeleccionada { get; }

        string TotalHits { set; }

        ListView ListaResultados { get; set; }


    }
}
