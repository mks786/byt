using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IFiltroPlantillaServicios: IServicioBase<FiltroPlantilla>
    {
        /// <summary>
        /// Servicio que consulta los filtros de una plantilla determinada
        /// </summary>
        /// <param name="plantilla">Plantilla a consultar sus filtros de encabezado</param>
        /// <returns>Lista de filtros de encabezado de la plantilla seleccionada</returns>
        IList<FiltroPlantilla> ObtenerFiltrosEncabezadoPlantilla(Plantilla plantilla);

        /// <summary>
        /// Servicio que consulta los filtros del detalle de una plantilla determinada
        /// </summary>
        /// <param name="plantilla">Plantilla a consultar sus filtros de detalle</param>
        /// <returns>Lista de filtros de detalle de la plantilla seleccionada</returns>
        IList<FiltroPlantilla> ObtenerFiltrosDetallePlantilla(Plantilla plantilla);

    }
}
