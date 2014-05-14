using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosPresentacionSapi;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosPresentacionSapi
    {
        /// <summary>
        /// Metodo que obtiene el comando necesario para insertar o actualizar el Encabezado de una Solicitud de Presentacion SAPI
        /// </summary>
        /// <param name="presentacionSapiEncabezado">Encabezado de la Presentacion Sapi a actualizar</param>
        /// <returns>Comando para realizar la operacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(PresentacionSapi presentacionSapiEncabezado)
        {
            return new ComandoInsertarOModificarPresentacionSapi(presentacionSapiEncabezado);
        }
    }
}
