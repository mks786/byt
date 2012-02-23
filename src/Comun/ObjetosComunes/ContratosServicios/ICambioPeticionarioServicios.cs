using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioPeticionarioServicios : IServicioBase<CambioPeticionario>
    {
        IList<CambioPeticionario> ObtenerCambioPeticionarioFiltro(CambioPeticionario CambioPeticionario);
    }
}
