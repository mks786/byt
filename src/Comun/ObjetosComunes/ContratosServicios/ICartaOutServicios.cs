using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICartaOutServicios: IServicioBase<CartaOut>
    {
        IList<CartaOut> ObtenerCartasOutsFiltro(CartaOut carta);

        bool TransferirPlantilla(IList<CartaOut> cartas);
    }
}
