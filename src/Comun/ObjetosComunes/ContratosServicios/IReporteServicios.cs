using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IReporteServicios: IServicioBase<Reporte>
    {
        /// <summary>
        /// Serivicio que obtiene una lista de Reportes de Marca usando un filtro determinado
        /// </summary>
        /// <param name="reporteDeMarca">ReporteDeMarca filtro</param>
        /// <returns>Lista de reportes de marca por filtro</returns>
        IList<Reporte> ObtenerReporteFiltro(Reporte reporteDeMarca);

        /// <summary>
        /// Servicio que ejecuta la consulta del Reporte que se forma en el Cliente
        /// </summary>
        /// <param name="query">Consulta a ejecutar</param>
        /// <returns>DataSet que tiene la informacion resultante de la consulta</returns>
        DataSet EjecutarQuery(String query);
    }
}
