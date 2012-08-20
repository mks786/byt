using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInventor : IDaoBase<Inventor, int>
    {

        /// <summary>
        /// Metodo que Consulta los inventores dado una patente
        /// </summary>
        /// <param name="patente">patente</param>
        /// <returns>Lista de inventoes que tiene una patente</returns>
        IList<Inventor> ObtenerInventoresPorPatente(Patente patente);
    }
}
