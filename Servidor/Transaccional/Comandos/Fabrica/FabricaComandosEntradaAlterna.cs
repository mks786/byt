using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosEstado;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosEntradaAlterna;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosEntradaAlterna
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un entradaAlterna
        /// </summary>
        /// <param name="entradaAlterna">EntradaAlterna a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(EntradaAlterna entradaAlterna)
        {
            return new ComandoInsertarOModificarEntradaAlterna(entradaAlterna);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un entradaAlterna
        /// </summary>
        /// <param name="entradaAlterna">EntradaAlterna a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarEntradaAlterna(EntradaAlterna entradaAlterna)
        {
            return new ComandoEliminarEntradaAlterna(entradaAlterna);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los entradaAlternas
        /// </summary>
        /// <returns>Lista con todos los entradaAlternas</returns>
        public static ComandoBase<IList<EntradaAlterna>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosEntradaAlternas();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="entradaAlterna">EntradaAlterna a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaEntradaAlterna(EntradaAlterna entradaAlterna)
        {
            return new ComandoVerificarExistenciaEntradaAlterna(entradaAlterna);
        }
    }
}