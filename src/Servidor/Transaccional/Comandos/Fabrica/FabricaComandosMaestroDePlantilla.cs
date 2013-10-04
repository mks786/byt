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
        /// Metodo estatico que obtiene el comando para obtener todos los registros de la tabla ENV_MAESTRO_PLANT
        /// </summary>
        /// <returns>Comando para obtener todos los registros de la tabla ENV_MAESTRO_PLANT</returns>
        public static ComandoBase<IList<MaestroDePlantilla>> ObtenerComandoConsultarTodos()
        {
            return new ComandoConsultarTodosMaestrosDePlantilla();
        }


        /// <summary>
        /// Metodo estatico para cargar los comandos para insertar o modificar un Maestro de Plantilla
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de PLantilla a insertar y/o modificar</param>
        /// <returns>True si la operacion se realiza correctamente; false en caso contrario.</returns>
        public static ComandoBase<bool> ObtenerComandoInsertarOModificar(MaestroDePlantilla maestroPlantilla)
        {
            return new ComandoInsertarOModificarMaestroDePlantilla(maestroPlantilla);
        }

        /// <summary>
        /// Metodo estatico para cargar los comandos necesarios para obtener un maestro de plantilla por filtro
        /// </summary>
        /// <param name="maestroPlantilla">Maestro de plantilla usado como filtro</param>
        /// <returns>Comando usado para la obtecion del resultado de la consulta</returns>
        public static ComandoBase<IList<MaestroDePlantilla>> ObtenerComandoObtenerMaestroDePlantillaFiltro(MaestroDePlantilla maestroPlantilla)
        {
            return new ComandoObtenerMaestrosDePlantillaFiltro(maestroPlantilla);
        }

        
    }
}
