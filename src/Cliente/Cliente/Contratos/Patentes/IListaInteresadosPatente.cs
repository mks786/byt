using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;
namespace Trascend.Bolet.Cliente.Contratos.Patentes
{
    interface IListaInteresadosPatente : IPaginaBase
    {        
        object InteresadosDePatente { get; set; }

        object InteresadoSeleccionado { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }
    }
}
