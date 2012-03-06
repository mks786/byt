using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoRenovacion : IDaoBase<Renovacion, int>
    {
        IList<Renovacion> ObtenerRenovacionesFiltro(Renovacion renovacion);
    }
}
