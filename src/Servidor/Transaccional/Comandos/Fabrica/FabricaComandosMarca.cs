using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosMarca;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosMarca
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un Marca
        /// </summary>
        /// <param name="marca">Marca a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Marca marca)
        {
            return new ComandoInsertarOModificarMarca(marca);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un Marca
        /// </summary>
        /// <param name="marca">Estado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(Marca marca)
        {
            return new ComandoEliminarMarca(marca);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Marcas
        /// </summary>
        /// <returns>Lista con todos los Marcas</returns>
        public static ComandoBase<IList<Marca>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasMarcas();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="marca">Marca a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaMarca(Marca marca)
        {
            return new ComandoVerificarExistenciaMarca(marca);
        }

        public static ComandoBase<IList<Marca>> ObtenerComandoConsultarMarcasFiltro(Marca marca)
        {
            return new ComandoConsultarMarcasFiltro(marca);
        }

        public static ComandoBase<Marca> ObtenerComandoConsultarMarcaConTodo(Marca marca)
        {
            return new ComandoConsultarMarcaConTodo(marca);
        }

        /// <summary>
        /// Método que devuelve el ComandoConsultarPorId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ComandoConsultarPorId</returns>
        public static ComandoBase<Marca> ObtenerComandoConsultarPorId(int id)
        {
            return new ComandoConsultarMarcaPorId(id);
        }
    }
}