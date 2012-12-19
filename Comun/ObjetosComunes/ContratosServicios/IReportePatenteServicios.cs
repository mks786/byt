using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IReportePatenteServicios : IServicioBase<ReportePatente>
    {
        /// <summary>
        /// Servicio que realiza la ejecución de procedimientos de Base de Datos
        /// </summary>
        /// <param name="parametroProcedimiento">objeto que contiene todos los datos necesarios para la ejecución del procedimiento</param>
        /// <returns>Planilla que insertó</returns>
        ReportePatente ImprimirProcedimiento(ParametroProcedimiento parametroProcedimiento);
    }
}
