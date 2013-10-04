using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IMaestroDePlantillaServicios: IServicioBase<MaestroDePlantilla>
    {
        /// <summary>
        /// Servicio que obtiene los maestros de plantillas mediante una consulta por un filtro
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de Plantilla usado como filtro para la consulta</param>
        /// <returns>Lista de Maestros de Plantilla resultantes de la consulta</returns>
        IList<MaestroDePlantilla> ObtenerMaestroDePlantillaFiltro(MaestroDePlantilla maestroPlantilla);
    }
}
