using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICamposReporteRelacionServicios: IServicioBase<CamposReporteRelacion>
    {
        /// <summary>
        /// Servicio para consultar los campos seleccionados de un reporte
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca</param>
        /// <returns>Lista de campos de un reporte especifico</returns>
        IList<CamposReporteRelacion> ConsultarCamposDeReporte(Reporte reporte);

        /// <summary>
        /// Servicio que borra los campos definidos para un Reporte de Marca seleccionado 
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>True si el proceso se realiza exitosamente; False en caso contrario</returns>
        bool EliminarCamposReporte(Reporte reporte);
    }
}
