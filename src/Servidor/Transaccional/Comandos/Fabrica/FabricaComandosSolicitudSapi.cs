using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosSolicitudSapi;
using Trascend.Bolet.ObjetosComunes.Entidades;
using System;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosSolicitudSapi
    {
        /// <summary>
        /// Metodo que obtiene el comando necesario para insertar/actualizar un item de Solicitud de Materiales Sapi
        /// </summary>
        /// <param name="solicitudSapi">Detalle Solicitud Sapi a insertar/actualizar</param>
        /// <returns>Comando para realizar la accion respectiva</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(SolicitudSapi solicitudSapi)
        {
            return new ComandoInsertarOModificarSolicitudMaterialSapi(solicitudSapi);
        }

        /// <summary>
        /// Metodo que obtiene el comando necesario para consultar Solicitudes Sapi por filtro
        /// </summary>
        /// <param name="solicitudAux">Filtro utilizado</param>
        /// <returns>Comando para realizar la accion respectiva</returns>
        public static ComandoBase<IList<SolicitudSapi>> ObtenerComandoConsultarSolicitudesSapiFiltro(SolicitudSapi solicitudAux)
        {
            return new ComandoConsultarSolicitudesSapiFiltro(solicitudAux);
        }

        /// <summary>
        /// Metodo que obtiene el comando necesario para eliminar Solicitudes Sapi
        /// </summary>
        /// <param name="solicitud">Solicitud Sapi a eliminar</param>
        /// <returns>Comando para realizar la accion respectiva</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarSolicitudMaterialSapi(SolicitudSapi solicitud)
        {
            return new ComandoEliminarSolicitudMaterialSapi(solicitud);
        }

        /// <summary>
        /// Metodo que obtiene el comando necesario para
        /// </summary>
        /// <param name="solicitudSapi">Solicitud Sapi filtro</param>
        /// <returns>Comando para realizar la accion respectiva</returns>
        public static ComandoBase<IList<SolicitudSapi>> ObtenerComandoConsultarSolicitudesSapiPendientesFiltro(SolicitudSapi solicitudSapi)
        {
            return new ComandoConsultarSolicitudesSapiPendientesFiltro(solicitudSapi);
        }
    }
}
