using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoAsignacion : IDaoBase<Asignacion, int>
    {
        IList<Asignacion> ObtenerAsignacionesPorCarta(Carta carta);
        
    }
}
