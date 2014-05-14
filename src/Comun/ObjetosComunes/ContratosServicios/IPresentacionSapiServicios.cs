using System;
using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IPresentacionSapiServicios : IServicioBase<PresentacionSapi>
    {
        /// <summary>
        /// Servicio que inserta o actualiza el encabezado de una Solicitud de Presentacion SAPI
        /// </summary>
        /// <param name="presentacionSapi">Encabezado de la Presentacion SAPI a insertar</param>
        /// <param name="hash">Hash del usuario logueado</param>
        /// <returns>True si la operacion se realiza satisfactoriamente; False, en caso contrario</returns>
        bool InsertarOModificarPresentacionSapi(ref PresentacionSapi presentacionSapi, int hash);
    }
}
