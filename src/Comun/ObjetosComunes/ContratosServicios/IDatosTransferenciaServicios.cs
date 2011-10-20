using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IDatosTransferenciaServicios : IServicioBase<DatosTransferencia>
    {
        IList<DatosTransferencia> ConsultarDatosTransferenciaPorAsociado(Asociado asociado);
    }
}
