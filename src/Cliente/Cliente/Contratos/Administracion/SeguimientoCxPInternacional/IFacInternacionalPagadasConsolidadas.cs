using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional
{
    interface IFacInternacionalPagadasConsolidadas : IPaginaBase
    {
        object FacturasConsolidadas { get; set; }

        object FacturaConsolidada { get; set; }

    }
}
