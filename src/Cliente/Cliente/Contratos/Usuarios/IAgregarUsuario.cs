using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Usuarios
{
    interface IAgregarUsuario : IPaginaBase
    {
        object Usuario { get; set; }

        object Rol { get; set; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        object Roles { get; set; }
    }
}
