using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInfoBol : IDaoBase<InfoBol, int>
    {
        IList<InfoBol> ObtenerInfoBolesPorMarca(Marca marca);
    }
}
