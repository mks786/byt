using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosTarifa;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosTarifa
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar una tarifa
        /// </summary>
        /// <param name="tarifa">Tarifa a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Tarifa tarifa)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Tarifa"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarTarifa(Tarifa Tarifa)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devulve una lista con todas las tarifas
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<Tarifa>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosTarifas();
        }
    }
}
