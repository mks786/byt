using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IAsignacionServicios : IServicioBase<Asignacion>
    {
        IList<Asignacion> ObtenerAsignacionesPorCarta(Carta carta);

    }
}
