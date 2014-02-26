using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoJustificacion : IDaoBase<Justificacion, int>
    {
        /// <summary>
        /// Metodo que obtiene una lista de justificaciones filtradas por Concepto
        /// </summary>
        /// <param name="justificacion">Justificacion filtro</param>
        /// <returns>Lista de justificaciones filtradas por Concepto</returns>
        IList<Justificacion> ObtenerJustificacionesPorConcepto(Justificacion justificacion);
    }
}
