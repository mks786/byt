using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoBoletin : IDaoBase<Boletin, int>
    {

        /// <summary>
        /// Metodo que consulta todas las resoluciones que tiene un boletin
        /// </summary>
        /// <param name="id">id del boletin</param>
        /// <returns>lista de resoluciones del boletin</returns>
        IList<Resolucion> ObtenerResolucionesDeBoletin(int id);
    }
}
