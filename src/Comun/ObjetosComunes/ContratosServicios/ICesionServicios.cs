using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICesionServicios: IServicioBase<Cesion>
    {
        /// <summary>
        /// Servicio que se encarga de obtener las cesiones segun el filtro
        /// </summary>
        /// <param name="CesionAuxiliar">cesión filtro</param>
        /// <returns>Lista de cesiones</returns>
        IList<Cesion> ObtenerCesionFiltro(Cesion CesionAuxiliar);


        /// <summary>
        /// Servicio que se encarga de insertar la cesion
        /// </summary>
        /// <param name="marca">cesion a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id de la cesion insertada</returns>
        int? InsertarOModificarCesion(Cesion cesion, int hash);
    }
}
