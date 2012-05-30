using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ILicenciaServicios : IServicioBase<Licencia>
    {
        /// <summary>
        /// Servicio que busca una licencia dependiendo de un filtro
        /// </summary>
        /// <param name="LicenciaAuxiliar">Licencia filtro</param>
        /// <returns>Lista de licencias</returns>
        IList<Licencia> ObtenerLicenciaFiltro(Licencia LicenciaAuxiliar);
    }
}
