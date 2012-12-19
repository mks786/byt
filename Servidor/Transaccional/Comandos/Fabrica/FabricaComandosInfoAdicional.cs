using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosInfoAdicional;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosInfoAdicional
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todos los InfoAdicionals
        /// </summary>
        /// <returns>El Comando para consultar todos los InfoAdicionals</returns>
        public static ComandoBase<IList<InfoAdicional>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosInfoAdicionals();
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar un InfoAdicional por su ID
        /// </summary>
        /// <returns></returns>
        public static ComandoBase<InfoAdicional> ObtenerComandoConsultarPorID(InfoAdicional infoAdicional)
        {
            return new ComandoConsultarInfoAdicionalPorID(infoAdicional);
        }

        /// <summary>
        /// Método que devuelve el Comando para modificar un InfoAdicional
        /// </summary>
        /// <param name="infoAdicional">InfoAdicional a insertar o modificar</param>
        /// <returns>El comando para realizar la insercion o modificacion</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(InfoAdicional infoAdicional)
        {
            return new ComandoInsertarOModificarInfoAdicional(infoAdicional);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar un InfoAdicional
        /// </summary>
        /// <param name="infoAdicional">InfoAdicional que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarInfoAdicional(InfoAdicional infoAdicional)
        {
            return new ComandoEliminarInfoAdicional(infoAdicional);
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="infoAdicional">InfoAdicional a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaInfoAdicional(InfoAdicional infoAdicional)
        {
            return new ComandoVerificarExistenciaInfoAdicional(infoAdicional);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar la info adicinal por id
        /// </summary>
        /// <param name="infoAdicional">InfoAdicional a consultar</param>
        /// <returns>InfoAdicional que devuelve la consulta</returns>
        public static ComandoBase<InfoAdicional> ObtenerComandoConsultarInfoAdicionalPorId(InfoAdicional infoAdicional)
        {
            return new ComandoConsultarInfoAdicionalPorID(infoAdicional);
        }
    }
    
}
