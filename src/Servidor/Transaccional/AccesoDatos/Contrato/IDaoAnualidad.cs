using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAnualidad : IDaoBase<Anualidad, int>
    {
        IList<Anualidad> ObtenerAnualidadesFiltro(int idAnualidad);

        int ObtenerMaxIdAnualidad();

        Anualidad ObtenerAnualidadConTodo(Anualidad Anualidad);

    }
}
