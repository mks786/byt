using System.Collections.Generic;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Pirateria.Casos
{
    interface IGestionarInfoTerceros : IPaginaBase
    {
        object Caso { get; set; }

        string NombreInteresado { get; set; }

        string NombreAsociado { get; set; }
    }
}
