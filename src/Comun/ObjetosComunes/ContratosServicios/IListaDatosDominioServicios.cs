using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IListaDatosDominioServicios : IServicioBase<ListaDatosDominio>
    {
        /// <summary>
        /// Servicio que consulta la Lista de Valores por parametro
        /// </summary>
        /// <param name="parametro">parametro a buscar</param>
        /// <returns>Lista de Valores</returns>
        IList<ListaDatosDominio> ConsultarListaDatosDominioPorParametro(ListaDatosDominio parametro);
    }
}
