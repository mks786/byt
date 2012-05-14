using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IPatenteServicios : IServicioBase<Patente>
    {
        IList<Patente> ObtenerPatentesFiltro(Patente Patente);

        IList<Fecha> ConsultarFechasPorPatente(Patente Patente);

        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);

        Patente ConsultarPatenteConTodo(Patente Patente);

        bool InsertarOModificarAnualidad(Patente Patente, int hash);
    }
}
