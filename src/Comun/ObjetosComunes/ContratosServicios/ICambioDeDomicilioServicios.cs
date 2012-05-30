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
    }
}