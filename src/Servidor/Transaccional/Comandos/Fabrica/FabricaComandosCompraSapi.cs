using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCompraSapi;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCompraSapi
    {
        /// <summary>
        /// Metodo para obtener el comando para insertar o actualizar una Compra Sapi
        /// </summary>
        /// <param name="compra">Compra Sapi para insertar o actualizar</param>
        /// <returns>Comando para realizar la operacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CompraSapi compra)
        {
            return new ComandoInsertarOModificarCompra(compra);
        }
    }
}
