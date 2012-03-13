using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMarcaTerceroServicios : IServicioBase<MarcaTercero>
    {
        IList<MarcaTercero> ObtenerMarcaTerceroFiltro(MarcaTercero marcaTercero);

        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);

        MarcaTercero ConsultarMarcaConTodo(MarcaTercero marcaTercero);
    }
}
