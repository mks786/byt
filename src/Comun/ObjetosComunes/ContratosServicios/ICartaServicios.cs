using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICartaServicios: IServicioBase<Carta>
    {
        IList<Carta> ObtenerCartasFiltro(Carta carta);
    }
}
