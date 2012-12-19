using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Recordatorios
{
    interface IConsultarRecordatorios : IPaginaBase
    {

        DateTime? FechaDesdeFiltro { get; set; }

        DateTime? FechaHastaFiltro { get; set; }

        string MesFiltro { get; set; }

        string AnoFiltro { get; set; }

        bool? EmailFiltro { get; }

        bool? FaxFiltro { get; }

        bool? TodosFiltro { get; }

        bool? AutomaticoFiltro { get; set; }

        object Resultados { get;  set; }

        object Resultado { get; set; }

        object Recordatorio { get; set; }

        object Recordatorios { get; set; }
            
        GridViewColumnHeader CurSortCol { get; set; }

        SortAdorner CurAdorner { get; set; }

        ListView ListaResultados { get; set; }

        void Mensaje(string mensaje, int opcion);

        bool MensajeAlerta(string mensaje);

        void LimpiarFiltros();

        void SeleccionarTodos(int longitud);

        void GestionarEnableChecksFiltro(bool value);

        string TotalHits { set; }
    }
}
