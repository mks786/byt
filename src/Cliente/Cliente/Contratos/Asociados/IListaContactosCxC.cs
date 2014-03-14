using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Asociados
{
    interface IListaContactosCxC : IPaginaBase 
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }

        object ContactosCxC { get; set; }

        object ContactoCxC { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
