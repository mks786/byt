using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICamposReporteServicios: IServicioBase<CamposReporte>
    {
        /// <summary>
        /// Servicio que obtiene los campos que pertenecen al reporte de Marca
        /// </summary>
        /// <returns>Lista de campos para el reporte de marca</returns>
        IList<CamposReporte> ObtenerCamposReporteDeMarca();

        /// <summary>
        /// Servicio que obtiene los campos que pertenecen al reporte de Patente
        /// </summary>
        /// <returns>Lista de campos para el reporte de patente</returns>
        IList<CamposReporte> ObtenerCamposReportePatente();

    }
}
