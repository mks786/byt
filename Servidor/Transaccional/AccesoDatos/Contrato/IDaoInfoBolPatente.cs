using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInfoBolPatente : IDaoBase<InfoBolPatente, int>
    {

        /// <summary>
        /// Metodo que consulta todos los InfoBoles por patente
        /// </summary>
        /// <param name="marca">Patente</param>
        /// <returns>Lista de Infoboles de la patente solicitada</returns>
        IList<InfoBolPatente> ObtenerInfoBolesPorPatente(Patente patente);
    }
}
