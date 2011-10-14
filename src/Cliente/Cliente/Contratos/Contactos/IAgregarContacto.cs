using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Contactos
{
    interface IAgregarContacto : IPaginaBase
    {
        object Contacto { get; set; }

        void borrarId();
    }
}
