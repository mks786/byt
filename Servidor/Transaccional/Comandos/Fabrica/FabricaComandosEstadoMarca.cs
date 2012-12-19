using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosEstadoMarca;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosEstadoMarca
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un estado de marca
        /// </summary>
        /// <param name="Estadomarca">estado de marca a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el EstadoMarca en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(EstadoMarca estadoMarca)
        {
            return new ComandoInsertarOModificarEstadoMarca(estadoMarca);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Estado Marca
        /// </summary>
        /// <returns>El Comando para consultar todos los EstadoMarca</returns>
        public static ComandoBase<IList<EstadoMarca>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosEstadoMarca();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un EstadoMarca
        /// </summary>
        /// <param name="usuario">Estado de marca que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarEstadoMarca(EstadoMarca estadoMarca)
        {
            return new ComandoEliminarEstadoMarca(estadoMarca);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un estado de marca por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<EstadoMarca> ObtenerComandoConsultarPorID(EstadoMarca estadoMarca)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">estado de marca a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaEstadoMarca(EstadoMarca estadoMarca)
        {
            return new ComandoVerificarExistenciaEstadoMarca(estadoMarca);
        }
    }
}
