using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCesion : IDaoBase<Cesion, int>
    {

        /// <summary>
        /// Metodo que consulta las cesiones dado unos parametros
        /// </summary>
        /// <param name="cesion">Casion con parametros</param>
        /// <returns>Lista de Cesiones con parametros solicitados</returns>
        IList<Cesion> ObtenerCesionesFiltro(Cesion cesion);
    }
}
