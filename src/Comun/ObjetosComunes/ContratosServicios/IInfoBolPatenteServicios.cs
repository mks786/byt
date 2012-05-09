using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInfoBolPatenteServicios : IServicioBase<InfoBolPatente>
    {   
        IList<InfoBolPatente> ConsultarInfoBolesPorPatente(Patente patente);
    }
}
