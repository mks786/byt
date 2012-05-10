using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Anualidades
{
    interface IConsultarAnualidades : IPaginaBase
    {
        string Id { get; set; }

        object AnualidadSeleccionada { get; set; }

        object Resultados { get; set; }

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string DescripcionFiltrar { get; set; }

        string Fecha { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string AsociadoFiltro { set; }

        string TotalHits { set; }
    }
}
