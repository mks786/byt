using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCartaOut;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosCartaOut
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un carta
        /// </summary>
        /// <param name="carta">Carta a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Carta carta)
        {
            throw new NotImplementedException();
            //return new ComandoInsertarOModificarCarta(carta);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un carta
        /// </summary>
        /// <param name="carta">Carta a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCarta(Carta carta)
        {
            throw new NotImplementedException();
            //return new ComandoEliminarCarta(carta);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los cartas
        /// </summary>
        /// <returns>Lista con todos los cartas</returns>
        public static ComandoBase<IList<CartaOut>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCartaOuts();
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un carta
        /// </summary>
        /// <param name="carta">Carta a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCarta(Carta carta)
        {
            throw new NotImplementedException();
        }
    }
}