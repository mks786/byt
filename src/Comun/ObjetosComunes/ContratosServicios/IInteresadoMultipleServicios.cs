using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInteresadoMultipleServicios : IServicioBase<InteresadoMultiple>
    {
        
        /// <summary>
        /// Servicio para obtener los Interesados asociados a una patente especifica
        /// </summary>
        /// <param name="patente">Patente usada para filtrar los interesados asociados a la misma</param>
        /// <returns>Lista de interesados asociados a una patente especifica</returns>
        IList<InteresadoMultiple> ConsultarInteresadosDePatente(Patente patente);

        /// <summary>
        /// Servicio qeu obtiene los Interesados asociados a una marca especifica
        /// </summary>
        /// <param name="marca">Marca usada para filtrar los interesados asociados a la misma</param>
        /// <returns>Lista de interesados asociados a una marca especifica</returns>
        IList<InteresadoMultiple> ConsultarInteresadosDeMarca(Marca marca);
    }
}
