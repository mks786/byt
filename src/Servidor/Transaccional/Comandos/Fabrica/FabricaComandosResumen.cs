using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosEstado;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosResumen;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosResumen
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar un resumen
        /// </summary>
        /// <param name="resumen">Resumen a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Resumen resumen)
        {
            return new ComandoInsertarOModificarResumen(resumen);
        }

        /// <summary>
        /// Método que devuelve el Comando para eliminar un resumen
        /// </summary>
        /// <param name="resumen">Resumen a eliminar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarResumen(Resumen resumen)
        {
            return new ComandoEliminarResumen(resumen);
        }

        /// <summary>
        /// Método que devuelve el Comando para consultar todos los resumenes
        /// </summary>
        /// <returns>Lista con todos los resumenes</returns>
        public static ComandoBase<IList<Resumen>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosResumenes();
        }

        /// <summary>
        /// Método que devuelve el Comando verificar existencia
        /// </summary>
        /// <param name="resumen">Resumen a verificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoVerificarExistenciaResumen(Resumen resumen)
        {
            return new ComandoVerificarExistenciaResumen(resumen);
        }
    }
}