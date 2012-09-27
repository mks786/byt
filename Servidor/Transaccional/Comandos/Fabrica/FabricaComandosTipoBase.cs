using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoBase;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosTipoBase
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un tipoBase
        /// </summary>
        /// <param name="tipoBase">TipoBase a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(TipoBase tipoBase)
        {
            return new ComandoInsertarOModificarTipoBase(tipoBase);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un tipoBase
        /// </summary>
        /// <param name="tipoBase">TipoBase a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarTipoBase(TipoBase tipoBase)
        {
            return new ComandoEliminarTipoBase(tipoBase);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los tipoBases
        /// </summary>
        /// <returns>Lista con todos los tipoBases</returns>
        public static ComandoBase<IList<TipoBase>> ObtenerComandoConsultarTodos()
        {
            return new  ComandoConsultarTodosTiposBase();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="agente">Agente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaTipoBase(TipoBase tipoBase)
        {
            return new ComandoVerificarExistenciaTipoBase(tipoBase);
        }
    }
}