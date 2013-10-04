using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInstruccionEnvioOriginales : IDaoBase<InstruccionEnvioOriginales,int>
    {
        /// <summary>
        /// Metodo que obtiene una instruccion de envio de originales de una marca o de una patente segun su aplicacion, el codigo 
        /// de la marca o de la patente y el concepto utilizado para esta instruccion
        /// </summary>
        /// <param name="instruccion">Instruccion de Envio de Originales a buscar</param>
        /// <returns>Retorna una instruccion de Envio de Originales; en caso contrario retorna NULL</returns>
        InstruccionEnvioOriginales ObtenerInstruccionDeEnvioDeOriginales(InstruccionEnvioOriginales instruccion);
    }
}
