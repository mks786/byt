using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Asociados
{
    interface IListaDatosTransferencia : IPaginaBase
    {
        object DatosTransferenciaSeleccionada { get; }

        object DatosTransferencias { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }

        void PresentarBotonSeleccionarDatos();
    }
}
