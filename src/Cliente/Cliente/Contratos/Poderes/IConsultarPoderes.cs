using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Poderes
{
    interface IConsultarPoderes : IPaginaBase
    {
        object PoderSeleccionado { get; set; }

        object Resultados { get; set; }

        string Id { get; set; }

        string IdInteresadoFiltrar { get; set; }

        string NombreInteresadoFiltrar { get; set; }

        string NumPoder { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        string Facultad { get; set; }

        string Anexo { get; set; }

        string Observaciones { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
