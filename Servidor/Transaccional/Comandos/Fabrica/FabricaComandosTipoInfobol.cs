using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoInfobol;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosTipoInfobol
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un tipoInfobol
        /// </summary>
        /// <param name="tipoInfobol">TipoInfobol a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(TipoInfobol tipoInfobol)
        {
            return new ComandoInsertarOModificarTipoInfobol(tipoInfobol);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un tipoInfobol
        /// </summary>
        /// <param name="tipoInfobol">Estado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(TipoInfobol tipoInfobol)
        {
            return new ComandoEliminarTipoInfobol(tipoInfobol);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los tipoInfobol
        /// </summary>
        /// <returns>Lista con todos los tipoInfoboles</returns>
        public static ComandoBase<IList<TipoInfobol>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosTipoInfoboles();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="tipoInfobol">TipoInfobol a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaTipoInfobol(TipoInfobol tipoInfobol)
        {
            return new ComandoVerificarExistenciaTipoInfoBol(tipoInfobol);
        }
    }
}