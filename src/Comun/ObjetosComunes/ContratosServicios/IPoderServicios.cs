using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IPoderServicios: IServicioBase<Poder>
    {
        /// <summary>
        /// Servicio que consulta las auditorias de un Poder
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de Auditorias del Poder</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);


        /// <summary>
        /// Servicio que consulta los poderes pertenecientes a un Interesado
        /// </summary>
        /// <param name="interesado">Interesado a consultar sus poderes</param>
        /// <returns>Lista de poderes del interesado</returns>
        IList<Poder> ConsultarPoderesPorInteresado(Interesado interesado);


        /// <summary>
        /// Servicio que consulta los poderes de un agente
        /// </summary>
        /// <param name="agente">Agente a consultar sus poderes</param>
        /// <returns>Lista de poderes del Agente</returns>
        IList<Poder> ConsultarPoderesPorAgente(Agente agente);


        /// <summary>
        /// Servicio que obtiene los poderes basados en un filtro
        /// </summary>
        /// <param name="poder">Poder a filtrar</param>
        /// <returns>Lista de poderes que cumpla con el filtro</returns>
        IList<Poder> ObtenerPoderesFiltro(Poder poder);


        /// <summary>
        /// Servicio que obtiene los poderes pertenecientes a un Agente y a un interesado
        /// </summary>
        /// <param name="agente">Agente a buscar</param>
        /// <param name="interesado">Interesado a buscar</param>
        /// <returns>Poderes pertenecientes a los parametros</returns>
        IList<Poder> ObtenerPoderesEntreAgenteEInteresado(Agente agente, Interesado interesado);
    }
}
