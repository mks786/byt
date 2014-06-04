using System.Collections.Generic;
using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Pirateria.Casos
{
    interface IConsultarCasos : IPaginaBase
    {
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        object Caso { get; set; }

        string IdCaso { get; set; }

        string DescripcionCaso { get; set; }

        string FechaCaso { get; set; }

        object OrigenesCaso { get; set; }

        object OrigenCaso { get; set; }

        object SituacionesCaso { get; set; }

        object SituacionCaso { get; set; }

        object TiposDeCaso { get; set; }

        object TipoDeCaso { get; set; }

        object AccionesDeCaso { get; set; }

        object AccionDeCaso { get; set; }

        string AsociadoFiltro { set; }

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        string InteresadoFiltro { set; }

        string IdInteresadoFiltrar { get; set; }

        string NombreInteresadoFiltrar { get; set; }

        object Interesados { get; set; }

        object Interesado { get; set; }

        object Resultados { get; set; }

        object CasoSeleccionado { get; set; }

        string TotalHits { set; }
        
        void Mensaje(string mensaje, int opcion);
        
    }
}
