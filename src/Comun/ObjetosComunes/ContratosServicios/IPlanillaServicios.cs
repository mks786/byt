using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IPlanillaServicios : IServicioBase<Planilla>
    {
        Planilla ImprimirFM02(Marca marca, int hash, int way);
    }
}
