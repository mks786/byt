using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInventorServicios : IServicioBase<Inventor>
    {
        /// <summary>
        /// Servicio que consulta los inventores de una patente
        /// </summary>
        /// <param name="patente">Patente a consultar</param>
        /// <returns>Lista de inventores de la patente</returns>
        IList<Inventor> ConsultarInventoresPorPatente(Patente patente);
    }
}
