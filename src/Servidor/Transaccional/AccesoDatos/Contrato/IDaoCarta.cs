using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCarta : IDaoBase<Carta, int>
    {
        IList<Carta> ObtenerCartasFiltro(Carta carta);
    }
}
