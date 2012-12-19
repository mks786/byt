using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoRenovacion : IDaoBase<Renovacion, int>
    {

        /// <summary>
        /// Método que consulta las Renovaciones dependiendo de un filtro
        /// </summary>
        /// <param name="renovacion">Renovación a filtrar</param>
        /// <returns>Lista de renovaciones que cumplan con el filtro</returns>
        IList<Renovacion> ObtenerRenovacionesFiltro(Renovacion renovacion);


        /// <summary>
        /// Método que obtiene la última renovacion
        /// </summary>
        /// <param name="renovacion">Renovacion perteneciente a una marca</param>
        /// <returns>Id de la ultima renovación</returns>
        int ObtenerUltimaRenovacion(Renovacion renovacion);
    }
}
