using System.Windows.Controls;

namespace Trascend.Bolet.Cliente.Contratos.Inventores
{
    interface IAgregarInventor : IPaginaBase
    {
        object Inventor { get; set; }

        object Paises { get; set; }

        object Pais { get; set; }

        object Nacionalidades { get; set; }

        object Nacionalidad { get; set; }

        //void borrarId();

        void mensaje(string mensaje);
    }
}
