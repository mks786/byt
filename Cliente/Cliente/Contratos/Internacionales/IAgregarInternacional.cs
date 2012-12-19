using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.Internacionales
{
    interface IAgregarInternacional: IPaginaBase
    {
        object Internacional { get; set; }

        void Mensaje(string mensaje);
    }
}