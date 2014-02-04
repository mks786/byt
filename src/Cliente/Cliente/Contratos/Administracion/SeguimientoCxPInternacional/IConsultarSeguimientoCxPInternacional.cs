using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoCxPInternacional
{
    interface IConsultarSeguimientoCxPInternacional : IPaginaBase
    {
        object FiltroDataCruda { get; set; }

        //object Monedas { get; set; }

        //object Moneda { get; set; }

        object TiposDeudas { get; set; }

        object TipoDeuda { get; set; }

        string RangoInferior {set;}

        string RangoSuperior { get; set; }

        object Ordenamientos { get; set; }

        object Ordenamiento { get; set; }

        string IdAsociado { get; set; }

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        object Resultados { get; set; }

        string TotalUSD { get; set; }

        string TotalPorVencer { get; set; }

        string TotalVencido { get; set; }

        string TotalHits {set;}

        object CamposEjeXPivot { get; set; }

        object EjeXSeleccionado { get; set; }

        object CamposEjeYPivot { get; set; }

        object EjeYSeleccionado { get; set; }

        object CamposEjeZPivot { get; set; }

        object EjeZSeleccionado { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ActivarEjesPivot();

        void DesactivarEjesPivot();
    }
}
