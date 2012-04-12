using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoFusionPatente : IDaoBase<FusionPatente, int>
    {
        IList<FusionPatente> ObtenerFusionesPatenteFiltro(FusionPatente FusionPatente);
    }
}
