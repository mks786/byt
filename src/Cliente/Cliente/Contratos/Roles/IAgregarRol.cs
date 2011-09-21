using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Roles
{
    interface IAgregarRol : IPaginaBase
    {
        object Rol { get; set; }

        string Id { get; }

        string Descripcion { get; }
    }
}
