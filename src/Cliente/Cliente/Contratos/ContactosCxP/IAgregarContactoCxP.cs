using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.ContactosCxP
{
    interface IAgregarContactoCxP: IPaginaBase 
    {
        object ContactoCxP { get; set; }

        object FormasDePago { get; set; }

        object FormaDePago { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
