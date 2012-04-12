using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioDeNombrePatenteServicios : IServicioBase<CambioDeNombrePatente>
    {
        IList<CambioDeNombrePatente> ObtenerCambioDeNombreFiltro(CambioDeNombrePatente cambioDeNombre);
    }
}
