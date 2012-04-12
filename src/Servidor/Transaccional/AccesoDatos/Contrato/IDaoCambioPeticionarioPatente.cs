using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioPeticionarioPatente : IDaoBase<CambioPeticionarioPatente, int>
    {
        IList<CambioPeticionarioPatente> ObtenerCambiosPeticionarioPatenteFiltro(CambioPeticionarioPatente CambioPeticionario);
    }
}
