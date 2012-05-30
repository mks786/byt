using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICesionPatenteServicios : IServicioBase<CesionPatente>
    {
        /// <summary>
        /// Servicio que se encarga de obtener las cesiones segun el filtro
        /// </summary>
        /// <param name="CesionAuxiliar">cesión filtro</param>
        /// <returns>Lista de cesiones</returns>
        IList<CesionPatente> ObtenerCesionPatenteFiltro(CesionPatente CesionAuxiliar);
    }
}
