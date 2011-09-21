using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosEstado;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosResolucion;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosResolucion
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un resolucion
        /// </summary>
        /// <param name="resolucion">Resolucion a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Resolucion resolucion)
        {
            return new ComandoInsertarOModificarResolucion(resolucion);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un resolucion
        /// </summary>
        /// <param name="resolucion">Resolucion a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarResolucion(Resolucion resolucion)
        {
            return new ComandoEliminarResolucion(resolucion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los resoluciones
        /// </summary>
        /// <returns>Lista con todos los resoluciones</returns>
        public static ComandoBase<IList<Resolucion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosResoluciones();
        }
    }
}