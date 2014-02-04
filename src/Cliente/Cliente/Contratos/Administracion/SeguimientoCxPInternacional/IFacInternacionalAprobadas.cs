using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional
{
    interface IFacInternacionalAprobadas : IPaginaBase
    {
        object FacturasAutorizadas { get; set; }

        object FacturasSeleccionadas { get; set; }

        string TotalMontoAprobado { get; set; }

        string TotalHits { set; }

        void Mensaje(string mensaje, int opcion);
    }
}
