using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoPatente : IDaoBase<Patente, int>
    {
        /// <summary>
        /// Método que obtiene las patentes que cumplan con un filtro determinado
        /// </summary>
        /// <param name="Patente">Patente filtro</param>
        /// <returns>Lista de patentes que cumplan con el filtro</returns>
        IList<Patente> ObtenerPatentesFiltro(Patente Patente);

        /// <summary>
        /// Método que obtiene las fechas de una patente
        /// </summary>
        /// <param name="Patente">Patente a consultarle las fechas</param>
        /// <returns>Lista de fechas de la patente</returns>
        IList<Fecha> ObtenerFechasPatente(Patente Patente);

        /// <summary>
        /// Método que obtiene una patente con todos sus objetos
        /// </summary>
        /// <param name="Patente">Patente a consultar</param>
        /// <returns>Patente con todos los objetos que la componen</returns>
        Patente ObtenerPatenteConTodo(Patente Patente);

        /// <summary>
        /// Metodo que obtiene las patentes las cuales esta por vencer su prioridad de acuerdo a una cantidad de dias de recordatorio
        /// </summary>
        /// <param name="cantidadDiasRecordatorio">Cantidad de dias usadas para el recordatorio</param>
        /// <returns>Lista de patentes que estan por vencer su prioridad</returns>
        IList<VencimientoPrioridadPatente> ObtenerPatentesPorVencerPrioridad(int cantidadDiasRecordatorio);
    }
}
