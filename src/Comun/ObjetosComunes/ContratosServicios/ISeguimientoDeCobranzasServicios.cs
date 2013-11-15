using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ISeguimientoDeCobranzasServicios
    {
        /// <summary>
        /// Servicio que consulta la vista SEG_COB para obtener los datos de resumen iniciales segun el filtro dado
        /// </summary>
        /// <param name="filtro">Filtro a ejecutar sobre la base de datos</param>
        /// <returns>DataTable con Datos de Resumen General</returns>
        DataTable GenerarDatosResumenGeneral(FiltroDataCrudaCobranza filtro);

        /// <summary>
        /// Servicio que obtiene la data cruda segun los filtros de los ejes. Esta data se usa en la data pivot
        /// </summary>
        /// <param name="filtro">Filtro a ejecutar sobre la base de datos</param>
        /// <returns>DataTable con los datos crudos a usar antes del pivot</returns>
        DataTable ObtenerDataCruda(FiltroDataCrudaCobranza filtro);

        
        DataTable ObtenerDetalle(FiltroDataCrudaCobranza filtroDetalle, String cadenaFiltroDetalle);
    }
}
