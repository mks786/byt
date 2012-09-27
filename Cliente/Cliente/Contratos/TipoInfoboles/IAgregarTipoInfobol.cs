using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.TipoInfoboles
{
    interface IAgregarTipoInfobol: IPaginaBase
    {
        object TipoInfobol { get; set; }

        void Mensaje(string mensaje);
    }
}