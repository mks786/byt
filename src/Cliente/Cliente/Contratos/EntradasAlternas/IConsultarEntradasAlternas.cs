using System;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.EntradasAlternas
{
    interface IConsultarEntradasAlternas : IPaginaBase
    {
        object EntradaAlternaSeleccionado { get; set; }

        string Id { get; set; }

        string Descripcion { get; set; }

        string FechaEntradaAlterna { get; set; }

        object Resultados { get; set; }

        object Medio { get; set; }

        object Medios { get; set; }

        object Receptor { get; set; }

        object Receptores { get; set; }

        object Remitente { get; set; }

        object Remitentes { get; set; }

        object Categoria { get; set; }

        object Categorias { get; set; }

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        string TotalHits { set; }

    }
}
