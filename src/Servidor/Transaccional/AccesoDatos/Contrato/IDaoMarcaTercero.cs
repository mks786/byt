using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoMarcaTercero : IDaoBase<MarcaTercero, int>
    {
        IList<MarcaTercero> ObtenerMarcaTerceroFiltro(MarcaTercero marcaTercero);

        string ObtenerMaxId(string maxId);

        int ObtenerMaxAnexo(string maxAnexo);

   
    }
}
