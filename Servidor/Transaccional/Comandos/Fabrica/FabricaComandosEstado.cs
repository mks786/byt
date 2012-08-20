using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosEstado;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosEstado
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un estado
        /// </summary>
        /// <param name="estado">Estado a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Estado estado)
        {
            return new ComandoInsertarOModificarEstado(estado);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un estado
        /// </summary>
        /// <param name="estado">Estado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarEstado(Estado estado)
        {
            return new ComandoEliminarEstado(estado);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los estados
        /// </summary>
        /// <returns>Lista con todos los estados</returns>
        public static ComandoBase<IList<Estado>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosEstados();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="agente">Agente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaEstado(Estado estado)
        {
            return new ComandoVerificarExistenciaEstado(estado);
        }
    }
}