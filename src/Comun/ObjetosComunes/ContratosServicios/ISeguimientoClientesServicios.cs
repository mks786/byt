using System;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Linq;
using System.Text;
using System.Data;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ISeguimientoClientesServicios
    {
        /// <summary>
        /// Servicio que obtiene los datos crudos para luego ser transformados en tabla pivot
        /// </summary>
        /// <param name="filtro">Filtro para la data cruda</param>
        /// <returns>DataTable con el resultado de la consulta en base de datos; en caso contrario, retorna NULL</returns>
        DataTable ObtenerDatosSaldos(FiltroDataCruda filtro);

        /// <summary>
        /// Servicio que obtiene los datos crudos a partir de los datos de los saldos
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        DataTable ObtenerDataCruda(FiltroDataCruda filtro);

        /// <summary>
        /// Servicio que presenta el detalle de una celda seleccionada con todas las facturas del cliente
        /// </summary>
        /// <param name="ejeX">Parametro X a consultar</param>
        /// <param name="ejeY">Parametro Y a consultar</param>
        /// <param name="cadenaFiltroDetalle">Datos a filtrar en la consulta</param>
        /// <returns>DataTable con el detalle de lo seleccionado</returns>
        DataTable ObtenerDetalle(ListaDatosValores ejeX, ListaDatosValores ejeY, String cadenaFiltroDetalle, FiltroDataCruda filtro);

        /// <summary>
        /// Servicio que obtiene los datos de detalle de los totales por columna
        /// </summary>
        /// <param name="filtro">Filtro para filtrar los datos</param>
        /// <param name="ejeX">Eje X seleccionado</param>
        /// <param name="ejeY">Eje Y seleccionado</param>
        /// <param name="parametros">Parametros necesarios para el WHERE en la consulta</param>
        /// <returns>DataTable con el detalle por columna</returns>
        DataTable ObtenerDetalleDeTotales(FiltroDataCruda filtro, ListaDatosValores ejeX, ListaDatosValores ejeY, String[] parametros);
        
    }
}
