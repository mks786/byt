using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAgente : IDaoBase<Agente, string>
    {
        /// <summary> Metodo que consulta los agentes con sus poderes </summary>
        /// <returns>Lista de agente</returns>
        IList<Agente> ObtenerAgentesYPoderes();


        /// <summary>
        /// Metodo que Consulta los agentes dado unos parametros en especificos
        /// </summary>
        /// <param name="agente">Agente con Filtros</param>
        /// <returns>Lista de Agentes</returns>
        IList<Agente> ObtenerAgentesFiltro(Agente agente);


        /// <summary>
        /// Metodo que consulta los agentes de un poder
        /// </summary>
        /// <param name="poder">Poder solicitado</param>
        /// <returns>Lista de Agentes</returns>
        IList<Agente> ObtenerAgentesDeUnPoder(Poder poder);


        /// <summary>
        /// metodo que consulta todos los agentes que no tienen poder asignado
        /// </summary>
        /// <param name="agente">Agente con Filtro</param>
        /// <returns>Lista de Agentes</returns>
        IList<Agente> ObtenerAgentesSinPoderesFiltro(Agente agente);


        /// <summary>
        /// Metodo que regresa los Agentes que estan Vacios
        /// </summary>
        /// <param name="agente">Agente solicitado</param>
        /// <returns>Lista de agentes</returns>
        IList<Agente> ObtenerAgentesVacios(Agente agente);

    }
}
