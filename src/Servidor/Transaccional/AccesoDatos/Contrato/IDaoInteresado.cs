using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInteresado : IDaoBase<Interesado, int>
    {
        Interesado ObtenerInteresadoConTodo(Interesado interesado);

        IList<Interesado> ObtenerInteresadosFiltro(Interesado interesado);

        Interesado ObtenerInteresadosDeUnPoder(Poder poder);
    }
}
