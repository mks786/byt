using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioDeDomicilioPatenteServicios : IServicioBase<CambioDeDomicilioPatente>
    {
        /// <summary>
        /// Servicio que se encarga de consultar los CambioDeDomicilio segun el filtro
        /// </summary>
        /// <param name="CambioPeticionario">CambioDeDomicilio filtro</param>
        /// <returns>CambioDeDomicilio que cumplen con el filtro</returns>
        IList<CambioDeDomicilioPatente> ObtenerCambioDeDomicilioPatenteFiltro(CambioDeDomicilioPatente CambioDeDomicilio);


        /// <summary>
        /// Servicio que se encarga de insertar la cambioDomicilio
        /// </summary>
        /// <param name="marca">cambioDomicilio a insertar</param>
        /// <param name="hash">hash del usuario que ejecuta la insercion</param>
        /// <returns>Id de la cambioDomicilio insertada</returns>
        int? InsertarOModificarCambioDeDomicilio(CambioDeDomicilioPatente cambioDomicilio, int hash);
    }
}