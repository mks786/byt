using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosContador;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosContador
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una Contador
        /// </summary>
        /// <param name="contador">Contador a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Contador contador)
        {
            return new ComandoInsertarOModificarContador(contador);
        }

        /// <summary>
        /// Método que devuelve el Comando dado el dominio
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Contador> ObtenerComandoConsultarPorId(string id)
        {
            return new ComandoConsultarPorIdContador(id);
        }
    }
}
