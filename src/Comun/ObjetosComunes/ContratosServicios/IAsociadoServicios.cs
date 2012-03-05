using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAsociadoServicios: IServicioBase<Asociado>
    {
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);

        Asociado ConsultarAsociadoConTodo(Asociado asociado);

        IList<Asociado> ObtenerAsociadosFiltro(Asociado asociado);
    }
}
