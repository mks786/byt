using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMaestroDePlantilla: IDaoBase<MaestroDePlantilla,int>
    {
        /// <summary>
        /// Metodo para obtener maestros de plantilla por filtro
        /// </summary>
        /// <param name="maestroDePlantilla">Maestro de plantilla filtro</param>
        /// <returns>Lista de maestros de plantilla obtenidas por un filtro</returns>
        IList<MaestroDePlantilla> ObtenerMaestroPlantillaFiltro(MaestroDePlantilla maestroDePlantilla);
    }
}
