using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IPlanillaServicios : IServicioBase<Planilla>
    {
        Planilla ImprimirProcedimiento(ParametroProcedimiento parametroProcedimiento);
    }
}
