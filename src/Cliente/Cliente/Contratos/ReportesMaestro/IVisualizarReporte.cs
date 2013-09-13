using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.ReportesMaestro
{
    interface IVisualizarReporte: IPaginaBase
    {
        object Resultados { get; set; }
    }
}
