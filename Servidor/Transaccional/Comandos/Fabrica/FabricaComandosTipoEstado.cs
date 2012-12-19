using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoEstado;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosTipoEstado
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un TipoEstado
        /// </summary>
        /// <param name="pais">TipoEstado a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el TipoEstado en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(TipoEstado tipoEstado)
        {
            throw new NotImplementedException();
            //return new ComandoInsertarOModificarPais(pais);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los TipoEstados
        /// </summary>
        /// <returns>El Comando para consultar todos los TipoEstados</returns>
        public static ComandoBase<IList<TipoEstado>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosTipoEstado();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un TipoEstado
        /// </summary>
        /// <param name="usuario">TipoEstado que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarTipoEstado(TipoEstado tipoEstado)
        {
            throw new NotImplementedException();
            //return new ComandoEliminarPais(pais);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un TipoEstado por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<TipoEstado> ObtenerComandoConsultarPorID(TipoEstado tipoEstado)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">TipoEstado a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaTipoEstado(TipoEstado tipoEstado)
        {
            throw new NotImplementedException();
            //return new ComandoVerificarExistenciaPais(pais);
        }
    }
}
