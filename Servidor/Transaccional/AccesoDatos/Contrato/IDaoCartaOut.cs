using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCartaOut : IDaoBase<CartaOut, int>
    {

        /// <summary>
        /// Metodo que consulta las CatasOuts dado unos parametros
        /// </summary>
        /// <param name="carta">CartaOut con parametros establecidos</param>
        /// <returns>Lista de CartasOuts con parametros solicitados</returns>
        IList<CartaOut> ObtenerCartasOutsFiltro(CartaOut carta);

        /// <summary>
        /// Metodo que se usa para transferir la planilla de cartas
        /// </summary>
        /// <param name="cartas">Cartas que se encuentran en la planilla</param>
        /// <param name="cartasOut">CartasOuts que se encuentran en la planilla</param>
        /// <returns>True si se inserto correctamente, False de lo contrario</returns>
        bool TransferirPlantilla(IList<Carta> cartas, IList<CartaOut> cartasOut);
    }
}
