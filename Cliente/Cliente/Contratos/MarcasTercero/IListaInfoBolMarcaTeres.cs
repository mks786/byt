using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.MarcasTercero
{
    interface IListaInfoBolMarcaTeres : IPaginaBase
    {
        object InfoBolMarcaTerSeleccionado { get; }

        object InfoBolMarcaTeres { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
