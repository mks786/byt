using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Usuarios
{
    interface IConsultarUsuarios: IPaginaBase
    {

        string Id { get; set; }

        string NombreCompleto { get; set; }

        string Iniciales { get; set; }

        object Rol { get; set; }

        object Departamento { get; set; }

        string Email { get; set; }

        object Departamentos { get; set; }

        object Resultados { get; set; }

        object Roles { get; set; }

        object UsuarioSeleccionado { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
