using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAgenteServicios: IServicioBase<Agente>
    {
        /// <summary>
        /// Servicio que se encarga de consultar los Agentes con sus poderes
        /// </summary>
        /// <returns>Lista de agentes con sus poderes</returns>
        IList<Agente> ConsultarAgentesYPoderes();


        /// <summary>
        /// Servicio que se encarga de consultar los agentes basados en un filtro
        /// </summary>
        /// <param name="agente">Agente filtro</param>
        /// <returns>Lista de agentes que cumplen con el filtro</returns>
        IList<Agente> ObtenerAgentesFiltro(Agente agente);


        /// <summary>
        /// Servicio que se encarga de consultar los agentes sin sus poderes (método mas rápido) basados en un filtro
        /// </summary>
        /// <param name="agente">Agent filtro</param>
        /// <returns>Lista de agentes que cumplen con el filtro</returns>
        IList<Agente> ObtenerAgentesSinPoderesFiltro(Agente agente);


        /// <summary>
        /// Servicio que se encarga de consultar los agentes pertenecientes a un poder 
        /// </summary>
        /// <param name="poder">Poder a consultar los agentes </param>
        /// <returns>Agentes que pertenecen al poder</returns>
        IList<Agente> ObtenerAgentesDeUnPoder(Poder poder);
    }
}
