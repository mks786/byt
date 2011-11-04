using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Poderes
{
    interface IConsultarPoderes : IPaginaBase
    {
        object PoderSeleccionado { get; }

        object Resultados { get; set; }

        string Id { get; }

        string IdInteresadoFiltrar { get; }

        string NombreInteresadoFiltrar { get; }

        string NumPoder { get; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        string Facultad { get; }

        string Anexo { get; }

        string Observaciones { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
