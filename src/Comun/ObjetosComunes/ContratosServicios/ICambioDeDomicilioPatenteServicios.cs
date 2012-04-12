using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioDeDomicilioPatenteServicios : IServicioBase<CambioDeDomicilioPatente>
    {
        IList<CambioDeDomicilioPatente> ObtenerCambioDeDomicilioFiltro(CambioDeDomicilioPatente CambioDeDomicilio);
    }
}