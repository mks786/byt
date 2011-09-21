using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosInteresado;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInteresado
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Interesado
        /// </summary>
        /// <param name="Interesado">Interesado a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar un Interesado en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Interesado interesado)
        {
            return new ComandoInsertarOModificarInteresado(interesado);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los interesados
        /// </summary>
        /// <returns>El Comando para consultar todos los interesados</returns>
        public static ComandoBase<IList<Interesado>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInteresados();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un poder
        /// </summary>
        /// <param name="interesado">interesado que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarInteresado(Interesado interesado)
        {
            return new ComandoEliminarInteresado(interesado);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un interesado por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Interesado> ObtenerComandoConsultarPorID(Interesado interesado)
        {
            throw new NotImplementedException();
        }
    }
}
