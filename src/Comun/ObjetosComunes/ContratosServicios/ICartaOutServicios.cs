using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICartaOutServicios: IServicioBase<CartaOut>
    {
        /// <summary>
        /// Servicio que se encarga de consultar las cartaOuts segun el filtro
        /// </summary>
        /// <param name="carta">CartaOut a buscar</param>
        /// <returns>Lista de Cartas Out que cumplan con el filtro</returns>
        IList<CartaOut> ObtenerCartasOutsFiltro(CartaOut carta);


        /// <summary>
        /// Servicio que se encarga de realizar la transferencia de plantilla de una tabla (COR_MAIL_OUT) a otra (ENTRADA)
        /// </summary>
        /// <param name="cartas">Cartas a transferir</param>
        /// <returns>true en caso de ser exitoso, false en caso contrario</returns>
        bool TransferirPlantilla(IList<CartaOut> cartas, int hash);
    }
}
