using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInfoBolPatente : IDaoBase<InfoBolPatente, int>
    {
        IList<InfoBolPatente> ObtenerInfoBolesPorPatente(Patente patente);
    }
}
