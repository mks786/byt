using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCesionPatente : IDaoBase<CesionPatente, int>
    {

        /// <summary>
        /// Metodo que consulta las cesionesPatentes dado unos parametros
        /// </summary>
        /// <param name="cesion">cesionePatente con parametros</param>
        /// <returns>Lista de cesionePatente con parametros solicitados</returns>
        IList<CesionPatente> ObtenerCesionesPatenteFiltro(CesionPatente cesion);
    }
}
