using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoListaDatosValores : IDaoBase<ListaDatosValores, string>
    {
        IList<ListaDatosValores> ObtenerListaDatosValoresPorParametro(ListaDatosValores listaDatosValores);
    }
}
