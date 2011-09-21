using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAgenteServicios: IServicioBase<Agente>
    {
        IList<Agente> ConsultarAgentesYPoderes();
    }
}
