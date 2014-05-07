using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ISolicitudSapiServicios : IServicioBase<SolicitudSapi>
    {
        /// <summary>
        /// Servicio que inserta o actualiza una lista de Detalles de Solicitud de Materiales SAPI
        /// </summary>
        /// <param name="solicitudesMateriales">Renglones de Detalle de las Solicitudes Materiales</param>
        /// <param name="operacion">Operacion de Base de Datos (CREATE o MODIFY)</param>
        /// <param name="hash">Hash del Usuario Logueado</param>
        /// <returns>True si la operacion se realiza correctamente; False, en caso contrario</returns>
        bool InsertarOModificarSolicitudMaterialSapi(ref IList<SolicitudSapi> solicitudesMateriales, string operacion, int hash);

        /// <summary>
        /// Servicio que obtiene un grupo de Solicitudes Sapi de acuerdo a un filtro aplicado
        /// </summary>
        /// <param name="solicitudAux">Solicitud Sapi usada como filtro</param>
        /// <returns>Lista de Solicitudes Sapi que cumplen con el filtro</returns>
        IList<SolicitudSapi> ObtenerSolicitudesSapiFiltro(SolicitudSapi solicitudAux);

        /// <summary>
        /// Servicio que obtiene los Movimientos tipo SOLICITUD pendientes por ENTREGAR Y RECIBIR
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi filtro</param>
        /// <returns>Lista de Movimientos tipo SOLICITUD pendientes por ENTREGAR Y RECIBIR</returns>
        IList<SolicitudSapi> ObtenerSolicitudesSapiPendientesFiltro(SolicitudSapi solicitudSapi);

    }
}
