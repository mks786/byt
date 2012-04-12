using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCesionPatente : IDaoBase<CesionPatente, int>
    {
        IList<CesionPatente> ObtenerCesionesPatenteFiltro(CesionPatente cesion);
    }
}
