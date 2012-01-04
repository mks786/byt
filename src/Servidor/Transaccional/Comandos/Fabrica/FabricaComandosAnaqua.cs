using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosAnaqua;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosAnaqua
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un anaqua
        /// </summary>
        /// <param name="anaqua">anaqua a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el anaqua en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Anaqua anaqua)
        {
            return new ComandoInsertarOModificarAnaqua(anaqua);
        }
            
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los anaqua
        /// </summary>
        /// <returns>El Comando para consultar todos los anaqua</returns>
        public static ComandoBase<IList<Anaqua>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosAnaqua();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un anaqua
        /// </summary>
        /// <param name="usuario">anaqua que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarAnaqua(Anaqua anaqua)
        {
            return new ComandoEliminarAnaqua(anaqua);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un anaqua por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Anaqua> ObtenerComandoConsultarPorID(Anaqua anaqua)
        {
            return new ComandoConsultarAnaquaPorID(anaqua);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">Anexo a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaAnaqua(Anaqua anaqua)
        {
            return new ComandoVerificarExistenciaAnaqua(anaqua);
        }
    }
}
