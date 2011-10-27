using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Resumenes
{
    interface IConsultarBoletines : IPaginaBase
    {
        object ResumenSeleccionado { get; }

        object ResumenFiltrar { get; set; }

        object Resultados { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

    }
}
