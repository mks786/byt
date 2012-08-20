using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Asociados
{
    interface IListaJustificaciones : IPaginaBase
    {
        object JustificacionSeleccionada { get; }

        object Justificaciones { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
