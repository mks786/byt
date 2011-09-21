using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Agentes
{
    interface IConsultarAgentes : IPaginaBase
    {
        object AgenteFiltrar { get; set; }

        object AgenteSeleccionado { get; }

        object Resultados { get; set; }

        char EstadoCivil { get; }

        char Sexo { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
