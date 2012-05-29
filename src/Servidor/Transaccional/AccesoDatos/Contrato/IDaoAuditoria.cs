using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAuditoria : IDaoBase<Auditoria, string>
    {

        /// <summary>
        /// Consulta las Auditorias por cada FKyTabla
        /// </summary>
        /// <param name="auditoria">Auditoria con parametros</param>
        /// <returns>lista de auditorias</returns>
        IList<Auditoria> AuditoriaPorFkYTabla(Auditoria auditoria);
    }
}
