using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Marcas
{
    interface IConsultarMarcas : IPaginaBase
    {
        string Id { get; }

        object MarcaSeleccionada { get; }

        object Resultados { get; set; }

        string IdAsociadoFiltrar { get; }

        string NombreAsociadoFiltrar { get; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string IdInteresadoFiltrar { get; }

        string NombreInteresadoFiltrar { get; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        string DescripcionFiltrar { get; }

        string FichasFiltrar { get; }

        string Fecha { get; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        string AsociadoFiltro { set; }

        string InteresadoFiltro { set; }

        string TotalHits { set; }
    }
}
