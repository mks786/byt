using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICertificadoMarcaServicios : IServicioBase<CertificadoMarca>
    {
        /// <summary>
        /// Servicio que se encarga de consultar la auditoria de Certificados de Marca
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditoria de la marca</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
    }
}
