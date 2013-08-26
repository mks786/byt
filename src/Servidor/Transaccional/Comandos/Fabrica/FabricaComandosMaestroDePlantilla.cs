using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trascend.Bolet.Comandos.Comandos;
using Trascend.Bolet.ObjetosComunes.Entidades;
using Trascend.Bolet.Comandos.Comandos.ComandosMaestroDePlantilla;

namespace Trascend.Bolet.Comandos.Fabrica
{
    public static class FabricaComandosMaestroDePlantilla
    {

        /// <summary>
        /// Metodo estatico para cargar los comandos para insertar o modificar un Maestro de Plantilla
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de PLantilla a insertar y/o modificar</param>
        /// <returns>True si la operacion se realiza correctamente; false en caso contrario.</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(MaestroDePlantilla maestroPlantilla)
        {
            return new ComandoInsertarOModificarMaestroDePlantilla(maestroPlantilla);
        }
    }
}
