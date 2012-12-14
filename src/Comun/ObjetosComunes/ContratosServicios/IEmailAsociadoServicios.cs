using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IEmailAsociadoServicios : IServicioBase<EmailAsociado>
    {
        /// <summary>
        /// Servicio que se encarga de realizar la auditoria de una carta
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditorias de la carta</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
    }
}
