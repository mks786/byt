using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosAccion;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosAccion
    {
        /// <summary>
        /// Metodo que obtiene el comando necesario para obtener todas las Acciones
        /// </summary>
        /// <returns>Comando para realizar la accion</returns>
        public static ComandoBase<IList<Accion>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosAcciones();
        }
    }
}
