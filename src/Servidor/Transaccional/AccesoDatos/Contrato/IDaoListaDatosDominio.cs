using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoListaDatosDominio : IDaoBase<ListaDatosDominio, int>
    {
        IList<ListaDatosDominio> ObtenerListaDatosDominioPorParametro(ListaDatosDominio parametro);
    }
}
