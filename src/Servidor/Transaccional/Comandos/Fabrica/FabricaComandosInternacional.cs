using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosInternacional;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInternacional
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un internacional
        /// </summary>
        /// <param name="internacional">Internacional a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Internacional internacional)
        {
            return new ComandoInsertarOModificarInternacional(internacional);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un internacional
        /// </summary>
        /// <param name="internacional">Estado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(Internacional internacional)
        {
            return new ComandoEliminarInternacional(internacional);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los internacionales
        /// </summary>
        /// <returns>Lista con todos los internacionales</returns>
        public static ComandoBase<IList<Internacional>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInternacionales();
        }
    }
}