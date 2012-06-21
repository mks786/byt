using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IFusionServicios : IServicioBase<Fusion>
    {
        /// <summary>
        /// Servicio que consulta las fusiones segun el filtro
        /// </summary>
        /// <param name="Fusion">fusión filtro</param>
        /// <returns>Lista de fusiones que cumplan con el filtro</returns>
        IList<Fusion> ObtenerFusionFiltro(Fusion Fusion);


        /// <summary>
        /// Servicio que se encarga de insertar la fusion
        /// </summary>
        /// <param name="marca">fusion a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id de la fusion insertada</returns>
        int? InsertarOModificarFusion(Fusion fusion, int hash);
    }
}
