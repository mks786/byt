using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.Logines
{
    interface ILogin: IPaginaBase
    {
        string Id { get; }

        string Password { get; }

        string MensajeError { get; set; }

    }
}
