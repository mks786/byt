using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Objetos
{
    interface IConsultarObjetos : IPaginaBase
    {
        object ObjetoSeleccionado { get; }

        string Id { get; }

        string Descripcion { get; }

        object Resultados { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
