using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.TipoFechas
{
    interface IConsultarTipoFechas : IPaginaBase
    {
        object TipoFechaSeleccionada { get; }

        string Id { get; }

        string Descripcion { get; }

        object Resultados { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
