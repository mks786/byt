using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoFechaMarca : IDaoBase<FechaMarca,string>
    {
        /// <summary>
        /// Metodo que consulta todas las fechas de una marca
        /// </summary>
        /// <param name="marca">Marca a consultar</param>
        /// <returns>Listas de fechas de una marca determinada</returns>
        IList<FechaMarca> ObtenerFechasPorMarca(Marca marca);
    }
}
