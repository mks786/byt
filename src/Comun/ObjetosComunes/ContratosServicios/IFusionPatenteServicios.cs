using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IFusionPatenteServicios : IServicioBase<FusionPatente>
    {
        IList<FusionPatente> ObtenerFusionFiltro(FusionPatente Fusion);
    }
}
