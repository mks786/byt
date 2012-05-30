using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICartaServicios: IServicioBase<Carta>
    {
        /// <summary>
        /// Servicio que se encarga de consultar las cartas segun el filtro
        /// </summary>
        /// <param name="carta">carta filtro</param>
        /// <returns>Lista de cartas que cumplen con el filtro</returns>
        IList<Carta> ObtenerCartasFiltro(Carta carta);


        /// <summary>
        /// Servicio que se encarga de realizar la auditoria de una carta
        /// </summary>
        /// <param name="auditoria">Auditoria a consultar</param>
        /// <returns>Lista de auditorias de la carta</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);
    }
}
