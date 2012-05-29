using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAnualidad : IDaoBase<Anualidad, int>
    {
        /// <summary>
        /// Metodo que que consulta los datos de una anualidad dado el id
        /// </summary>
        /// <param name="idAnualidad">Id de la anualidad</param>
        /// <returns>Anualidad</returns>
        IList<Anualidad> ObtenerAnualidadesFiltro(int idAnualidad);


        /// <summary>
        /// Metodo que que consulta los datos de una anualidad dado el id
        /// </summary>
        /// <param name="idAnualidad">Id de la anualidad</param>
        /// <returns>Anualidad</returns>
        IList<Anualidad> ObtenerAnualidadesPorPatente(int idAnualidad);


        /// <summary>
        /// Metodo que obtiene el id de la ultima Anualidad inserata en la BD
        /// </summary>
        /// <returns>un numero Entero</returns>
        int ObtenerMaxIdAnualidad();


        /// <summary>
        /// Metodo que consulta una anualidad
        /// </summary>
        /// <param name="Anualidad">Anualidad</param>
        /// <returns>Anualidad</returns>
        Anualidad ObtenerAnualidadConTodo(Anualidad Anualidad);

    }
}
