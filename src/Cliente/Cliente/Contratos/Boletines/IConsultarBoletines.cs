using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Boletines
{
    interface IConsultarBoletines : IPaginaBase
    {
        object BoletinSeleccionado { get; }

        string Id { get; }

        string FechaBoletin { get; }

        string FechaBoletinVence { get; }

        object Resultados { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }

    }
}
