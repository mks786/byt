using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosFecha;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosFecha
    {
        /// <summary>
        /// Método que devuelve el Comando para agregar una fecha
        /// </summary>
        /// <param name="fecha">fecha a agregar en la base de datos</param>
        /// <returns>El Comando que permite agregar la fecha en la base de datos</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(Fecha fecha)
        {
            return new ComandoInsertarOModificarFecha(fecha);
        }

        /// <summary>
        /// Método que devuelve el Comando para elimnar una fecha
        /// </summary>
        /// <param name="fecha">fecha que se va a eliminar</param>
        /// <returns>Comando para eliminar</returns>
        public static ComandoBase<bool> ObtenerComandoEliminarInventor(Fecha fecha)
        {
            return new ComandoEliminarFecha(fecha);
        }
    }
}
