using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosContadorFac;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosContadorFac
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una Contador
        /// </summary>
        /// <param name="contador">Contador a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(ContadorFac contador)
        {
            return new ComandoInsertarOModificarContadorFac(contador);
        }

        /// <summary>
        /// Método que devuelve el Comando dado el dominio
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<ContadorFac> ObtenerComandoConsultarPorId(string id)
        {
            return new ComandoConsultarPorIdContadorFac(id);
        }
    }
}
