using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMemoria : IDaoBase<Memoria, int>
    {
        IList<Memoria> ObtenerMemoriasPorPatente(Patente patente);
    }
}
