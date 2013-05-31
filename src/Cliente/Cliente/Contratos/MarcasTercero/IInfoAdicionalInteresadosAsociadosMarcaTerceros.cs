using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.MarcasTercero
{
    interface IInfoAdicionalInteresadosAsociadosMarcaTerceros: IPaginaBase
    {
        object MarcaTercero { get; set; }
        
        string NombreInteresadoTercero { get; set; }

        string NombreAsociadoTercero { get; set; }

        string InteresadoTercero { get; set; }

        string Tab { get; }
    }
}
