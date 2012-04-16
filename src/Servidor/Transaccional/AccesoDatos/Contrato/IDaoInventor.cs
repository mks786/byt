using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInventor : IDaoBase<Inventor, int>
    {
        IList<Inventor> ObtenerInventoresPorPatente(Patente patente);
    }
}
