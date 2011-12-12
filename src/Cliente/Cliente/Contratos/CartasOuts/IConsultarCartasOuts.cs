using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.CartasOuts
{
    interface IConsultarCartasOuts : IPaginaBase
    {
        string Id { get; }

        object CartaSeleccionado { get; }

        object Resultados { get; set; }

        object CartaFiltrar { get; set; }

        string Fecha { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje);
    }
}
