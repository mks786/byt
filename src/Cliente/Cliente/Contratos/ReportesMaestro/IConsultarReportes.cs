using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.ReportesMaestro
{
    interface IConsultarReportes : IPaginaBase
    {

        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        object ReporteDeMarcaFiltrar { get; set; }

        string IdReporte { get; set; }

        string DescripcionReporte { get; set; }

        string TituloEnIngles { get; set; }

        string TituloEnEspanol { get; set; }

        object Usuarios { get; set; }

        object Usuario { get; set; }

        object Idiomas { get; set; }

        object Idioma { get; set; }

        object Resultados { get; set; }

        object ReporteDeMarcaSeleccionado { get; set; }

        string TotalHits { set; }

        object TiposDeReporte { get; set; }

        object TipoDeReporte { get; set; }

        void Mensaje(string mensaje, int opcion);
    }
}
