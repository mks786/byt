using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoFiltroReporte : IDaoBase<FiltroReporte, int>
    {
        /// <summary>
        /// Metodo para obtener los filtros de un Reporte de Marca
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>Lista de filtros de un reporte de marca determinado</returns>
        IList<FiltroReporte> ObtenerFiltrosDeReporte(Reporte reporte);
    }
}
