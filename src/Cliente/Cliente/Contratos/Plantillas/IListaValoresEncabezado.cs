using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Plantillas
{
    interface IListaValoresEncabezado: IPaginaBase
    {
        object FiltrosEncabezado { get; set; }

        object FiltroEncabezado { get; set; }

        string TotalHits { get; set; }
    }
}
