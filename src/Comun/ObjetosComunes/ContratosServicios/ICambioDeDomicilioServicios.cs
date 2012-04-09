﻿using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioDeDomicilioServicios : IServicioBase<CambioDeDomicilio>
    {
        IList<CambioDeDomicilio> ObtenerCambioDeDomicilioFiltro(CambioDeDomicilio CambioDeDomicilio);
    }
}