﻿using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioPeticionarioPatenteServicios: IServicioBase<CambioPeticionarioPatente>
    {
        /// <summary>
        /// Servicio que se encarga de consultar los cambios de peticionario segun el filtro
        /// </summary>
        /// <param name="CambioPeticionario">Cambio de Peticionario filtro</param>
        /// <returns>Cambios de Peticionario que cumplen con el filtro</returns>
        IList<CambioPeticionarioPatente> ObtenerCambioPeticionarioFiltro(CambioPeticionarioPatente CambioPeticionario);
    }
}