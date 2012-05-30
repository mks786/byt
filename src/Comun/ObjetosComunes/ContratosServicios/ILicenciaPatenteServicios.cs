using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ILicenciaPatenteServicios : IServicioBase<LicenciaPatente>
    {
        /// <summary>
        /// Servicio que busca una licencia dependiendo de un filtro
        /// </summary>
        /// <param name="LicenciaAuxiliar">Licencia filtro</param>
        /// <returns>Lista de licencias</returns>
        IList<LicenciaPatente> ObtenerLicenciaFiltro(LicenciaPatente LicenciaAuxiliar);
    }
}
