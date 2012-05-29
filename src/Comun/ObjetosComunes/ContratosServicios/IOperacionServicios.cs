using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IOperacionServicios : IServicioBase<Operacion>
    {
        /// <summary>
        /// Servicio que consulta las operaciones por marca
        /// </summary>
        /// <param name="marca">Marca a consultarle las operaciones</param>
        /// <returns>Lista de operaciones de la marca</returns>
        IList<Operacion> ConsultarOperacionesPorMarca(Marca marca);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patente"></param>
        /// <returns></returns>
        IList<Operacion> ConsultarOperacionesPorPatente(Patente patente);

        /// <summary>
        /// Servicio que obtiene las operaciones por marca y servicio
        /// </summary>
        /// <param name="operacion">operacion a consultar</param>
        /// <returns>Lista de Operaciones</returns>
        IList<Operacion> ObtenerOperacionPorMarcaYServicio(Operacion operacion);

        /// <summary>
        /// Servicio que consulta las operaciones basadas en una operacion filtro
        /// </summary>
        /// <param name="operacionAuxiliar">operacion filtro</param>
        /// <returns>Lista de operaciones que cumplan con el filtro</returns>
        IList<Operacion> ObtenerOperacionFiltro(Operacion operacionAuxiliar);
    }
}
