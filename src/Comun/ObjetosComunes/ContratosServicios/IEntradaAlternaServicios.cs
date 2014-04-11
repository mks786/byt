using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;
using System;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IEntradaAlternaServicios: IServicioBase<EntradaAlterna>
    {
        /// <summary>
        /// Servicio para obtener la lista de Auditorias de una Entrada Alterna
        /// </summary>
        /// <param name="auditoria">Auditoria filtro</param>
        /// <returns>Lista de Auditorias de una Entrada Alterna</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
    }
}
