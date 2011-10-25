using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.TipoFechas
{
    interface IAgregarTipoFecha: IPaginaBase
    {
        object TipoFecha { get; set; }

        void Mensaje(string mensaje);
    }
}