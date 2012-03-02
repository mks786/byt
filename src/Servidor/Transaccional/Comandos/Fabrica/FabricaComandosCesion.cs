using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCesion;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCesion
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una Cesion
        /// </summary>
        /// <param name="cesion">Cesion a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Cesion cesion)
        {
            return new ComandoInsertarOModificarCesion(cesion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarObjeto(Objeto objeto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todas la Cesiones
        /// </summary>
        /// <returns>El Comando para consultar todas las Cesiones</returns>
        public static ComandoBase<IList<Cesion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCesion();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Cesion por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Cesion> ObtenerComandoConsultarPorID(Cesion cesion)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar una cesion
        /// </summary>
        /// <param name="cesion">cesion que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCesion(Cesion cesion)
        {
            return new ComandoEliminarCesion(cesion);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="cesion">Cesion a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCesion(Cesion cesion)
        {
            return new ComandoVerificarExistenciaCesion(cesion);
        }

        /// <summary>
        /// Metodo que obtiene el comando ConsultarCesionesFiltro
        /// </summary>
        /// <param name="cesion">Cesion a consultar</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>         
        public static ComandoBase<IList<Cesion>> ObtenerComandoConsultarCesionesFiltro(Cesion cesion)
        {
            return new ComandoConsultarCesionesFiltro(cesion);
        }
    }
}
