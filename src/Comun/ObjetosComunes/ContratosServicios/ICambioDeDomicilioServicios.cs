using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioDeDomicilioServicios : IServicioBase<CambioDeDomicilio>
    {
        /// <summary>
        /// Servicio que se encarga de consultar los CambioDeDomicilio segun el filtro
        /// </summary>
        /// <param name="CambioPeticionario">CambioDeDomicilio filtro</param>
        /// <returns>CambioDeDomicilio que cumplen con el filtro</returns>
        IList<CambioDeDomicilio> ObtenerCambioDeDomicilioFiltro(CambioDeDomicilio CambioDeDomicilio);


        /// <summary>
        /// Servicio que se encarga de insertar la cambioDomicilio
        /// </summary>
        /// <param name="marca">cambioDomicilio a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id de la cambioDomicilio insertada</returns>
        int? InsertarOModificarCambioDeDomicilio(CambioDeDomicilio cambioDomicilio, int hash);
    }
}