using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.CartaOuts
{
    interface IConsultarCartaOuts : IPaginaBase
    {
        object CartaOutFiltrar { get; set; }

        object CartaOutSeleccionado { get; }

        object Resultados { get; set; }

        int Id { get; set; }

        int IdAsociado { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
