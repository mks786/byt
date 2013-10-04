using Trascend.Bolet.ObjetosComunes.Entidades;
using System.Collections.Generic;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoInstruccionCorrespondencia : IDaoBase<InstruccionCorrespondencia,int>
    {
        /// <summary>
        /// Metodo que obtiene una instruccion de correspondencia por codigo, aplica a y concepto. 
        /// Este metodo aplica para las Marcas y para las Patentes
        /// </summary>
        /// <param name="instruccionCorrespondencia">Instruccion de Correspondencia a consultar</param>
        /// <returns>Instruccion de Correspondencia de acuerdo a los filtros; NULL en caso contrario</returns>
        InstruccionCorrespondencia ObtenerInstruccionDeCorrespondencia(InstruccionCorrespondencia instruccion);
    }
}
