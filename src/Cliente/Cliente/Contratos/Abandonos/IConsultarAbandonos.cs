using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Abandonos
{
    interface IConsultarAbandonos : IPaginaBase
    {
        string Id { get; set; }

        string NombreMarca { set; }

        string IdMarca { set; }

        object AbandonoSeleccionado { get; set; }

        object Resultados { get; set; }

        string IdMarcaFiltrar { get; set; }

        string NombreMarcaFiltrar { get; set; }

        object Marcas { get; set; }

        object Marca { get; set; }

        string Fecha { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string TotalHits { set; }

        void ConvertirEnteroMinimoABlanco();
    }
}
