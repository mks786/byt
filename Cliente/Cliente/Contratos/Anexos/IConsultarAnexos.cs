using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Anexos
{
    interface IConsultarAnexos : IPaginaBase
    {
        object AnexoFiltrar { get; set; }

        object AnexoSeleccionado { get; }

        object Resultados { get; set; }

        string Id { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
