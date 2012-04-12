using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ILicenciaPatenteServicios : IServicioBase<LicenciaPatente>
    {
        IList<LicenciaPatente> ObtenerLicenciaFiltro(LicenciaPatente LicenciaAuxiliar);
    }
}
