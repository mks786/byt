using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IListaInteresadosMarca : IPaginaBase
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        object InteresadosDeMarca { get; set; }

        object InteresadoSeleccionado { get; }

        string TotalHits { set; }
    }
}
