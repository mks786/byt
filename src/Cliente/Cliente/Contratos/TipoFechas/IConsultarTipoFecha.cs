using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.TipoFechas
{
    interface IConsultarTipoFecha : IPaginaBase
    {
        object TipoFecha { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
