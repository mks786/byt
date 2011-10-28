using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Resumenes
{
    interface IConsultarResumenes : IPaginaBase
    {
        object ResumenSeleccionado { get; }

        object ResumenFiltrar { get; set; }

        object Resultados { get; set; }

        string Dias { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

    }
}
