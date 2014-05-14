using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IPresentacionSapiDetalleServicios : IServicioBase<PresentacionSapiDetalle>
    {
        /// <summary>
        /// Servicio que, usando un filtro, obtiene un conjunto de Solicitudes de Presentaciones SAPI
        /// </summary>
        /// <param name="filtro">Filtro utilizado para obtener un conjunto de Solicitudes de Presentaciones Sapi</param>
        /// <returns>Lista de Solicitudes de Presentaciones Sapi de acuerdo a un filtro determinado</returns>
        IList<PresentacionSapiDetalle> ObtenerSolicitudesPresentacionSapiFiltro(PresentacionSapiDetalle filtro);
    }
}
