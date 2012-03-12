using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Abandonos
{
    interface IConsultarAbandonos : IPaginaBase
    {
        string Id { get; }

        string NombreMarca { set; }

        object AbandonoSeleccionado { get; }

        object Resultados { get; set; }

        string IdMarcaFiltrar { get; }

        string NombreMarcaFiltrar { get; }

        object Marcas { get; set; }

        object Marca { get; set; }      

        string Fecha { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string TotalHits { set; }
    }
}
