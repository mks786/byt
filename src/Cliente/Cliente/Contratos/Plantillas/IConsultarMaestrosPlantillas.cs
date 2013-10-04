using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Plantillas
{
    interface IConsultarMaestrosPlantillas : IPaginaBase
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        object MaestroPlantillaFiltrar { get; set; }

        string IdMaestroPlantilla { get; set; }

        object Idiomas { get; set; }

        object Idioma { get; set; }

        object Referidos { get; set; }

        object Referido { get; set; }

        object Criterios { get; set; }

        object Criterio { get; set; }

        //object Departamentos { get; set; }

        //object Departamento { get; set; }

        object Encabezados { get; set; }

        object Encabezado { get; set; }

        object Detalles { get; set; }

        object Detalle { get; set; }

        object Resultados { get; set; }

        object MaestroDePlantillaSeleccionado { get; set; }

        string TotalHits { set; }

        void Mensaje(string mensaje, int opcion);
    }
}
