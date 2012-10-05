using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICorresponsalServicios : IServicioBase<Corresponsal>
    {
        /// <summary>
        /// Servicio que se encarga consultar la auditoria de un Corresponsal
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditorias del asociado</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
    }
}
