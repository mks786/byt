using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Poderes
{
    interface IGestionarPoderesXAgentes : IPaginaBase
    {
        object Poderes { get; set; }

        object PoderesXAgentes { get; set; }

        object Agentes { get; set; }

        object AgenteSeleccionado { get; }

        object PoderesSeleccionados { get; }

        object PoderesXAgentesSeleccionados { get; }

        string Id { get; }

        string NumPoder { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaAgentes { get; set; }

        ListView ListaPoderes { get; set; }

        ListView ListaAgentesXPoderes { get; set; }
    }
}
