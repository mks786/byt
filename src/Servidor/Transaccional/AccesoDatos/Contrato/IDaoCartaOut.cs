using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCartaOut : IDaoBase<CartaOut, int>
    {
        IList<CartaOut> ObtenerCartasOutsFiltro(CartaOut carta);

        bool TransferirPlantilla(IList<Carta> cartas, IList<CartaOut> cartasOut);
    }
}
