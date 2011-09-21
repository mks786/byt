
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
namespace Trascend.Bolet.Cliente.Contratos.Poderes
{
    interface IAgregarPoder : IPaginaBase
    {
        object Poder { get; set; }

        object Boletines { get; set; }

        object Boletin { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        bool InteresadoEsEditable { get; set; }

        bool ConInteresado { get; set; }

        string TextoBotonCancelar { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }
    }
}
