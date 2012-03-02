using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMarca : IDaoBase<Marca, int>
    {
        IList<Marca> ObtenerMarcasFiltro(Marca marca);

        Marca ObtenerMarcaConTodo(Marca marca);

    }
}
