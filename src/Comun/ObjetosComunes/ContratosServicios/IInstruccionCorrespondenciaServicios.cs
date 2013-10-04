using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInstruccionCorrespondenciaServicios : IServicioBase<InstruccionCorrespondencia>
    {
        /// <summary>
        /// Servicio para obtener la instruccion de correspondencia tomando en cuenta el codigo de la Marca o Patente, 
        /// a que se aplica y el concepto de la instruccion
        /// </summary>
        /// <param name="instruccion">Instruccion de Correspondencia que sirve como filtro</param>
        /// <returns>Instruccion de Correspondencia buscada; en caso contrario retorna NULL</returns>
        InstruccionCorrespondencia ObtenerInstruccionCorrespondencia(InstruccionCorrespondencia instruccion);
    }
}
