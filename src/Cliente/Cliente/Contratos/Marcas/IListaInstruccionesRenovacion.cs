using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IListaInstruccionesRenovacion : IPaginaBase
    {
        object InstruccionSeleccionada { get; }

        object Resultados { get; set; }

        object TiposBusqueda { get; set; }

        object TipoBusqueda { get; set; }

        object BusquedaFiltrar { get; set; }

        string IdInstruccion { get; }

        string IdMarca { get; set; }

        string Tab { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
