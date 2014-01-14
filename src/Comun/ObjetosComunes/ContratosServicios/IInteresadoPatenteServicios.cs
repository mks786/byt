using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInteresadoPatenteServicios : IServicioBase<InteresadoPatente>
    {
        
        /// <summary>
        /// Servicio para obtener los Interesados asociados a una patente especifica
        /// </summary>
        /// <param name="patente">Patente usada para filtrar los interesados asociados a la misma</param>
        /// <returns>Lista de interesados asociados a una patente especifica</returns>
        IList<InteresadoPatente> ConsultarInteresadosDePatente(Patente patente);
    }
}
