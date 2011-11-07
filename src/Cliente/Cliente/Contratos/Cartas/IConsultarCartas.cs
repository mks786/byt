using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Cartas
{
    interface IConsultarCartas : IPaginaBase
    {
        string Id { get; }
        object CartaSeleccionado { get; }

        object Resultados { get; set; }

        object CartaFiltrar { get; set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string ResumenFiltrar { get; }

        string Fecha { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
