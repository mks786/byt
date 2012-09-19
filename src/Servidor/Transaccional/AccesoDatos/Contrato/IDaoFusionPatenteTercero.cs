﻿using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoFusionPatenteTercero : IDaoBase<FusionPatenteTercero, int>
    {
        FusionPatenteTercero ConsultarFusionMarcaTerceroPorFusion(Fusion fusion);
    }
}