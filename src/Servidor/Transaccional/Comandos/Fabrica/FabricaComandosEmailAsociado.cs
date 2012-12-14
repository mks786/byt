using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosEmailAsociado;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosEmailAsociado
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un EmailAsociado
        /// </summary>
        /// <param name="EmailAsociado">EmailAsociado a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(EmailAsociado EmailAsociado)
        {
            return new ComandoInsertarOModificarEmailAsociado(EmailAsociado);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un EmailAsociado
        /// </summary>
        /// <param name="EmailAsociado">EmailAsociado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarEmailAsociado(EmailAsociado EmailAsociado)
        {
            return new ComandoEliminarEmailAsociado(EmailAsociado);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los EmailAsociados
        /// </summary>
        /// <returns>Lista con todos los EmailAsociados</returns>
        public static ComandoBase<IList<EmailAsociado>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodasEmailAsociado();
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un EmailAsociado
        /// </summary>
        /// <param name="EmailAsociado">EmailAsociado a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaEmailAsociado(EmailAsociado EmailAsociado)
        {
            return new ComandoVerificarExistenciaEmailAsociado(EmailAsociado);
        }
    }
}