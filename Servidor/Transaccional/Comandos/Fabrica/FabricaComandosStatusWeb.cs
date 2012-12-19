using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosStatusWeb;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosStatusWeb
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un status
        /// </summary>
        /// <param name="status">StatusWeb a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el status en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(StatusWeb status)
        {
            return new ComandoInsertarOModificarStatusWeb(status);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los StatusWebes
        /// </summary>
        /// <returns>El Comando para consultar todos los StatusWebes</returns>
        public static ComandoBase<IList<StatusWeb>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosStatusWebes();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un status
        /// </summary>
        /// <param name="usuario">StatusWeb que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarStatusWeb(StatusWeb status)
        {
            return new ComandoEliminarStatusWeb(status);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un StatusWeb por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<StatusWeb> ObtenerComandoConsultarPorID(StatusWeb status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="status">StatusWeb a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaStatusWeb(StatusWeb status)
        {
            return new ComandoVerificarExistenciaStatusWeb(status);
        }
    }
}
