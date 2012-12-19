using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCategoria;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCategoria
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un anexo
        /// </summary>
        /// <param name="categoria">Categoria a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el anexo en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Categoria categoria)
        {
            return new ComandoInsertarOModificarCategoria(categoria);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos las Categoria
        /// </summary>
        /// <returns>El Comando para consultar todos las Categoria</returns>
        public static ComandoBase<IList<Categoria>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCategorias();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar una Categoria
        /// </summary>
        /// <param name="usuario">Categoria que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCategoria(Categoria categoria)
        {
            return new ComandoEliminarCategoria(categoria);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar una Categoria por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Categoria> ObtenerComandoConsultarPorID(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">Categoria a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCategoria(Categoria categoria)
        {
            return new ComandoVerificarExistenciaCategoria(categoria);
        }
    }
}
