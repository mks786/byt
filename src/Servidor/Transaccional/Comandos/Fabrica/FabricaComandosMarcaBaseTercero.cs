using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosMarcaBaseTercero;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosMarcaBaseTercero
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un marcaBaseTercero
        /// </summary>
        /// <param name="marcaBaseTercero">MarcaBaseTercero a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el marcaBaseTercero en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(MarcaBaseTercero marcaBaseTercero)
        {
            return new ComandoInsertarOModificarMarcaBaseTercero(marcaBaseTercero);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los MarcaBaseTerceros
        /// </summary>
        /// <returns>El Comando para consultar todos los MarcaBaseTerceros</returns>
        public static ComandoBase<IList<MarcaBaseTercero>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosMarcaBaseTercero();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un MarcaBaseTercero
        /// </summary>
        /// <param name="usuario">MarcaBaseTercero que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarMarcaBaseTercero(MarcaBaseTercero marcaBaseTercero)
        {
            return new ComandoEliminarMarcaBaseTercero(marcaBaseTercero);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un MarcaBaseTercero por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<MarcaBaseTercero> ObtenerComandoConsultarPorID(MarcaBaseTercero marcaBaseTercero)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">MarcaBaseTercero a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaMarcaBaseTercero(MarcaBaseTercero marcaBaseTercero)
        {
            return new ComandoVerificarExistenciaMarcaBaseTercero(marcaBaseTercero);
        }

        public static ComandoBase<IList<MarcaBaseTercero>> ObtenerComandoConsultarMarcasBaseTerceroFiltro(MarcaBaseTercero marcaBaseTercero)
        {
            return new ComandoConsultarMarcasBaseTerceroFiltro(marcaBaseTercero);
        }
    }
}
