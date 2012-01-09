using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoOperacion : IDaoBase<Operacion, int>
    {
        IList<Operacion> ObtenerOperacionesPorMarca(Marca marca);
    }
}
