using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Usuarios
{
    interface IConsultarUsuarios: IPaginaBase
    {

        string Id { get; }

        string NombreCompleto { get; }

        string Iniciales { get; }

        object Rol { get; }

        object Departamento { get; }

        string Email { get; }

        object Departamentos { get; set; }

        object Resultados { get; set; }

        object Roles { get; set; }

        object UsuarioSeleccionado { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
