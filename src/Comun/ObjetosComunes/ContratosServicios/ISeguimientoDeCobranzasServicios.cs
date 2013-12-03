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

        /// <summary>
        /// Servicio que obtiene el detalle de los datos presentado en el cuadro Resumen
        /// </summary>
        /// <param name="filtroDetalle">Filtro utilizado para obtener la data cruda</param>
        /// <param name="cadenaFiltroDetalle">Parametros necesarios para la consulta para obtener el detalle de las gestiones segun el cuadro Resumen</param>
        /// <returns>DataTable con la informacion de detalle</returns>
        DataTable ObtenerDetalle(FiltroDataCrudaCobranza filtroDetalle, String cadenaFiltroDetalle);

        /// <summary>
        /// Servicio que obtiene el detalle por columna de los datos presentados en el cuadro Resumen
        /// </summary>
        /// <param name="filtro">Filtro utilizado para la data cruda usando tambien en este servicio</param>
        /// <param name="ejeX">Parametro para el eje X utilizado en el cuadro Resumen</param>
        /// <param name="ejeY">Parametro para el eje Y utilizado en el cuadro Resumen</param>
        /// <param name="parametros">Parametros filtro para los ejes X y Y</param>
        /// <returns>DataTable con los datos de detalle por columna</returns>
        DataTable ObtenerDetalleDeTotales(FiltroDataCrudaCobranza filtro, ListaDatosValores ejeX, ListaDatosValores ejeY, String[] parametros);
    }
}
