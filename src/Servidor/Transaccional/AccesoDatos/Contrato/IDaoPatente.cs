using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoPatente : IDaoBase<Patente, int>
    {
        IList<Patente> ObtenerPatentesFiltro(Patente Patente);

        Patente ObtenerPatenteConTodo(Patente Patente);

    }
}
