using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAnaquaServicios : IServicioBase<Anaqua>
    {
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
    }
}
