using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Estatuses
{
    interface IConsultarEstatuses : IPaginaBase
    {
        object EstatusFiltrar { get; set; }

        object EstatusSeleccionado { get; }

        object Resultados { get; set; }

        string Id { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
