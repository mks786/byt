using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInstruccionEnvioOriginalesServicios: IServicioBase<InstruccionEnvioOriginales>
    {
        /// <summary>
        /// Servicio que obtiene la instruccion de Envio de Originales de una marca o una patente filtrado por: Codigo de la 
        /// marca o la patente, a que aplica (M de Marca o P de Patente) y que concepto posee (C de Correspondencia o F de Facturacion)
        /// </summary>
        /// <param name="instruccion">Instruccion de Envio de Originales filtro</param>
        /// <returns>Instruccion de Envio de Originales; en caso contrario retorna NULL</returns>
        InstruccionEnvioOriginales ObtenerInstruccionEnvioOriginales(InstruccionEnvioOriginales instruccion);
    }
}
