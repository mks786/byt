using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosDetallePago;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosDetallePago
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un detalle de pago
        /// </summary>
        /// <param name="detallePago">Detalle de pago a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(DetallePago detallePago)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detallePago"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarDetallePago(DetallePago detallePago)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devulve una lista con todas los detalles de pago
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<DetallePago>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosDetallePagos();
        }
    }
}
