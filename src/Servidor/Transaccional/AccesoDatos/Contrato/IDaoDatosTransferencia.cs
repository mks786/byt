using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoDatosTransferencia : IDaoBase<DatosTransferencia, int>
    {
        IList<DatosTransferencia> ObtenerDatosTransferenciaPorAsociado(Asociado asociado);
    }
}
