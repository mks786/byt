using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInfoBolMarcaTer : IDaoBase<InfoBolMarcaTer, int>
    {
        /// <summary>
        /// Metodo que consulta todos los InfoBolMarcaTeres por marca
        /// </summary>
        /// <param name="marca">Marca</param>
        /// <returns>Lista de Infoboles de la marca solicitada</returns>
        IList<InfoBolMarcaTer> ObtenerInfoBolMarcaTeresPorMarca(MarcaTercero marca);
    }
}
