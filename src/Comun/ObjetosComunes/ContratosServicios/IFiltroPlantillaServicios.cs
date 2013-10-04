using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IFiltroPlantillaServicios: IServicioBase<FiltroPlantilla>
    {
        /// <summary>
        /// Servicio que consulta los filtros del encabezado de un maestro de plantilla determinado
        /// </summary>
        /// <param name="plantilla">Maestro de Plantilla a consultar sus filtros de encabezado</param>
        /// <returns>Lista de filtros de encabezado del maestro de plantilla seleccionado</returns>
        IList<FiltroPlantilla> ObtenerFiltrosEncabezadoPlantilla(MaestroDePlantilla plantilla);

        /// <summary>
        /// Servicio que consulta los filtros del detalle de un maestro de plantilla determinado
        /// </summary>
        /// <param name="plantilla">Maestro de Plantilla a consultar sus filtros de detalle</param>
        /// <returns>Lista de filtros de detalle de la plantilla seleccionada</returns>
        IList<FiltroPlantilla> ObtenerFiltrosDetallePlantilla(MaestroDePlantilla plantilla);

    }
}
