using System.Collections.Generic;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.AccesoDatos.Contrato
{
    public interface IDaoCompraSapiDetalle : IDaoBase<CompraSapiDetalle,int>
    {
        /// <summary>
        /// Metodo que obtiene los detalles de compra Sapi por filtro
        /// </summary>
        /// <param name="compraSapiDetalle">Detalle de Compra Sapi usado como filtro</param>
        /// <returns>Lista de detalles de compra Sapi resultantes</returns>
        IList<CompraSapiDetalle> ObtenerCompraSapiDetalleFiltro(CompraSapiDetalle compraSapiDetalle);
    }
}
