using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IListaDatosValoresServicios : IServicioBase<ListaDatosValores>
    {
        /// <summary>
        /// Servicio que consulta la Lista de Valores por parametro
        /// </summary>
        /// <param name="listaDatosValores">parametro a buscar</param>
        /// <returns>Lista de Valores</returns>
        IList<ListaDatosValores> ConsultarListaDatosValoresPorParametro(ListaDatosValores listaDatosValores);
    }
}
