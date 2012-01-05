using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoBusqueda : IDaoBase<Busqueda, int>
    {
        IList<Busqueda> ObtenerBusquedasPorMarca(Marca marca);
    }
}
