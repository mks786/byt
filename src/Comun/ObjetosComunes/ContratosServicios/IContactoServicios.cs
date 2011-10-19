using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IContactoServicios : IServicioBase<Contacto>
    {
        IList<Contacto> ConsultarContactosPorAsociado(Asociado asociado);
    }
}
