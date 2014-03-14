using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCadenaDeCambios : IDaoBase<CadenaDeCambios,int>
    {
        /// <summary>
        /// Metodo que obtiene una lista de cadenas de cambios a partir de un filtro
        /// </summary>
        /// <param name="cadenaDeCambios">Cadena de Cambios filtro</param>
        /// <returns>Lista de cadenas de cambios</returns>
        IList<CadenaDeCambios> ObtenerCadenaDeCambiosFiltro(CadenaDeCambios cadenaDeCambios);
    }
}
