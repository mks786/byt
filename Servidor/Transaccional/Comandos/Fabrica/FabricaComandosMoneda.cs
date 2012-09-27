using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosMoneda;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosMoneda
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una moneda
        /// x
        /// </summary>
        /// <param name="moneda">Moneda a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Moneda moneda)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moneda"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarMoneda(Moneda moneda)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devulve una lista con todas las monedas
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Moneda>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosMonedas();
        }
    }
}
