using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IBoletinServicios: IServicioBase<Boletin>
    {
        IList<Resolucion> ConsultarResolucionesDeBoletin(Boletin boletin);
    }
}
