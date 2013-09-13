using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCamposReporteRelacion: IDaoBase<CamposReporteRelacion,int>
    {

        /// <summary>
        /// Metodo para obtener los campos de un reporte de marca especifico
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>Lista de campos del reporte de marca seleccionado</returns>
        IList<CamposReporteRelacion> ObtenerCamposDeReporte(Reporte reporte);

        /// <summary>
        /// Metodo que elimina los campos definidos para un reporte de marca especifico
        /// </summary>
        /// <param name="reporteDeMarca">Reporte de Marca seleccionado</param>
        /// <returns>True si se realiza correctamente; False en caso contrario</returns>
        bool EliminarCamposReporte(Reporte reporte);
    }
}
