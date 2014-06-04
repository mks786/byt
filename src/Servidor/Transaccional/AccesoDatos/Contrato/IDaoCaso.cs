using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCaso : IDaoBase<Caso,int>
    {
        /// <summary>
        /// Metodo que consulta Casos por medio de un filtro determinado
        /// </summary>
        /// <param name="caso">Caso usado como filtro</param>
        /// <returns>Lista de casos que cumplen con el filtro determinado</returns>
        IList<Caso> ObtenerCasosFiltro(Caso caso);
    }
}
