using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Paises
{
    interface IConsultarPaises : IPaginaBase
    {
        object PaisFiltrar { get; set; }

        object PaisSeleccionado { get; }

        object Resultados { get; set; }

        string Id { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
