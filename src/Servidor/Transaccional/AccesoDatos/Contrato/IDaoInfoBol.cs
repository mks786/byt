using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInfoBol : IDaoBase<InfoBol, int>
    {
        /// <summary>
        /// Metodo que consulta todos los InfoBoles por marca
        /// </summary>
        /// <param name="marca">Marca</param>
        /// <returns>Lista de Infoboles de la marca solicitada</returns>
        IList<InfoBol> ObtenerInfoBolesPorMarca(Marca marca);
    }
}
