using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IRenovacionServicios : IServicioBase<Renovacion>
    {
        IList<Renovacion> ObtenerRenovacionFiltro(Renovacion renovacion);

        int ConsultarUltimaRenovacion(Renovacion renovacion);
    }
}
