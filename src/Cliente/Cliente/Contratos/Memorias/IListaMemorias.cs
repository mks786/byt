using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Memorias
{
    interface IListaMemorias : IPaginaBase
    {
        //string IdMemoria { get; }

        object MemoriaSeleccionada { get; }

        object Memorias { get; set; }

        //object TipoMensaje { get; set; }

        //object TiposMensajes { get; set; }

        //object FormatosDocumentos { get; set; }

        //object FormatoDocumento { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        object ListaResultados { get; set; }

        string TotalHits { set; }

        void Mensaje(string mensaje, int opcion);
    }
}
