using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Remitentes
{
    interface IConsultarRemitentes : IPaginaBase
    {
        object RemitenteSeleccionado { get; set; }

        object Resultados { get; set; }

        char TipoRemitente { get; set; }

        object RemitenteFiltrar { get; set; }

        object Paises { get; set; }

        object Pais { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
