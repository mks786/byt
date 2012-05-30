using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInfoBolServicios : IServicioBase<InfoBol>
    {
        /// <summary>
        /// Servicio que consulta los infoboles por una marca
        /// </summary>
        /// <param name="marca">marca a consultar las infoboles</param>
        /// <returns>lista de infoboles pertenecientes a la marca</returns>
        IList<InfoBol> ConsultarInfoBolesPorMarca(Marca marca);
    }
}
