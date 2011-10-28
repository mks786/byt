using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosAnexo;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosAnexo
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un anexo
        /// </summary>
        /// <param name="anexo">Anexo a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el anexo en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Anexo anexo)
        {
            return new ComandoInsertarOModificarAnexo(anexo);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Anexos
        /// </summary>
        /// <returns>El Comando para consultar todos los Anexos</returns>
        public static ComandoBase<IList<Anexo>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosAnexos();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un Anexo
        /// </summary>
        /// <param name="usuario">Anexo que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarAnexo(Anexo anexo)
        {
            return new ComandoEliminarAnexo(anexo);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Anexo por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Anexo> ObtenerComandoConsultarPorID(Anexo anexo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">Anexo a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaAnexo(Anexo anexo)
        {
            return new ComandoVerificarExistenciaAnexo(anexo);
        }
    }
}
