using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IRenovacionServicios : IServicioBase<Renovacion>
    {
        /// <summary>
        /// Servicio que obtiene una lista de renovaciones que cumplan ciertos parametros
        /// </summary>
        /// <param name="renovacion">Renovacion a filtrar</param>
        /// <returns>Lista de renovaciones que cumplan con los parametros especificados</returns>
        IList<Renovacion> ObtenerRenovacionFiltro(Renovacion renovacion);


        /// <summary>
        /// Servicio que consulta la ultima renovacion de una marca
        /// </summary>
        /// <param name="renovacion">Renovacion a consultar</param>
        /// <returns>Id de la ultima renovacion de la marca</returns>
        int ConsultarUltimaRenovacion(Renovacion renovacion);

        /// <summary>
        /// Método que se encarga de insertar una nueva la renovacion
        /// </summary>
        /// <param name="renovacion">Renovacion a insertar</param>
        /// <param name="hash">hash del usuario que realiza la accion</param>
        /// <returns>codigo de la renovacion insertada</returns>
        int? InsertarOModificarRenovacion(Renovacion renovacion, int hash);
    }
}
