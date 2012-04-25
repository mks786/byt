using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosAnualidad;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosAnualidad
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un Anualidad
        /// </summary>
        /// <param name="anualidad">Anualidad a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Anualidad anualidad)
        {
            return new ComandoInsertarOModificarAnualidad(anualidad);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un Anualidad
        /// </summary>
        /// <param name="anualidad">Estado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(Anualidad anualidad)
        {
            return new ComandoEliminarAnualidad(anualidad);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Anualidads
        /// </summary>
        /// <returns>Lista con todos los Anualidads</returns>
        public static ComandoBase<IList<Anualidad>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasAnualidades();
        }

        public static ComandoBase<IList<Anualidad>> ObtenerComandoConsultarAnualidadesFiltro(Anualidad anualidad)
        {
            return new ComandoConsultarAnualidadesFiltro(anualidad);
        }

        #region Sin utilizar
        ///// <summary>
        ///// Método que devuelve el Comando verificar existencia
        ///// </summary>
        ///// <param name="anualidad">Anualidad a verificar</param>
        ///// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        //public static ComandoBase<bool> ObtenerComandoVerificarExistenciaAnualidad(Anualidad anualidad)
        //{
        //    return new ComandoVerificarExistenciaAnualidad(anualidad);
        //}

        //public static ComandoBase<Anualidad> ObtenerComandoConsultarAnualidadConTodo(Anualidad anualidad)
        //{
        //    return new ComandoConsultarAnualidadConTodo(anualidad);
        //}

        ///// <summary>
        ///// Método que devuelve el ComandoConsultarPorId
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>ComandoConsultarPorId</returns>
        //public static ComandoBase<Anualidad> ObtenerComandoConsultarPorId(int id)
        //{
        //    return new ComandoConsultarAnualidadPorId(id);
        //}

        #endregion

    }
}