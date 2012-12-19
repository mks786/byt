using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.Internacionales
{
    interface IConsultarInternacional : IPaginaBase
    {
        object Internacional { get; set; }

        bool HabilitarCampos { set; }

        string TextoBotonModificar { get; set; }
    }
}
