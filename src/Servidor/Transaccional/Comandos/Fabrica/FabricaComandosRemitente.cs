using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosRemitente;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosRemitente
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Remitente
        /// </summary>
        /// <param name="Remitente">Remitente a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar un Remitente en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Remitente remitente)
        {
            return new ComandoInsertarOModificarRemitente(remitente);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los remitentes
        /// </summary>
        /// <returns>El Comando para consultar todos los remitentes</returns>
        public static ComandoBase<IList<Remitente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosRemitentes();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un poder
        /// </summary>
        /// <param name="remitente">Remitente que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarRemitente(Remitente remitente)
        {
            return new ComandoEliminarRemitente(remitente);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un remitente por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Remitente> ObtenerComandoConsultarPorID(Remitente remitente)
        {
            throw new NotImplementedException();
        }
    }
}
