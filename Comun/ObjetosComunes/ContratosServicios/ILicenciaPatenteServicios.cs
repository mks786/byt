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


        /// <summary>
        /// Servicio que se encarga de insertar la licencia
        /// </summary>
        /// <param name="marca">licencia a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id de la licencia insertada</returns>
        int? InsertarOModificarLicencia(LicenciaPatente licencia, int hash);
    }
}
