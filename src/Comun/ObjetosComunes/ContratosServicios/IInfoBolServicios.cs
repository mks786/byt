using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInfoBolServicios : IServicioBase<InfoBol>
    {
        IList<InfoBol> ConsultarInfoBolesPorMarca(Marca marca);
    }
}
