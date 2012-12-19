using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosObjeto;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosObjeto
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un Objeto
        /// </summary>
        /// <param name="objeto">Objeto a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Objeto objeto)
        {
            return new ComandoInsertarOModificarObjeto(objeto);
        }

        /// <summary>
        /// Método que devuelve el Comando para para eliminar un Objeto
        /// </summary>
        /// <param name="objeto">Objeto a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(Objeto objeto)
        {
            return new ComandoEliminarObjeto(objeto);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los objetos
        /// </summary>
        /// <returns>Lista con todos los objetos</returns>
        public static ComandoBase<IList<Objeto>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosObjetos();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="objeto">Objeto a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaObjeto(Objeto objeto)
        {
            return new ComandoVerificarExistenciaObjeto(objeto);
        }
    }
}