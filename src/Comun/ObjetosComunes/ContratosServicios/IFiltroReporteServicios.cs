using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IFiltroReporteServicios : IServicioBase<FiltroReporte>
    {
        /// <summary>
        /// Servicio que obtiene los Filtros de un Reporte de Marca
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca usado en la consulta</param>
        /// <returns>Devuelve una lista de los filtros de un reporte de marca</returns>
        IList<FiltroReporte> ConsultarFiltrosReporte(Reporte reporteDeMarca);

    }
}
