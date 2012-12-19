using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosCesionPatente;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCesionPatente
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una CesionPatente
        /// </summary>
        /// <param name="cesion">CesionPatente a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CesionPatente cesion)
        {
            return new ComandoInsertarOModificarCesionPatente(cesion);
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
        /// Método que devuelve el Comando para consultar todas la CesionPatentees
        /// </summary>
        /// <returns>El Comando para consultar todas las CesionPatentees</returns>
        public static ComandoBase<IList<CesionPatente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCesionPatente();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un CesionPatente por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<CesionPatente> ObtenerComandoConsultarPorID(CesionPatente cesion)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar una cesion
        /// </summary>
        /// <param name="cesion">cesion que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCesionPatente(CesionPatente cesion)
        {
            return new ComandoEliminarCesionPatente(cesion);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="cesion">CesionPatente a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCesionPatente(CesionPatente cesion)
        {
            return new ComandoVerificarExistenciaCesionPatente(cesion);
        }

        /// <summary>
        /// Metodo que obtiene el comando ConsultarCesionPatenteesFiltro
        /// </summary>
        /// <param name="cesion">CesionPatente a consultar</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>         
        public static ComandoBase<IList<CesionPatente>> ObtenerComandoConsultarCesionPatenteesFiltro(CesionPatente cesion)
        {
            return new ComandoConsultarCesionesPatenteFiltro(cesion);
        }
    }
}
