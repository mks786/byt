using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.Patentes
{
    public interface IListaPatentesPrioridadVencidaStartUp : IPaginaBase
    {
        object PatentesPorVencerPrioridad { get; set; }

        string TotalHits { set; }
    }
}
