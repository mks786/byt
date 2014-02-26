using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IJustificacionServicios : IServicioBase<Justificacion>
    {
        /// <summary>
        /// Servicio que obtiene una lista de justificaciones usando como filtro el tipo de Concepto
        /// </summary>
        /// <param name="justificacion">Justificacion para filtrar los datos</param>
        /// <returns>Lista de justificaciones filtradas</returns>
        IList<Justificacion> ObtenerJustificacionesPorConcepto(Justificacion justificacion);
    }
}
