using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoListaDatosValores : IDaoBase<ListaDatosValores, string>
    {

        /// <summary>
        /// Metodo que consulta las ListaDatosValores dado unos parametros
        /// </summary>
        /// <param name="parametro">ListaDatosValores conparametros</param>
        /// <returns>Lista de ListaDatosValores solicitados</returns>
        IList<ListaDatosValores> ObtenerListaDatosValoresPorParametro(ListaDatosValores listaDatosValores);
    }
}
