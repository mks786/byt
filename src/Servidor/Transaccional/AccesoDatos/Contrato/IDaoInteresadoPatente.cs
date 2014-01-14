using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInteresadoPatente : IDaoBase<InteresadoPatente, int>
    {
        /// <summary>
        /// Metodo que obtiene los interesados de una patente especifica
        /// </summary>
        /// <param name="patente">Patente a consultar</param>
        /// <returns>Lista de interesados asociados a una patente especifica</returns>
        IList<InteresadoPatente> ObtenerInteresadosPorPatente(Patente patente);
    }
}
