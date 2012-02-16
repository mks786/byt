using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAgenteServicios: IServicioBase<Agente>
    {
        IList<Agente> ConsultarAgentesYPoderes();

        IList<Agente> ObtenerAgentesFiltro(Agente agente);

        IList<Agente> ObtenerAgentesDeUnPoder(Poder poder);
    }
}
