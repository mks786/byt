using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoDatosTransferencia : IDaoBase<DatosTransferencia, int>
    {

        /// <summary>
        /// Metodo que consulta los DatosDeTransferencia de un Asociado
        /// </summary>
        /// <param name="asociado">Asociado</param>
        /// <returns>Lista de DatosTranferencias del asociado solicitado</returns>
        IList<DatosTransferencia> ObtenerDatosTransferenciaPorAsociado(Asociado asociado);
    }
}
