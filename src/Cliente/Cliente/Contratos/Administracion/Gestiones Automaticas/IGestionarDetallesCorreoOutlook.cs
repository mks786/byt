using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.Gestiones_Automaticas
{
    interface IGestionarDetallesCorreoOutlook : IPaginaBase
    {
        String Remitente { get; set; }

        String Destinatario { get; set; }

        String ConCopiaA { get; set; }

        String Subject { get; set; }

        String CuerpoDelCorreo { get; set; }
    }
}
