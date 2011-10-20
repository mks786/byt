using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosDatosTransferencia;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosDatosTransferencia
    {
        /// <summary>
        /// Método que devuelve el Comando para modificar un DatosTransferencia
        /// </summary>
        /// <param name="datosTrasferencia">Contador a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(DatosTransferencia datosTrasferencia)
        {
            return new ComandoInsertarOModificarDatosTransferencia(datosTrasferencia);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los Datos Transferencia
        /// </summary>
        /// <returns>El Comando para consultar todos los Datos de Transferencia</returns>
        public static ComandoBase<IList<DatosTransferencia>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosDatosTransferencia();
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar los datos de transferencia de un asociado
        /// </summary>
        /// <param name="datosTransferencia">Datos de Transferencia que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarDatosTransferencia(DatosTransferencia datosTransferencia)
        {
            return new ComandoEliminarDatosTransferencia(datosTransferencia);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar los datos de transferencia de un asociado
        /// </summary>
        /// <param name="asociado">Asocaido para consultar sus datos de trasnferencia</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<IList<DatosTransferencia>> ObtenerComandoConsultarPorAsociado(Asociado asociado)
        {
            return new ComandoConsultarDatosTransferenciaPorAsociado(asociado);
        }

        

    }
}
