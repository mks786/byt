using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.TipoInfoboles
{
    interface IConsultarTipoInfobol : IPaginaBase
    {
        object TipoInfobol { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
