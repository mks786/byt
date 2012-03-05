using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAsociado : IDaoBase<Asociado, int>
    {
        Asociado ObtenerAsociadoConTodo(Asociado usuario);

        IList<Asociado> ObtenerAsociadosFiltro(Asociado asociado);

    }
}
