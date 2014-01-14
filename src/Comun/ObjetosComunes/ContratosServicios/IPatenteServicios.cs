using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IPatenteServicios : IServicioBase<Patente>
    {
        /// <summary>
        /// Servicio que obtiene las patentes basadas en un filtro
        /// </summary>
        /// <param name="Patente">Patente filtro</param>
        /// <returns>Lista de Patentes que cumplan con el filtro</returns>
        IList<Patente> ObtenerPatentesFiltro(Patente Patente);


        /// <summary>
        /// Servicio que devuelve las fechas relacionadas a una patente
        /// </summary>
        /// <param name="Patente">Patente a buscarle fechas</param>
        /// <returns>Lista de fechas relacionadas a la patente</returns>
        IList<Fecha> ConsultarFechasPorPatente(Patente Patente);


        /// <summary>
        /// Servicio que consulta la auditoria de una Patente
        /// </summary>
        /// <param name="auditoria">Auditoria a consultarS</param>
        /// <returns>Lista de auditorias de la patente</returns>
        IList<Auditoria> AuditoriaPorFkyTabla(Auditoria auditoria);


        /// <summary>
        /// Servicio que consulta una patente con todos sus datos y objetos
        /// </summary>
        /// <param name="Patente">Patente a buscar</param>
        /// <returns>Patente consultada</returns>
        Patente ConsultarPatenteConTodo(Patente Patente);


        /// <summary>
        /// Servicio que InsertaOModifica una Patente
        /// </summary>
        /// <param name="Patente">Patente a insertar</param>
        /// <param name="hash">hash del usuario que inserta</param>
        /// <returns>id de la patente recién insertada</returns>
        int? InsertarOModificarPatente(Patente Patente, int hash);


        /// <summary>
        /// Servicio que obtiene una lista de las patentes que tienen su prioridad proxima a vencer tomando en cuenta una duracion especifica
        /// </summary>
        /// <param name="cantidadDiasRecordatorio">Cantidad de dias usadas para el recordatorio</param>
        /// <returns>Lista de patentes proximas a vencer dentro de los limites de la cantidad de dias de recordatorio</returns>
        IList<VencimientoPrioridadPatente> ObtenerPatentesPorVencerPrioridad(int cantidadDiasRecordatorio);

    }
}
