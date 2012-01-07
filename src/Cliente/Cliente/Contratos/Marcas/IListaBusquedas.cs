using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IListaBusquedas : IPaginaBase
    {
        object BusquedaSeleccionada { get; }

        object Resultados { get; set; }

        object TiposBusqueda { get; set; }

        object TipoBusqueda { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
