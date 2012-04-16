using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInventorServicios : IServicioBase<Inventor>
    {
        IList<Inventor> ConsultarInventoresPorPatente(Patente patente);
    }
}
