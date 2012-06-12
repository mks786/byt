using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoPoder : IDaoBase<Poder, int>
    {

        /// <summary>
        /// Método que obtiene los poderes por un interesado
        /// </summary>
        /// <param name="interesado">Interesado a consultar los poderes</param>
        /// <returns>Lista de poderes pertenecientes al interesado</returns>
        IList<Poder> ObtenerPoderesPorInteresado(Interesado interesado);


        /// <summary>
        /// Método que obtiene los Poderes por un Agente
        /// </summary>
        /// <param name="agente">Agente a consultar sus poderes</param>
        /// <returns>Poderes del Agente</returns>
        IList<Poder> ObtenerPoderesPorAgente(Agente agente);


        /// <summary>
        /// Método que obtiene un poder con uno o mas filtros
        /// </summary>
        /// <param name="poder">filtros de poder</param>
        /// <returns>poder filtrado</returns>
        IList<Poder> ObtenerPoderesFiltro(Poder poder);


        IList<Poder> ObtenerPoderesEntreAgenteEInteresado(Agente agente, Interesado interesado);
    }
}
