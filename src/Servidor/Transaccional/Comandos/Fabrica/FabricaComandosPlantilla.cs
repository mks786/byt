using System;
using System.Collections.Generic;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.Comandos.Comandos.ComandosPlantilla;
using Trascend.Bolet.ObjetosComunes.Entidades;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosPlantilla
    {
        /// <summary>
        /// Método que devuelve el Comando para consultar todas las Plantillas
        /// </summary>
        /// <returns>Lista con todas las Plantillas</returns>
        public static ComandoBase<IList<Plantilla>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosPlantilla();
        }
    }
}
