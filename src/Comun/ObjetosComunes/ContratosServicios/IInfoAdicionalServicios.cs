using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInfoAdicionalServicios : IServicioBase<InfoAdicional>
    {
        /// <summary>
        /// Servicio que consulta la auditoria de la InfoAdicional
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditorias</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
    }
}
