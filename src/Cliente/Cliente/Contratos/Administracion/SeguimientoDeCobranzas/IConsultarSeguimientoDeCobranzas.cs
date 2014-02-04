using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trascend.Bolet.Cliente.Contratos.Administracion.SeguimientoDeCobranzas
{
    interface IConsultarSeguimientoDeCobranzas : IPaginaBase
    {
        
        string TotalHits { set; }

        object FiltroDeSeguimientoCobranzas { get; set; }

        object Monedas { get; set; }

        object Moneda { get; set; }

        object Usuarios { get; set; }

        object Usuario { get; set; }

        object Medios { get; set; }

        object Medio { get; set; }

        object Ordenamientos { get; set; }

        object Ordenamiento { get; set; }

        String Anio { get; set; }

        String Mes { get; set; }

        string IdAsociado { get; set; }

        string IdAsociadoFiltrar { get; set; }

        string NombreAsociadoFiltrar { get; set; }

        object Asociados { get; set; }

        object Asociado { get; set; }

        object CamposEjeXPivot { get; set; }

        object EjeXSeleccionado { get; set; }

        object CamposEjeYPivot { get; set; }

        object EjeYSeleccionado { get; set; }

        object CamposEjeZPivot { get; set; }

        object EjeZSeleccionado { get; set; }

        object Resultados { get; set; }

        string TotalGestiones { get; set; }

        void Mensaje(string mensaje, int opcion);

        void ActivarEjesPivot();

        void DesactivarEjesPivot();

    }
}
