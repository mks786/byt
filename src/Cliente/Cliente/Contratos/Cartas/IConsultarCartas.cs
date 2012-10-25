using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Cartas
{
    interface IConsultarCartas : IPaginaBase
    {
        string Id { get; set; }

        object CartaSeleccionado { get; }

        object Resultados { get; set; }

        object CartaFiltrar { get; set; }

        object Responsable { get; set; }

        object Responsables { get; set; }

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string ResumenFiltrar { get; set; }

        string ReferenciaFiltrar { get; set; }

        string Fecha { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje,int opcion);

        string TotalHits { set; }

        string NombreAsociado { set; }
    }
}
