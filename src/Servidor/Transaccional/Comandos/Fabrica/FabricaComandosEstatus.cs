using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosEstatuses;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosEstatus
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un país
        /// </summary>
        /// <param name="estatus">estatus a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el país en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Estatus estatus)
        {
            return new ComandoInsertarOModificarEstatus(estatus);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Estatuss
        /// </summary>
        /// <returns>El Comando para consultar todos los Estatuss</returns>
        public static ComandoBase<IList<Estatus>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosEstatus();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un estatus
        /// </summary>
        /// <param name="usuario">estatus que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarEstatus(Estatus estatus)
        {
            return new ComandoEliminarEstatus(estatus);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Estatus por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Estatus> ObtenerComandoConsultarPorID(Estatus estatus)
        {
            throw new NotImplementedException();
        }
    }
}
