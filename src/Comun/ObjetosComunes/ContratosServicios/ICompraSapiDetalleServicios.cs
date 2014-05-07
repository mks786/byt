using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.ObjetosComunes.ContratosServicios
{
    public interface ICompraSapiDetalleServicios : IServicioBase<CompraSapiDetalle>
    {
        /// <summary>
        /// Servicio que permite consultar detalles de compra Sapi por filtro
        /// </summary>
        /// <param name="compraDetalle">Detalle de compra Sapi filtro</param>
        /// <returns>Lista de detalles de acuerdo a un filtro determinado</returns>
        IList<CompraSapiDetalle> ObtenerCompraSapiDetalleFiltro(CompraSapiDetalle compraDetalle);
    }
}
