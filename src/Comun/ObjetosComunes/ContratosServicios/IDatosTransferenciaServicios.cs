using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;


namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IDatosTransferenciaServicios : IServicioBase<DatosTransferencia>
    {
        /// <summary>
        /// Servicio que se encarga de consultar los Datos de transferencia de un asociado
        /// </summary>
        /// <param name="asociado">Asociado a consultar datos de transferencia</param>
        /// <returns>Lista de Datos Transferencia del Asociado</returns>
        IList<DatosTransferencia> ConsultarDatosTransferenciaPorAsociado(Asociado asociado);
    }
}
