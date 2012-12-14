using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoEmailAsociado;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosTipoEmailAsociado
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un TipoEmailAsociado
        /// </summary>
        /// <param name="TipoEmailAsociado">TipoEmailAsociado a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(TipoEmailAsociado TipoEmailAsociado)
        {
            return new ComandoInsertarOModificarTipoEmailAsociado(TipoEmailAsociado);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un TipoEmailAsociado
        /// </summary>
        /// <param name="TipoEmailAsociado">TipoEmailAsociado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarTipoEmailAsociado(TipoEmailAsociado TipoEmailAsociado)
        {
            return new ComandoEliminarTipoEmailAsociado(TipoEmailAsociado);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los TipoEmailAsociados
        /// </summary>
        /// <returns>Lista con todos los TipoEmailAsociados</returns>
        public static ComandoBase<IList<TipoEmailAsociado>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasTipoEmailAsociado();
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un TipoEmailAsociado
        /// </summary>
        /// <param name="TipoEmailAsociado">TipoEmailAsociado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaTipoEmailAsociado(TipoEmailAsociado TipoEmailAsociado)
        {
            return new ComandoVerificarExistenciaTipoEmailAsociado(TipoEmailAsociado);
        }
    }
}