using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMarcaBaseTercero : IDaoBase<MarcaBaseTercero, int>
    {
        IList<MarcaBaseTercero> ObtenerMarcaBaseTerceroFiltro(MarcaBaseTercero marcaBaseTercero);

        int ObtenerMaxSecuencia();

        List<MarcaBaseTercero> ObtenerTodosPorId(MarcaBaseTercero marcaBaseTercero);

    //    Marca ObtenerMarcaTerceroConTodo(MarcaTercero marcaTercero);
    }
}
