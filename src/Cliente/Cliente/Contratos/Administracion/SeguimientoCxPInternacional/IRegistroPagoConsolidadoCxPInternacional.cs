using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional
{
    interface IRegistroPagoConsolidadoCxPInternacional : IPaginaBase
    {
        String FechaPago { get; set; }

        object TiposPago { get; set; }

        object TipoPago { get; set; }

        object Bancos { get; set; }

        object Banco { get; set; }

        String DescripcionPago { get; set; }

        void Mensaje(string mensaje, int opcion);

        void CerrarVentanaException();
    }
}
