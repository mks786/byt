using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface IInstruccionDescuentoServicios : IServicioBase<InstruccionDescuento>
    {
        /// <summary>
        /// Servicio que obtiene todas las instrucciones de Descuento de una marca o de una patente cualquiera
        /// </summary>
        /// <param name="instruccionFiltro">Instruccion de Descuento filtro</param>
        /// <returns>Lista de Instrucciones de Descuento de una marca o de una patente</returns>
        IList<InstruccionDescuento> ObtenerInstruccionesDeDescuentoMarcaOPatente(InstruccionDescuento instruccionFiltro);
    }
}
