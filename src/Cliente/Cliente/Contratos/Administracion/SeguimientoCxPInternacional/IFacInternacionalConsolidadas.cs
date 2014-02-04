using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional
{
    interface IFacInternacionalConsolidadas : IPaginaBase
    {
        object FacturasAprobadas { get; set; }

        object FacturasAprobadasSoloVer { get; set; }

        string TotalMontoConsolidado { get; set; }

        void HabilitarListaSoloVer();

        void HabilitarBotonModificar();
    }
}
