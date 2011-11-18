using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosContadorAsignacion;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosContadorAsignacion
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una Contador
        /// </summary>
        /// <param name="contadorAsignacion">Contador a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(ContadorAsignacion contadorAsignacion)
        {
            return new ComandoInsertarOModificarContadorAsignacion(contadorAsignacion);
        }

        /// <summary>
        /// Método que devuelve el Comando dado el dominio
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<ContadorAsignacion> ObtenerComandoConsultarPorId(string id)
        {
            return new ComandoConsultarPorIdContadorAsignacion(id);
        }
    }
}
