using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosServicio;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosServicio
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Servicio
        /// </summary>
        /// <param name="pais">Servicio a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el Servicio en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Servicio servicio)
        {
            throw new NotImplementedException();
            //return new ComandoInsertarOModificarPais(pais);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Servicios
        /// </summary>
        /// <returns>El Comando para consultar todos los Servicios</returns>
        public static ComandoBase<IList<Servicio>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosServicios();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un Servicio
        /// </summary>
        /// <param name="usuario">Servicio que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarServicio(Servicio servicio)
        {
            throw new NotImplementedException();
            //return new ComandoEliminarPais(pais);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Servicio por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Servicio> ObtenerComandoConsultarPorID(Servicio servicio)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="pais">Servicio a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaServicio(Servicio servicio)
        {
            throw new NotImplementedException();
            //return new ComandoVerificarExistenciaPais(pais);
        }
    }
}
