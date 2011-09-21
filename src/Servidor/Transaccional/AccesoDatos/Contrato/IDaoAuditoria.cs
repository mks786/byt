using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAuditoria : IDaoBase<Auditoria, string>
    {
        IList<Auditoria> AuditoriaPorFkYTabla(Auditoria auditoria);
    }
}
