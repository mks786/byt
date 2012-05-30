using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAnaquaServicios : IServicioBase<Anaqua>
    {
        /// <summary>
        /// Servicio que se encarga de consultar la auditoria de una anaqua
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditorias de la marca</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
    }
}
