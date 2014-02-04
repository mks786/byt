using System;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Linq;
using System.Text;
using System.Data;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ISeguimientoCxPInternacionalServicios
    {
        /// <summary>
        /// Servicio que obtiene el Resumen General de Saldos de CxP Internacional de la vista FAC_ASO_SALDO_CXP
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <returns>DataTable con el Resumen General de Saldo de CxP Internacional</returns>
        DataTable ObtenerResumenGeneralCxPInternacional(FiltroDataCrudaCxPInternacional filtro);

        /// <summary>
        /// Servicio que obtiene la data cruda para generar la tabla pivot (Tabla Resumen de Datos) a partir de la vista 
        /// FAC_CXP_INT_VI
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <returns>DataTable con la data necesaria para generar la Tabla de Resumen de Datos</returns>
        DataTable ObtenerDataCruda(FiltroDataCrudaCxPInternacional filtro);


        /// <summary>
        /// Servicio que presenta el detalle de una celda seleccionada con las CxP Internacional
        /// </summary>
        /// <param name="ejeX">Parametro X a consultar</param>
        /// <param name="ejeY">Parametro Y a consultar</param>
        /// <param name="cadenaFiltroDetalle">Datos a filtrar en la consulta</param>
        /// <returns>DataTable con el detalle de lo seleccionado</returns>
        DataTable ObtenerDetalle(ListaDatosValores ejeX, ListaDatosValores ejeY, String cadenaFiltroDetalle, FiltroDataCrudaCxPInternacional filtro);

        /// <summary>
        /// Servicio que obtiene los datos de detalle de los totales por columna
        /// </summary>
        /// <param name="filtro">Datos filtro para ejecutar la consulta</param>
        /// <param name="ejeX">Eje X implementado para la consulta de la data cruda</param>
        /// <param name="ejeY">Eje Y implementado para la consulta de la data cruda</param>
        /// <param name="parametros">Parametros necesarios para el WHERE en la consulta</param>
        /// <returns>DataTable con los datos de detalles por totales verticales</returns>
        DataTable ObtenerDetalleDeTotales(FiltroDataCrudaCxPInternacional filtro, ListaDatosValores ejeX, ListaDatosValores ejeY, String[] parametros);


    }
}
