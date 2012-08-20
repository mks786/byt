using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCorresponsal;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCorresponsal
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar un Corresponsal
        /// </summary>
        /// <param name="corresponsal">Corresponsal a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar el Corresponsal en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Corresponsal corresponsal)
        {
            return new ComandoInsertarOModificarCorresponsal(corresponsal);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Corresponsals
        /// </summary>
        /// <returns>El Comando para consultar todos los Corresponsals</returns>
        public static ComandoBase<IList<Corresponsal>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCorresponsales();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un Corresponsal
        /// </summary>
        /// <param name="corresponsal">Corresponsal que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarCorresponsal(Corresponsal corresponsal)
        {
            return new ComandoEliminarCorresponsal(corresponsal);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un Corresponsal por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<Corresponsal> ObtenerComandoConsultarPorID(Corresponsal corresponsal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="corresponsal">Corresponsal a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaCorresponsal(Corresponsal corresponsal)
        {
            return new ComandoVerificarExistenciaCorresponsal(corresponsal);
        }
    }
}
