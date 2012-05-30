using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInfoBolPatenteServicios : IServicioBase<InfoBolPatente>
    {
        /// <summary>
        /// Servicio que consulta los infoboles por una patente
        /// </summary>
        /// <param name="patente">patente a consultar las infoboles</param>
        /// <returns>lista de infoboles pertenecientes a la patente</returns>
        IList<InfoBolPatente> ConsultarInfoBolesPorPatente(Patente patente);
    }
}
