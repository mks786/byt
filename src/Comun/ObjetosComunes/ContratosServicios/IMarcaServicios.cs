using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMarcaServicios : IServicioBase<Marca>
    {
        IList<Marca> ObtenerMarcasFiltro(Marca Marca);

        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);

        Marca ConsultarMarcaConTodo(Marca marca);
    }
}
