using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosNacional;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosNacional
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un nacional
        /// </summary>
        /// <param name="nacional">Nacional a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Nacional nacional)
        {
            return new ComandoInsertarOModificarNacional(nacional);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un nacional
        /// </summary>
        /// <param name="nacional">Nacional a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarNacional(Nacional nacional)
        {
            return new ComandoEliminarNacional(nacional);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los nacionales
        /// </summary>
        /// <returns>Lista con todos los nacionales</returns>
        public static ComandoBase<IList<Nacional>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosNacionales();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="nacional">Nacional a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaNacional(Nacional nacional)
        {
            return new ComandoVerificarExistenciaNacional(nacional);
        }
    }
}