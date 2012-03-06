using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosRenovacion;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosRenovacion
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una Renovacion
        /// </summary>
        /// <param name="renovacion">Renovacion a insertar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Renovacion renovacion)
        {
            return new ComandoInsertarOModificarRenovacion(renovacion);
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
        /// Método que devuelve el Comando para consultar todas las Renovaciones
        /// </summary>
        /// <returns>El Comando para consultar todas las Renovaciones</returns>
        public static ComandoBase<IList<Renovacion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosRenovacion();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar una Renovacion por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Renovacion> ObtenerComandoConsultarPorID(Renovacion renovacion)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="renovacion">renovacion a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaRenovacion(Renovacion renovacion)
        {
            return new ComandoVerificarExistenciaRenovacion(renovacion);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar una renovacion
        /// </summary>
        /// <param name="renovacion">renovacion que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarRenovacion(Renovacion renovacion)
        {
            return new ComandoEliminarRenovacion(renovacion);
        }        

        /// <summary>
        /// Metodo que obtiene el comando ConsultarRenovacionesFiltro
        /// </summary>
        /// <param name="renovacion">Renovacion a consultar</param>
        /// <returns>Lista de renovaciones que cumplan con el filtro</returns>         
        public static ComandoBase<IList<Renovacion>> ObtenerComandoConsultarRenovacionesFiltro(Renovacion renovacion)
        {
            return new ComandoConsultarRenovacionesFiltro(renovacion);
        }
    }
}
