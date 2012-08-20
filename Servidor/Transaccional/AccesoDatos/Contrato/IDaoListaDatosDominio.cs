using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoListaDatosDominio : IDaoBase<ListaDatosDominio, int>
    {

        /// <summary>
        /// Metodo que consulta las ListaDeDatosDeDominio dado unos parametros
        /// </summary>
        /// <param name="parametro">ListaDatosDominio conparametros</param>
        /// <returns>Lista de ListaDatosDominio solicitados</returns>
        IList<ListaDatosDominio> ObtenerListaDatosDominioPorParametro(ListaDatosDominio parametro);
    }
}
