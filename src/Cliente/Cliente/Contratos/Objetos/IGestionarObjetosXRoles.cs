using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Objetos
{
    interface IGestionarObjetosXRoles : IPaginaBase
    {
        object Objetos { get; set; }

        object ObjetosXRoles { get; set; }

        object Roles { get; set; }

        object RolSeleccionado { get; }

        object ObjetosSeleccionados { get; }

        object ObjetosXRolesSeleccionados { get; }

        string Id { get; }

        string Descripcion { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaRoles { get; set; }

        ListView ListaObjetos { get; set; }

        ListView ListaRolesXObjetos { get; set; }
    }
}
