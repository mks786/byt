using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInteresadoServicios: IServicioBase<Interesado>
    {
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
        Interesado ConsultarInteresadoConTodo(Interesado interesado);
    }
}
