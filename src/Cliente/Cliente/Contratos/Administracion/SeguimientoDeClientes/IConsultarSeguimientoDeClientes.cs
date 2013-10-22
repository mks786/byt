using System.Windows.Controls;
using Trascend.Bolet.Cliente.Ayuda;
using System;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeClientes
{
    interface IConsultarSeguimientoDeClientes : IPaginaBase
    {

        object FiltroDataCruda { get; set; }

        object Monedas {get; set;}

        object Moneda { get; set; }

        //string Annio { get; set; }

        //object Meses { get; set; }
        
        //object Mes { get; set; }

        string RangoInferior {set;}

        string RangoSuperior { get; set; }

        object Ordenamientos { get; set; }

        object Ordenamiento { get; set; }
        
        string IdAsociado { get; set; }

        string IdAsociadoFiltrar {get; set;}
        
        string NombreAsociadoFiltrar {get; set;}
        
        object Asociados {get; set;}
        
        object Asociado {get; set;}

        object Resultados { get; set; }
        
        string TotalHits {set;}

        //Ejes para la tabla Pivot

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
