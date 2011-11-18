using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosAsignacion;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public class FabricaComandosAsignacion
    {
        /// <summary>
        /// Método que devuelve el Comando para insertar o modificar una asignación
        /// </summary>
        /// <param name="asignacion">Carta a intersar o modificar</param>
        /// <returns>True: si se realizo el comando con exito; False: en caso contrario</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Asignacion asignacion)
        {
            return new ComandoInsertarOModificarAsignacion(asignacion);
        }
    }
}