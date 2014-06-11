using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInteresadoMultiple : IDaoBase<InteresadoMultiple, int>
    {
        /// <summary>
        /// Metodo que obtiene los interesados de una patente especifica
        /// </summary>
        /// <param name="patente">Patente a consultar</param>
        /// <returns>Lista de interesados asociados a una patente especifica</returns>
        IList<InteresadoMultiple> ObtenerInteresadosPorPatente(Patente patente);

        /// <summary>
        /// Metodo que obtiene los interesados de una marca especifica
        /// </summary>
        /// <param name="marca">Marca a consultar</param>
        /// <returns>Lista de interesados asociados a una marca especifica</returns>
        IList<InteresadoMultiple> ObtenerInteresadosPorMarca(Marca marca);
    }
}
