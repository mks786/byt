using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IPoderServicios: IServicioBase<Poder>
    {
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
        
        IList<Poder> ConsultarPoderesPorInteresado(Interesado interesado);

        IList<Poder> ObtenerPoderesFiltro(Poder poder);
    }
}
