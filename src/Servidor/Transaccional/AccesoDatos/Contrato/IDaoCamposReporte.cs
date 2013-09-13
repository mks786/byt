using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCamposReporte: IDaoBase<CamposReporte,string>
    {
        /// <summary>
        /// Metodo que obtiene todos los campos del reporte de marca
        /// </summary>
        /// <returns>Lista de campos para el reporte de marca</returns>
        IList<CamposReporte> ObtenerCamposReporteDeMarca();

        /// <summary>
        /// Metodo que obtiene todos los campos del reporte de patente
        /// </summary>
        /// <returns>Lista de campos para el reporte de patente</returns>
        IList<CamposReporte> ObtenerCamposReporteDePatente();

    }
}
