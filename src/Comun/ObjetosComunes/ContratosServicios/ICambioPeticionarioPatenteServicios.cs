using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICambioPeticionarioPatenteServicios: IServicioBase<CambioPeticionarioPatente>
    {
        IList<CambioPeticionarioPatente> ObtenerCambioPeticionarioFiltro(CambioPeticionarioPatente CambioPeticionario);
    }
}
