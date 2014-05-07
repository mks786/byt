using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCompraSapiDetalle;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCompraSapiDetalle
    {
        /// <summary>
        /// Metodo que obtiene el comando para insertar o actualizar un reglon de detalle de Compra Sapi
        /// </summary>
        /// <param name="detalleCompra">Renglon de detalle de Compra Sapi</param>
        /// <returns>Comando para realizar la operacion de registro de detalle de Compra Sapi</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(CompraSapiDetalle detalleCompra)
        {
            return new ComandoInsertarOModificarDetalleCompraSapi(detalleCompra);
        }

        /// <summary>
        /// Metodo que obtiene el comando para eliminar un reglon de detalle de Compra Sapi
        /// </summary>
        /// <param name="detalleCompra">Renglon de detalle de Compra Sapi</param>
        /// <returns>Comando para realizar la operacion de eliminacion de detalle de Compra Sapi</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarDetalleCompraSapi(CompraSapiDetalle detalleCompra)
        {
            return new ComandoEliminarDetalleCompraSapi(detalleCompra);
        }

        /// <summary>
        /// Metodo que obtiene el comando para  consultar detalles de compra Sapi por filtro
        /// </summary>
        /// <param name="compraDetalle"></param>
        /// <returns></returns>
        public static ComandoBase<IList<CompraSapiDetalle>> ObtenerComandoConsultarCompraSapiDetalleFiltro(CompraSapiDetalle compraDetalle)
        {
            return new ComandoConsultarCompraSapiDetalleFiltro(compraDetalle);
        }
    }
}
