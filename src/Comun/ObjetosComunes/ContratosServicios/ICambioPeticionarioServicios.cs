using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioPeticionarioServicios : IServicioBase<CambioPeticionario>
    {
        /// <summary>
        /// Servicio que se encarga de consultar los cambios de peticionario segun el filtro
        /// </summary>
        /// <param name="CambioPeticionario">Cambio de Peticionario filtro</param>
        /// <returns>Cambios de Peticionario que cumplen con el filtro</returns>
        IList<CambioPeticionario> ObtenerCambioPeticionarioFiltro(CambioPeticionario CambioPeticionario);
    }
}
