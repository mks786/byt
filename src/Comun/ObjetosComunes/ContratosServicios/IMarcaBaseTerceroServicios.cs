using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMarcaBaseTerceroServicios : IServicioBase<MarcaBaseTercero>
    {
        IList<MarcaBaseTercero> ObtenerMarcaBaseTerceroFiltro(MarcaBaseTercero marcaBaseTercero);

        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);

        MarcaBaseTercero ConsultarMarcaConTodo(MarcaBaseTercero marcaTercero);
    }
}
