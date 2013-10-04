using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInstruccionOtros : IDaoBase<InstruccionOtros,int>
    {
        /// <summary>
        /// Metodo que obtiene en el Dao las Instrucciones No Tipificadas de una marca o patente
        /// </summary>
        /// <param name="instruccionOtros">Instruccion no tipificada que sirve de filtro</param>
        /// <returns>Lista de instrucciones no tipificadas de una marca o de una patente</returns>
        IList<InstruccionOtros> ObtenerInstruccionesNoTipificadasPorCodigo(InstruccionOtros instruccionOtros);
    }
}
