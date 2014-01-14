using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Usuarios
{
    interface ICambiarClaveAcceso: IPaginaBase
    {
        object Usuario { get; set; }

        string Password { get; set; }

        string NuevoPassword { get; set; }

        string NuevoPassword_Rep { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
