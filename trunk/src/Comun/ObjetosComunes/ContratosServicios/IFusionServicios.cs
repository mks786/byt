﻿using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IFusionServicios : IServicioBase<Fusion>
    {
        IList<Fusion> ObtenerFusionFiltro(Fusion Fusion);
    }
}