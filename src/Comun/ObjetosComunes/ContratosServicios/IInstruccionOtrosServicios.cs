using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInstruccionOtrosServicios : IServicioBase<InstruccionOtros>
    {
        /// <summary>
        /// Servicio que obtiene las instrucciones no tipificadas de una marca o de una patente segun el codigo 
        /// </summary>
        /// <param name="instruccionNoTipificada">Lista de instrucciones no tipificadas</param>
        /// <returns></returns>
        IList<InstruccionOtros> ObtenerInstruccionesNoTipificadasPorFiltro(InstruccionOtros instruccionNoTipificada);

    }
}
