using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoPoder : IDaoBase<Poder, int>
    {
        IList<Poder> ObtenerPoderesPorInteresado(Interesado interesado);

        IList<Poder> ObtenerPoderesFiltro(Poder poder);
    }
}
