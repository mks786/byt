using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAsignacion : IDaoBase<Asignacion, int>
    {

        /// <summary>
        /// Obtiene todas las asignaciones que tiene una carta
        /// </summary>
        /// <param name="carta">La Carta</param>
        /// <returns>Lista de Asignaciones</returns>
        IList<Asignacion> ObtenerAsignacionesPorCarta(Carta carta);

        /// <summary>
        /// Obtiene todas las asignaciones que tiene un usuario
        /// </summary>
        /// <param name="user">El Usuario</param>
        /// <returns>Lista de Asignaciones</returns>
        IList<Asignacion> ObtenerAsignacionesPorUsuario(Usuario user);
    }
}
