using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosTipoCliente;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosTipoCliente
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un tipo de cliente
        /// </summary>
        /// <param name="tipoCliente">Tipo de cliente a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(TipoCliente tipoCliente)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoCliente"></param>
        /// <returns></returns>
        public static ComandoBase<bool> ObtenerComandoEliminarTipoCliente(TipoCliente tipoCliente)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devulve una lista con todas los tipos de clientes
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<IList<TipoCliente>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosTipoClientes();
        }
    }
}
