using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosOperacion;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosOperacion
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Operacions
        /// </summary>
        /// <returns>El Comando para consultar todos los Operacions</returns>
        public static ComandoBase<IList<Operacion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosOperacions();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Operacion por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Operacion> ObtenerComandoConsultarPorID(Operacion operacion)
        {
            return new ComandoConsultarOperacionPorID(operacion);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar un Operacion
        /// </summary>
        /// <param name="operacion">Operacion a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Operacion operacion)
        {
            return new ComandoInsertarOModificarOperacion(operacion);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un Operacion
        /// </summary>
        /// <param name="operacion">Operacion que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarOperacion(Operacion operacion)
        {
            return new ComandoEliminarOperacion(operacion);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="operacion">Operacion a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaOperacion(Operacion operacion)
        {
            return new ComandoVerificarExistenciaOperacion(operacion);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar la operacion por id
        /// </summary>
        /// <param name="operacion">Operacion a consultar</param>
        /// <returns>Operacion que devuelve la consulta</returns>
        public static ComandoBase<Operacion> ObtenerComandoConsultarOperacionPorId(Operacion operacion)
        {
            return new ComandoConsultarOperacionPorID(operacion);
        }

        /// <summary>
        /// Metodo que devuelve el comando para consultar todos los Operaciones de una marca
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public static ComandoBase<IList<Operacion>> ObtenerComandoConsultarOperacionesPorMarca(Marca marca)
        {
            return new ComandoConsultarOperacionesPorMarca(marca);
        }

        /// <summary>
        /// Metodo que devuelve el comando para consultar todos los Operaciones de una Patente
        /// </summary>
        /// <param name="patente"></param>
        /// <returns></returns>
        public static ComandoBase<IList<Operacion>> ObtenerComandoConsultarOperacionesPorPatente(Patente patente)
        {
            return new ComandoConsultarOperacionesPorPatente(patente);
        }

        /// <summary>
        /// Metodo que devuelve el comando para consultar todos los Operaciones de una marca y un servicio
        /// </summary>
        /// <param name="operacion"></param>
        /// <returns></returns>
        public static ComandoBase<IList<Operacion>> ObtenerComandoConsultarOperacionesPorMarcaYServicio(Operacion operacion)
        {
            return new ComandoConsultarOperacionesPorMarcaYServicio(operacion);
        }

        /// <summary>
        /// Metodo que obtiene el comando ConsultarOperacionesFiltro
        /// </summary>
        /// <param name="operacion">Operacion a consultar</param>
        /// <returns>Lista de operaciones que cumplan con el filtro</returns>         
        public static ComandoBase<IList<Operacion>> ObtenerComandoConsultarOperacionesFiltro(Operacion operacion)
        {
            return new ComandoConsultarOperacionesFiltro(operacion);
        }
    }
    
}
