using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosCondicion;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosCondicion
    {

        /// <summary>
        /// Método que devuelve el Comando para consultar todos las condiciones
        /// </summary>
        /// <returns>El Comando para consultar todos las condiciones</returns>
        public static ComandoBase<IList<Condicion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosCondiciones();
        }
    }
}
