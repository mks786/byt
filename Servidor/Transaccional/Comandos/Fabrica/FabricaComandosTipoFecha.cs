using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoFecha;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosTipoFecha
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un tipoFecha
        /// </summary>
        /// <param name="tipoFecha">TipoFecha a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(TipoFecha tipoFecha)
        {
            return new ComandoInsertarOModificarTipoFecha(tipoFecha);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un tipoFecha
        /// </summary>
        /// <param name="tipoFecha">Estado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(TipoFecha tipoFecha)
        {
            return new ComandoEliminarTipoFecha(tipoFecha);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los tipoFecha
        /// </summary>
        /// <returns>Lista con todos los tipoFechas</returns>
        public static ComandoBase<IList<TipoFecha>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosTipoFechas();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="tipoFecha">TipoFecha a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaTipoFecha(TipoFecha tipoFecha)
        {
            return new ComandoVerificarExistenciaTipoFecha(tipoFecha);
        }
    }
}