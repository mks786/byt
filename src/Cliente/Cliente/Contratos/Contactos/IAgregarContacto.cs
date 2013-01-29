using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Contactos
{
    interface IAgregarContacto : IPaginaBase
    {
        object Contacto { get; set; }

        object Departamento { get; set; }

        object Departamentos { get; set; }

        string setFuncion { set; }

        string getFuncion { get; }

        string getCorrespondencia { get; }

        string setCorrespondencia { set; }

        void borrarId();

        void mensaje(string mensaje);
    }
}
