using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System;


namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoSolicitudSapi : IDaoBase<SolicitudSapi,int>
    {
        /// <summary>
        /// Metodo que obtiene Solicitudes Sapi por filtro
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi usada como filtro</param>
        /// <returns>Lista de Solicitudes Sapi que cumplen con el filtro enviado desde el cliente</returns>
        IList<SolicitudSapi> ObtenerSolicitudesSapiFiltro(SolicitudSapi solicitudSapi);

        /// <summary>
        /// Metodo que obtiene solicitudes con estatus SOLICITADO para ENTREGA Y RECEPCION
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi filtro</param>
        /// <returns>Lista de solicitudes con estatus SOLICITADO para ENTREGA Y RECEPCION</returns>
        IList<SolicitudSapi> ObtenerSolicitudesSapiPendientesFiltro(SolicitudSapi solicitudSapi);
    }
}
