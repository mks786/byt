using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosPresentacionSapiDetalle;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosPresentacionSapiDetalle
    {
        /// <summary>
        /// Metodo que obtiene el comando para insertar o actualizar un renglon de detalle de la Solicitud de Presentacion Sapi
        /// </summary>
        /// <param name="presentacionSapiDetalle">Detalle de la Solicitud de Presentacion Sapi a insertar o actualizar</param>
        /// <returns>Comando para la ejecucion de la accion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(PresentacionSapiDetalle presentacionSapiDetalle)
        {
            return new ComandoInsertarOModificarDetallePresentacion(presentacionSapiDetalle);
        }


        /// <summary>
        /// Metodo que obtiene el comando para eliminar un renglon de detalle de la Solicitud de Presentacion Sapi
        /// </summary>
        /// <param name="presentacionSapiDetalle">Detalle de la Solicitud de Presentacion Sapi a eliminar</param>
        /// <returns>Comando para la ejecucion de la accion</returns>
        public static ComandoBase<bool> ObtenerComandoEliminar(PresentacionSapiDetalle presentacionSapiDetalle)
        {
            return new ComandoEliminarDetallePresentacion(presentacionSapiDetalle);
        }

        /// <summary>
        /// Metodo que obtiene el comando para obtener Solicitudes de Presentacion Sapi por filtro
        /// </summary>
        /// <param name="filtro">Filtro utilizado para la consulta</param>
        /// <returns>Comando para la ejecucion de la accion</returns>
        public static ComandoBase<IList<PresentacionSapiDetalle>> ObtenerComandoConsultarPresentacionesSapiFiltro(PresentacionSapiDetalle filtro)
        {
            return new ComandoConsultarPresentacionesSapiFiltro(filtro);
        }
    }
}
