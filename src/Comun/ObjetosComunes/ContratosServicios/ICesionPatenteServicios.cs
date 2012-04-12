using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICesionPatenteServicios : IServicioBase<CesionPatente>
    {
        IList<CesionPatente> ObtenerCesionFiltro(CesionPatente CesionAuxiliar);
    }
}
