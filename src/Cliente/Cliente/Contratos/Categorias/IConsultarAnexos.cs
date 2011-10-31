using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Categorias
{
    interface IConsultarCategorias : IPaginaBase
    {
        object CategoriaFiltrar { get; set; }

        object CategoriaSeleccionado { get; }

        object Resultados { get; set; }

        string Id { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
