using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;

namespace Trascend.Bolet.Cliente.Contratos.Recordatorios
{
    interface IConsultarRecordatorios : IPaginaBase
    {        

        string FechaDesdeFiltro { get; }

        string FechaHastaFiltro { get; }

        string MesFiltro { get; }

        string AnoFiltro { get; }

        bool? EmailFiltro { get; }

        bool? FaxFiltro { get; }

        bool? TodosFiltro { get; }        

        object Resultados { get;  set; }

        object Recordatorio { get; set; }

        object Recordatorios { get; set; }
            
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        void LimpiarFiltros();

        void GestionarEnableChecksFiltro(bool value);

        string TotalHits { set; }
    }
}
