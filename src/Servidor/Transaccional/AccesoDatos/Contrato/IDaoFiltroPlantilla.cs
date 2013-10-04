using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoFiltroPlantilla: IDaoBase<FiltroPlantilla,int>
    {
        /// <summary>
        /// Metodo que devuelve los encabezados de una plantilla especifica
        /// </summary>
        /// <param name="plantilla">Plantilla a consultar</param>
        /// <returns>Lista de encabezados de una plantilla</returns>
        IList<FiltroPlantilla> ObtenerFiltrosEncabezadoPlantilla(MaestroDePlantilla plantilla);


        /// <summary>
        /// Metodo que devuelve los detalles de una plantilla especifica
        /// </summary>
        /// <param name="plantilla">Plantilla a consultar</param>
        /// <returns>Lista de detalles de una plantilla</returns>
        IList<FiltroPlantilla> ObtenerFiltrosDetallePlantilla(MaestroDePlantilla plantilla);
    }
}
