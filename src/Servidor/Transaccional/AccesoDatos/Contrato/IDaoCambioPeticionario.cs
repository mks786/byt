using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCambioPeticionario : IDaoBase<CambioPeticionario, int>
    {
        IList<CambioPeticionario> ObtenerCambiosPeticionarioFiltro(CambioPeticionario CambioPeticionario);
    }
}
